using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat;
using TaechIdeas.Core.Core.Chat.Dto;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Network;

namespace TaechIdeas.Core.BusinessLogic.Chat
{
    public class MessageTypeManager : IMessageTypeManager
    {
        private readonly ILogManager _logManager;
        private readonly INetworkManager _networkManager;

        public MessageTypeManager(ILogManager logManager, INetworkManager networkManager)
        {
            _logManager = logManager;
            _networkManager = networkManager;
        }

        #region GetTypeOfMessageInfoByID

        /// <summary>
        ///     Get TypeOfMessage Informations according to ID
        /// </summary>
        /// <param name="idMessageType"></param>
        /// <returns></returns>
        public IEnumerable<TypeOfMessageInfoByIdOutput> TypeOfMessageInfoById(TypeOfMessageInfoByIdInput typeOfMessageInfoByIdInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var messageTypeList = new List<TypeOfMessageInfoByIdOutput>();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_GetTypeOfMessageInfoByID(idMessageType);

            //    foreach (var t in resultList)
            //    {
            //        messageTypeList.Add(
            //            new TypeOfMessageInfoByIdOutput()
            //            {
            //                IdMessageType = (MessageType) t.IDMessageType,
            //                MessageType = t.MessageType,
            //                MessageMaxLength = t.MessageMaxLength,
            //                MessageTypeEnabled = t.MessageTypeEnabled
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
            //            ErrorMessage = $"Error in GetTypeOfMessageInfoByID(): {ex.Message}",
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
            //return messageTypeList;
        }

        #endregion
    }
}