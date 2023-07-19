using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MyCookin.DAL.ErrorAndMessage.ds_ErrorAndMessageTableAdapters;
using MyCookin.Common;
using System.Data.SqlClient;
using System.Data;


namespace MyCookin.Log
{
    public enum LogLevel:int
    {
        CriticalErrors = 1,
        Errors = 2,
        Warnings = 3,
        InfoMessages = 4,
        Debug = 5,
        None = 100
    }

    public static class LogManager
    {
        #region Methods
        /// <summary>
        /// Write log from SP on Db
        /// </summary>
        /// <param name="DebugLevel">Log Level to be write</param>
        /// <param name="LogRow">Row to write into Log</param>
        public static void WriteDBLog(LogLevel DebugLevel, LogRow LogRow)
        {
            int ActualDebugLevel = Convert.ToInt32(AppConfig.GetValue("DebugLevel", AppDomain.CurrentDomain));
            ErrorsAndMessagesDAL WriteDBLogSP = new ErrorsAndMessagesDAL();

            string _errorNumber = "0";
            string _errorSeverity = LogRow.ErrorSeverity.ToString();
            string _errorState = ((int)DebugLevel).ToString();
            string _errorProcedure = "";
            string _errorLine = LogRow.ErrorLine;
            string _errorMessage = LogRow.ErrorMessage;
            string _fileOrigin = LogRow.FileOrigin;
            DateTime _dateError = LogRow.DateAndTime;
            string _errorMessageCode = LogRow.ErrorMessageCode;
            bool _isStoredProcedureError = false;
            bool _isTriggerError = false;
            string _IDUser = LogRow.IDUser;
            bool _isApplicationError = LogRow.IsApplicationError;
            bool _isApplicationLog = LogRow.IsApplicationLog;

            if ((int)DebugLevel <= ActualDebugLevel)
            {
                try
                {
                    WriteDBLogSP.USP_InsertErrorLog(_errorNumber, _errorSeverity, _errorState, _errorProcedure, _errorLine, _errorMessage, _fileOrigin, _dateError, _errorMessageCode, _isStoredProcedureError, _isTriggerError, _IDUser, _isApplicationError, _isApplicationLog);
                }
                catch (SqlException sqlEx)
                {
                    try
                    {
                        string ErrorMessage = sqlEx.Message;

                        //WRITE A ROW IN LOG FILE - Do not insert in Db and do not send Email because could be a DB Connection Error
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in WriteDBLogSP.USP_InsertErrorLog(): " + ErrorMessage + "", "", true, false);
                        LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);
                    }
                    catch
                    {
                    }
                }
                catch (Exception Ex)
                {
                    try
                    {
                        string ErrorMessage = Ex.Message;

                        //WRITE A ROW IN LOG FILE - Not in Db because could be a DB Error
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in WriteDBLogSP.USP_InsertErrorLog(): " + ErrorMessage + "", "", true, false);
                        LogManager.WriteFileLog(LogLevel.Warnings, true, NewRow);
                    }
                    catch
                    {
                    }
                }
            }
        }

        #region WriteFileLog
        /// <summary>
        /// Write log from SP on file system
        /// </summary>
        /// <param name="DebugLevel">Log Level to be write</param>
        /// <param name="LogRow">Row to write into Log</param>
        public static void WriteFileLog(LogLevel DebugLevel, bool SendEmail, LogRow LogRow)
        {
            int ActualDebugLevel = Convert.ToInt32(AppConfig.GetValue("DebugLevel", AppDomain.CurrentDomain));

            string LogPath = AppConfig.GetValue("LogPath", AppDomain.CurrentDomain);

            string FileName = AppConfig.GetValue("LogBaseFileName", AppDomain.CurrentDomain);

            string FileNameDateFormat = AppConfig.GetValue("LogFileNameDateFormat", AppDomain.CurrentDomain);

            #region CheckLastErrorLogDate
            //Check Last ErrorLog Sent By Email
            //If email was sent more than X minutes, then send again
            ErrorsLogsTableAdapter taErrorsLog = new ErrorsLogsTableAdapter();
            DataTable dtLastError = new DataTable();
            bool CheckIntervalPassed = false;
            
            try
            {
                dtLastError = taErrorsLog.GetLastErrorLogDate(LogRow.FileOrigin.ToString(), LogRow.ErrorMessageCode.ToString());

                //If we have no errors, DAL give us one row in anycase, but LastErrorLog generate error.
                DateTime LastErrorLog = dtLastError.Rows[0].Field<DateTime>("LastErrorLogDate");

                //Get the interval to send email again
                int ErrorIntervalTime = Convert.ToInt32(AppConfig.GetValue("SendEmailIntervalTime", AppDomain.CurrentDomain));

                TimeSpan IntervalTime = DateTime.UtcNow.Subtract(LastErrorLog);

                int minutesFromLastError = IntervalTime.Minutes;

                if (minutesFromLastError > ErrorIntervalTime)
                {
                    CheckIntervalPassed = true;
                }
            }
            catch
            {
                //We have no error until now
                CheckIntervalPassed = true;
            }
            #endregion

            try
            {
                if ((int)DebugLevel <= ActualDebugLevel)
                {
                    FileName += DateTime.UtcNow.ToString(FileNameDateFormat) + ".log";

                    string lines = LogRow.GetLogRow() + "\n";

                    // Write the logrow to file.
                    System.IO.StreamWriter file = new System.IO.StreamWriter(LogPath + FileName, true);
                    file.WriteLine(lines);

                    file.Close();
                }
            }
            catch
            {
            }

            //Check if would Email of this log
            if (SendEmail && CheckIntervalPassed)
            {
                //Send email if set on Web.config
                if (Convert.ToBoolean(AppConfig.GetValue("SendEmailForLog", AppDomain.CurrentDomain)))
                {
                    string From = AppConfig.GetValue("EmailFromLog", AppDomain.CurrentDomain);
                    string To = AppConfig.GetValue("EmailToLog", AppDomain.CurrentDomain);
                    string Subject = "MyCookin Internal Email - Log or Error";
                    string Message = LogRow.GetLogRow() + "\n";

                    Network Mail = new Network(From, To, "", "", Subject, Message, "");

                    System.Threading.Thread ThreadMail = new System.Threading.Thread(new System.Threading.ThreadStart(Mail.SendEmailThread));
                    ThreadMail.IsBackground = true;
                    ThreadMail.Start();
                }
            }
        }
        #endregion

