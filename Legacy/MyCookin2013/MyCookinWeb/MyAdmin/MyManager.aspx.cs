using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.MyAdmin
{
    public partial class MyManager :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Check Authorization to Visualize this Page
            * If not authorized, redirect to login.
           //*****************************************/
            PageSecurity SecurityPage = new PageSecurity(Session["IDUser"].ToString(), Network.GetCurrentPageName());

            if (!SecurityPage.CheckAuthorization())
            {
                Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl()).ToLower(), true);
            }
            //******************************************

            //Check if user belong group authorized to view this page
            if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("292d13f2-738f-487b-b739-96c52b9e8d21") >= 0)
            {
                pnlMyManager.Visible = true;
                pnlNoAuth.Visible = false;
            }
            else
            {
                pnlMyManager.Visible = false;
                pnlNoAuth.Visible = true;
            }

        }
    }
}