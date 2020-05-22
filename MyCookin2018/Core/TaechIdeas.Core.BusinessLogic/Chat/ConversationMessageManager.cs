using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat;
using TaechIdeas.Core.Core.Chat.Dto;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.User;

namespace TaechIdeas.Core.BusinessLogic.Chat
{
    public class ConversationMessageManager : IConversationMessageManager
    {
        private readonly ILogManager _logManager;
        private readonly IUserManager _userManager;
        private readonly IMessageManager _messageManager;
        private readonly IUserConversationManager _userConversationManager;
        private readonly IMessageRecipientManager _messageRecipientManager;
        private readonly INetworkManager _networkManager;
        private readonly IMyConvertManager _myConvertManager;

        public ConversationMessageManager(ILogManager logManager, IUserManager userManager, IMessageManager messageManager, IUserConversationManager userConversationManager,
            IMessageRecipientManager messageRecipientManager, INetworkManager networkManager, IMyConvertManager myConvertManager)
        {
            _logManager = logManager;
            _userManager = userManager;
            _messageManager = messageManager;
            _userConversationManager = userConversationManager;
            _messageRecipientManager = messageRecipientManager;
            _networkManager = networkManager;
            _myConvertManager = myConvertManager;
        }

        #region ViewConversation

        /// <summary>
        ///     View All messages in a conversation
        /// </summary>
        /// <param name="idUserConversationOwner"></param>
        /// <param name="idConversation"></param>
        /// <returns></returns>
        public IEnumerable<ConversationMessage> ViewConversation(Guid? idUserConversationOwner, Guid? idConversation)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var messagesOfConversationList = new List<ConversationMessage>();

            //var entMessagesOfConversation = new DBMessageChatEntity();

            //try
            //{
            //    var resultList = entMessagesOfConversation.USP_GetMessagesOfAConversation(idUserConversationOwner,
            //        idConversation);

            //    foreach (var t in resultList)
            //    {
            //        messagesOfConversationList.Add(
            //            new ConversationMessage()
            //            {
            //                CreatedOn = t.CreatedOn,
            //                IdConversation = t.IDConversation,
            //                IdMessage = t.IDMessage,
            //                IdMessageRecipient = t.IDMessageRecipient,
            //                IdUserConversation = t.IDUserConversation,
            //                IdUserSender = t.IDUserSender,
            //                Message = t.Message,
            //                SentOn = t.SentOn
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

            //return messagesOfConversationList;
        }

        #endregion

        #region ViewConversationPaged

        /// <summary>
        ///     View All messages in a conversation - Version for Paging
        /// </summary>
        /// <returns></returns>
        public List<ConversationMessage> ViewConversationPaged(Guid idUserConversationOwner, Guid idConversation,
            int offset, int pageSize)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var messagesOfConversationList = new List<ConversationMessage>();

            //var entMessagesOfConversation = new DBMessageChatEntity();

            //try
            //{
            //    var resultList = entMessagesOfConversation.USP_GetMessagesOfAConversationPaged(
            //        idUserConversationOwner, idConversation, offset, pageSize);

            //    foreach (var t in resultList)
            //    {
            //        var userByIdInput = new UserByIdInput()
            //        {
            //            UserId = t.IDUserSender
            //        };

            //        //Get User Informations
            //        var userInfo = _userManager.UserById(userByIdInput);

            //        //Convert data to Local Time
            //        var localTime = _myConvertManager.ToLocalTime(t.SentOn, userInfo.Offset);

            //        messagesOfConversationList.Add(
            //            new ConversationMessage()
            //            {
            //                CreatedOn = t.CreatedOn,
            //                IdConversation = t.IDConversation,
            //                IdMessage = t.IDMessage,
            //                IdMessageRecipient = t.IDMessageRecipient,
            //                IdUserConversation = t.IDUserConversation,
            //                IdUserSender = t.IDUserSender,
            //                Message = t.Message,
            //                SentOn = localTime,
            //                Name = userInfo.Name,
            //                Surname = userInfo.Surname
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
            //            ErrorMessage = $"Error in ViewConversationPaged(): {ex.Message}",
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

