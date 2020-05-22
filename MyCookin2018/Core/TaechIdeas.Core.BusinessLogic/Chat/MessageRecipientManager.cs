using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat;
using TaechIdeas.Core.Core.Chat.Dto;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.User;

namespace TaechIdeas.Core.BusinessLogic.Chat
{
    public class MessageRecipientManager : IMessageRecipientManager
    {
        private readonly IUserManager _userManager;
        private readonly ILogManager _logManager;
        private readonly INetworkManager _networkManager;

        public MessageRecipientManager(IUserManager userManager, ILogManager logManager, INetworkManager networkManager)
        {
            _userManager = userManager;
            _logManager = logManager;
            _networkManager = networkManager;
        }

        #region InsertNewMessageRecipient

        /// <summary>
        ///     Insert new Message Recipient For each User
        /// </summary>
        /// <param name="userConversationComponents"></param>
        /// <param name="idUserSender"></param>
        /// <param name="idMessage"></param>
        /// <returns>List of Message Recipients</returns>
        public IEnumerable<NewMessageRecipientOutput> NewMessageRecipient(NewMessageRecipientInput newMessageRecipientInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var messageRecipientList = new List<NewMessageRecipientOutput>();

            //var entMessageChat = new DBMessageChatEntity();

            //try
            //{
            //    //First, insert MessageRecipient for the SENDER (sender is the owner). Notification is not necessary.
            //    for (var i = 0; i < userConversationComponents.Count; i++)
            //    {
            //        //Individuiamo la posizione del sender tra i due componenti
            //        var idUserReceiver = new Guid();

            //        if (userConversationComponents[i].IdUser == idUserSender)
            //        {
            //            if (i == 0)
            //            {
            //                var idUser = userConversationComponents[1].IdUser;
            //                if (idUser != null) idUserReceiver = (Guid) idUser;
            //            }
            //            else
            //            {
            //                idUserReceiver = (Guid) userConversationComponents[0].IdUser;
            //            }

            //            //For Sender, set ViewedOn valorized
            //            var viewedOn = DateTime.UtcNow;

            //            var firstResultList =
            //                entMessageChat.USP_InsertMessageRecipient(idMessage,
            //                    userConversationComponents[i].IdUserConversation, idUserSender,
            //                    idUserReceiver, DateTime.UtcNow, viewedOn, null);

            //            var result = firstResultList.First();

            //            var isError = result.isError;
            //            var resultExecutionCode = result.ResultExecutionCode;
            //            var uspReturnValue = result.USPReturnValue;

            //            messageRecipientList.Add(
            //                new NewMessageRecipientOutput()
            //                {
            //                    DeletedOn = null,
            //                    IdMessage = idMessage,
            //                    IdMessageRecipient = new Guid(uspReturnValue),
            //                    IdUserRecipient = (Guid) userConversationComponents[1].IdUser,
            //                    IdUserSender = idUserSender,
            //                    SentOn = DateTime.UtcNow,
            //                    ViewedOn = viewedOn
            //                }
            //                );
            //        }
            //        else
            //        {
            //            if (i == 1)
            //            {
            //                idUserReceiver = (Guid) userConversationComponents[1].IdUser;
            //            }
            //            else
            //            {
            //                idUserReceiver = (Guid) userConversationComponents[0].IdUser;
            //            }

            //            var resultList =
            //                entMessageChat.USP_InsertMessageRecipient(idMessage,
            //                    userConversationComponents[i].IdUserConversation, idUserSender,
            //                    userConversationComponents[i].IdUser, DateTime.UtcNow, null, null);

            //            var result = resultList.First();

            //            var isError = result.isError;
            //            var resultExecutionCode = result.ResultExecutionCode;
            //            var uspReturnValue = result.USPReturnValue;

            //            messageRecipientList.Add(
            //                new NewMessageRecipientOutput()
            //                {
            //                    DeletedOn = null,
            //                    IdMessage = idMessage,
            //                    IdMessageRecipient = new Guid(uspReturnValue),
            //                    IdUserRecipient = (Guid) userConversationComponents[1].IdUser,
            //                    IdUserSender = idUserSender,
            //                    SentOn = DateTime.UtcNow,
            //                    ViewedOn = null
            //                }
            //                );

            //            #region Notification

            //            try
            //            {
            //                var idLanguage = 1;

            //                try
            //                {
            //                    idLanguage = Convert.ToInt32(HttpContext.Current.Session["RecipeLanguageId"]);
            //                }
            //                catch
            //                {
            //                }

            //                //Template and Notification
            //                //var userBoardAction = _userBoardManager.GetTemplate(ActionTypes.NewMessageReceived, idLanguage);

            //                //Get Correct Template
            //                //userBoardAction.GetTemplate();

            //                var userByIdInput = new UserByIdInput()
            //                {
            //                    UserId = idUserSender
            //                };

            //                //User Recipient Information
            //                var userInAction = _userManager.UserById(userByIdInput);

            //                //URLNotification = "#";
            //                //IdRelatedObject = idMessage;
            //                //NotificationImage = null;

            //                //Create Notification String

            //                //Insert Notification - Disactivated
            //                //UserBoardNotification newNotification = new UserBoardNotification(__IdUserReceiver, ActionTypes.NewMessageReceived, URLNotification, IdRelatedObject, NotificationImage, UserNotification);
            //                //newNotification.InsertNotification();
            //            }
            //            catch
            //            {
            //            }

            //            #endregion
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in InsertNewMessageRecipients(): {ex.Message}",
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

            //return messageRecipientList;
        }

