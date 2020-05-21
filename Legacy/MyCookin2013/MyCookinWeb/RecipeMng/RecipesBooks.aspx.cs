using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;
using MyCookinWeb.CustomControls;
using MyCookin.ObjectManager.CityManager;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb.RecipeMng
{
    public partial class RecipesBooks : MyCookinWeb.Form.MyPageBase
    {
        int IDLanguage;
        bool _readOnly = true;
        string _searchQuery = "";
        string _recipeSource = "0";
        string _recipeType = "0";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!PageSecurity.IsPublicProfile())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
                _readOnly = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IDUser"] != null || !String.IsNullOrEmpty(hfIDUser.Value))
            {
                if (Request.QueryString["IDUser"] != null)
                {
                    hfIDUser.Value = Request.QueryString["IDUser"].ToString();
                }

                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                switch (IDLanguage)
                {
                    case 1:
                        lnkAddNewRecipe.NavigateUrl = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/add").ToLower();
                        break;
                    case 2:
                        lnkAddNewRecipe.NavigateUrl = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/aggiungi").ToLower();
                        break;
                    case 3:
                        lnkAddNewRecipe.NavigateUrl = (AppConfig.GetValue("RoutingRecipe" + Session["IDLanguage"].ToString(), AppDomain.CurrentDomain) + "/nuevo").ToLower();
                        break;
                    default:
                        lnkAddNewRecipe.NavigateUrl = ("/RecipeMng/CreateRecipe.aspx").ToLower();
                        break;
                }

                hfIDLanguage.Value = IDLanguage.ToString();
                //This avoid issue on routed pages
                Page.Form.Action = Request.RawUrl;
                Form.DefaultButton = lnkFilter.UniqueID;
                Guid IDRequester = new Guid();
                try
                {
                    hfLightRecipeThreshold.Value = AppConfig.GetValue("LightRecipeThreshold", AppDomain.CurrentDomain);
                    hfQuickRecipeThreshold.Value = AppConfig.GetValue("QuickRecipeThreshold", AppDomain.CurrentDomain);
                    hfRecipeOf.Value = GetLocalResourceObject("hfRecipeOf.Value").ToString();
                    hfRecipeOf2.Value = GetLocalResourceObject("hfRecipeOf2.Value").ToString();
                    hfEditRecipeText.Value = GetLocalResourceObject("hfEditRecipeText.Value").ToString();
                }
                catch
                {

                }

                if(!string.IsNullOrEmpty(Session["IDUser"].ToString()))
                {
                    try
                    {
                        IDRequester=new Guid(Session["IDUser"].ToString());
                    }
                    catch
                    {}
                }

                hfIDRequester.Value = IDRequester.ToString();
                //clear navigation history and add itself
                NavHistoryClear();
                NavHistoryAddUrl(Request.RawUrl);

                if (!Page.IsPostBack)
                {
                    try
                    {
                        foreach (RecipeProperty recipeProp in RecipeProperty.GetAllRecipePropertyListByType(1, IDLanguage))
                        {
                            ListItem _item = new ListItem(recipeProp.RecipeProp, recipeProp.IDRecipeProperty.ToString());
                            _item.Attributes.Add("data-imagesrc", "/Images/IconRecipeProperty/50x50/RecipeProperty-" + recipeProp.IDRecipeProperty.ToString() + ".png");
                            ddlRecipeType.Items.Add(_item);
                        }

                    }
                    catch
                    {
                    }
                }
                try
                {
                    GetDataFromDatabase();
                }
                catch
                {
                    Response.Redirect("/default.aspx", false);
                }
            }
            else
            {
                Response.Redirect("/default.aspx", false);
            }
        }

        protected void GetDataFromDatabase()
        {
            if (!String.IsNullOrEmpty(hfIDUser.Value))
            {
                Guid _idUser = new Guid(hfIDUser.Value);
                MyUser _user = new MyUser(_idUser);
                _user.GetUserInfoAllByID();
                if (_user.IDProfilePhoto != null)
                {
                    string _imagePath = "";
                    _user.IDProfilePhoto.QueryMediaInfo();
                    _imagePath = _user.IDProfilePhoto.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                    if (String.IsNullOrEmpty(_imagePath))
                    {
                        _imagePath = _user.IDProfilePhoto.GetCompletePath(false, false, true);
                    }
                    imgUserImage.ImageUrl = _imagePath;
                }
                else
                {
                    imgUserImage.ImageUrl = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.ProfileImagePhoto);
                }
                if (Session["IDUser"] != null && (hfIDUser.Value == Session["IDUser"].ToString()))
                {
                    lblUserName.Text = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-IN-0006");
                }
                else
                {
                    lblUserName.Text = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-IN-0007").Replace("{0}", _user.Name + " " + _user.Surname);
                }
                try
                {
                    hfOgpUrl.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + Request.RawUrl;
                    hfOgpFbAppID.Value = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                    hfOgpTitle.Value = lblUserName.Text;
                    Page.Title = lblUserName.Text + " - MyCookin";
                    string _bigImgPath = _user.IDProfilePhoto.GetCompletePath(false, false, true);
                    if (_bigImgPath.IndexOf("http://") > -1 || _bigImgPath.IndexOf("https://") > -1)
                    {
                        hfOgpImage.Value = _bigImgPath;
                    }
                    else
                    {
                        hfOgpImage.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + _bigImgPath;
                    }
                    hfKeywords.Value = "MyCookin , Recipes , Ricette , Receta , Cooking";
                    hfLanguageCode.Value = MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage);
                    Random random = new Random();
                    hfCreationDate.Value = DateTime.UtcNow.AddMinutes(random.Next(10, 200)).ToString();
                }
                catch
                {
                }
                try
                {
                    lblUserRecipesDetails.Text = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-IN-0008");
                    lblUserRecipesDetails.Text = lblUserRecipesDetails.Text.Replace("{0}", Recipe.NumberOfRecipesInsertedByUser(_user.IDUser).ToString());
                    lblUserRecipesDetails.Text = lblUserRecipesDetails.Text.Replace("{1}", RecipeBook.NumberOfRecipesInsertedByUser(_user.IDUser).ToString());
                    hfOgpDescription.Value = lblUserRecipesDetails.Text;
                }
                catch
                {
                }
            }
        }
    }
}