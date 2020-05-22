using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat;
using TaechIdeas.Core.Core.Chat.Dto;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.User;

namespace TaechIdeas.Core.BusinessLogic.Chat
{
    public class UserConversationManager : IUserConversationManager
    {
        private readonly ILogManager _logManager;
        private readonly IUserManager _userManager;
        private readonly INetworkManager _networkManager;

        public UserConversationManager(ILogManager logManager, IUserManager userManager, INetworkManager networkManager)
        {
            _logManager = logManager;
            _userManager = userManager;
            _networkManager = networkManager;
        }

        #region NewConversation

        public IEnumerable<NewConversationOutput> NewConversation(NewConversationInput nwNewConversationInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //bool isError;
            //string uspReturnValue;
            //var createdOn = DateTime.UtcNow;

            //var newConversationList = new List<NewConversationOutput>();

            //var entMessageChat = new DBMessageChatEntity();

            ////Insert row for Sender
            //try
            //{
            //    var resultList = entMessageChat.USP_InsertConversation(idUserSender, idConversation, createdOn);
            //    var result = resultList.First();

            //    isError = result.isError;
            //    uspReturnValue = result.USPReturnValue;

            //    if (!isError)
            //    {
            //        newConversationList.Add(
            //            new NewConversationOutput()
            //            {
            //                IdUserConversation = new Guid(uspReturnValue),
            //                IdUser = idUserSender,
            //                CreatedOn = createdOn,
            //                IdConversation = idConversation,
            //                ArchivedOn = null
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
            //            ErrorMessage = $"Error in InsertNewConversation(): {ex.Message}",
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

            ////Insert row for Recipient
            //try
            //{
            //    var resultList = entMessageChat.USP_InsertConversation(idUserRecipient, idConversation, createdOn);
            //    var result = resultList.First();

            //    isError = result.isError;
            //    uspReturnValue = result.USPReturnValue;

            //    if (!isError)
            //    {
            //        newConversationList.Add(
            //            new NewConversationOutput()
            //            {
            //                IdUserConversation = new Guid(uspReturnValue),
            //                IdUser = idUserRecipient,
            //                CreatedOn = createdOn,
            //                IdConversation = idConversation,
            //                ArchivedOn = null
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
            //            ErrorMessage = $"Error in InsertNewConversation(): {ex.Message}",
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

            //return newConversationList;
        }

        #endregion

        #region GetConversationIdBetweenTwoUsers

        public GetConversationIdBetweenTwoUsersOutput GetConversationIdBetweenTwoUsers(GetConversationIdBetweenTwoUsersInput getConversationIdBetweenTwoUsersInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //Guid? idConversation = null;

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_GetConversationIdBetweenTwoUsers(idUserSender, idUserRecipient);

            //    var result = resultList.First();

            //    idConversation = new Guid(result.IDConversation.ToString());
            //}
            //catch (Exception ex)
            //{
            //    //WRITE A ROW IN LOG FILE AND DB
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = $"Error in GetConversationIdBetweenTwoUsers(): {ex.Message}",
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

            //return idConversation;
        }

        #endregion

        #region SetUserConversationAsArchived

