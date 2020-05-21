using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.Message;
using System.Data.Objects;
using MyCookin.ObjectManager.UserBoardManager;
using System.Web;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.UserBoardNotificationsManager;
using MyCookin.Common;
using MyCookin.Log;

namespace MyCookin.ObjectManager.MessageManager
{
    public class MyMessageRecipient
    {
        #region Privatefields
        private Guid _IDMessageRecipient;
        private Guid _IDMessage;
        private UserConversation _Conversation;
        private Guid _IDUserSender;
        private Guid _IDUserRecipient;
        private DateTime _SentOn;
        private DateTime? _ViewedOn;
        private DateTime? _DeletedOn;

        List<UserConversation> _UserConversationComponents;
        private Guid _IDUserConversationOwner;  //Owner of the conversation
        private bool _OnlyMessageToRead;
        private Guid _IDConversation;
        #endregion

        #region PublicFields
        public Guid IDMessageRecipient
        {
        get { return _IDMessageRecipient;}
        set { _IDMessageRecipient = value;}
        }
        public Guid IDMessage
        {
        get { return _IDMessage;}
        set { _IDMessage = value;}
        }
        public UserConversation Conversation
        {
            get { return _Conversation; }
            set { _Conversation = value; }
        }
        public Guid IDUserSender
        {
            get { return _IDUserSender; }
            set { _IDUserSender = value; }
        }
        public Guid IDUserRecipient
        {
        get { return _IDUserRecipient;}
        set { _IDUserRecipient = value;}
        }
        public DateTime SentOn
        {
            get { return _SentOn; }
            set { _SentOn = value; }
        }
        public DateTime? ViewedOn
        {
        get { return _ViewedOn;}
        set { _ViewedOn = value;}
        }
        public DateTime? DeletedOn
        {
        get { return _DeletedOn;}
        set { _DeletedOn = value;}
        }

        public List<UserConversation> UserConversationComponents
        {
            get { return _UserConversationComponents; }
            set { _UserConversationComponents = value; }
        }
        public Guid IDUserConversationOwner
        {
            get { return _IDUserConversationOwner; }
            set { _IDUserConversationOwner = value; }
        }
        public bool OnlyMessageToRead
        {
            get { return _OnlyMessageToRead; }
            set { _OnlyMessageToRead = value; }
        }
        public Guid IDConversation
        {
            get { return _IDConversation; }
            set { _IDConversation = value; }
        }
        #endregion

        #region Constructors
        public MyMessageRecipient()
        { 
        }

        /// <summary>
        /// Insert new MessageRecipient
        /// </summary>
        /// <param name="IDMessage"></param>
        /// <param name="IDUserRecipient"></param>
        public MyMessageRecipient(Guid IDMessage, List<UserConversation> UserConversationComponents, Guid IDUserSender)
        {
            _IDMessage = IDMessage;
            _UserConversationComponents = UserConversationComponents;
            _IDUserSender = IDUserSender;
            
            _SentOn = DateTime.UtcNow;
            _ViewedOn = null;
            _DeletedOn = null;
        }

        /// <summary>
        /// Update Informations
        /// </summary>
        /// <param name="IDMessageRecipient"></param>
        //public MyMessageRecipient(Guid IDMessageRecipient)
        //{
        //    _IDMessageRecipient = IDMessageRecipient;
        //}

        /// <summary>
        /// Set All Conversation Messages As Viewed
        /// </summary>
        /// <param name="IDUserConversationOwner"></param>

        public MyMessageRecipient(Guid IDUserConversationOwner)
        {
            _IDUserConversationOwner = IDUserConversationOwner;
        }

        /// <summary>
        /// Set All Conversation Messages As Viewed
        /// </summary>
        /// <param name="IDUserConversationOwner"></param>
        /// <param name="IDConversation"></param>
        //public MyMessageRecipient(Guid IDUserConversationOwner, Guid IDConversation)
        //{
        //    _IDUserConversationOwner = IDUserConversationOwner;
        //    _IDConversation = IDConversation;
        //}

        #endregion

        #region Methods
       
