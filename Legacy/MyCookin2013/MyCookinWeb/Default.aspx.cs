using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.Common;

namespace MyCookinWeb.UserInfo
{
    public partial class Default :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (MyUser.CheckUserLogged())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            }
            else
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Public.Master";
            }

            //if (!PageSecurity.IsPublicProfile())
            //{
            //    this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = lnkSearch.UniqueID;
            txtSearchString.Focus();
            Session["FoudRecipeList"] = "";
            Session["RecipeInRecipesBookList"] = "";
            NavHistoryClear();
            NavHistoryAddUrl(Request.RawUrl);
            
            try
            {
                if(Request.QueryString["IDLanguage"]!=null)
                {
                    Session["IDLanguage"] = MyConvert.ToInt32(Request.QueryString["IDLanguage"].ToString(),1);
                }
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                hfIDLanguage.Value = IDLanguage.ToString();
                try
                {
                    string _allValue = "all";
                    switch (IDLanguage)
                    {
                        case 1:
                            _allValue = "all";
                            break;
                        case 2:
                            _allValue = "tutte";
                            break;
                        case 3:
                            _allValue = "todo";
                            break;
                        default:
                            _allValue = "all";
                            break;
                    }
                    lnkAllRecipes.PostBackUrl = (AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + _allValue).ToLower();
                }
                catch
                {
                    lnkAllRecipes.PostBackUrl = "/recipemng/AllRecipes.aspx";
                }
                try
                {
                    if (!MyUser.CheckUserLogged())
                    {
                        switch (IDLanguage)
                        {
                            case 1:
                                btnLangEn.Visible = false;
                                btnLangIt.Visible = true;
                                btnLangEs.Visible = true;
                                break;
                            case 2:
                                btnLangEn.Visible = true;
                                btnLangIt.Visible = false;
                                btnLangEs.Visible = true;
                                break;
                            case 3:
                                btnLangEn.Visible = true;
                                btnLangIt.Visible = true;
                                btnLangEs.Visible = false;
                                break;
                            default:
                                btnLangEn.Visible = true;
                                btnLangIt.Visible = true;
                                btnLangEs.Visible = true;
                                break;
                        }
                    }
                    else
                    {
                        btnLangEn.Visible = false;
                        btnLangIt.Visible = false;
                        btnLangEs.Visible = false;
                    }
                }
                catch
                {
                }
                try
                {
                    hfOgpUrl.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + Request.RawUrl;
                    hfOgpFbAppID.Value = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                    hfOgpTitle.Value = "MyCookin'";
                    hfOgpImage.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + "/Images/MyCookinLogo-200x200.png";
                    hfKeywords.Value = GetLocalResourceObject("hfKeywords.Value").ToString();
                    hfOgpDescription.Value = GetLocalResourceObject("hfOgpDescription.Value").ToString();
                    hfLanguageCode.Value = MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage);
                    Random random = new Random();
                    hfCreationDate.Value = DateTime.UtcNow.AddMinutes(random.Next(10,200)).ToString();
                }
                catch
                {
                }
                if (!Page.IsPostBack)
                {
                    DataTable _dtTopRecipes = RecipeLanguage.GetTopRecipes(IDLanguage);
                    if (_dtTopRecipes.Rows.Count > 0)
                    {
                        foreach (DataRow _drRecipe in _dtTopRecipes.Rows)
                        {
                            try
                            {
                                Session["FoudRecipeList"] += _drRecipe.Field<Guid>("IDRecipe").ToString() + ";";
                            }
                            catch
                            {
                            }
                            try
                            {
                                RecipeLanguage _recipeLang = new RecipeLanguage(_drRecipe.Field<Guid>("IDRecipe"), IDLanguage);
                                _recipeLang.QueryBaseRecipeInfo();
                                Image _imgRecipe = new Image();
                                _imgRecipe.ID = "imgRecipe" + _drRecipe.Field<Guid>("IDRecipeLanguage").ToString();
                                HyperLink _hlRecipe = new HyperLink();
                                _hlRecipe.ID = "hlRecipe" + _drRecipe.Field<Guid>("IDRecipeLanguage").ToString();

                                string _imageURL = _recipeLang.RecipeImage.GetAlternativeSizePath(MyCookin.ObjectManager.MediaManager.MediaSizeTypes.Small, false, false, true);

                                if (String.IsNullOrEmpty(_imageURL))
                                {
                                    _imageURL = _recipeLang.RecipeImage.GetCompletePath(false, false, true);
                                }
                                if (!String.IsNullOrEmpty(_imageURL))
                                {
                                    _imgRecipe.ImageUrl = _imageURL;
                                    _imgRecipe.Height = 100;
                                    _imgRecipe.Width = 100;
                                    _imgRecipe.CssClass = "btnTopRecipe MyTooltip";
                                    _imgRecipe.ToolTip = _drRecipe.Field<string>("RecipeName").ToString();
                                    _imgRecipe.AlternateText = _drRecipe.Field<string>("RecipeName").ToString();
                                    _hlRecipe.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + _drRecipe.Field<string>("RecipeName").ToString().Replace(" ", "-") + "/" + _recipeLang.IDRecipe.ToString()).ToLower();
                                    _hlRecipe.Controls.Add(_imgRecipe);
                                    pnlTopHomeRecipes.Controls.Add(_hlRecipe);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            Session["FoudRecipeList"] = "";
            Response.Redirect(("/RecipeMng/FindRecipes.aspx?" + "SearchQuery=" + txtSearchString.Text + "&Vegan=" + chkVegan.Checked.ToString() + "&Vegetarian=" + chkVegetarian.Checked.ToString() + "&GlutenFree=" + chkGlutenFree.Checked.ToString() + "&FrigoMix=" + chkFrigo.Checked.ToString() + "&Light=" + chkLight.Checked.ToString() + "&Quick=" + chkQuick.Checked.ToString() + "&RowOffset=0").ToLower(), true);
        }

        protected void btnLang_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton _btn = (ImageButton)sender;
                int _idlang = MyConvert.ToInt32(_btn.CommandArgument, 1);
                Session["IDLanguage"] = _idlang;
                Response.Redirect("/" + MyCulture.GetLangShortCodeFromIDLanguage(_idlang), true);
            }
            catch
            {
            }
        }
    }
}