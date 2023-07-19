using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.DAL.UserBoard;
using MyCookin.Log;
using System.Data.Objects;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;

namespace MyCookin.ObjectManager.UserBoardNotificationsManager
{
    public class UserBoardNotification
    {
        #region Privatefields
        private Guid _IDUserNotification;
        private Guid _IDUser;
        private ActionTypes? _IDUserActionType;
        private string _URLNotification;
        private Guid? _IDRelatedObject;
        private Guid? _NotificationImage;
        private string _UserNotification;
        private DateTime _CreatedOn;
        private DateTime? _ViewedOn;
        private DateTime? _NotifiedOn;
        private Guid? _IDUserOwnerRelatedObject;

        private bool _AllNotifications;
        #endregion

        #region PublicFields
        public Guid IDUserNotification
        {
            get { return _IDUserNotification;}
            set { _IDUserNotification = value;}
        }
        public Guid IDUser
        {
            get { return _IDUser;}
            set { _IDUser = value;}
        }
        public ActionTypes? IDUserActionType
        {
            get { return _IDUserActionType;}
            set { _IDUserActionType = value;}
        }
        public string URLNotification
        {
            get { return _URLNotification;}
            set { _URLNotification = value;}
        }
        public Guid? IDRelatedObject
        {
            get { return _IDRelatedObject;}
            set { _IDRelatedObject = value;}
        }
        public Guid? NotificationImage
        {
            get { return _NotificationImage;}
            set { _NotificationImage = value;}
        }
        public string UserNotification
        {
            get { return _UserNotification;}
            set { _UserNotification = value;}
        }
        public DateTime CreatedOn
        {
            get { return _CreatedOn;}
            set { _CreatedOn = value;}
        }
        public DateTime? ViewedOn
        {
            get { return _ViewedOn;}
            set { _ViewedOn = value;}
        }
        public DateTime? NotifiedOn
        {
            get { return _NotifiedOn; }
            set { _NotifiedOn = value; }
        }

        public bool AllNotifications
        {
            get { return _AllNotifications; }
            set { _AllNotifications = value; }
        }
        public Guid? IDUserOwnerRelatedObject
        {
            get { return _IDUserOwnerRelatedObject; }
            set { _IDUserOwnerRelatedObject = value; }
        }
        #endregion

        #region Constructors
        public UserBoardNotification()
        { 
        }