        #endregion

        #region SetMessageAsViewed

        /// <summary>
        ///     Set All Messages To Read As Viewed
        /// </summary>
        /// <param name="idUserConversationOwner"></param>
        /// <returns></returns>
        public SetMessageAsViewedOutput SetMessageAsViewed(SetMessageAsViewedInput setMessageAsViewedInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_MessageSetAsViewed(idUserConversationOwner);
            //    var result = resultList.First();

            //    var isError = result.isError;
            //    var resultExecutionCode = result.ResultExecutionCode;
            //    var uspReturnValue = result.USPReturnValue;

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    var logRow = new LogRowIn()
            //    {
            //        ErrorMessage = $"Error in SetMessageAsViewed. This is generally call in Public.Master click on the Message Icon.: {ex.Message}",
            //        ErrorMessageCode = "ME-ER-9999",
            //        ErrorSeverity = LogLevel.Errors,
            //        FileOrigin = _networkManager.GetCurrentPageName(),
            //        IdUser = HttpContext.Current.Session["IDUser"].ToString(),
            //    };

            //    _logManager.WriteLog(logRow);

            //    return false;
            //}
        }

        #endregion

        #region SetAllConversationMessagesAsViewed

        /// <summary>
        ///     Set All Conversation Messages As Viewed
        /// </summary>
        /// <param name="idUserConversationOwner"></param>
        /// <param name="idConversation"></param>
        /// <returns></returns>
        public SetAllConversationMessagesAsViewedOutput SetAllConversationMessagesAsViewed(SetAllConversationMessagesAsViewedInput setAllConversationMessagesAsViewedInpu)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_MessagesOfAConversationSetAsViewed(idUserConversationOwner,
            //        idConversation);
            //    var result = resultList.First();

            //    var isError = result.isError;
            //    var resultExecutionCode = result.ResultExecutionCode;
            //    var uspReturnValue = result.USPReturnValue;

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in SetAllConversationMessagesAsViewed: {ex.Message}",
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

            //    return false;
            //}
        }

        #endregion

        #region SetMessageAsDeleted

        /// <summary>
        ///     Set Message As Deleted
        /// </summary>
        /// <param name="idMessageRecipient"></param>
        /// <returns></returns>
        public SetMessageAsDeletedOutput SetMessageAsDeleted(SetMessageAsDeletedInput setMessageAsDeletedInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_MessageSetAsDeleted(idMessageRecipient);
            //    var result = resultList.First();

            //    var isError = result.isError;
            //    var resultExecutionCode = result.ResultExecutionCode;
            //    var uspReturnValue = result.USPReturnValue;

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in SetMessageAsDeleted: {ex.Message}",
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
            //    return false;
            //}
        }

        #endregion
    }
}