        #region InsertNewMessageRecipient
        /// <summary>
        /// Insert new Message Recipient For each User
        /// </summary>
        /// <returns>List of Message Recipients</returns>
        public List<MyMessageRecipient> InsertNewMessageRecipients()
        {
            List<MyMessageRecipient> MessageRecipientList = new List<MyMessageRecipient>();
            
            DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

            Guid __IDUserReceiver = new Guid();

            try
            {
                //First, insert MessageRecipient for the SENDER (sender is the owner). Notification is not necessary.
                for (int i = 0; i < _UserConversationComponents.Count; i++)
                {                    
                    //Individuiamo la posizione del sender tra i due componenti
                    if (_UserConversationComponents[i].IDUser == _IDUserSender)
                    {
                        if (i == 0)
                        {
                            __IDUserReceiver = (Guid)_UserConversationComponents[1].IDUser;
                        }
                        else
                        {
                            __IDUserReceiver = (Guid)_UserConversationComponents[0].IDUser;
                        }

                        //For Sender, set ViewedOn valorized
                        DateTime __ViewedOn = DateTime.UtcNow;

                        ObjectResult<USPResult> FirstResultList =
                                ent_MessageChat.USP_InsertMessageRecipient(_IDMessage, _UserConversationComponents[i].IDUserConversation, _IDUserSender,
                                __IDUserReceiver, _SentOn, __ViewedOn, _DeletedOn);

                        USPResult _result = FirstResultList.First();

                        bool IsError = _result.isError;
                        string ResultExecutionCode = _result.ResultExecutionCode;
                        string USPReturnValue = _result.USPReturnValue;

                        MessageRecipientList.Add(
                            new MyMessageRecipient()
                            {
                                _DeletedOn = _DeletedOn,
                                _IDMessage = _IDMessage,
                                _IDMessageRecipient = new Guid(USPReturnValue),
                                _IDUserRecipient = (Guid)_UserConversationComponents[1].IDUser,
                                _IDUserSender = _IDUserSender,
                                _SentOn = _SentOn,
                                _ViewedOn = __ViewedOn
                            }
                        );
                    }
                    else
                    {
                        if (i == 1)
                        {
                            __IDUserReceiver = (Guid)_UserConversationComponents[1].IDUser;
                        }
                        else
                        {
                            __IDUserReceiver = (Guid)_UserConversationComponents[0].IDUser;
                        }

                        ObjectResult<USPResult> ResultList =
                                                    ent_MessageChat.USP_InsertMessageRecipient(_IDMessage, _UserConversationComponents[i].IDUserConversation, _IDUserSender,
                                                    _UserConversationComponents[i].IDUser, _SentOn, _ViewedOn, _DeletedOn);

                        USPResult _result = ResultList.First();

                        bool IsError = _result.isError;
                        string ResultExecutionCode = _result.ResultExecutionCode;
                        string USPReturnValue = _result.USPReturnValue;

                        MessageRecipientList.Add(
                            new MyMessageRecipient()
                            {
                                _DeletedOn = _DeletedOn,
                                _IDMessage = _IDMessage,
                                _IDMessageRecipient = new Guid(USPReturnValue),
                                _IDUserRecipient = (Guid)_UserConversationComponents[1].IDUser,
                                _IDUserSender = _IDUserSender,
                                _SentOn = _SentOn,
                                _ViewedOn = _ViewedOn
                            }
                        );

                        #region Notification
                        try
                        {
                            Guid IDRelatedObject = new Guid();
                            string URLNotification = String.Empty;
                            Guid? NotificationImage = new Guid();
                            string UserNotification = String.Empty;

                            int IDLanguage = 1;

                            try
                            {
                                IDLanguage = Convert.ToInt32(HttpContext.Current.Session["IDLanguage"]);
                            }
                            catch
                            { 
                            }

                            //Template and Notification
                            UserBoard UserBoardAction = new UserBoard(ActionTypes.NewMessageReceived, IDLanguage, null);

                            //Get Correct Template
                            UserBoardAction.GetTemplate();

                            //User Recipient Information
                            MyUser UserInAction = new MyUser(_IDUserSender);
                            UserInAction.GetUserBasicInfoByID();

                            URLNotification = "#";
                            IDRelatedObject = IDMessage;
                            NotificationImage = null;

                            //Create Notification String
                            UserNotification = string.Format("<a href=\"" + URLNotification + "\">" + UserBoardAction.NotificationTemplate + "</a>", UserInAction.Name + " " + UserInAction.Surname);

                            //Insert Notification - Disactivated
                            //UserBoardNotification newNotification = new UserBoardNotification(__IDUserReceiver, ActionTypes.NewMessageReceived, URLNotification, IDRelatedObject, NotificationImage, UserNotification);
                            //newNotification.InsertNotification();
                        }
                        catch
                        {
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in InsertNewMessageRecipients(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return MessageRecipientList;
        }
        #endregion

        #region SetMessageAsViewed
        /// <summary>
        /// Set All Messages To Read As Viewed
        /// </summary>
        /// <returns></returns>
        public bool SetMessageAsViewed()
        {
            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<USPResult> ResultList = ent_MessageChat.USP_MessageSetAsViewed(_IDUserConversationOwner);
                USPResult _result = ResultList.First();

                bool IsError = _result.isError;
                string ResultExecutionCode = _result.ResultExecutionCode;
                string USPReturnValue = _result.USPReturnValue;

                return true;
            }
            catch(Exception ex)
            {

                LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in SetMessageAsViewed. This is generally call in Public.Master click on the Message Icon. " + ex.Message.ToString(), HttpContext.Current.Session["IDUser"].ToString(), true, false);
                LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);


                return false;
            }
        }
        #endregion

        #region SetAllConversationMessagesAsViewed
        /// <summary>
        /// Set All Conversation Messages As Viewed
        /// </summary>
        /// <returns></returns>
        public bool SetAllConversationMessagesAsViewed()
        {
            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<USPResult> ResultList = ent_MessageChat.USP_MessagesOfAConversationSetAsViewed(_IDUserConversationOwner, _IDConversation);
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
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in SetAllConversationMessagesAsViewed(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }

                return false;
            }
        }
        #endregion

        #region SetMessageAsDeleted
        /// <summary>
        /// Set Message As Deleted
        /// </summary>
        /// <returns></returns>
        public bool SetMessageAsDeleted()
        {
            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<USPResult> ResultList = ent_MessageChat.USP_MessageSetAsDeleted(_IDMessageRecipient);
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
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in SetMessageAsDeleted(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
                return false;
            }
        }
        #endregion
        
        #endregion
    }
}
