using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.ObjectManager.UserManager;
using System.Data.Objects;
using MyCookin.DAL.Message;
using MyCookin.Log;
using MyCookin.Common;
using System.Web;

namespace MyCookin.ObjectManager.MessageManager
{
    public class UserConversation
    {
        #region Privatefields
        private Guid _IDUserConversation;
        private Guid? _IDConversation;
        private Guid? _IDUser;
        private DateTime _CreatedOn;
        private DateTime? _ArchivedOn;

        private MyUser[] _UsersRecipient;

        private Guid _IDUserSender;
        private Guid _IDUserRecipient;
        private DateTime? _LastMessageViewedOn;  //Date of the last message viewed in the conversation
        private Guid _IDUserConversationSender;

        private string _Name;
        private string _Surname;
        private bool _UserIsOnLine;
        #endregion

        #region PublicFields
        public Guid IDUserConversation
        {
            get { return _IDUserConversation;}
            set { _IDUserConversation = value;}
        }
        public Guid? IDConversation
        {
            get { return _IDConversation;}
            set { _IDConversation = value;}
        }
        public Guid? IDUser
        {
            get { return _IDUser;}
            set { _IDUser = value;}
        }
        public DateTime CreatedOn
        {
            get { return _CreatedOn;}
            set { _CreatedOn = value;}
        }
        public DateTime? ArchivedOn
        {
            get { return _ArchivedOn;}
            set { _ArchivedOn = value;}
        }
        public MyUser[] UsersRecipient
        {
            get { return _UsersRecipient; }
            set { _UsersRecipient = value; }
        }
        public Guid IDUserSender
        {
            get { return _IDUserSender; }
            set { _IDUserSender = value; }
        }
        public Guid IDUserRecipient
        {
            get { return _IDUserRecipient; }
            set { _IDUserRecipient = value; }
        }
        public DateTime? LastMessageViewedOn
        {
            get { return _LastMessageViewedOn; }
            set { _LastMessageViewedOn = value; }
        }
        public Guid IDUserConversationSender
        {
            get { return _IDUserConversationSender; }
            set { _IDUserConversationSender = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }
        public bool UserIsOnLine
        {
            get { return _UserIsOnLine; }
            set { _UserIsOnLine = value; }
        }
        
        #endregion

        #region Constructors
        public UserConversation()
        { 
        }
        
        /// <summary>
        /// Create new conversation for two people
        /// </summary>
        /// <param name="IDUserSender">Sender</param>
        /// <param name="IDUserRecipient">Recipient</param>
        /// <param name="IDConversation">Conversation ID, the same for all participants</param>
        /// <param name="ArchivedOn">Pass NULL. Just to choose correct constructor later</param>
        public UserConversation(Guid IDUserSender, Guid IDUserRecipient, Guid IDConversation, DateTime? ArchivedOn)
        {
            _IDUserSender = IDUserSender;
            _IDUserRecipient = IDUserRecipient;
            _IDConversation = IDConversation;
            _ArchivedOn = ArchivedOn;
        }

        /// <summary>
        /// Actions on a specific conversation
        /// </summary>
        /// <param name="IDUserConversation"></param>
        public UserConversation(Guid IDUserConversation)
        {
            _IDUserConversation = IDUserConversation;
        }

        /// <summary>
        /// Check if a User is part of a Conversation OR Get Users of a Conversation
        /// </summary>
        /// <param name="IDConversation"></param>
        public UserConversation(Guid? IDConversation, Guid? IDUser)
        {
            _IDConversation = IDConversation;
            _IDUser = IDUser;
        }

        #endregion

        #region Methods
        #region InsertNewConversation
        public List<UserConversation> InsertNewConversation()
        {
            bool IsError;
            string ResultExecutionCode;
            string USPReturnValue;
            DateTime CreatedOn = DateTime.UtcNow;

            List<UserConversation> NewConversationList = new List<UserConversation>();

            DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();
            
            //Insert row for Sender
            try
            {
                ObjectResult<USPResult> ResultList = ent_MessageChat.USP_InsertConversation(_IDUserSender, _IDConversation, CreatedOn);
                USPResult _result = ResultList.First();

                IsError = _result.isError;
                ResultExecutionCode = _result.ResultExecutionCode;
                USPReturnValue = _result.USPReturnValue;

                if (!IsError)
                {
                    NewConversationList.Add(
                        new UserConversation()
                        {
                            _IDUserConversation = new Guid(USPReturnValue),
                            _IDUser = _IDUserSender,
                            _CreatedOn = CreatedOn,
                            _IDConversation = _IDConversation,
                            _ArchivedOn = null
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in InsertNewConversation(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            //Insert row for Recipient
            try
            {
                ObjectResult<USPResult> ResultList = ent_MessageChat.USP_InsertConversation(IDUserRecipient, _IDConversation, CreatedOn);
                USPResult _result = ResultList.First();

                IsError = _result.isError;
                ResultExecutionCode = _result.ResultExecutionCode;
                USPReturnValue = _result.USPReturnValue;

                if (!IsError)
                {
                    NewConversationList.Add(
                        new UserConversation()
                        {
                            _IDUserConversation = new Guid(USPReturnValue),
                            _IDUser = IDUserRecipient,
                            _CreatedOn = CreatedOn,
                            _IDConversation = _IDConversation,
                            _ArchivedOn = null
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in InsertNewConversation(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return NewConversationList;
        }
        #endregion

        #region GetConversationIdBetweenTwoUsers
        public static Guid? GetConversationIdBetweenTwoUsers(Guid IDUserSender, Guid IDUserRecipient)
        {
            Guid? IDConversation = null;

            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<Conversation> ResultList = ent_MessageChat.USP_GetConversationIdBetweenTwoUsers(IDUserSender, IDUserRecipient);

                Conversation _result = ResultList.First();

                IDConversation = new Guid(_result.IDConversation.ToString());
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in GetConversationIdBetweenTwoUsers(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }
            
            return IDConversation;

        }
        #endregion

        #region SetUserConversationAsArchived
        /// <summary>
        /// Update a conversation as archived
        /// </summary>
        /// <returns></returns>
        public bool SetUserConversationAsArchived()
        {
            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<USPResult> ResultList = ent_MessageChat.USP_UserConversationSetAsArchived(_IDUserConversation);
                USPResult _result = ResultList.First();

                bool IsError = _result.isError;
                string ResultExecutionCode = _result.ResultExecutionCode;
                string USPReturnValue = _result.USPReturnValue;

                return true;
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in SetUserConversationAsArchived(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }

                return false;
            }
        }
        #endregion

        #region SetUserConversationAsActive
        /// <summary>
        /// Update a conversation as active (Set null the column ArchivedOn)
        /// </summary>
        /// <returns></returns>
        public bool SetUserConversationAsActive()
        {
            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<USPResult> ResultList = ent_MessageChat.USP_UserConversationSetAsActive(_IDConversation);
                USPResult _result = ResultList.First();

                bool IsError = _result.isError;
                string ResultExecutionCode = _result.ResultExecutionCode;
                string USPReturnValue = _result.USPReturnValue;

                return true;
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in SetUserConversationAsActive(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }

                return false;
            }
        }
        #endregion

        #region GetUsersConversationList
        /// <summary>
        /// Get List of Conversation by ID
        /// </summary>
        /// <returns></returns>
        public List<UserConversation> GetUsersConversationList()
        {
            List<UserConversation> UserConversationList = new List<UserConversation>();

            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<UsersConversations> ResultList = ent_MessageChat.USP_GetUsersOfAConversation(_IDConversation);

                foreach (UsersConversations t in ResultList)
                {
                    UserConversationList.Add(
                        new UserConversation()
                        {
                            _ArchivedOn = t.ArchivedOn,
                            _CreatedOn = t.CreatedOn,
                            _IDConversation = t.IDConversation,
                            _IDUser = t.IDUser,
                            _IDUserConversation = t.IDUserConversation
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in GetUsersConversationList(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return UserConversationList;
        }
        #endregion

        #region GetMyConversations
        /// <summary>
        /// Get List of My Conversations not archived
        /// </summary>
        /// <returns>My IDUserConversation and all users of conversations</returns>
        public List<UserConversation> GetMyConversations()
        {
            List<UserConversation> MyConversationList = new List<UserConversation>();

            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<ListOfConversations> ResultList = ent_MessageChat.USP_GetMyConversations(_IDUser);

                foreach (ListOfConversations t in ResultList)
                {
                    //Ottengo le informazioni dell'utente della lista
                    MyUser UserInfo = new MyUser(t.Friend);
                    UserInfo.GetUserInfoAllByID();

                    MyConversationList.Add(
                        new UserConversation()
                        {
                            _IDConversation = t.IDConversation,
                            _IDUser = _IDUser,
                            _IDUserConversation = t.IDUserConversation,
                            _IDUserRecipient = t.Friend,
                            _Name = UserInfo.Name,
                            _Surname = UserInfo.Surname,
                            _UserIsOnLine = UserInfo.UserIsOnLine != null ? (bool)UserInfo.UserIsOnLine : false
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in GetMyConversations(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return MyConversationList;
        }
        #endregion

        #region IsUserPartOfAConversation
        /// <summary>
        /// Is a User part of a Conversation?
        /// </summary>
        /// <returns></returns>
        public List<UserConversation> IsUserPartOfAConversation()
        {
            List<UserConversation> UserConversationList = new List<UserConversation>();

            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<UsersConversations> ResultList = ent_MessageChat.USP_IsUserPartOfAConversation(_IDUser, _IDConversation);

                //Will return always 1 record at maximum
                foreach (UsersConversations t in ResultList)
                {
                    UserConversationList.Add(
                        new UserConversation()
                        {
                            _ArchivedOn = t.ArchivedOn,
                            _CreatedOn = t.CreatedOn,
                            _IDConversation = t.IDConversation,
                            _IDUser = t.IDUser,
                            _IDUserConversation = t.IDUserConversation
                        });
                }
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in IsUserPartOfConversation(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return UserConversationList;
        }
        #endregion

        #region GetNumberOfMessagesToRead
        #endregion
        public int GetNumberOfMessagesToRead()
        {
            int numberOfMessagesToRead = 0;

            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<NumberOfMessages> ResultNumber = ent_MessageChat.USP_CountMessagesNotRead(_IDUserConversation);

                NumberOfMessages _ObjNumber = ResultNumber.First();

                numberOfMessagesToRead = _ObjNumber.MessagesNumber;
            }
            catch 
            { 
            
            }

            return numberOfMessagesToRead;
  
        }

        #endregion

    }
}
