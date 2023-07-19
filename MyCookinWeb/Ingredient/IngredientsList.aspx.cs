using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.Common;

namespace MyCookinWeb.IngredientWeb
{
    public partial class IngredientsList : MyCookinWeb.Form.MyPageBase
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
                hfOgpUrl.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + Request.RawUrl;
                hfOgpFbAppID.Value = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                hfOgpTitle.Value = "MyCookin";
                hfOgpImage.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + "/Images/MyCookinLogo-200x200.png";
                hfKeywords.Value = "MyCookin , Ingredients nutrictional facts , Calorie Ingredienti, caloría Ingredientes , Cooking";
                hfLanguageCode.Value = MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage);
                Random random = new Random();
                hfCreationDate.Value = DateTime.UtcNow.AddMinutes(random.Next(10, 200)).ToString();
            }
            catch
            {
            }
        }
    }
}