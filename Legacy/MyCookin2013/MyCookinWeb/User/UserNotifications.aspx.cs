using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserBoardNotificationsManager;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.User
{
    public partial class UserNotifications :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid IDUserGuid = new Guid();
            int IDLanguage = 1;

            //If not logged go to Login
            //*****************************
            if (!MyUser.CheckUserLogged())
            {
                Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl()).ToLower(), true);
            }
            else
            {
                IDUserGuid = new Guid(Session["IDUser"].ToString());

                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
            }
            //*****************************

            LoadNotifications();
        }

        protected void LoadNotifications() 
        {
            Guid IDUserOwnerRelatedObject = new Guid(Session["IDUser"].ToString());

            List<UserBoardNotification> NotificationsList = new List<UserBoardNotification>();

            UserBoardNotification Notifications = new UserBoardNotification(IDUserOwnerRelatedObject, true);

            NotificationsList = Notifications.GetNotificationsForUser();

            gwNotifications.DataSource = NotificationsList;
            gwNotifications.DataBind();
            
        }
    }
}