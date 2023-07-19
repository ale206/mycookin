using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.User;
using System.Data.Objects;
using MyCookin.Log;
using MyCookin.Common;

namespace MyCookin.ObjectManager.MyUserNotificationManager
{
    public enum NotificationTypes : int
    {
        //See DB UsersProfile -> UsersNotificationsTypes
        NewFollower = 1,
        NewMessage = 2,
        FriendRegistered = 3,
        NewComment = 4,
        NewLike = 5,
        FollowingInsertRecipe = 6,
        NewsFromMyCookin = 7,
        MyCookinNewsletter = 8
    }

    public class MyUserNotification
    {
        #region Privatefields
        private NotificationTypes _IDUserNotificationType;
        private string _NotificationType;
        private bool _NotificationTypeEnabled;
        private int _NotificationTypeOrder;
        private bool _IsVisible;
        private int _IDUserNotificationLanguage;
        private int _IDLanguage;
        private string _NotificationQuestion;
        private string _NotificationComment;
        private Guid _IDUserNotification;
        private Guid _IDUser;
        private bool _IsEnabled;
        
        #endregion

        #region PublicFields
        public NotificationTypes IDUserNotificationType
        {
            get { return _IDUserNotificationType;}
            set { _IDUserNotificationType = value;}
        }
        public string NotificationType
        {
            get { return _NotificationType;}
            set { _NotificationType = value;}
        }
        public bool NotificationTypeEnabled
        {
            get { return _NotificationTypeEnabled;}
            set { _NotificationTypeEnabled = value;}
        }
        public int NotificationTypeOrder
        {
            get { return _NotificationTypeOrder; }
            set { _NotificationTypeOrder = value; }
        }
        public bool IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        public int IDUserNotificationLanguage
        {
            get { return _IDUserNotificationLanguage;}
            set { _IDUserNotificationLanguage = value;}
        }
        public int IDLanguage
        {
            get { return _IDLanguage;}
            set { _IDLanguage = value;}
        }
        public string NotificationQuestion
        {
            get { return _NotificationQuestion;}
            set { _NotificationQuestion = value;}
        }
        public string NotificationComment
        {
            get { return _NotificationComment;}
            set { _NotificationComment = value;}
        }
        public Guid IDUserNotification
        {
            get { return _IDUserNotification;}
            set { _IDUserNotification = value;}
        }
        public Guid IDUser
        {
            get { return _IDUser; }
            set { _IDUser = value; }
        }
        public bool IsEnabled
        {
            get { return _IsEnabled;}
            set { _IsEnabled = value;}
        }

        #endregion

        #region Constructors
        public MyUserNotification()
        { 
        }

        /// <summary>
        /// To get all notfications enabled
        /// </summary>
        /// <param name="IDUser"></param>
        public MyUserNotification(Guid IDUser, int IDLanguage)
        {
            _IDUser = IDUser;
            _IDLanguage = IDLanguage;
        }

        /// <summary>
        /// To Update Notification Setting
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="IDUserNotification"></param>
        /// <param name="IsEnabled"></param>
        public MyUserNotification(Guid IDUser, Guid IDUserNotification, bool IsEnabled)
        {
            _IDUser = IDUser;
            _IDUserNotification = IDUserNotification;
            _IsEnabled = IsEnabled;
        }

        /// <summary>
        /// To check if a Notification is Enabled for a user
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="IDUserNotificationType"></param>
        public MyUserNotification(Guid IDUser, NotificationTypes IDUserNotificationType, int IDLanguage)
        {
            _IDUser = IDUser;
            _IDUserNotificationType = IDUserNotificationType;
            _IDLanguage = IDLanguage;
        }
        #endregion

        #region Methods

        #region GetNotificationList
        /// <summary>
        /// Get all notifications enabled by user. 
        /// If User is not yet present in the table the SP will insert and return default.
        /// </summary>
        /// <returns></returns>
        public List<MyUserNotification> GetNotificationList()
        {
            List<MyUserNotification> UsersNotificationsList = new List<MyUserNotification>();

            try
            {
                DBUserNotificationsEntity ent_UserNotification = new DBUserNotificationsEntity();

                ObjectResult<vGetUsersNotificationsByIDUserAndIDLanguage> ResultList = ent_UserNotification.USP_GetNotificationsByUser(_IDUser, _IDLanguage);

                foreach (vGetUsersNotificationsByIDUserAndIDLanguage t in ResultList)
                {
                    UsersNotificationsList.Add(
                        new MyUserNotification()
                        {
                            _IDUserNotificationType = (NotificationTypes)t.IDUserNotificationType,
                            _NotificationType = t.NotificationType,
                            _NotificationTypeEnabled = t.NotificationTypeEnabled,
                            _NotificationTypeOrder = t.NotificationTypeOrder,
                            _IsVisible = t.IsVisible,
                            _IDUserNotificationLanguage = t.IDUserNotificationLanguage,
                            _IDLanguage = t.IDLanguage,
                            _NotificationQuestion = t.NotificationQuestion,
                            _NotificationComment = t.NotificationComment,
                            _IDUserNotification = t.IDUserNotification,
                            _IDUser = t.IDUser,
                            _IsEnabled = t.IsEnabled
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on GetNotificationList: " + ex.Message, _IDUser.ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
            
            return UsersNotificationsList;
        }
        #endregion

        #region UpdateUserNotificationSetting
        /// <summary>
        /// Update User Notification Setting
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserNotificationSetting()
        {
            try
            {
                DBUserNotificationsEntity ent_UserNotification = new DBUserNotificationsEntity();

                ObjectResult<vGetUsersNotificationsByIDUserAndIDLanguage> ResultList = ent_UserNotification.USP_UpdateUserNotification(_IDUserNotification, _IsEnabled);

                return true;
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on UpdateUserNotificationSetting: " + ex.Message, _IDUser.ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }

                return false;
            }
        }
        #endregion

        #region IsNotificationEnabled
        public bool IsNotificationEnabled()
        {
            bool IsEnabled = false;

            try
            {
                DBUserNotificationsEntity ent_UserNotification = new DBUserNotificationsEntity();

                ObjectResult<vGetUsersNotificationsByIDUserAndIDLanguage> ResultList = ent_UserNotification.USP_IsNotificationEnabled(_IDUser, (int)_IDUserNotificationType, _IDLanguage);

                foreach (vGetUsersNotificationsByIDUserAndIDLanguage t in ResultList)
                {
                    IsEnabled = t.IsEnabled;
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on IsNotificationEnabled: " + ex.Message, _IDUser.ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
            
            return IsEnabled;
        }
        #endregion

        #endregion
    }
}