        /// <summary>
        /// Insert new Notification
        /// </summary>
        /// <param name="IDUser">Id of the user who have to receive notification</param>
        /// <param name="IDUserActionType"></param>
        /// <param name="URLNotification"></param>
        /// <param name="IDRelatedObject"></param>
        /// <param name="NotificationImage"></param>
        /// <param name="UserNotification"></param>
        /// <param name="IDUserOwnerRelatedObject"></param>
        public UserBoardNotification(Guid IDUser, ActionTypes IDUserActionType, string URLNotification, Guid? IDRelatedObject, Guid? NotificationImage, string UserNotification, Guid? IDUserOwnerRelatedObject)
        {
            _IDUser = IDUser;
            _IDUserActionType = IDUserActionType;
            _URLNotification = URLNotification;
            _IDRelatedObject = IDRelatedObject;
            _NotificationImage = NotificationImage;
            _UserNotification = UserNotification;
            _IDUserOwnerRelatedObject = IDUserOwnerRelatedObject;

            _CreatedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Mark User Notifications As Read
        /// </summary>
        /// <param name="IDUserOwnerRelatedObject"></param>
        public UserBoardNotification(Guid IDUserOwnerRelatedObject)
        {
            _IDUserOwnerRelatedObject = IDUserOwnerRelatedObject;
        }

        /// <summary>
        /// Get Notifications for a user
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="AllNotifications">True if we want ALL Notifications</param>
        public UserBoardNotification(Guid IDUserOwnerRelatedObject, bool AllNotifications)
        {
            _IDUserOwnerRelatedObject = IDUserOwnerRelatedObject;
            _AllNotifications = AllNotifications;
        }

        /// <summary>
        /// Mark Notification As Notified
        /// </summary>
        /// <param name="IDUser"></param>
        public UserBoardNotification(Guid IDUserOwnerRelatedObject, Guid IDUserNotification)
        {
            _IDUserOwnerRelatedObject = IDUserOwnerRelatedObject;
            _IDUserNotification = IDUserNotification;
        }

        /// <summary>
        /// Get Notifications for a user of a determinated ActionType
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="IDUserActionType"></param>
        /// <param name="AllNotifications">True if we want ALL Notifications</param>
        public UserBoardNotification(Guid IDUserOwnerRelatedObject, ActionTypes? IDUserActionType, bool AllNotifications)
        {
            _IDUserOwnerRelatedObject = IDUserOwnerRelatedObject;
            _IDUserActionType = IDUserActionType;
            _AllNotifications = AllNotifications;
        }

        #endregion

        #region Methods

        #region InsertNotification
        public Guid InsertNotification()
        {
            Guid IDUserNotification = new Guid();

            try
            {
                DBUsersBoardNotificationsEntities ent_Notification = new DBUsersBoardNotificationsEntities();

                ObjectResult<USPResult> ResultList = ent_Notification.USP_NotificationInsert(_IDUser, (int)_IDUserActionType, _URLNotification, _IDRelatedObject, _NotificationImage,
                                                        _UserNotification, _CreatedOn, _IDUserOwnerRelatedObject);

                USPResult _result = ResultList.First();

                bool IsError = _result.isError;
                string ResultExecutionCode = _result.ResultExecutionCode;
                string USPReturnValue = _result.USPReturnValue;

                IDUserNotification = new Guid(USPReturnValue);
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", "UserBoardNotification.cs", "US-ER-9999", "Error in Insert Notification " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }

            return IDUserNotification;
        }
        #endregion

        #region GetNotifications
        public List<UserBoardNotification> GetNotificationsForUser()
        {
            DBUsersBoardNotificationsEntities ent_Notification = new DBUsersBoardNotificationsEntities();

            List<UserBoardNotification> NotificationsList = new List<UserBoardNotification>();

            int NotificationsRead = MyConvert.ToInt32(AppConfig.GetValue("NotificationsRead", AppDomain.CurrentDomain), 5);
            
            int MaxNotificationsNumber = 1;

            
            if (AllNotifications)
            {
                //Anyway we set a limit for AllNotifications!
                MaxNotificationsNumber = 50;
            }
            else
            {
                MaxNotificationsNumber = MyConvert.ToInt32(AppConfig.GetValue("MaxNotificationsNumber", AppDomain.CurrentDomain), 50);
            }

            ObjectResult<UsersNotifications> ResultList = ent_Notification.USP_NotificationsGet(_IDUserOwnerRelatedObject, null, NotificationsRead, _AllNotifications, MaxNotificationsNumber);

            foreach (UsersNotifications t in ResultList)
            {
                //Get User Informations
                MyUser UserInfo = new MyUser(t.IDUser);
                UserInfo.GetUserBasicInfoByID();

                //Convert data to Local Time
                DateTime LocalTime = MyConvert.ToLocalTime(t.CreatedOn, UserInfo.Offset);

                NotificationsList.Add(
                    new UserBoardNotification()
                    {
                        _IDRelatedObject = t.IDRelatedObject,
                        _CreatedOn = LocalTime,
                        _IDUser = t.IDUser,
                        _IDUserActionType = (ActionTypes)t.IDUserActionType,
                        _IDUserNotification = t.IDUserNotification,
                        _NotificationImage = t.NotificationImage,
                        _URLNotification = t.URLNotification,
                        _UserNotification = t.UserNotification,
                        _ViewedOn = t.ViewedOn,
                        _NotifiedOn = t.NotifiedOn
                    }
                );
            }
               
            return NotificationsList;
        }
        #endregion

        #region MarkNotificationsAsRead (Viewed)
        public bool MarkNotificationsAsRead()
        {
            try
            {
                DBUsersBoardNotificationsEntities ent_Notification = new DBUsersBoardNotificationsEntities();

                ObjectResult<USPResult> ResultList = ent_Notification.USP_NotificationsSetAsViewed(_IDUserOwnerRelatedObject);
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
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", "UserBoardNotification.cs", "US-ER-9999", "Error in Mark Notifications as read " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }

                return false;
            }
        }
        #endregion

        #region MarkNotificationsAsNotified
        public bool MarkNotificationsAsNotified()
        {
            try
            {
                DBUsersBoardNotificationsEntities ent_Notification = new DBUsersBoardNotificationsEntities();

                ObjectResult<USPResult> ResultList = ent_Notification.USP_NotificationsSetAsNotified(_IDUserNotification);
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
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", "UserBoardNotification.cs", "US-ER-9999", "Error in Mark Notifications as Notified " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }

                return false;
            }
        }
        #endregion

        #endregion
    }
}
