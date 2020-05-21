using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.Common;

namespace MyCookinWeb.MyAdmin
{
    public partial class AllIngredientChecked :  MyCookinWeb.Form.MyPageBase
    {
        int IDLanguage;
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

            IDLanguage = Convert.ToInt32(Session["IDLanguage"]);

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

            if (!IsPostBack)
            {
                DataTable dt = IngredientLanguage.GetAllIngredientChecked(IDLanguage);
                gvIngrChecked.DataSource = dt;
                gvIngrChecked.DataBind();

                lblIngrChecked.Text = "Ingredient just checked: " + dt.Rows.Count;
            }
        }

        protected void gvIngrModByMe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIngrChecked.DataSource = IngredientLanguage.GetAllIngredientChecked(IDLanguage);
            gvIngrChecked.PageIndex = e.NewPageIndex;
            gvIngrChecked.DataBind();
        }

        protected void lblIDIngredient_DataBinding(object sender, EventArgs e)
        {
            Label _myLabel = (Label)sender;
            _myLabel.Text = "<a href=\"/Ingredient/MngIngredients.aspx?IDIngr=" + _myLabel.Text + "\"><img src=\"/Images/Form/iconWrite1.png\"/></a>";
        }
    }
}