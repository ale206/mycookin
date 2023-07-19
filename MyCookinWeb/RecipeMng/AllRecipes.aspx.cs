using MyCookin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.RecipeManager;

namespace MyCookinWeb.RecipeMng
{
    public partial class AllRecipes : MyCookinWeb.Form.MyPageBase
    {
        int IDLanguage;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!PageSecurity.IsPublicProfile())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
            hfIDLanguage.Value = IDLanguage.ToString();
            //This avoid issue on routed pages
            Page.Form.Action = Request.RawUrl;
            NavHistoryAddUrl(Request.RawUrl);
            try
            {
                hfLightRecipeThreshold.Value = AppConfig.GetValue("LightRecipeThreshold", AppDomain.CurrentDomain);
                hfQuickRecipeThreshold.Value = AppConfig.GetValue("QuickRecipeThreshold", AppDomain.CurrentDomain);
                hfRecipeOf.Value = GetLocalResourceObject("hfRecipeOf.Value").ToString();
                hfRecipeOf2.Value = GetLocalResourceObject("hfRecipeOf2.Value").ToString();
            }
            catch
            {

            }
            try
            {
                hfOgpUrl.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + Request.RawUrl;
                hfOgpFbAppID.Value = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                hfOgpTitle.Value = "MyCookin";
                Page.Title = "MyCookin";
                hfOgpImage.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + "/Images/MyCookinLogo-200x200.png";
                hfKeywords.Value = "MyCookin , Recipes , Ricette , Receta , Cooking";
                hfLanguageCode.Value = MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage);
                Random random = new Random();
                hfCreationDate.Value = DateTime.UtcNow.AddMinutes(random.Next(10, 200)).ToString();
            }
            catch
            {
            }
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
        }
    }
}