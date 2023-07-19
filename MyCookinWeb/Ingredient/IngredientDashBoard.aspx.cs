using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Common;

namespace MyCookinWeb.IngredientWeb
{
    public partial class IngredientDashBoard :  MyCookinWeb.Form.MyPageBase
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

            MyUser modUser = new MyUser(new Guid(Session["IDUser"].ToString()));
            modUser.GetUserBasicInfoByID();

            if (!IsPostBack)
            {
                
                //Check if user belong group authorized to view this page
                if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("5b6650ae-4430-4e64-98f5-0c08f78e2392") >= 0)
                {

                    //IDLanguage = Convert.ToInt32(Session["IDLanguage"]);

                    //MyUser modUser = new MyUser(new Guid(Session["IDUser"].ToString()));
                    //modUser.GetUserBasicInfoByID();

                    lblNumIngrToDo.Text = Ingredient.GetNumIngredientNotChecked().ToString();
                    lblNumIngrDoByUser.Text = Ingredient.GetNumIngredientCheckedByUser(modUser.IDUser).ToString();
                    gvIngrToDo.DataSource = IngredientLanguage.GetAllIngredientLangNotChecked(IDLanguage);
                    gvIngrToDo.DataBind();

                    if (Session["gvPageIndex"] == null)
                    {
                        Random rdn = new Random();
                        gvIngrToDo.PageIndex = rdn.Next(gvIngrToDo.PageCount);
                        Session["gvPageIndex"] = gvIngrToDo.PageIndex;
                    }
                    else
                    {
                        gvIngrToDo.PageIndex = Convert.ToInt32(Session["gvPageIndex"].ToString());
                    }

                    gvIngrToDo.DataBind();

                    if (modUser != null)
                    {
                        gvIngrModByMe.DataSource = IngredientLanguage.GetAllIngredientLangNotCheckedByIDLangIDUser(IDLanguage, modUser.IDUser);
                        gvIngrModByMe.DataBind();
                    }

                }
                else
                {
                    pnlMainTab.Visible = false;
                    pnlNoAuth.Visible = true;
                }
            }
        }

        protected void gvIngrToDo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIngrToDo.DataSource = IngredientLanguage.GetAllIngredientLangNotChecked(IDLanguage);
            Session["gvPageIndex"] = e.NewPageIndex;
            gvIngrToDo.PageIndex = e.NewPageIndex;
            gvIngrToDo.DataBind();
        }

        protected void lblIDIngredient_DataBinding(object sender, EventArgs e)
        {
            Label _myLabel = (Label)sender;
            _myLabel.Text = "<a href=\"MngIngredients.aspx?IDIngr=" + _myLabel.Text + "\"><img src=\"/Images/Form/iconWrite1.png\"/></a>";
        }

        protected void gvIngrModByMe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIngrModByMe.DataSource = IngredientLanguage.GetAllIngredientLangNotCheckedByIDLangIDUser(IDLanguage, new Guid(Session["IDUser"].ToString()));
            gvIngrModByMe.PageIndex = e.NewPageIndex;
            gvIngrModByMe.DataBind();
        }
    }
}