            //return messagesOfConversationList;
        }

        #endregion

        #region GetMessagesToRead

        /// <summary>
        ///     Get Messages To Read
        /// </summary>
        /// <param name="idUserConversationOwner"></param>
        /// <returns></returns>
        public IEnumerable<ConversationMessage> GetMessagesToRead(Guid? idUserConversationOwner)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var messagesToReadList = new List<ConversationMessage>();

            //var entMessagesOfConversation = new DBMessageChatEntity();

            //try
            //{
            //    var resultList = entMessagesOfConversation.USP_GetMessagesToRead(idUserConversationOwner);

            //    foreach (var t in resultList)
            //    {
            //        messagesToReadList.Add(
            //            new ConversationMessage()
            //            {
            //                IdMessage = t.IDMessage,
            //                IdUserSender = t.IDUserSender,
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
            //            ErrorMessage = $"Error in GetMessagesToRead(): {ex.Message}",
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

            //return messagesToReadList;
        }

        #endregion

        #region GetMessagesToReadByUser

        /// <summary>
        ///     Get Messages To Read By User
        /// </summary>
        /// <param name="idUserConversationOwner"></param>
        /// <param name="idUserSender"></param>
        /// <returns></returns>
        public IEnumerable<ConversationMessage> GetMessagesToReadByUser(Guid? idUserConversationOwner, Guid? idUserSender)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var messagesToReadList = new List<ConversationMessage>();

            //var entMessagesOfConversation = new DBMessageChatEntity();

            //try
            //{
            //    var resultList = entMessagesOfConversation.USP_GetMessagesToReadByUser(idUserConversationOwner,
            //        idUserSender);

            //    foreach (var t in resultList)
            //    {
            //        var userByIdInput = new UserByIdInput()
            //        {
            //            UserId = t.IDUserSender
            //        };

            //        //Ottengo le informazioni dell'utente della lista
            //        var userInfo = _userManager.UserById(userByIdInput);

            //        messagesToReadList.Add(
            //            new ConversationMessage()
            //            {
            //                IdMessage = t.IDMessage,
            //                IdUserSender = t.IDUserSender,
            //                Message = t.Message,
            //                SentOn = t.SentOn,
            //                Name = userInfo.Name,
            //                Surname = userInfo.Surname
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
            //            ErrorMessage = $"Error in GetMessagesToReadByUser(): {ex.Message}",
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

            //return messagesToReadList;
        }

        #endregion

        #region GetMessagesNumber

        /// <summary>
        ///     Get Number Of Messages in a conversation
        /// </summary>
        /// <param name="idUserConversationOwner"></param>
        /// <param name="idConversation"></param>
        /// <returns></returns>
        public int GetMessagesNumber(Guid? idUserConversationOwner, Guid? idConversation)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var messagesNumber = 0;

            //var entMessagesOfConversation = new DBMessageChatEntity();

            //try
            //{
            //    var resultList = entMessagesOfConversation.USP_MessagesNumberOfAConversation(idUserConversationOwner,
            //        idConversation);

            //    var result = resultList.First();

            //    messagesNumber = result.MessagesNumber;
            //}
            //catch (Exception ex)
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in GetMessagesNumber(): {ex.Message}",
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

            //return messagesNumber;
        }

        #endregion

        #region SendNewMessage

        public IEnumerable<ConversationMessage> SendNewMessage(Guid idUserSender, string recipientsIDs, string message)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var newConversationMessageList = new List<ConversationMessage>();

            //var conversationId = new Guid();

            //try
            //{
            //    var userByIdInput = new UserByIdInput()
            //    {
            //        UserId = idUserSender
            //    };

            //    var userSender = _userManager.UserById(userByIdInput);

            //    MyUser[] usersRecipient;
            //    var recipientsList = new string[1];

            //    if (recipientsIDs.IndexOf(',') > -1)
            //    {
            //        recipientsList = recipientsIDs.Split(',');

            //        usersRecipient = new MyUser[recipientsList.Length];
            //    }
            //    else
            //    {
            //        usersRecipient = new MyUser[1];
            //        recipientsList[0] = recipientsIDs;
            //    }

            //    for (var i = 0; i < recipientsList.Length; i++)
            //    {
            //        //TODO: RESTORE
            //        //usersRecipient[i] = _userManager.UserById(new Guid(recipientsList[i]));
            //    }

            //    //Insert new message - Unique
            //    var newMessageId = _messageManager.InsertNewMessage((int) MessageType.Message, message);

            //    var userConversationComponents = new List<NewConversationOutput>();
            //    var recipient = new List<NewMessageRecipientOutput>();

            //    for (var i = 0; i < usersRecipient.Length; i++)
            //    {
            //        //Get Conversation Id - Between two people
            //        var idConversation = _userConversationManager.GetConversationIdBetweenTwoUsers(userSender.UserId,
            //            usersRecipient[i].IdUser);
            //        if (idConversation != null)
            //        {
            //            //Get all conversation components to associate them new messages
            //            //var conversationObj = new UserConversation(conversationId, null);
            //            userConversationComponents = _userConversationManager.UsersConversations(conversationId).ToList();

            //            //If conversation had been set as archivied, set it as active again
            //            if (userConversationComponents[0].ArchivedOn != null)
            //            {
            //                _userConversationManager.SetUserConversationAsActive(conversationId);
            //            }
            //        }
            //        else
            //        {
            //            //Create new conversation
            //            conversationId = Guid.NewGuid();

            //            //var NewConversation = new UserConversation(UserSender.IdUser, UsersRecipient[i].IdUser,
            //            //    ConversationID, null);

            //            //Insert n rows for each user
            //            //UserConversationComponents = NewConversation.NewConversation();
            //            userConversationComponents = _userConversationManager.NewConversation(userSender.UserId, conversationId, usersRecipient[i].IdUser).ToList();
            //        }

            //        //Insert conversation for TWO Users in Recipient
            //        //var NewRecipient = new NewMessageRecipientOutput(NewMessageID, UserConversationComponents, UserSender);
            //        recipient = _messageRecipientManager.NewMessageRecipient(userConversationComponents, userSender.UserId, newMessageId).ToList();
            //    }

            //    //POPULATE OBJECT
            //    //NOTICE: In the future, if multi-recipients will be active, be careful with the [0] in the Lists!
            //    //*************************************************
            //    newConversationMessageList.Add(
            //        new ConversationMessage()
            //        {
            //            CreatedOn = userConversationComponents[0].CreatedOn,
            //            IdConversation = conversationId,
            //            IdMessage = newMessageId,
            //            //IDMessageRecipient = t.IDMessageRecipient,
            //            IdUserConversation = userConversationComponents[0].IdUserConversation,
            //            //IDUserSender = t.IDUserSender,
            //            Message = message,
            //            IdUserConversationOwner = userSender.UserId,
            //            RecipientsIDs = recipientsIDs,
            //            SentOn = recipient[0].SentOn,
            //            Result = true,
            //            ErrorMessage = ""
            //            //_Name = "" //Is null..
            //            //_Surname = "" //Is null..                           
            //        }
            //        );

            //    //*************************************************
            //}
            //catch (Exception ex)
            //{
            //    newConversationMessageList.Add(
            //        new ConversationMessage()
            //        {
            //            Message = message,
            //            RecipientsIDs = recipientsIDs,
            //            Result = false,
            //            ErrorMessage = ex.Message
            //            //Name = "" //Is null..
            //            //Surname = "" //Is null..                           
            //        }
            //        );

            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in SendNewMessage(): {ex.Message}",
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

            //return newConversationMessageList;
        }

        #endregion
    }
}