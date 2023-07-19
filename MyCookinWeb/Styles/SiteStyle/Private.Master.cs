using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Log;
using System.Data;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookinWeb.Styles.SiteStyle
{
    public partial class Private : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfIpAddress.Value = HttpContext.Current.Request.UserHostAddress;

            try
            {
                hfBgPath.Value = Session["hfBgPath"].ToString();
            }
            catch
            {
            }

            try
            {
                hfOffset.Value = Session["Offset"].ToString();
            }
            catch
            {
            }

            try
            {
                hfIDLangage.Value = Session["IDLanguage"].ToString();
            }
            catch
            {
                hfIDLangage.Value = "1";
            }

            //BOTTOM
            lblCopyright.Text = "MyCookin &copy; " + DateTime.UtcNow.Year;

            //hlContact.Attributes["onclick"] = "OpenNewContactDialog(); return false;";

                if (!IsPostBack)
                {

                    #region Notifications
                    //Load PopBoxWithCloseFunction (Function Passed: StartTimer, registered in this page)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "StartPopBoxWithCloseFunction('#" + pnlNotifications.ClientID + "','#" + hlImageNotification.ClientID + "','#" + pnlBoxInternal.ClientID + "');", true);
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                       "StartPopBoxWithCloseFunction('#" + pnlUserSettings.ClientID + "','#" + hlSettings.ClientID + "','#" + pnlUserSettingsPopBox.ClientID + "');", true);

                    //Hide the label with "No Notification"
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "$('#lblNoNotifications').hide();", true);

                    //First Load of Panel with notifications (When document is ready)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "$(document).ready(function(){UsersNotificationsLoad('" + pnlContainerNotificationList.ClientID + "', '" + Session["IDUser"].ToString() + "')});", true);

                    //Start Timer for Notification. It will be stopped on Logout Button Click - NOTICE: The Global Variable for this timer is in MyNotifications.js
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "$(document).ready(function(){NotificationTimer=setInterval(function(){UsersNotificationsLoad('" + pnlContainerNotificationList.ClientID + "', '" + Session["IDUser"].ToString() + "')}," + AppConfig.GetValue("NotificationTimerValue", AppDomain.CurrentDomain) + ");});", true);

                    //PopBoxWithCloseFunction OnClose Event: Reactivate Timer and reload panel. - NOTICE: The Global Variable for this timer is in MyNotifications.js
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
                    //    "function StartTimer() { var ReloadAfter = setTimeout( function(){ UsersNotificationsLoad('" + pnlContainerNotificationList.ClientID + "', '" + Session["IDUser"].ToString() + "')},1000 ); " +
                    //                            "NotificationTimer=setInterval(function(){UsersNotificationsLoad('" + pnlContainerNotificationList.ClientID + "', '" + Session["IDUser"].ToString() + "')}," + AppConfig.GetValue("NotificationTimerValue", AppDomain.CurrentDomain) + ");}", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
                          "function StartTimer() { NotificationTimer=setInterval(function(){UsersNotificationsLoad('" + pnlContainerNotificationList.ClientID + "', '" + Session["IDUser"].ToString() + "')}," + AppConfig.GetValue("NotificationTimerValue", AppDomain.CurrentDomain) + ");}", true);


                    //Open Notifications Panel - NOTICE: The Global Variable for this timer is in MyNotifications.js
                    hlImageNotification.Attributes["onclick"] = "clearInterval(NotificationTimer);MarkNotificationsAsRead('" + pnlContainerNotificationList.ClientID + "', '" + Session["IDUser"].ToString() + "');";

                    #endregion


                    #region MessagesNotifications
                    //Start Timer for Notification. It will be stopped on Logout Button Click - NOTICE: The Global Variable for this timer is in MyMessagesNotifications.js
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "$(document).ready(function(){MessageCountTimer=setInterval(function(){MessagesNotificationsLoad('" + Session["IDUser"].ToString() + "')}," + AppConfig.GetValue("NotificationTimerValue", AppDomain.CurrentDomain) + ");});", true);

                    hlImageMessageNotification.Attributes["href"] = "#";
                    hlImageMessageNotification.Attributes["onclick"] = "MarkMessagesAsRead('" + Session["IDUser"].ToString() + "');location.href=\"/Message/Messages.aspx\"";
                    #endregion


                    //if(!IsPostBack)
                    //{
                    //If logged Show button for LOGGED And Hide Buttons for NO LOGGED 
                    pnlButtonsForLogged.Visible = true;

                    //Show Logo
                    //pnlLogo.Visible = true;

                    #region UserInfoForPictureAndLink
                    Guid IDUserGuid = new Guid();
                    try
                    {
                        IDUserGuid = new Guid(Session["IDUser"].ToString());
                    }
                    catch
                    {
                        Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain)).ToLower(), true);
                    }

                    MyUser UserInfo = new MyUser(IDUserGuid);

                    UserInfo.GetUserBasicInfoByID();

                    if (UserInfo.IDProfilePhoto != null)
                    {
                        string _profileImagePath = "";
                        try
                        {
                            _profileImagePath = UserInfo.IDProfilePhoto.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                            if(String.IsNullOrEmpty(_profileImagePath))
                            {
                                _profileImagePath = UserInfo.IDProfilePhoto.GetCompletePath(false, false, true);
                            }
                        }
                        catch
                        {
                            _profileImagePath = UserInfo.IDProfilePhoto.GetCompletePath(false, false, true);
                        }
                        
                        ProfilePic.ImageUrl = _profileImagePath;
                    }
                    else
                    {
                        ProfilePic.ImageUrl = "/Images/icon-user.png";
                    }

                    //Name of the User
                    hlUser.Text = UserInfo.Name;
                    //hlUser.NavigateUrl = AppConfig.GetValue("RoutingUser", AppDomain.CurrentDomain).ToString() + UserInfo.UserName;
                    //hlUser.NavigateUrl = AppConfig.GetValue("PrincipalUserBoard", AppDomain.CurrentDomain).ToString();
                    hlUser.NavigateUrl = ("/" + UserInfo.UserName + "/news").ToLower();
                    hlUserImage.NavigateUrl = hlUser.NavigateUrl;
                    #endregion
                }
        }

        #region BUTTONS

        protected void btnLogo_Click(object sender, ImageClickEventArgs e)
        {
            //Redirect to the Principal UserBoard (MyNews.aspx)
            Response.Redirect((AppConfig.GetValue("HomePage", AppDomain.CurrentDomain)).ToLower(), true);
        }

        protected void btnLogout_Click(object sender, ImageClickEventArgs e)
        {
            //STOP NOTIFICATIONS TIMER - NOTICE: The Global Variable for this timer is in MyNotifications.js
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                "clearInterval(NotificationTimer);", true);

            //STOP MESSAGES NOTIFICATIONS TIMER - NOTICE: The Global Variable for this timer is in MessagesSetAsRead.js
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                "clearInterval(MessageCountTimer);", true);

            string link = AppConfig.GetValue("LogoutPage", AppDomain.CurrentDomain);
            Response.Redirect((link).ToLower(), true);
        }

        protected void btnIngredients_Click(object sender, ImageClickEventArgs e)
        {
            string link = "/recipemng/recipedashboard.aspx";
            Response.Redirect(link, true);
        }

        protected void btnUserProfile_Click(object sender, ImageClickEventArgs e)
        {
            //string link = "/User/UserProfile.aspx?IDUserRequested=" + Session["IDUser"].ToString();
            string link = ("/" + Session["Username"].ToString() + "/").ToLower();
            Response.Redirect(link, true);
        }

        protected void btnMyProfile_Click(object sender, ImageClickEventArgs e)
        {
            //string link = "/User/EditProfile.aspx";
            string link = ("/" + Session["Username"].ToString() + "/").ToLower();
            Response.Redirect(link, true);
        }

        protected void btnSettings_Click(object sender, ImageClickEventArgs e)
        {
            string link = "/user/editprofile.aspx";
            Response.Redirect(link, true);
        }
        
        protected void btnMyRecipeBook_Click(object sender, ImageClickEventArgs e)
        {
            string link = ("/" + Session["Username"].ToString() + AppConfig.GetValue("RoutingRecipeBook" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain)).ToLower();		
            Response.Redirect(link, true);
        }

        protected void btnAddRecipe_Click(object sender, ImageClickEventArgs e)
        {
            string link = "";
            switch (Session["IDLanguage"].ToString())
            {
                case "1":
                    link = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/add").ToLower();
                    break;
                case "2":
                    link = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/aggiungi").ToLower();
                    break;
                case "3":
                    link = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/nuevo").ToLower();
                    break;
                default:
                    link = ("/RecipeMng/CreateRecipe.aspx").ToLower();
                    break;
            }
             
            Response.Redirect(link, true);
        }
            

        #endregion

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            //STOP NOTIFICATIONS TIMER - NOTICE: The Global Variable for this timer is in MyNotifications.js
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                "clearInterval(NotificationTimer);", true);

            //STOP MESSAGES NOTIFICATIONS TIMER - NOTICE: The Global Variable for this timer is in MessagesSetAsRead.js
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                "clearInterval(MessageCountTimer);", true);

            string link = (AppConfig.GetValue("LogoutPage", AppDomain.CurrentDomain)).ToLower();
            Response.Redirect(link, true);
        }

        protected void lnkSettings_Click(object sender, EventArgs e)
        {
            string link = "/user/editprofile.aspx";
            Response.Redirect(link, true);
        }

        protected void lnkMyProfile_Click(object sender, EventArgs e)
        {

            //string link = "/User/UserProfile.aspx?IDUserRequested=" + Session["IDUser"].ToString();
            string link = ("/" + Session["Username"].ToString() + "/").ToLower();
            Response.Redirect(link, true);
        }

        protected void lnkIngrdient_Click(object sender, EventArgs e)
        {
            string link = "/RecipeMng/RecipeDashBoard.aspx";
            Response.Redirect(link, true);
        }

        protected void lnkMyRecipeBook_Click(object sender, EventArgs e)
        {
            string link = ("/" + Session["Username"].ToString() + AppConfig.GetValue("RoutingRecipeBook" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain)).ToLower();
            Response.Redirect(link, true);
        }

        protected void lnkAddRecipe_Click(object sender, EventArgs e)
        {
            string link = "";
            switch (Session["IDLanguage"].ToString())
            {
                case "1":
                    link = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/add").ToLower();
                    break;
                case "2":
                    link = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/aggiungi").ToLower();
                    break;
                case "3":
                    link = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/nuevo").ToLower();
                    break;
                default:
                    link = ("/RecipeMng/CreateRecipe.aspx").ToLower();
                    break;
            }
             
            Response.Redirect(link, true);
        }
        
    }
}
