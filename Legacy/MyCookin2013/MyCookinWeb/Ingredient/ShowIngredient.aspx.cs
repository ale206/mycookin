using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb.IngredientWeb
{
    public partial class ShowIngredient :  MyCookinWeb.Form.MyPageBase
    {
        int IDLanguage;
        IngredientLanguage ingr;
        Guid _IDUser = new Guid();
        string _query = "";
        bool _continueLoad = true;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!PageSecurity.IsPublicProfile())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //This avoid issue on routed pages
            Page.Form.Action = Request.RawUrl;

            
            //If id parameter for page not exist
            if (Request.QueryString["IDIngr"] == null)
            {
                Response.Redirect("/default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                _continueLoad = false;
            }
            if (_continueLoad)
            {
                try
                {
                    _IDUser = new Guid(Session["IDUser"].ToString());
                }
                catch
                { }

                try
                {
                    _query = Request.QueryString["IngrName"].ToString();
                }
                catch
                { }

                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                if(Request.QueryString["IDLanguage"]!=null)
                {
                    IDLanguage = MyConvert.ToInt32(Request.QueryString["IDLanguage"].ToString(), 1);
                }

                ShowRecipe1.IDLanguage = IDLanguage;
                ShowRecipe2.IDLanguage = IDLanguage;
                ShowRecipe3.IDLanguage = IDLanguage;
                ShowRecipe4.IDLanguage = IDLanguage;

                pnlContent.Attributes.Add("itemscope", "");
                pnlContent.Attributes.Add("itemtype", "http://schema.org/Thing");

                if (!Page.IsPostBack)
                {
                    if (Request.UrlReferrer != null)
                    {
                        try
                        {
                            btnGoBack.Visible = true;
                            hfReferrerURL.Value = NavHistoryGetPrevUrl(Request.RawUrl.ToString());
                            NavHistoryAddUrl(Request.RawUrl.ToString());
                        }
                        catch
                        {
                            btnGoBack.Visible = false;
                        }
                    }
                    try
                    {
                        GetDataFromDatabase();
                    }
                    catch (Exception ex)
                    {
                        LogRow logRowOpenIngredient = new LogRow(DateTime.UtcNow, "0", "0", Network.GetCurrentPageName(),
                            "", "Open Ingredient " + ingr.IDIngredient.ToString() + " - " + ex.Message, Session["IDUser"].ToString(), false, true);
                        LogManager.WriteDBLog(LogLevel.Errors, logRowOpenIngredient);
                        LogManager.WriteFileLog(LogLevel.Errors, false, logRowOpenIngredient);
                    }
                    try
                    {
                        MyStatistics _stat = new MyStatistics(_IDUser, ingr.IDIngredient, StatisticsActionType.IN_ViewIngredient, "", "", _query, Session.SessionID);
                        _stat.InsertNewRow();
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Load the data from database for the ingredient indicated in the querystring of the page
        /// </summary>
        protected void GetDataFromDatabase()
        {
            //hfOgpUrl.Value = Request.Url.ToString();
            hfOgpUrl.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + Request.RawUrl;
            hfOgpFbAppID.Value = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
            try
            {
                ingr = new IngredientLanguage(Request.QueryString["IDIngr"].ToString(), IDLanguage);
            }
            catch
            {
                Response.Redirect("/default.aspx", true);
            }

            //If selected ingredient not exist
            if (ingr == null)
            {
                Response.Redirect("/default.aspx", true);
            }

            try
            {
                ingr.QueryIngredientLanguageInfo();
                ingr.QueryIngredientInfo();
            }
            catch (Exception ex)
            {
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error load Ingredient Info", ex.Message, "Ingredient: " + Request.QueryString["IDIngr"].ToString(), false, true);
                LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
            }



            lblIngredientName.Text = ingr.IngredientPlural;
            lblIngredientName.Attributes.Add("itemprop", "name");
            hfOgpTitle.Value = ingr.IngredientPlural;
            ingr.GetIngredientCategories();
            hfKeywords.Value = ingr.IngredientPlural + " " + GetLocalResourceObject("KcalSEO.Value").ToString() + " , " + (IngredientCategoryLanguage.GetIngredientCategoryLang(ingr.IngredientCategories[0].IDIngredientCategory, IDLanguage)).Replace("->", ",");
            hfLanguageCode.Value = MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage);
            hfCreationDate.Value = ingr.IngredientLastMod.ToString();

            Page.Title = ingr.IngredientPlural + " - MyCookin";

            if (ingr.IngredientPreparationRecipe == null)
            {
                pnlPreparationRecipe.Visible = false;
            }
            else
            {
                pnlPreparationRecipe.Visible = true;
                //lnkPreparationRecipe.NavigateUrl = "/RecipeMng/ShowRecipe.aspx?IDRecipe=" + ingr.IngredientPreparationRecipe.IDRecipe.ToString();
                lnkPreparationRecipe.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + ingr.IngredientPlural.Replace(" ", "-") + "/" + ingr.IngredientPreparationRecipe.IDRecipe.ToString()).ToLower();
            }


            if (ingr.IngredientImage != null)
            {
                imgIngredient.Visible = true;
                imgIngredient.ImageUrl = ingr.IngredientImage.GetCompletePath(false, false,true);
                imgIngredient.AlternateText = ingr.IngredientPlural;
               
                if (imgIngredient.ImageUrl.IndexOf("http://") > -1 || imgIngredient.ImageUrl.IndexOf("https://") > -1)
                {
                    hfOgpImage.Value = imgIngredient.ImageUrl;
                }
                else
                {
                    hfOgpImage.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + imgIngredient.ImageUrl;
                }
                imgIngredient.Attributes.Add("itemprop", "image");
                imgIngredient.AlternateText = ingr.IngredientSingular;
            }
            else
            {
                imgIngredient.Visible = false;
            }


            lblKcalValue.Text = Math.Round(Convert.ToDouble(ingr.Kcal100gr), 0).ToString();
            lblProteins.Text = Math.Round(Convert.ToDouble(ingr.grProteins), 0).ToString();
            lblCarbohydrates.Text = Math.Round(Convert.ToDouble(ingr.grCarbohydrates), 0).ToString();
            lblFats.Text = Math.Round(Convert.ToDouble(ingr.grFats), 0).ToString();
            lblAlcohol.Text = Math.Round(Convert.ToDouble(ingr.grAlcohol), 0).ToString();

            int _fatLimit = MyConvert.ToInt32(AppConfig.GetValue("FatRecipeThreshold", AppDomain.CurrentDomain), 100);
            int _proteinstLimit = MyConvert.ToInt32(AppConfig.GetValue("ProteinsRecipeThreshold", AppDomain.CurrentDomain), 100);
            int _carbohydratesLimit = MyConvert.ToInt32(AppConfig.GetValue("CarbohydratesRecipeThreshold", AppDomain.CurrentDomain), 100);
            int _alcoholLimit = MyConvert.ToInt32(AppConfig.GetValue("AlcoholRecipeThreshold", AppDomain.CurrentDomain), 100);

            if (ingr.grFats >= _fatLimit)
            {
                lblFats.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Orange", AppDomain.CurrentDomain));
            }
            else
            {
                lblFats.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Green",AppDomain.CurrentDomain));
            }

            if (ingr.grProteins >= _proteinstLimit)
            {
                lblProteins.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Orange", AppDomain.CurrentDomain));
            }
            else
            {
                lblProteins.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Green", AppDomain.CurrentDomain));
            }

            if (ingr.grCarbohydrates >= _carbohydratesLimit)
            {
                lblCarbohydrates.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Orange", AppDomain.CurrentDomain));
            }
            else
            {
                lblCarbohydrates.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Green", AppDomain.CurrentDomain));
            }

            if (ingr.grAlcohol >= _alcoholLimit)
            {
                lblAlcohol.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Orange", AppDomain.CurrentDomain));
            }
            else
            {
                lblAlcohol.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Green", AppDomain.CurrentDomain));
            }

            pnlVegetarian.Visible = ingr.IsVegetarian;
            pnlVegan.Visible = ingr.IsVegan;
            pnlGlutenFree.Visible = ingr.IsGlutenFree;
            pnlHotSpicy.Visible = ingr.IsHotSpicy;

            if (!String.IsNullOrEmpty(ingr.IngredientDescription))
            {

                lblIngredientDesc.Visible = true;
                lblIngredientDesc.Text = ingr.IngredientDescription;
                lblIngredientDesc.Attributes.Add("itemprop", "description");
                if (ingr.IngredientDescription.Length > 247)
                {
                    hfOgpDescription.Value = ingr.IngredientDescription.Substring(0, 247) + "...";
                }
                else
                {
                    hfOgpDescription.Value = ingr.IngredientDescription;
                }
            }
            else
            {
                lblIngredientDesc.Visible = false;
            }

            int _recipeInserted = 0;
            try
            {
                pnlRecipe1.Visible = true;
                pnlRecipe2.Visible = true;
                pnlRecipe3.Visible = true;
                pnlRecipe4.Visible = true;
             
                DataTable dtRecipes = Recipe.GetRecipeWithIngredient(4, ingr.IDIngredient, false,lblIngredientName.Text);

                ShowRecipe1.IDRecipe = dtRecipes.Rows[0].Field<Guid>("IDRecipe").ToString();
                _recipeInserted++;
                ShowRecipe2.IDRecipe = dtRecipes.Rows[1].Field<Guid>("IDRecipe").ToString();
                _recipeInserted++;
                ShowRecipe3.IDRecipe = dtRecipes.Rows[2].Field<Guid>("IDRecipe").ToString();
                _recipeInserted++;
                ShowRecipe4.IDRecipe = dtRecipes.Rows[3].Field<Guid>("IDRecipe").ToString();
                _recipeInserted++;

            }
            catch
            {
                switch (_recipeInserted)
                {
                    case 0:
                        pnlRecipe1.Visible = false;
                        pnlRecipe2.Visible = false;
                        pnlRecipe3.Visible = false;
                        pnlRecipe4.Visible = false;
                        break;
                    case 1:
                        pnlRecipe2.Visible = false;
                        pnlRecipe3.Visible = false;
                        pnlRecipe4.Visible = false;
                        break;
                    case 2:
                        pnlRecipe3.Visible = false;
                        pnlRecipe4.Visible = false;
                        break;
                    case 3:
                        pnlRecipe4.Visible = false;
                        break;
                }
            }
            switch(IDLanguage)
            {
                case 1:
                    lnkAllIngredients.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingIngredient" + IDLanguage.ToString(), AppDomain.CurrentDomain) + "all").ToLower();
                    break;
                case 2:
                    lnkAllIngredients.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingIngredient" + IDLanguage.ToString(), AppDomain.CurrentDomain) + "lista").ToLower();
                    break;
                case 3:
                    lnkAllIngredients.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingIngredient" + IDLanguage.ToString(), AppDomain.CurrentDomain) + "todos").ToLower();
                    break;
                default:
                    lnkAllIngredients.NavigateUrl = ("/Ingredient/IngredientList.aspx").ToLower();
                    break;
            }
            
        }
        
        protected void btnGoBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                NavHistoryRemoveUrlFrom(Request.RawUrl.ToString());
                Response.Redirect((hfReferrerURL.Value).ToLower(), true);
            }
            catch
            {
            }
        }
    }
}