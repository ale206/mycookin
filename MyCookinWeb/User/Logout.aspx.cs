using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Log;
using MyCookin.Common;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb.User
{
    public partial class Logout :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserId;
            int IDLanguage;

            //Just to use in Log Writing
            Guid IDUserGuid = new Guid();

            try
            {
                UserId = Session["IDUser"].ToString();
                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                IDUserGuid = new Guid(Session["IDUser"].ToString());

                MyUser User = new MyUser(IDUserGuid);
                User.GetUserBasicInfoByID();

                User.LogoutUser();
            }
            catch
            {
                UserId = "0";
                IDLanguage = 1;
            }

            try
            {
                //Destroy Session Variables
                Session["Name"] = "";
                Session["Surname"] = "";
                Session["eMail"] = "";
                Session["IDUser"] = "";
                Session["IDLanguage"] = "";
                Session["imgName"] = "";
                Session["FileMD5"] = "";
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();

                //WRITE A ROW IN STATISTICS DB - not necessary
                //MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_SessionDestroyed, "Session Destroyed", Network.GetCurrentPageName(), "", "");
                //NewStatisticUser.InsertNewRow();
                
            }
            catch(Exception ex)
            {
                string ErrorMessage = ex.Message;

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-IN-0010", "Error in Logout User: Sessions not destroyed! - ", UserId, true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }

            //LOGOUT
            try
            {
                //Destroy Cookie
                Network.DestroyCookie();

                //WRITE A ROW IN STATISTICS DB - not necessary
                //MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_CookieDestroyed, "Cookie Destroyed", Network.GetCurrentPageName(), "", "");
                //NewStatisticUser.InsertNewRow();
            }
            catch(Exception ex)
            {
                string ErrorMessage = ex.Message;

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-IN-0010", "Error in Logout User: Cookies not destroyed! - " + ErrorMessage, UserId, true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }

            string requestedPage;
            requestedPage = Request.QueryString["requestedPage"];

            //If null, set requestedPage to default page (MyProfile or something else)
            if (requestedPage == null)
            {
                requestedPage = ResolveClientUrl(AppConfig.GetValue("HomePage", AppDomain.CurrentDomain));
            }

            Response.Redirect(requestedPage, true);
        }
    }
}