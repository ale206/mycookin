using System;
using System.Data.SqlClient;
using System.IO;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Network.Dto;

namespace TaechIdeas.Core.BusinessLogic.LogAndMessage
{
    public class LogManager : ILogManager
    {
        private readonly ILogConfig _logConfig;
        private readonly IErrorAndMessageRepository _errorAndMessageRepository;
        private readonly INetworkManager _networkManager;

        public LogManager(ILogConfig logConfig, IErrorAndMessageRepository errorAndMessageRepository, INetworkManager networkManager)
        {
            _logConfig = logConfig;
            _errorAndMessageRepository = errorAndMessageRepository;
            _networkManager = networkManager;
        }

        #region WriteLog

        public void WriteLog(LogRowIn logRowIn)
        {
            var actualDbLevel = _logConfig.DebugLevel;
            var actualFileLevel = _logConfig.FileLevel;
            var sendEmail = _logConfig.SendEmailForLog;

            if ((int) logRowIn.ErrorSeverity <= actualFileLevel)
            {
                WriteFileLog(logRowIn);
            }

            if ((int) logRowIn.ErrorSeverity <= actualDbLevel)
            {
                WriteDbLog(logRowIn);
            }

            //Disable email if it is forced or if it is just a log.
            var disableEmail = logRowIn.ErrorSeverity.Equals(LogLevel.JustALog) || logRowIn.DisableEmail;

            if (sendEmail && !disableEmail)
            {
                SendEmail(logRowIn);
            }
        }

        #endregion

        #region WriteDbLog

