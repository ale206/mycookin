using System;
using System.Data.SqlClient;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Network;

namespace TaechIdeas.Core.BusinessLogic.LogAndMessage
{
    public class RetrieveMessageManager : IRetrieveMessageManager
    {
        private readonly ILogManager _logManager;
        private readonly INetworkManager _networkManager;
        private readonly IMessageConfig _messageConfig;
        private readonly IErrorAndMessageRepository _errorAndMessageRepository;

        public RetrieveMessageManager(ILogManager logManager, INetworkManager networkManager, IMessageConfig messageConfig, IErrorAndMessageRepository errorAndMessageRepository)
        {
            _logManager = logManager;
            _networkManager = networkManager;
            _messageConfig = messageConfig;
            _errorAndMessageRepository = errorAndMessageRepository;
        }

        //TODO: IMPROVE!

        #region RetrieveDbMessage

        /// <summary>
        ///     Get Error or Message Text to show the user
        /// </summary>
        /// <param name="idLanguage">Language of Message</param>
        /// <param name="code">Code of Message (xx-xx-0000)</param>
        /// <returns>Message Text</returns>
        public string RetrieveDbMessage(int idLanguage, string code)
        {
            try
            {
                var getErrorOrMessageIn = new GetErrorOrMessageIn
                {
                    LanguageId = idLanguage,
                    ErrorOrMessageCode = code
                };

                var _return = _errorAndMessageRepository.GetErrorOrMessage(getErrorOrMessageIn).ResultExecutionCode;

                return _return;
            }
            catch (SqlException sqlEx)
            {
                var errorMessage = sqlEx.Message;

                //WRITE A ROW IN LOG FILE AND DB
                var newLogRow = new LogRowIn
                {
                    ErrorMessage = $"Sql Error in RetrieveDBMessage(): {errorMessage}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.Warnings,
                    FileOrigin = _networkManager.GetCurrentPageName(),
                    IdUser = ""
                };

                _logManager.WriteLog(newLogRow);

                //Return a Default Message according to language
                return DefaultErrorMessage(idLanguage);
            }
            catch (Exception Ex)
            {
                var errorMessage = Ex.Message;

                //WRITE A ROW IN LOG FILE AND DB
                var newLogRow = new LogRowIn
                {
                    ErrorMessage = $"Error in RetrieveDBMessage(): {errorMessage}",
                    ErrorMessageCode = "US-ER-9999",
                    ErrorSeverity = LogLevel.Warnings,
                    FileOrigin = _networkManager.GetCurrentPageName(),
                    IdUser = ""
                };

                _logManager.WriteLog(newLogRow);

                //Return a Default Message according to language
                return DefaultErrorMessage(idLanguage);
            }
        }

        #endregion

        #region DefaultErrorMessage

        /// <summary>
        ///     //Return a Default Message according to language
        /// </summary>
        /// <param name="idLanguage">User Language</param>
        /// <returns>Default Error Message get from WebConfig</returns>
        public string DefaultErrorMessage(int idLanguage)
        {
            string defaultErrorMessage;

            switch (idLanguage)
            {
                case 1:
                    defaultErrorMessage = _messageConfig.GeneralError1;
                    break;
                case 2:
                    defaultErrorMessage = _messageConfig.GeneralError2;
                    break;
                case 3:
                    defaultErrorMessage = _messageConfig.GeneralError3;
                    break;
                case 4:
                    defaultErrorMessage = _messageConfig.GeneralError4;
                    break;
                case 5:
                    defaultErrorMessage = _messageConfig.GeneralError5;
                    break;
                default:
                    defaultErrorMessage = _messageConfig.GeneralError1;
                    break;
            }

            return defaultErrorMessage;
        }

        #endregion
    }
}