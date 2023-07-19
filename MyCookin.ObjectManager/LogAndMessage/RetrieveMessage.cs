using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using MyCookin.DAL.ErrorAndMessage.ds_ErrorAndMessageTableAdapters;
using MyCookin.Common;
using MyCookin.Log;

namespace MyCookin.ErrorAndMessage
{
    public class RetrieveMessage
    {
        /// <summary>
        /// Get Error or Message Text to show the user
        /// </summary>
        /// <param name="IDLanguage">Language of Message</param>
        /// <param name="Code">Code of Message (xx-xx-0000)</param>
        /// <returns>Message Text</returns>
        public static string RetrieveDBMessage(int IDLanguage, string Code)
        {
            ErrorsAndMessagesDAL GetMessage = new ErrorsAndMessagesDAL();
            try
            {
                if (HttpContext.Current.Application[Code + "-" + IDLanguage.ToString()] != null && (DateTime)HttpContext.Current.Application[Code + "-" + IDLanguage.ToString() + "-TimeOut"] > DateTime.UtcNow)
                {
                    return HttpContext.Current.Application[Code + "-" + IDLanguage.ToString()].ToString();
                }
                else
                {
                    string _return = GetMessage.GetErrorOrMessageByLang(IDLanguage, Code)[0]["ResultExecutionCode"].ToString();
                    try
                    {
                        HttpContext.Current.Application[Code + "-" + IDLanguage.ToString()] = _return;
                        HttpContext.Current.Application[Code + "-" + IDLanguage.ToString() + "-TimeOut"] = DateTime.UtcNow.AddDays(3);
                    }
                    catch
                    {
                    }
                    return _return;
                }
            }
            catch (SqlException sqlEx)
            {
                //RetrieveMessageLog(LogLevel.Errors, LogLevel.Errors, sqlEx.Message);

                string ErrorMessage = sqlEx.Message;

                //WRITE A ROW IN LOG FILE AND DB
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Sql Error in RetrieveDBMessage(): " + ErrorMessage + "", "", true, false);
                LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);

                //Return a Default Message according to language
                return DefaultErrorMessage(IDLanguage);

            }
            catch (Exception Ex)
            {
                //RetrieveMessageLog(LogLevel.CriticalErrors, LogLevel.CriticalErrors, Ex.Message);

                string ErrorMessage = Ex.Message;

                //WRITE A ROW IN LOG FILE AND DB
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in RetrieveDBMessage(): " + ErrorMessage + "", "", true, false);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                LogManager.WriteFileLog(LogLevel.Warnings, false, NewRow);

                //Return a Default Message according to language
                return DefaultErrorMessage(IDLanguage);
            }
        }

        /// <summary>
        /// //Return a Default Message according to language
        /// </summary>
        /// <param name="IDLanguage">User Language</param>
        /// <returns>Default Error Message get from WebConfig</returns>
        public static string DefaultErrorMessage(int IDLanguage)
        {
            string DefaultErrorMessage;

            switch (IDLanguage)
            {
                case 1:
                    DefaultErrorMessage = AppConfig.GetValue("GeneralError1", AppDomain.CurrentDomain);
                    break;
                case 2:
                    DefaultErrorMessage = AppConfig.GetValue("GeneralError2", AppDomain.CurrentDomain);
                    break;
                case 3:
                    DefaultErrorMessage = AppConfig.GetValue("GeneralError3", AppDomain.CurrentDomain);
                    break;
                case 4:
                    DefaultErrorMessage = AppConfig.GetValue("GeneralError4", AppDomain.CurrentDomain);
                    break;
                case 5:
                    DefaultErrorMessage = AppConfig.GetValue("GeneralError5", AppDomain.CurrentDomain);
                    break;
                default:
                    DefaultErrorMessage = AppConfig.GetValue("GeneralError1", AppDomain.CurrentDomain);
                    break;
            }

            return DefaultErrorMessage;
        }
    }
}
