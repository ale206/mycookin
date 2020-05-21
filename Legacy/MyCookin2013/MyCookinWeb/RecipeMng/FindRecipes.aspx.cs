using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.Log;

namespace MyCookinWeb.RecipeWeb
{
    public partial class FindRecipes :  MyCookinWeb.Form.MyPageBase
    {
        int _IDLanguage;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!PageSecurity.IsPublicProfile())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string _searchQuery = "";
            bool _vegan = false;
            bool _vegetarian = false;
            bool _glutenFree = false;
            bool _quick = false;
            bool _light = false;
            bool _mix = false;
            string _originPage = "";

            //Questo mi sa che non va, bisogna mettere i button :(
            //Page.Form.DefaultButton = lnkSearch.UniqueID;
            Page.Form.DefaultButton = lnkSearch.UniqueID;

            _IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
            if (String.IsNullOrEmpty(hfRowOffSet.Value))
            {
                hfRowOffSet.Value = "0";
                pnlPrevPage.Visible = false;
                pnlNextPage.Visible = false;
            }
            try
            {
                _originPage = Request.UrlReferrer.AbsolutePath;
            }
            catch
            {
            }
            //clear navigation history and add itself
            NavHistoryClear();
            NavHistoryAddUrl(Request.RawUrl);

            switch (_IDLanguage)
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

