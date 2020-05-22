using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat;
using TaechIdeas.Core.Core.Chat.Dto;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Network;

namespace TaechIdeas.Core.BusinessLogic.Chat
{
    public class MessageManager : IMessageManager
    {
        private readonly ILogManager _logManager;
        private readonly INetworkManager _networkManager;

        public MessageManager(ILogManager logManager, INetworkManager networkManager)
        {
            _logManager = logManager;
            _networkManager = networkManager;
        }

        #region InsertNewMessage

        /// <summary>
        ///     Insert New Message
        /// </summary>
        /// <param name="idMessageType"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public InsertNewMessageOutput InsertNewMessage(InsertNewMessageInput insertNewMessageInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var idMessage = new Guid();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_InsertMessage(idMessageType, message);
            //    var result = resultList.First();

            //    var isError = result.isError;
            //    var resultExecutionCode = result.ResultExecutionCode;
            //    var uspReturnValue = result.USPReturnValue;

            //    idMessage = new Guid(uspReturnValue);
            //}
            //catch (Exception ex)
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in ViewConversation(): {ex.Message}",
            //            ErrorMessageCode = "ME-ER-9999",
            //            ErrorSeverity = LogLevel.Errors,
            //            FileOrigin = _networkManager.GetCurrentPageName(),
            //            IdUser = HttpContext.Current.Session["IDUser"].ToString(),
            //        };

            //        _logManager.WriteLog(logRow);
            //    }
            //    catch
            //    {
            //    }
            //}

            //return idMessage;
        }

        #endregion

        #region GetMessageInfoByID

        /// <summary>
        ///     Get Message Info By ID
        /// </summary>
        /// <param name="idMessage"></param>
        /// <returns></returns>
        public IEnumerable<MessageByIdOutput> MessageById(MessageByIdInput messageInfoByIdInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var messagesList = new List<MessageByIdOutput>();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_GetMessageInfoByID(idMessage);

            //    foreach (var t in resultList)
            //    {
            //        messagesList.Add(
            //            new MessageByIdOutput()
            //            {
            //                IdMessage = t.IDMessage,
            //                IdMessageType = (MessageType) t.IDMessageType,
            //                Message = t.Message
            //            });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in GetMessageInfoByID(): {ex.Message}",
            //            ErrorMessageCode = "ME-ER-9999",
            //            ErrorSeverity = LogLevel.Errors,
            //            FileOrigin = _networkManager.GetCurrentPageName(),
            //            IdUser = HttpContext.Current.Session["IDUser"].ToString(),
            //        };

            //        _logManager.WriteLog(logRow);
            //    }
            //    catch
            //    {
            //    }
            //}

            //return messagesList;
        }

        #endregion
    }
}