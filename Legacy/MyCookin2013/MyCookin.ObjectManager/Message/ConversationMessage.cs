using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.Message;
using System.Data.Objects;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Common;
using MyCookin.Log;
using System.Web;

namespace MyCookin.ObjectManager.MessageManager
{
    public class ConversationMessage
    {
        #region Privatefields
        private Guid _IDMessage;
        private string _Message;
        private Guid _IDMessageRecipient;
        private Guid _IDUserConversation;
        private Guid _IDUserSender;
        private DateTime _SentOn;
        private Guid _IDConversation;
        private DateTime _CreatedOn;

        private string _RecipientsIDs;

        private Guid _IDUserConversationOwner;

        private string _Name;
        private string _Surname;

        private bool _Result;
        private string _ErrorMessage;

        #endregion

        #region PublicFields
        public Guid IDMessage
        {
        get { return _IDMessage;}
        set { _IDMessage = value;}
        }
        public string Message
        {
        get { return _Message;}
        set { _Message = value;}
        }
        public Guid IDMessageRecipient
        {
        get { return _IDMessageRecipient;}
        set { _IDMessageRecipient = value;}
        }
        public Guid IDUserConversation
        {
        get { return _IDUserConversation;}
        set { _IDUserConversation = value;}
        }
        public Guid IDUserSender
        {
        get { return _IDUserSender;}
        set { _IDUserSender = value;}
        }
        public DateTime SentOn
        {
        get { return _SentOn;}
        set { _SentOn = value;}
        }
        public Guid IDConversation
        {
        get { return _IDConversation;}
        set { _IDConversation = value;}
        }
        public DateTime CreatedOn
        {
        get { return _CreatedOn;}
        set { _CreatedOn = value;}
        }
        public string RecipientsIDs
        {
            get { return _RecipientsIDs; }
            set { _RecipientsIDs = value; }
        }
        public Guid IDUserConversationOwner
        {
            get { return _IDUserConversationOwner; }
            set { _IDUserConversationOwner = value; }
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
        public bool Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        #endregion

        #region Constructors
        public ConversationMessage()
        { 
        }

        /// <summary>
        /// View Conversation between two user
        /// </summary>
        /// <param name="IDUserConversationOwner">IDUser, get it from Session</param>
        /// <param name="IDConversation">Conversation ID</param>
        public ConversationMessage(Guid IDUserConversationOwner, Guid IDConversation)
        {
            _IDUserConversationOwner = IDUserConversationOwner;
            _IDConversation = IDConversation;
        }

        /// <summary>
        /// View Conversation between two user
        /// </summary>
        /// <param name="IDUserConversationOwner">IDUser, get it from Session</param>
        /// <param name="IDConversation">Conversation ID</param>
        public ConversationMessage(Guid IDUserConversationOwner, Guid IDUserSender, bool IsCheckMessagesToReadByUser)
        {
            _IDUserConversationOwner = IDUserConversationOwner;
            _IDUserSender = IDUserSender;
        }

        /// <summary>
        /// Get Messages to read
        /// </summary>
        /// <param name="IDUserConversationOwner">IDUser, get it from Session</param>
        public ConversationMessage(Guid IDUserConversationOwner)
        {
            _IDUserConversationOwner = IDUserConversationOwner;
        }

        /// <summary>
        /// Send new Message
        /// </summary>
        /// <param name="IDUserSender"></param>
        /// <param name="RecipientsIDs"></param>
        /// <param name="Message"></param>
        public ConversationMessage(Guid IDUserSender, string RecipientsIDs, string Message)
        {
            _IDUserSender = IDUserSender;
            _RecipientsIDs = RecipientsIDs;
            _Message = Message;
        }
        #endregion

        #region Methods
        
        #region ViewConversation
        /// <summary>
        /// View All messages in a conversation
        /// </summary>
        /// <returns></returns>
        public List<ConversationMessage> ViewConversation()
        {
            List<ConversationMessage> MessagesOfConversationList = new List<ConversationMessage>();

            DBMessageChatEntity ent_MessagesOfConversation = new DBMessageChatEntity();

            try
            {
                ObjectResult<MessagesOfConversation> ResultList = ent_MessagesOfConversation.USP_GetMessagesOfAConversation(_IDUserConversationOwner, _IDConversation);

                foreach (MessagesOfConversation t in ResultList)
                {
                    MessagesOfConversationList.Add(
                        new ConversationMessage()
                        {
                            _CreatedOn = t.CreatedOn,
                            _IDConversation = t.IDConversation,
                            _IDMessage = t.IDMessage,
                            _IDMessageRecipient = t.IDMessageRecipient,
                            _IDUserConversation = t.IDUserConversation,
                            _IDUserSender = t.IDUserSender,
                            _Message = t.Message,
                            _SentOn = t.SentOn
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in ViewConversation(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return MessagesOfConversationList;
        }
        #endregion

        #region ViewConversationPaged
        /// <summary>
        /// View All messages in a conversation - Version for Paging
        /// </summary>
        /// <returns></returns>
        public static List<ConversationMessage> ViewConversationPaged(Guid IDUserConversationOwner, Guid IDConversation, int Offset, int PageSize)
        {
            List<ConversationMessage> MessagesOfConversationList = new List<ConversationMessage>();

            DBMessageChatEntity ent_MessagesOfConversation = new DBMessageChatEntity();

            try
            {
                ObjectResult<MessagesOfConversation> ResultList = ent_MessagesOfConversation.USP_GetMessagesOfAConversationPaged(IDUserConversationOwner, IDConversation, Offset, PageSize);

                foreach (MessagesOfConversation t in ResultList)
                {
                    //Get User Informations
                    MyUser UserInfo = new MyUser(t.IDUserSender);
                    UserInfo.GetUserBasicInfoByID();

                    //Convert data to Local Time
                    DateTime LocalTime = MyConvert.ToLocalTime(t.SentOn, UserInfo.Offset);

                    MessagesOfConversationList.Add(
                        new ConversationMessage()
                        {
                            _CreatedOn = t.CreatedOn,
                            _IDConversation = t.IDConversation,
                            _IDMessage = t.IDMessage,
                            _IDMessageRecipient = t.IDMessageRecipient,
                            _IDUserConversation = t.IDUserConversation,
                            _IDUserSender = t.IDUserSender,
                            _Message = t.Message,
                            _SentOn = LocalTime,
                            _Name = UserInfo.Name,
                            _Surname = UserInfo.Surname
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in ViewConversationPaged(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return MessagesOfConversationList;
        }
        #endregion

        #region GetMessagesToRead
        /// <summary>
        /// Get Messages To Read
        /// </summary>
        /// <returns></returns>
        public List<ConversationMessage> GetMessagesToRead()
        {
            List<ConversationMessage> MessagesToReadList = new List<ConversationMessage>();

            DBMessageChatEntity ent_MessagesOfConversation = new DBMessageChatEntity();

            try
            {
                ObjectResult<MessagesToRead> ResultList = ent_MessagesOfConversation.USP_GetMessagesToRead(_IDUserConversationOwner);

                foreach (MessagesToRead t in ResultList)
                {
                    MessagesToReadList.Add(
                        new ConversationMessage()
                        {
                            _IDMessage = t.IDMessage,
                            _IDUserSender = t.IDUserSender,
                            _Message = t.Message
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in GetMessagesToRead(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return MessagesToReadList;
        }
        #endregion

        #region GetMessagesToReadByUser
        /// <summary>
        /// Get Messages To Read By User
        /// </summary>
        /// <returns></returns>
        public List<ConversationMessage> GetMessagesToReadByUser()
        {
            List<ConversationMessage> MessagesToReadList = new List<ConversationMessage>();

            DBMessageChatEntity ent_MessagesOfConversation = new DBMessageChatEntity();

            try
            {
                ObjectResult<MessagesToRead> ResultList = ent_MessagesOfConversation.USP_GetMessagesToReadByUser(_IDUserConversationOwner, _IDUserSender);

                foreach (MessagesToRead t in ResultList)
                {
                    //Ottengo le informazioni dell'utente della lista
                    MyUser UserInfo = new MyUser(t.IDUserSender);
                    UserInfo.GetUserBasicInfoByID();

                    MessagesToReadList.Add(
                        new ConversationMessage()
                        {
                            _IDMessage = t.IDMessage,
                            _IDUserSender = t.IDUserSender,
                            _Message = t.Message,
                            _SentOn = t.SentOn,
                            _Name = UserInfo.Name,
                            _Surname = UserInfo.Surname
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in GetMessagesToReadByUser(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return MessagesToReadList;
        }
        #endregion

        #region GetMessagesNumber
        /// <summary>
        /// Get Number Of Messages in a conversation
        /// </summary>
        /// <returns></returns>
        public int GetMessagesNumber()
        {
            int MessagesNumber = 0;

            DBMessageChatEntity ent_MessagesOfConversation = new DBMessageChatEntity();

            try
            {
                ObjectResult<NumberOfMessages> ResultList = ent_MessagesOfConversation.USP_MessagesNumberOfAConversation(_IDUserConversationOwner, _IDConversation);

                NumberOfMessages _result = ResultList.First();

                MessagesNumber = _result.MessagesNumber;
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in GetMessagesNumber(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return MessagesNumber;
        }
        #endregion

        #region SendNewMessage
        public List<ConversationMessage> SendNewMessage()
        {
            List<ConversationMessage> NewConversationMessageList = new List<ConversationMessage>();

            Guid ConversationID = new Guid();

            try
            {
                MyUser UserSender = new MyUser(_IDUserSender);

                MyUser[] UsersRecipient;
                string[] recipientsList = new string[1];

                if (_RecipientsIDs.IndexOf(',') > -1)
                {
                    recipientsList = _RecipientsIDs.Split(',');

                    UsersRecipient = new MyUser[recipientsList.Length];
                }
                else
                {
                    UsersRecipient = new MyUser[1];
                    recipientsList[0] = _RecipientsIDs;
                }

                for (int i = 0; i < recipientsList.Length; i++)
                {
                    UsersRecipient[i] = new MyUser(new Guid(recipientsList[i]));
                }

                //Insert new message - Unique
                MyMessage NewMessage = new MyMessage(MessageType.Message, _Message);
                Guid NewMessageID = NewMessage.InsertNewMessage();

                List<UserConversation> UserConversationComponents = new List<UserConversation>();
                List<MyMessageRecipient> Recipient = new List<MyMessageRecipient>();

                for (int i = 0; i < UsersRecipient.Length; i++)
                {
                    //Get Conversation Id - Between two people
                    Guid? IDConversation = UserConversation.GetConversationIdBetweenTwoUsers(UserSender.IDUser, UsersRecipient[i].IDUser);
                    if (IDConversation != null)
                    {
                        ConversationID = (Guid)IDConversation;

                        //Get all conversation components to associate them new messages
                        UserConversation ConversationObj = new UserConversation(ConversationID, null);
                        UserConversationComponents = ConversationObj.GetUsersConversationList();

                        //If conversation had been set as archivied, set it as active again
                        if (UserConversationComponents[0].ArchivedOn != null)
                        {
                            ConversationObj.SetUserConversationAsActive();
                        }
                    }
                    else
                    {
                        //Create new conversation
                        ConversationID = Guid.NewGuid();

                        UserConversation NewConversation = new UserConversation(UserSender.IDUser, UsersRecipient[i].IDUser, ConversationID, null);

                        //Insert n rows for each user
                        UserConversationComponents = NewConversation.InsertNewConversation();
                    }

                    //Insert conversation for TWO Users in Recipient
                    MyMessageRecipient NewRecipient = new MyMessageRecipient(NewMessageID, UserConversationComponents, UserSender);
                    Recipient = NewRecipient.InsertNewMessageRecipients();
                }

                //POPULATE OBJECT
                //NOTICE: In the future, if multi-recipients will be active, be careful with the [0] in the Lists!
                //*************************************************
                NewConversationMessageList.Add(
                        new ConversationMessage()
                        {
                            _CreatedOn = UserConversationComponents[0].CreatedOn,
                            _IDConversation = ConversationID,
                            _IDMessage = NewMessageID,
                            //_IDMessageRecipient = t.IDMessageRecipient,
                            _IDUserConversation = UserConversationComponents[0].IDUserConversation,
                            //_IDUserSender = t.IDUserSender,
                            _Message = _Message,
                            _IDUserConversationOwner = UserSender.IDUser,
                            _RecipientsIDs = _RecipientsIDs,
                            _SentOn = Recipient[0].SentOn,
                            _Result = true,
                            _ErrorMessage = ""
                            //_Name = "" //Is null..
                            //_Surname = "" //Is null..                           
                        }
                 );

                //*************************************************
            }
            catch (Exception ex)
            {
                NewConversationMessageList.Add(
                        new ConversationMessage()
                        {
                            _Message = _Message,
                            _RecipientsIDs = _RecipientsIDs,
                            _Result = false,
                            _ErrorMessage = ex.Message
                            //_Name = "" //Is null..
                            //_Surname = "" //Is null..                           
                        }
                 );

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in SendNewMessage(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return NewConversationMessageList;
        }
        #endregion

        #endregion

    }
}
