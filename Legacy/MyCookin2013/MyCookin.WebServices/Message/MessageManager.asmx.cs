using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.ObjectManager.MessageManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.MyUserNotificationManager;
using MyCookin.Common;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;

namespace MyCookin.WebServices.MessageNotificationWS
{
    /// <summary>
    /// Summary description for UserNotifications
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class MessageNotifications : System.Web.Services.WebService
    {
        #region GetMessagesToRead
        [WebMethod]
        //Messages to read
        public List<ConversationMessage> GetMessagesToRead(string IDUserConversationOwner)
        {
            List<ConversationMessage> MessagesList = new List<ConversationMessage>();

            ConversationMessage MessagesToRead = new ConversationMessage(new Guid(IDUserConversationOwner));

            MessagesList = MessagesToRead.GetMessagesToRead();

            return MessagesList;
        }
        #endregion

        #region GetMessagesToReadByUser
        [WebMethod]
        //Messages to read
        public List<ConversationMessage> GetMessagesToReadByUser(string IDUserConversationOwner, string IDUserSender)
        {
            List<ConversationMessage> MessagesList = new List<ConversationMessage>();

            ConversationMessage MessagesToReadByUser = new ConversationMessage(new Guid(IDUserConversationOwner), new Guid(IDUserSender), true);

            MessagesList = MessagesToReadByUser.GetMessagesToReadByUser();

            return MessagesList;
        }
        #endregion

        #region GetListOfUsersConversations
        [WebMethod]
        //Get List of Users of whom we have an active conversation - Not Archived Conversations
        public List<UserConversation> GetListOfUsersConversations(string IDUser)
        {
            Guid me = new Guid(IDUser);

            List<UserConversation> MyConversationList = new List<UserConversation>();

            UserConversation MyConversations = new UserConversation(null, me);
            MyConversationList = MyConversations.GetMyConversations();          //This return 1 or 2 rows, we need just one

            return MyConversationList;
        }
        #endregion

        #region ViewConversation
        [WebMethod]
        //View Conversation Messages between two users
        public List<ConversationMessage> ViewConversationPaged(string IDUserConversationOwner, string IDConversation, int Offset, int PageSize)
        {
            List<ConversationMessage> MessagesOfConversationList = new List<ConversationMessage>();

            Guid IDUserConversationOwnerGuid = new Guid(IDUserConversationOwner);
            Guid IDConversationGuid = new Guid(IDConversation);

            MessagesOfConversationList = ConversationMessage.ViewConversationPaged(IDUserConversationOwnerGuid, IDConversationGuid, Offset, PageSize);

            return MessagesOfConversationList;
        }
        #endregion

        #region GetNumberOfMessages
        [WebMethod]
        //View Conversation Messages between two users
        public int GetNumberOfMessages(string IDUserConversationOwner, string IDConversation)
        {
            List<ConversationMessage> MessagesOfConversationList = new List<ConversationMessage>();

            Guid IDUserConversationOwnerGuid = new Guid(IDUserConversationOwner);
            Guid IDConversationGuid = new Guid(IDConversation);

            ConversationMessage MessagesOfConversation = new ConversationMessage(IDUserConversationOwnerGuid, IDConversationGuid);
            int NumberOfMessages = MessagesOfConversation.GetMessagesNumber();

            return NumberOfMessages;
        }
        #endregion

        #region SendNewMessage
        [WebMethod]
        //Send new message
        public List<ConversationMessage> SendNewMessage(string IDUserSender, string RecipientsIDs, string Message, int IDLanguage)
        {
            List<ConversationMessage> ConversationMessageResult = new List<ConversationMessage>();

            try
            {
                Guid IDUserSenderGuid = new Guid(IDUserSender);

                ConversationMessage NewConversationMessage = new ConversationMessage(IDUserSenderGuid, RecipientsIDs, Message);

                ConversationMessageResult = NewConversationMessage.SendNewMessage();

                #region SendEmailForNewMessageNotification
                try
                {
                    //If the user is not online and want to receive messages notification and numberOfMessageDontread is 1, send email.

                    MyUser User = new MyUser(new Guid(RecipientsIDs));
                    User.GetUserInfoAllByID();

                    MyUser UserSender = new MyUser(IDUserSenderGuid);
                    UserSender.GetUserBasicInfoByID();
                    
                    MyUserNotification Notification = new MyUserNotification(User.IDUser, NotificationTypes.NewMessage, IDLanguage);

                    bool isUserOnline = false;

                    try
                    {
                        isUserOnline = (bool)User.UserIsOnLine;
                    }
                    catch { }

                    //Get number of messages to read
                    UserConversation uc = new UserConversation(User.IDUser);
                    int NumberOfMessagesToRead = uc.GetNumberOfMessagesToRead();

                    if (Notification.IsNotificationEnabled() && (!isUserOnline) && (NumberOfMessagesToRead == 1))
                    {
                        //string error = "";

                        string From = AppConfig.GetValue("EmailFromProfileUser", AppDomain.CurrentDomain);
                        string To = User.eMail;

                        string Subject = "";

                        //This doesn't work here - don't know why!
                        UserBoard ub = new UserBoard(ActionTypes.NewMessageReceived, IDLanguage, null);
                        //automatically it take template ;)

                        try
                        {
                            Subject = string.Format(ub.UserActionTypeTemplate, UserSender.Name + " " + UserSender.Surname);
                        }
                        catch (Exception ex) 
                        { 
                            //error = "IDLanguage: " + IDLanguage + " " + " UsrActType: " + ActionTypes.NewMessageReceived + " - " + ub.UserActionTypeTemplate + " error: " + ex.Message; 
                            Subject = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0045");
                        }


                        string link = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + Server.UrlEncode("/Message/Messages.aspx");
                        string url = "/PagesForEmail/NewMessageReceived.aspx?link=" + link;

                        Network Mail = new Network(From, To, "", "", Subject, "", url);

                        if (!Mail.SendEmail())
                        {
                           //...
                        }
                    }
                }
                catch { }
                #endregion

                return ConversationMessageResult;
            }
            catch
            {
                return ConversationMessageResult;
            }
        }
        #endregion

        #region MarkAllConversationMessagesAsViewed
        [WebMethod]
        public bool MarkAllConversationMessagesAsViewed(string IDUserConversationOwner)
        {
            MyMessageRecipient MessageNotificationAction = new MyMessageRecipient(new Guid(IDUserConversationOwner));

            return MessageNotificationAction.SetMessageAsViewed();
        }
        #endregion

        #region ArchiveConversation
        [WebMethod]
        //View Conversation Messages between two users
        public bool ArchiveConversation(string IDUserConversation)
        {
            Guid IDUserConversationGuid = new Guid(IDUserConversation);

            UserConversation ConversationAction = new UserConversation(IDUserConversationGuid);

            bool ExecutionResult = ConversationAction.SetUserConversationAsArchived();

            return ExecutionResult;
        }
        #endregion
    }
}