        /// <summary>
        ///     Update a conversation as archived
        /// </summary>
        /// <param name="idUserConversation"></param>
        /// <returns></returns>
        public SetUserConversationAsArchivedOutput SetUserConversationAsArchived(SetUserConversationAsArchivedInput setUserConversationAsArchivedInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_UserConversationSetAsArchived(idUserConversation);
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
            //            ErrorMessage = $"Error in SetUserConversationAsArchived(): {ex.Message}",
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

        #region SetUserConversationAsActive

        /// <summary>
        ///     Update a conversation as active (Set null the column ArchivedOn)
        /// </summary>
        /// <param name="idConversation"></param>
        /// <returns></returns>
        public SetUserConversationAsActiveOutput SetUserConversationAsActive(SetUserConversationAsActiveInput setUserConversationAsActiveInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_UserConversationSetAsActive(idConversation);
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
            //            ErrorMessage = $"Error in SetUserConversationAsActive(): {ex.Message}",
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

        #region UsersConversations

        /// <summary>
        ///     Get List of Conversation by ID
        /// </summary>
        /// <param name="idConversation"></param>
        /// <returns></returns>
        public IEnumerable<UsersConversationsOutput> UsersConversations(UsersConversationsInput usersConversationsInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var userConversationList = new List<NewConversationOutput>();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_GetUsersOfAConversation(idConversation);

            //    foreach (var t in resultList)
            //    {
            //        userConversationList.Add(
            //            new NewConversationOutput()
            //            {
            //                ArchivedOn = t.ArchivedOn,
            //                CreatedOn = t.CreatedOn,
            //                IdConversation = t.IDConversation,
            //                IdUser = t.IDUser,
            //                IdUserConversation = t.IDUserConversation
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
            //            ErrorMessage = $"Error in GetUsersConversationList(): {ex.Message}",
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

            //return userConversationList;
        }

        #endregion

        #region MyConversations

        /// <summary>
        ///     Get List of My Conversations not archived
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>My IDUserConversation and all users of conversations</returns>
        public IEnumerable<MyConversationsOutput> MyConversations(MyConversationsInput myConversationsInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var myConversationList = new List<NewConversationOutput>();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_GetMyConversations(idUser);

            //    foreach (var t in resultList)
            //    {
            //        //Ottengo le informazioni dell'utente della lista

            //        var userByIdInput = new UserByIdInput()
            //        {
            //            UserId = t.Friend
            //        };

            //        var userInfo = _userManager.UserById(userByIdInput);

            //        myConversationList.Add(
            //            new NewConversationOutput()
            //            {
            //                IdConversation = t.IDConversation,
            //                IdUser = idUser,
            //                IdUserConversation = t.IDUserConversation,
            //                IdUserRecipient = t.Friend,
            //                Name = userInfo.Name,
            //                Surname = userInfo.Surname,
            //                UserIsOnLine = userInfo.UserIsOnLine ?? false
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
            //            ErrorMessage = $"Error in GetMyConversations(): {ex.Message}",
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

            //return myConversationList;
        }

        #endregion

        #region IsUserPartOfAConversation

        /// <summary>
        ///     Is a User part of a Conversation?
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idConversation"></param>
        /// <returns></returns>
        public IEnumerable<IsUserPartOfAConversationOutput> IsUserPartOfAConversation(IsUserPartOfAConversationInput isUserPartOfAConversationInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var userConversationList = new List<NewConversationOutput>();

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultList = entMessageChat.USP_IsUserPartOfAConversation(idUser, idConversation);

            //    //Will return always 1 record at maximum
            //    foreach (var t in resultList)
            //    {
            //        userConversationList.Add(
            //            new NewConversationOutput()
            //            {
            //                ArchivedOn = t.ArchivedOn,
            //                CreatedOn = t.CreatedOn,
            //                IdConversation = t.IDConversation,
            //                IdUser = t.IDUser,
            //                IdUserConversation = t.IDUserConversation
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
            //            ErrorMessage = $"Error in IsUserPartOfConversation(): {ex.Message}",
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

            //return userConversationList;
        }

        #endregion

        #region NumberOfMessagesToRead

        public NumberOfMessagesToReadOutput NumberOfMessagesToRead(NumberOfMessagesToReadInput numberOfMessagesToReadInput, Guid? idUserConversation)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var numberOfMessagesToRead = 0;

            //try
            //{
            //    var entMessageChat = new DBMessageChatEntity();

            //    var resultNumber = entMessageChat.USP_CountMessagesNotRead(idUserConversation);

            //    var objNumber = resultNumber.First();

            //    numberOfMessagesToRead = objNumber.MessagesNumber;
            //}
            //catch
            //{
            //}

            //return numberOfMessagesToRead;
        }

        #endregion
    }
}