            btnPrevPage.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnPrevPage, null) + ";");
            btnNextPage.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnNextPage, null) + ";");
            if (!IsPostBack && Request.QueryString["SearchQuery"] != null)
            {
                try
                {
                    _searchQuery = Request.QueryString["SearchQuery"].ToString();

                    if (Request.QueryString["Vegan"] != null)
                    {
                        _vegan = MyConvert.ToBoolean(Request.QueryString["Vegan"].ToString(), false);
                    }

                    if (Request.QueryString["Vegetarian"] != null)
                    {
                        _vegetarian = MyConvert.ToBoolean(Request.QueryString["Vegetarian"].ToString(), false);
                    }

                    if (Request.QueryString["GlutenFree"] != null)
                    {
                        _glutenFree = MyConvert.ToBoolean(Request.QueryString["GlutenFree"].ToString(), false);
                    }

                    if (Request.QueryString["FrigoMix"] != null)
                    {
                        _mix = MyConvert.ToBoolean(Request.QueryString["FrigoMix"].ToString(), false);
                    }

                    if (Request.QueryString["Light"] != null)
                    {
                        _light = MyConvert.ToBoolean(Request.QueryString["Light"].ToString(), false);
                    }

                    if (Request.QueryString["Quick"] != null)
                    {
                        _quick = MyConvert.ToBoolean(Request.QueryString["Quick"].ToString(), false);
                    }

                    if (Request.QueryString["RowOffset"] != null)
                    {
                        hfRowOffSet.Value = MyConvert.ToInt32(Request.QueryString["RowOffset"].ToString(), 0).ToString();
                    }

                    if (hfRowOffSet.Value == "0")
                    {
                        pnlPrevPage.Visible = false;
                    }
                    else
                    {
                        pnlPrevPage.Visible = true;
                    }
                    //if (String.IsNullOrEmpty(_searchQuery))
                    //{
                    //    //Response.Redirect("/Default.aspx", true);
                    //    try
                    //    {
                    //        Random _random = new Random();
                    //        int num = _random.Next(0, 26); // Zero to 25
                    //        char let = (char)('a' + num);
                    //        _searchQuery = let.ToString();
                    //    }
                    //    catch
                    //    {
                    //        _searchQuery = "a";
                    //    }
                    //}

                    Page.Title = _searchQuery + " - MyCookin";
                    txtSearchString.Text = _searchQuery;
                    chkVegan.Checked = _vegan;
                    chkVegetarian.Checked = _vegetarian;
                    chkGlutenFree.Checked = _glutenFree;
                    chkFrigo.Checked = _mix;
                    chkQuick.Checked = _quick;
                    chkLight.Checked = _light;

                    TurnOnIcon(_vegan, _vegetarian, _glutenFree, _mix, _light, _quick);
                    FindRecipesByQuery(_searchQuery, _vegan, _vegetarian, _glutenFree, _quick, _light, _mix, MyConvert.ToInt32(hfRowOffSet.Value, 0), 8);

                }
                catch
                {
                }
            }
            else if (IsPostBack)
            {
                try
                {
                    string _controlFire = "";
                    if (Request.Form["__EVENTTARGET"] != null)
                    {
                        _controlFire = Request.Form["__EVENTTARGET"].ToString();
                    }
                    if (_controlFire.IndexOf("lnkSearch") == -1)
                    {
                        _searchQuery = txtSearchString.Text;
                        _vegan = chkVegan.Checked;
                        _vegetarian = chkVegetarian.Checked;
                        _glutenFree = chkGlutenFree.Checked;
                        _quick = chkQuick.Checked;
                        _light = chkLight.Checked;
                        _mix = chkFrigo.Checked;

                        if (String.IsNullOrEmpty(_searchQuery))
                        {
                            try
                            {
                                Random _random = new Random();
                                int num = _random.Next(0, 26); // Zero to 25
                                char let = (char)('a' + num);
                                _searchQuery = let.ToString();
                            }
                            catch
                            {
                                _searchQuery = "a";
                            }
                        }

                        TurnOnIcon(_vegan, _vegetarian, _glutenFree, _mix, _light, _quick);
                        FindRecipesByQuery(_searchQuery, _vegan, _vegetarian, _glutenFree, _quick, _light, _mix, MyConvert.ToInt32(hfRowOffSet.Value, 0), 8);
                    }
                }
                catch
                {
                }
            }
        }

        protected void TurnOnIcon(bool Vegan, bool Vegetarian, bool GlutenFree, bool Mix, bool Light, bool Quick)
        {
            try
            {
                if (Vegan)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                    //                "$('#imgVegan').attr('src', $('#imgVegan').attr('src').replace('-off', '-on'));", true);
                    btnVegan.ImageUrl = btnVegan.ImageUrl.Replace("-off", "-on");
                }
                if (Vegetarian)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                    //                 "$('#imgVegetarian').attr('src', $('#imgVegetarian').attr('src').replace('-off', '-on'));", true);
                    btnVegetarian.ImageUrl = btnVegetarian.ImageUrl.Replace("-off", "-on");
                }
                if (GlutenFree)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                    //                 "$('#imgGlutenFree').attr('src', $('#imgGlutenFree').attr('src').replace('-off', '-on'));", true);
                    btnGlutenFree.ImageUrl = btnGlutenFree.ImageUrl.Replace("-off", "-on");
                }
                if (Mix)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                    //                 "$('#imgFrigo').attr('src', $('#imgFrigo').attr('src').replace('-off', '-on'));", true);
                    btnEmptyFridge.ImageUrl = btnEmptyFridge.ImageUrl.Replace("-off", "-on");

                }
                if (Quick)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                    //                 "$('#imgQuick').attr('src', $('#imgQuick').attr('src').replace('-off', '-on'));", true);
                    btnQuick.ImageUrl = btnQuick.ImageUrl.Replace("-off", "-on");
                }
                if (Light)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                    //                 "$('#imgLight').attr('src', $('#imgLight').attr('src').replace('-off', '-on'));", true);
                    btnLight.ImageUrl = btnLight.ImageUrl.Replace("-off", "-on");
                }
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in TurnOnIcon(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void FindRecipesByQuery(string Query, bool Vegan, bool Vegetarian, bool GlutenFree, bool Quick, bool Light, bool Mix, int RowOffSet, int ItemToDisplay)
        {
                DataTable dtSearchResult = null;
                int _recipeMaxTime = 10000;
                double _recipeMaxKcal = 10000;
                if (Quick)
                {
                    _recipeMaxTime = MyConvert.ToInt32(AppConfig.GetValue("QuickRecipeThreshold", AppDomain.CurrentDomain), 10000);
                }
                if (Light)
                {
                    _recipeMaxKcal = MyConvert.ToDouble(AppConfig.GetValue("LightRecipeThreshold", AppDomain.CurrentDomain), 10000);
                }

                //if (!String.IsNullOrEmpty(Query))
                //{
                int _recipeInserted = 0;

                try
                {
                    pnlRecipe1.Visible = true;
                    pnlRecipe2.Visible = true;
                    pnlRecipe3.Visible = true;
                    pnlRecipe4.Visible = true;
                    pnlRecipe5.Visible = true;
                    pnlRecipe6.Visible = true;
                    pnlRecipe7.Visible = true;
                    pnlRecipe8.Visible = true;

                    string[] _foudRecipes;

                    if (String.IsNullOrEmpty(Session["FoudRecipeList"].ToString()))
                    {
                        SearchRecipes _search = new SearchRecipes(Query, Vegan, Vegetarian, GlutenFree, _recipeMaxTime, _recipeMaxKcal, Mix, MyConvert.ToInt32(AppConfig.GetValue("RowToGetFromDBSearch", AppDomain.CurrentDomain), 40), 0, _IDLanguage);
                        dtSearchResult = _search.Find();
                        foreach (DataRow _dr in dtSearchResult.Rows)
                        {
                            Session["FoudRecipeList"] += _dr.Field<Guid>("IDRecipe").ToString() + ";";
                        }
                        _foudRecipes = Session["FoudRecipeList"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else
                    {
                        _foudRecipes = Session["FoudRecipeList"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        if (RowOffSet + ItemToDisplay >= _foudRecipes.Length)
                        {
                            SearchRecipes _search = new SearchRecipes(Query, Vegan, Vegetarian, GlutenFree, _recipeMaxTime, _recipeMaxKcal, Mix, MyConvert.ToInt32(AppConfig.GetValue("RowToGetFromDBSearch", AppDomain.CurrentDomain), 40), _foudRecipes.Length, _IDLanguage);
                            dtSearchResult = _search.Find();
                            foreach (DataRow _dr in dtSearchResult.Rows)
                            {
                                Session["FoudRecipeList"] += _dr.Field<Guid>("IDRecipe").ToString() + ";";
                            }
                            _foudRecipes = Session["FoudRecipeList"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        else
                        {
                        }
                    }


                    pnlNoSearchResult.Visible = false;
                    pnlNextPage.Visible = true;
                    hfLoadRecipeError.Value = "false";

                    pnlNoSearchResult.Visible = false;
                    pnlNextPage.Visible = true;
                    hfLoadRecipeError.Value = "false";

                    Uri _uri = new Uri(Request.Url.AbsoluteUri);
                    string _path = _uri.GetLeftPart(UriPartial.Path);

                    ShowRecipe1.IDRecipe = _foudRecipes[RowOffSet + 0];
                    ShowRecipe1.AdditionalQueryStringParameters = "SearchQuery=" + Query + "&Vegan=" + Vegan.ToString() + "&Vegetarian=" + Vegetarian.ToString() + "&GlutenFree=" + GlutenFree.ToString() + "&FrigoMix=" + Mix.ToString() + "&Light=" + Light.ToString() + "&Quick=" + Quick.ToString() + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                    ShowRecipe1.IDLanguage = _IDLanguage;
                    _recipeInserted++;
                    ShowRecipe2.IDRecipe = _foudRecipes[RowOffSet + 1];
                    ShowRecipe2.AdditionalQueryStringParameters = "SearchQuery=" + Query + "&Vegan=" + Vegan.ToString() + "&Vegetarian=" + Vegetarian.ToString() + "&GlutenFree=" + GlutenFree.ToString() + "&FrigoMix=" + Mix.ToString() + "&Light=" + Light.ToString() + "&Quick=" + Quick.ToString() + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                    ShowRecipe2.IDLanguage = _IDLanguage;
                    _recipeInserted++;
                    ShowRecipe3.IDRecipe = _foudRecipes[RowOffSet + 2];
                    ShowRecipe3.AdditionalQueryStringParameters = "SearchQuery=" + Query + "&Vegan=" + Vegan.ToString() + "&Vegetarian=" + Vegetarian.ToString() + "&GlutenFree=" + GlutenFree.ToString() + "&FrigoMix=" + Mix.ToString() + "&Light=" + Light.ToString() + "&Quick=" + Quick.ToString() + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                    ShowRecipe3.IDLanguage = _IDLanguage;
                    _recipeInserted++;
                    ShowRecipe4.IDRecipe = _foudRecipes[RowOffSet + 3];
                    ShowRecipe4.AdditionalQueryStringParameters = "SearchQuery=" + Query + "&Vegan=" + Vegan.ToString() + "&Vegetarian=" + Vegetarian.ToString() + "&GlutenFree=" + GlutenFree.ToString() + "&FrigoMix=" + Mix.ToString() + "&Light=" + Light.ToString() + "&Quick=" + Quick.ToString() + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                    ShowRecipe4.IDLanguage = _IDLanguage;
                    _recipeInserted++;
                    ShowRecipe5.IDRecipe = _foudRecipes[RowOffSet + 4];
                    ShowRecipe5.AdditionalQueryStringParameters = "SearchQuery=" + Query + "&Vegan=" + Vegan.ToString() + "&Vegetarian=" + Vegetarian.ToString() + "&GlutenFree=" + GlutenFree.ToString() + "&FrigoMix=" + Mix.ToString() + "&Light=" + Light.ToString() + "&Quick=" + Quick.ToString() + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                    ShowRecipe5.IDLanguage = _IDLanguage;
                    _recipeInserted++;
                    ShowRecipe6.IDRecipe = _foudRecipes[RowOffSet + 5];
                    ShowRecipe6.AdditionalQueryStringParameters = "SearchQuery=" + Query + "&Vegan=" + Vegan.ToString() + "&Vegetarian=" + Vegetarian.ToString() + "&GlutenFree=" + GlutenFree.ToString() + "&FrigoMix=" + Mix.ToString() + "&Light=" + Light.ToString() + "&Quick=" + Quick.ToString() + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                    ShowRecipe6.IDLanguage = _IDLanguage;
                    _recipeInserted++;
                    ShowRecipe7.IDRecipe = _foudRecipes[RowOffSet + 6];
                    ShowRecipe7.AdditionalQueryStringParameters = "SearchQuery=" + Query + "&Vegan=" + Vegan.ToString() + "&Vegetarian=" + Vegetarian.ToString() + "&GlutenFree=" + GlutenFree.ToString() + "&FrigoMix=" + Mix.ToString() + "&Light=" + Light.ToString() + "&Quick=" + Quick.ToString() + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                    ShowRecipe7.IDLanguage = _IDLanguage;
                    _recipeInserted++;
                    ShowRecipe8.IDRecipe = _foudRecipes[RowOffSet + 7];
                    ShowRecipe8.AdditionalQueryStringParameters = "SearchQuery=" + Query + "&Vegan=" + Vegan.ToString() + "&Vegetarian=" + Vegetarian.ToString() + "&GlutenFree=" + GlutenFree.ToString() + "&FrigoMix=" + Mix.ToString() + "&Light=" + Light.ToString() + "&Quick=" + Quick.ToString() + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                    ShowRecipe8.IDLanguage = _IDLanguage;
                    _recipeInserted++;
                }
                catch
                {
                    string nullGuid = new Guid().ToString();
                    switch (_recipeInserted)
                    {
                        case 0:
                            pnlRecipe1.Visible = false;
                            pnlRecipe2.Visible = false;
                            pnlRecipe3.Visible = false;
                            pnlRecipe4.Visible = false;
                            pnlRecipe5.Visible = false;
                            pnlRecipe6.Visible = false;
                            pnlRecipe7.Visible = false;
                            pnlRecipe8.Visible = false;
                            ShowRecipe1.IDRecipe = nullGuid;
                            ShowRecipe2.IDRecipe = nullGuid;
                            ShowRecipe3.IDRecipe = nullGuid;
                            ShowRecipe4.IDRecipe = nullGuid;
                            ShowRecipe5.IDRecipe = nullGuid;
                            ShowRecipe6.IDRecipe = nullGuid;
                            ShowRecipe7.IDRecipe = nullGuid;
                            ShowRecipe8.IDRecipe = nullGuid;
                            pnlNoSearchResult.Visible = true;
                            break;
                        case 1:
                            pnlRecipe2.Visible = false;
                            pnlRecipe3.Visible = false;
                            pnlRecipe4.Visible = false;
                            pnlRecipe5.Visible = false;
                            pnlRecipe6.Visible = false;
                            pnlRecipe7.Visible = false;
                            pnlRecipe8.Visible = false;
                            ShowRecipe2.IDRecipe = nullGuid;
                            ShowRecipe3.IDRecipe = nullGuid;
                            ShowRecipe4.IDRecipe = nullGuid;
                            ShowRecipe5.IDRecipe = nullGuid;
                            ShowRecipe6.IDRecipe = nullGuid;
                            ShowRecipe7.IDRecipe = nullGuid;
                            ShowRecipe8.IDRecipe = nullGuid;
                            break;
                        case 2:
                            pnlRecipe3.Visible = false;
                            pnlRecipe4.Visible = false;
                            pnlRecipe5.Visible = false;
                            pnlRecipe6.Visible = false;
                            pnlRecipe7.Visible = false;
                            pnlRecipe8.Visible = false;
                            ShowRecipe3.IDRecipe = nullGuid;
                            ShowRecipe4.IDRecipe = nullGuid;
                            ShowRecipe5.IDRecipe = nullGuid;
                            ShowRecipe6.IDRecipe = nullGuid;
                            ShowRecipe7.IDRecipe = nullGuid;
                            ShowRecipe8.IDRecipe = nullGuid;
                            break;
                        case 3:
                            pnlRecipe4.Visible = false;
                            pnlRecipe5.Visible = false;
                            pnlRecipe6.Visible = false;
                            pnlRecipe7.Visible = false;
                            pnlRecipe8.Visible = false;
                            ShowRecipe4.IDRecipe = nullGuid;
                            ShowRecipe5.IDRecipe = nullGuid;
                            ShowRecipe6.IDRecipe = nullGuid;
                            ShowRecipe7.IDRecipe = nullGuid;
                            ShowRecipe8.IDRecipe = nullGuid;
                            break;
                        case 4:
                            pnlRecipe5.Visible = false;
                            pnlRecipe6.Visible = false;
                            pnlRecipe7.Visible = false;
                            pnlRecipe8.Visible = false;
                            ShowRecipe5.IDRecipe = nullGuid;
                            ShowRecipe6.IDRecipe = nullGuid;
                            ShowRecipe7.IDRecipe = nullGuid;
                            ShowRecipe8.IDRecipe = nullGuid;
                            break;
                        case 5:
                            pnlRecipe6.Visible = false;
                            pnlRecipe7.Visible = false;
                            pnlRecipe8.Visible = false;
                            ShowRecipe6.IDRecipe = nullGuid;
                            ShowRecipe7.IDRecipe = nullGuid;
                            ShowRecipe8.IDRecipe = nullGuid;
                            break;
                        case 6:
                            pnlRecipe7.Visible = false;
                            pnlRecipe8.Visible = false;
                            ShowRecipe7.IDRecipe = nullGuid;
                            ShowRecipe8.IDRecipe = nullGuid;
                            break;
                        case 7:
                            pnlRecipe8.Visible = false;
                            ShowRecipe8.IDRecipe = nullGuid;
                            break;
                    }
                    pnlNextPage.Visible = false;
                    if (MyConvert.ToInt32(hfRowOffSet.Value, 0) == 0)
                    {
                        pnlPrevPage.Visible = false;
                    }
                    hfLoadRecipeError.Value = "true";
                }
                //}
        }

        protected void btnPrevPage_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (MyConvert.ToInt32(hfRowOffSet.Value, 0) > 7 && !MyConvert.ToBoolean(hfLoadRecipeError.Value,false))
                {
                    pnlPrevPage.Visible = true;
                }
                else
                {
                    pnlPrevPage.Visible = false;
                }
                txtSearchString.Focus();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnPrevPage_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (MyConvert.ToInt32(hfRowOffSet.Value, 0) > 7)
                {
                    pnlPrevPage.Visible = true;
                }
                else
                {
                    pnlPrevPage.Visible = false;
                }
                txtSearchString.Focus();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnPrevPage_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            Session["FoudRecipeList"] = "";
            Session["RecipeInRecipesBookList"] = "";
            Response.Redirect(("/RecipeMng/FindRecipes.aspx?" + "SearchQuery=" + txtSearchString.Text + "&Vegan=" + chkVegan.Checked.ToString() + "&Vegetarian=" + chkVegetarian.Checked.ToString() + "&GlutenFree=" + chkGlutenFree.Checked.ToString() + "&FrigoMix=" + chkFrigo.Checked.ToString() + "&Light=" + chkLight.Checked.ToString() + "&Quick=" + chkQuick.Checked.ToString()).ToLower(), true);
        }
    }
}