        #endregion
    }
   
    public class LogRow
    {
        #region PrivateFields
        private DateTime _dateAndTime;
        private string _dateAndTimeString;
        //private int _severityLevel;
        //private string _errorCode;
        //private string _errorOrigin;
        private string _errorMessage;
        //private string _unifiedMessage;
        //private string _logDelimiter;
        private string _errorMessageCode;
        private string _errorSeverity;
        private string _errorLine;
        private string _fileOrigin;
        private string _IDUser;
        private bool _IsApplicationError;
        private bool _IsApplicationLog;
        #endregion

        #region PublicFields
        public DateTime DateAndTime
        {
            get { return _dateAndTime; }
        }
        //public int SeverityLevel
        //{
        //    get { return _severityLevel; }
        //}
        //public string ErrorCode
        //{
        //    get { return _errorCode; }
        //}
        //public string ErrorOrigin
        //{
        //    get { return _errorOrigin; }
        //}
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }
        public string ErrorMessageCode
        {
            get { return _errorMessageCode; }
        }
        public string ErrorSeverity
        {
            get { return _errorSeverity; }
        }
        public string ErrorLine
        {
            get { return _errorLine; }
        }
        public string FileOrigin
        {
            get { return _fileOrigin; }
        }
        public string IDUser
        {
            get { return _IDUser; }
        }
        public bool IsApplicationError
        {
            get { return _IsApplicationError; }
        }
        public bool IsApplicationLog
        {
            get { return _IsApplicationLog; }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// New Row to write on FileSystem or Db Log
        /// </summary>
        /// <param name="DateAndTime">DateTime, usually Now</param>
        /// <param name="ErrorSeverity">Importance of log</param>
        /// <param name="ErrorLine">Error Line Code</param>
        /// <param name="FileOrigin">Current Page</param>
        /// <param name="ErrorMessageCode">Error Message Code</param>
        /// <param name="UnifiedMessage">Text of Message to write to log</param>
        /// <param name="IDUser">Current IDUser</param>
        /// <param name="IsApplicationError">Set true if Application Error</param>
        /// <param name="IsApplicationLog">Set true if Application Log</param>
        public LogRow(DateTime DateAndTime, string ErrorSeverity, string ErrorLine, string FileOrigin, string ErrorMessageCode, string UnifiedMessage, string IDUser, bool IsApplicationError, bool IsApplicationLog)
        {
            _dateAndTime = DateAndTime;
            _errorSeverity = ErrorSeverity;
            _errorLine = ErrorLine;
            _fileOrigin = FileOrigin;
            _errorMessageCode = ErrorMessageCode;
            _errorMessage = UnifiedMessage;
            _dateAndTimeString = DateAndTime.ToString("yyyyMMdd HH:mm:ss");    //To write in Logfile
            _IDUser = IDUser;
            _IsApplicationError = IsApplicationError;
            _IsApplicationLog = IsApplicationLog;
        }

        /// <summary>
        /// To Delete Errors Log from DB
        /// </summary>
        /// <param name="ErrorMessage"></param>
        public LogRow(string ErrorMessage)
        {
            _errorMessage = ErrorMessage;
        }
        #endregion

        #region Methods

        #region GetLogRow
        /// <summary>
        /// Line we want to write in LogFile
        /// </summary>
        /// <returns>Error Log Row</returns>
        public string GetLogRow()
        {
            return _dateAndTimeString + GetLogDelimiter() + _fileOrigin + GetLogDelimiter() + _errorMessageCode + GetLogDelimiter() + _errorMessage + GetLogDelimiter() + _IDUser;
        }
        #endregion

        #region GetLogDelimiter
        /// <summary>
        /// Get a default delimiter for error log line from Web.Config
        /// </summary>
        /// <returns>Error Log Row delimiter</returns>
        private string GetLogDelimiter()
        {
            return AppConfig.GetValue("LogDelimiter", AppDomain.CurrentDomain);
        }
        #endregion

        #region DeleteErrorLog
        public string DeleteErrorLog()
        {
            string ExecutionResult = "";

            try
            {
                ErrorsLogsTableAdapter taErrorsLog = new ErrorsLogsTableAdapter();
                taErrorsLog.DeleteError(_errorMessage);
            }
            catch(Exception ex)
            {
                ExecutionResult = ex.Message;
            }

            return ExecutionResult;
        }
        #endregion

        #endregion
    }


}
