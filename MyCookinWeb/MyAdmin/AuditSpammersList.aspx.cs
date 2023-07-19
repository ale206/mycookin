using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.AuditManager;
using MyCookin.ObjectManager;

namespace MyCookinWeb.MyAdmin
{
    public partial class AuditSpammersList :  MyCookinWeb.Form.MyPageBase
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
            if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("72f85f59-37e9-4bdd-ade2-ea45127f774b") >= 0)
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

        protected void lbtnUserNoSpam_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                string IDUser = lbtn.CommandArgument;

                Audit AuditObj = new Audit(new Guid(IDUser), false);

                string ExecutionDelete = AuditObj.DeleteByObjectID();

                if (String.IsNullOrEmpty(ExecutionDelete))
                {
                    Response.Redirect(("/MyAdmin/AuditSpammersList.aspx").ToLower(), true);
                }
                else
                {
                    lblError.Text = "Errore: " + ExecutionDelete;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Errore: " + ex.Message;
            }
        }
    }
}