using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Common;

namespace MyCookinWeb.MyAdmin
{
    public partial class UsersGoups :  MyCookinWeb.Form.MyPageBase
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
                pnlMain.Visible = true;
                pnlNoAuth.Visible = false;
            }
            else
            {
                pnlMain.Visible = false;
                pnlNoAuth.Visible = true;
            }
            Page.Form.DefaultButton = null;
        }

        protected void frmUserGroup_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            e.Values["MembershipDate"] = DateTime.UtcNow;
            e.Values["IDSecurityGroupUserMember"] = Guid.NewGuid().ToString();
        }

   }
}