        /// <summary>
        ///     Write log from SP on Db
        /// </summary>
        /// <param name="logRowIn">Row to write into Log</param>
        public void WriteDbLog(LogRowIn logRowIn)
        {
            //var writeDbLogSp = new ErrorsAndMessagesDAL();

            const string errorNumber = "0";
            var errorSeverity = logRowIn.ErrorSeverity;
            const string errorProcedure = "";
            var errorLine = logRowIn.ErrorLine;
            var errorMessage = logRowIn.ErrorMessage;
            var fileOrigin = logRowIn.FileOrigin;
            var dateError = logRowIn.DateAndTime;
            var errorMessageCode = logRowIn.ErrorMessageCode;
            var idUser = logRowIn.IdUser;

            var isApplicationLog = logRowIn.ErrorSeverity.Equals(LogLevel.JustALog);
            var isApplicationError = !logRowIn.ErrorSeverity.Equals(LogLevel.JustALog);

            try
            {
                var insertErrorLogIn = new InsertErrorLogIn
                {
                    UserId = idUser,
                    DateError = dateError,
                    ErrorLine = errorLine,
                    ErrorMessage = errorMessage,
                    ErrorMessageCode = errorMessageCode,
                    ErrorNumber = errorNumber,
                    ErrorProcedure = errorProcedure,
                    ErrorSeverity = errorSeverity.ToString(),
                    ErrorState = null,
                    FileOrigin = fileOrigin,
                    IsApplicationError = isApplicationError,
                    IsApplicationLog = isApplicationLog,
                    IsStoredProcedureError = false,
                    IsTriggerError = false
                };

                _errorAndMessageRepository.InsertErrorLog(insertErrorLogIn);
            }
            catch (SqlException sqlEx)
            {
                errorMessage = sqlEx.Message;
                try
                {
                    //WRITE A ROW IN LOG FILE - Do not insert in Db and do not send Email because could be a DB Connection Error

                    var newLogRow = new LogRowIn
                    {
                        ErrorMessage = $"Error in WriteDbLog: {errorMessage}",
                        ErrorMessageCode = "US-ER-9999",
                        ErrorSeverity = LogLevel.CriticalErrors,
                        FileOrigin = "LogManager"
                    };

                    WriteFileLog(newLogRow);
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

                try
                {
                    //WRITE A ROW IN LOG FILE - Not in Db because could be a DB Error
                    var newLogRow = new LogRowIn
                    {
                        ErrorMessage = $"Error in WriteDBLogSP.USP_InsertErrorLog(): {errorMessage}",
                        ErrorMessageCode = "US-ER-9999",
                        ErrorSeverity = LogLevel.CriticalErrors,
                        FileOrigin = "LogManager"
                    };

                    WriteFileLog(newLogRow);
                }
                catch
                {
                }
            }
        }

        #endregion

        #region WriteFileLog

        /// <summary>
        ///     Write log from SP on file system
        /// </summary>
        /// <param name="logRowIn">Row to write into Log</param>
        public void WriteFileLog(LogRowIn logRowIn)
        {
            var logPath = _logConfig.LogPath;

            var fileName = _logConfig.LogBaseFileName;

            var fileNameDateFormat = _logConfig.LogFileNameDateFormat;

            try
            {
                fileName += DateTime.UtcNow.ToString(fileNameDateFormat) + ".log";

                var lines = GetLogRow(logRowIn);

                // Write the logrow to file.
                var file = new StreamWriter(Path.Combine(logPath, fileName), true);
                file.WriteLine(lines);

                file.Close();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region SendEmail

        public void SendEmail(LogRowIn logRowIn)
        {
            #region CheckLastErrorLogDate

            //Check Last ErrorLog Sent By Email
            //If email was sent more than X minutes, then send again
            //var taErrorsLog = new ErrorsLogsTableAdapter();
            var checkIntervalPassed = false;

            try
            {
                //DataTable dtLastError = taErrorsLog.GetLastErrorLogDate(logRow.FileOrigin, logRow.ErrorMessageCode);
                //If we have no errors, DAL give us one row in anycase, but LastErrorLog generate error.
                //var lastErrorLog = dtLastError.Rows[0].Field<DateTime>("LastErrorLogDate");

                var getLastErrorLogDateIn = new GetLastErrorLogDateIn
                {
                    FileOrigin = logRowIn.FileOrigin,
                    ErrorMessageCode = logRowIn.ErrorMessageCode
                };

                var lastErrorLog = _errorAndMessageRepository.GetLastErrorLogDate(getLastErrorLogDateIn).LastErrorLogDate;

                //Get the interval to send email again
                var errorIntervalTime = _logConfig.SendEmailIntervalTime;

                var intervalTime = DateTime.UtcNow.Subtract(lastErrorLog);

                var minutesFromLastError = intervalTime.Minutes;

                if (minutesFromLastError > errorIntervalTime)
                {
                    checkIntervalPassed = true;
                }
            }
            catch
            {
                //We have no error until now
                checkIntervalPassed = true;
            }

            #endregion

            //Check if would Email of this log
            if (!checkIntervalPassed) return;

            //Send email if set on Web.config
            if (!_logConfig.SendEmailForLog) return;

            var from = _logConfig.EmailFromLog;
            var to = _logConfig.EmailToLog;
            var subject = _logConfig.EmailSubjectForLog;
            var message = GetLogRow(logRowIn) + "\n";

            var saveEmailToSendInput = new SaveEmailToSendInput
            {
                Message = message,
                Bcc = null,
                Cc = null,
                From = from,
                HtmlFilePath = null,
                Subject = subject,
                To = to
            };

            _networkManager.SaveEmailToSend(saveEmailToSendInput);
        }

        #endregion

        #region GetLogRow

        public string GetLogRow(LogRowIn logRowIn)
        {
            return logRowIn.DateAndTime.ToString("yyyyMMdd HH:mm:ss") + GetLogDelimiter() + logRowIn.FileOrigin + GetLogDelimiter() + logRowIn.ErrorMessageCode +
                   GetLogDelimiter() + logRowIn.ErrorMessage + GetLogDelimiter() + logRowIn.IdUser;
        }

        #endregion

        #region GetLogDelimiter

        /// <summary>
        ///     Get a default delimiter for error log line from Web.Config
        /// </summary>
        /// <returns>Error Log Row delimiter</returns>
        public string GetLogDelimiter()
        {
            return _logConfig.LogDelimiter;
        }

        #endregion

        #region DeleteErrorByErrorMessage

        public string DeleteErrorByErrorMessage(string errorMessage)
        {
            var executionResult = "";

            try
            {
                _errorAndMessageRepository.DeleteErrorByErrorMessage(new DeleteErrorByErrorMessageIn {ErrorMessageToDelete = errorMessage});
            }
            catch (Exception ex)
            {
                executionResult = ex.Message;
            }

            return executionResult;
        }

        #endregion
    }
}