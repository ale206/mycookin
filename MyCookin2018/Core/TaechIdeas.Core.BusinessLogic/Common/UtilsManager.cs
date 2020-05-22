using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Dto;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Network;

namespace TaechIdeas.Core.BusinessLogic.Common
{
    public class UtilsManager : IUtilsManager
    {
        private readonly ILogManager _logManager;
        private readonly IRetrieveMessageManager _retrieveMessageManager;
        private readonly INetworkManager _networkManager;
        private readonly ILogConfig _logConfig;

        public UtilsManager(ILogManager logManager, IRetrieveMessageManager retrieveMessageManager, INetworkManager networkManager, ILogConfig logConfig)
        {
            _logManager = logManager;
            _retrieveMessageManager = retrieveMessageManager;
            _networkManager = networkManager;
            _logConfig = logConfig;
        }

        #region GetAllPropertiesAndValues

        /// <summary>
        ///     Get all properties and values of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myObject"></param>
        /// <returns>string with all properties:values;</returns>
        public string GetAllPropertiesAndValues<T>(T myObject)
        {
            if (myObject == null)
            {
                return null;
            }

            var sb = new StringBuilder();

            foreach (var prop in typeof(T).GetProperties())
            {
                sb.Append($"{prop.Name}:{prop.GetValue(myObject)}; ");
            }

            return sb.ToString();
        }

        public bool IsIpValid(string ip)
        {
            //Valid Local Ip for test environment
            if (ip.Equals("::1"))
            {
                return true;
            }

            var match = Regex.Match(ip, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            return match.Success;
        }

        public bool IsEmailValid(string email)
        {
            var match = Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return match.Success;
        }

        #endregion

        #region LogStoredProcedure

        /// <summary>
        ///     Call WriteLog class for stored Procedure result
        /// </summary>
        /// <param name="logDbLevel">Log Level for write on Database Log Table</param>
        /// <param name="uspReturn">The return class of the Stored Procedure called</param>
        public void LogStoredProcedure(LogLevel logDbLevel, UspReturnValue uspReturn)
        {
            try
            {
                //Note: For All Users StoredProcedure, USPReturn.USPReturnValue is IDUser.

                int idLanguageForLog;
                try
                {
                    idLanguageForLog = _logConfig.IdLanguageForLog;
                }
                catch
                {
                    idLanguageForLog = 1;
                }

                try
                {
                    var newLogRow = new LogRowIn
                    {
                        ErrorMessage = $"Error in WriteDBLogSP.USP_InsertErrorLog(): {_retrieveMessageManager.RetrieveDbMessage(idLanguageForLog, uspReturn.ResultExecutionCode)}",
                        ErrorMessageCode = uspReturn.ResultExecutionCode,
                        ErrorSeverity = logDbLevel,
                        FileOrigin = _networkManager.GetCurrentPageName(),
                        IdUser = uspReturn.USPReturnValue
                    };

                    _logManager.WriteLog(newLogRow);
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

        #endregion

        /// <summary>
        ///     Return language code in two characters (ex.: en, it, es)
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public string GetLanguageCodeFromId(int languageId)
        {
            //TODO: Move in db method?

            switch (languageId)
            {
                case 1:
                    return "en";
                case 2:
                    return "it";
                case 3:
                    return "es";
                default:
                    return "en";
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime to, int dayInterval)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(dayInterval))
            {
                yield return day;
            }
        }
    }
}