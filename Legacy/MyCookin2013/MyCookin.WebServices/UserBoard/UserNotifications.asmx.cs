using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyCookin.ObjectManager.UserBoardNotificationsManager;
using System.Web.Script.Services;
using MyCookin.Common;

namespace MyCookin.WebServices.UserBoardWS
{
    /// <summary>
    /// Summary description for UserNotifications
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class UserNotifications : System.Web.Services.WebService
    {

        [WebMethod]
        public List<UserBoardNotification> GetNotificationsForUser(string IDUser)
        {
            List<UserBoardNotification> NotificationsList = new List<UserBoardNotification>();

            UserBoardNotification Notifications = new UserBoardNotification(new Guid(IDUser), false);

            NotificationsList = Notifications.GetNotificationsForUser();

            return NotificationsList;
        }

        [WebMethod]
        public bool MarkNotificationsAsRead(string IDUserOwnerRelatedObject)
        {
            UserBoardNotification NotificationAction = new UserBoardNotification(new Guid(IDUserOwnerRelatedObject));

            return NotificationAction.MarkNotificationsAsRead();
        }

        [WebMethod]
        public bool MarkNotificationsAsNotified(string IDUser, string IDUserNotification)
        {
            UserBoardNotification NotificationAction = new UserBoardNotification(new Guid(IDUser), new Guid(IDUserNotification));

            return NotificationAction.MarkNotificationsAsNotified();
        }

    }
}
