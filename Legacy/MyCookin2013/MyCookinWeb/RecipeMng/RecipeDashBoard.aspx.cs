using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Common;
using MyCookin.Log;

namespace MyCookinWeb.RecipeWeb
{
    public partial class RecipeDashBoard :  MyCookinWeb.Form.MyPageBase
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
                Response.Redirect(AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl(), true);
            }
            //******************************************

            IDLanguage = Convert.ToInt32(Session["IDLanguage"]);

            MyUser modUser = new MyUser(new Guid(Session["IDUser"].ToString()));
            modUser.GetUserBasicInfoByID();

            if (!IsPostBack)
            {
                try
                {
                    //Check if user belong group authorized to view this page
                    if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("5b6650ae-4430-4e64-98f5-0c08f78e2392") >= 0)
                    {
                        lblNumRecipeToDo.Text = Recipe.GetNumRecipeNotChecked().ToString();
                        //lblNumIngrDoByUser.Text = Ingredient.GetNumIngredientCheckedByUser(modUser.IDUser).ToString();
                        gvRecipeToDo.DataSource = RecipeLanguage.GetAllRecipeLangNotChecked(IDLanguage);
                        gvRecipeToDo.DataBind();

                        if (Session["gvPageIndex"] == null)
                        {
                            Random rdn = new Random();
                            gvRecipeToDo.PageIndex = rdn.Next(gvRecipeToDo.PageCount);
                            Session["gvPageIndex"] = gvRecipeToDo.PageIndex;
                        }
                        else
                        {
                            gvRecipeToDo.PageIndex = Convert.ToInt32(Session["gvPageIndex"].ToString());
                        }

                        gvRecipeToDo.DataBind();

                        if (modUser != null)
                        {
                            gvMyRecipe.DataSource = RecipeLanguage.GetRecipeByOwner(modUser.IDUser);
                            gvMyRecipe.DataBind();
                        }

                    }
                    else
                    {
                        pnlMainTab.Visible = false;
                        pnlNoAuth.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        //WRITE A ROW IN LOG FILE AND DB
                        LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in Loading Page RecipeDashBoard: " + ex.Message, "", true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                        LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    }
                    catch { }
                }
            }
            
        }

        protected void lblIDRecipe_DataBinding(object sender, EventArgs e)
        {
            try
            {
                Label _myLabel = (Label)sender;
                _myLabel.Text = "<a href=\"MngRecipes.aspx?IDRecipe=" + _myLabel.Text + "\"><img src=\"/Images/Form/iconWrite1.png\"/></a>";
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in lblIDRecipe_DataBinding(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void gvRecipeToDo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvRecipeToDo.DataSource = RecipeLanguage.GetAllRecipeLangNotChecked(IDLanguage);
                Session["gvPageIndex"] = e.NewPageIndex;
                gvRecipeToDo.PageIndex = e.NewPageIndex;
                gvRecipeToDo.DataBind();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in gvRecipeToDo_PageIndexChanging(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void gvMyRecipe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                MyUser modUser = new MyUser(new Guid(Session["IDUser"].ToString()));
                gvMyRecipe.DataSource = RecipeLanguage.GetRecipeByOwner(modUser.IDUser);
                gvMyRecipe.PageIndex = e.NewPageIndex;
                gvMyRecipe.DataBind();
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in gvMyecipe_PageIndexChanging(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }
    }
}