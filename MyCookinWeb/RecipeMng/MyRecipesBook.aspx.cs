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

namespace MyCookinWeb.RecipeWeb
{
    public partial class MyRecipesBook :  MyCookinWeb.Form.MyPageBase
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

                //This avoid issue on routed pages
                Page.Form.Action = Request.RawUrl;
                btnPrevPage.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnPrevPage, null) + ";");
                btnNextPage.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnNextPage, null) + ";");

                //clear navigation history and add itself
                NavHistoryClear();
                NavHistoryAddUrl(Request.RawUrl);

                if (String.IsNullOrEmpty(hfRowOffSet.Value))
                {
                    hfRowOffSet.Value = "0";
                    Session["RecipeInRecipesBookList"] = "";
                    pnlPrevPage.Visible = false;
                    pnlNextPage.Visible = false;
                }
                //else if (hfRowOffSet.Value == "0")
                //{
                //    Session["RecipeInRecipesBookList"] = "";
                //}

                if (!Page.IsPostBack)
                {
                    try
                    {
                        foreach (RecipeProperty recipeProp in RecipeProperty.GetAllRecipePropertyListByType(1, IDLanguage))
                        {
                            ListItem _item = new ListItem(recipeProp.RecipeProp, recipeProp.IDRecipeProperty.ToString());
                            ddlRecipeType.Items.Add(_item);
                        }

                    }
                    catch
                    {
                    }

                    if (Request.QueryString["RowOffset"] != null)
                    {
                        hfRowOffSet.Value = MyConvert.ToInt32(Request.QueryString["RowOffset"].ToString(), 0).ToString();
                    }
                    if (Request.QueryString["SearchQuery"] != null)
                    {
                        _searchQuery = Request.QueryString["SearchQuery"].ToString();
                        txtRecipeFilter.Text = _searchQuery;
                    }
                    if (Request.QueryString["RecipeSource"] != null)
                    {
                        _recipeSource = Request.QueryString["RecipeSource"].ToString();
                        try
                        {
                            ddlRecipeSource.Items.FindByValue(_recipeSource).Selected = true;
                        }
                        catch
                        {
                        }
                    }
                    if (Request.QueryString["RecipeType"] != null)
                    {
                        _recipeType = Request.QueryString["RecipeType"].ToString();
                        try
                        {
                            ddlRecipeType.Items.FindByValue(_recipeType).Selected = true;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        ddlRecipeType.Items.FindByValue("0").Selected = true;
                    }
                    
                }
                else
                {
                    _searchQuery = txtRecipeFilter.Text;
                    _recipeSource = ddlRecipeSource.SelectedValue;
                    _recipeType = ddlRecipeType.SelectedValue;
                }

                if (hfRowOffSet.Value == "0")
                {
                    pnlPrevPage.Visible = false;
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
            try
            {
                if (!String.IsNullOrEmpty(hfIDUser.Value))
                {
                    Guid _idUser = new Guid(hfIDUser.Value);
                    bool ShowDeleteButton = false;
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
                        ShowDeleteButton = true;
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
                        string _bigImgPath = _user.IDProfilePhoto.GetCompletePath(false, false, true);
                        if (_bigImgPath.IndexOf("http://") > -1 || _bigImgPath.IndexOf("https://") > -1)
                        {
                            hfOgpImage.Value = _bigImgPath;
                        }
                        else
                        {
                            hfOgpImage.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + _bigImgPath;
                        }
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

                    string[] _foudRecipes;
                    int RowOffSet = MyConvert.ToInt32(hfRowOffSet.Value, 0);
                    int _cacheSize = MyConvert.ToInt32(AppConfig.GetValue("RowToGetFromDBSearch", AppDomain.CurrentDomain), 40);

                    if (String.IsNullOrEmpty(Session["RecipeInRecipesBookList"].ToString()))
                    {
                        //DataTable dtSearchResult = RecipeBook.GetRecipes(_idUser, MyConvert.ToInt32(_recipeType, 0), MyConvert.ToInt32(_recipeSource, 0), _searchQuery, MyConvert.ToInt32(hfRowOffSet.Value, 0), 8);
                        DataTable dtSearchResult = RecipeBook.GetRecipes(_idUser, MyConvert.ToInt32(_recipeType, 0), MyConvert.ToInt32(_recipeSource, 0), _searchQuery, 0, _cacheSize);

                        foreach (DataRow _dr in dtSearchResult.Rows)
                        {
                            Session["RecipeInRecipesBookList"] += _dr.Field<Guid>("IDRecipe").ToString() + ";";
                        }
                        _foudRecipes = Session["RecipeInRecipesBookList"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else
                    {
                        _foudRecipes = Session["RecipeInRecipesBookList"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        if (RowOffSet + 8 >= _foudRecipes.Length)
                        {
                            DataTable dtSearchResult = RecipeBook.GetRecipes(_idUser, MyConvert.ToInt32(_recipeType, 0), MyConvert.ToInt32(_recipeSource, 0), _searchQuery, _foudRecipes.Length, _cacheSize);
                            foreach (DataRow _dr in dtSearchResult.Rows)
                            {
                                Session["RecipeInRecipesBookList"] += _dr.Field<Guid>("IDRecipe").ToString() + ";";
                            }
                            _foudRecipes = Session["RecipeInRecipesBookList"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        }
                    }
                    try
                    {
                        //Clear recipe cache
                        Session["FoudRecipeList"] = "";
                        if (Session["RecipeInRecipesBookList"] != null)
                        {
                            Session["FoudRecipeList"] = Session["RecipeInRecipesBookList"];
                        }
                    }
                    catch
                    {
                    }
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

                        pnlNoSearchResult.Visible = false;
                        pnlNextPage.Visible = true;
                        hfLoadRecipeError.Value = "false";

                        string _path = "";
                        try
                        {

                            if (_path.IndexOf("?") > -1)
                            {
                                Uri _uri = new Uri(Request.Url.AbsoluteUri);
                                _path = _uri.GetLeftPart(UriPartial.Path);
                            }
                            else
                            {
                                _path = Request.UrlReferrer.AbsolutePath;
                            }
                        }
                        catch
                        {
                        }

                        ShowRecipe1.IDRecipe = _foudRecipes[RowOffSet + 0];
                        ShowRecipe1.IDUser = _idUser.ToString();
                        ShowRecipe1.ShowEditRecipeButton = true;
                        ShowRecipe1.ShowDeleteButton = ShowDeleteButton;
                        ShowRecipe1.AdditionalQueryStringParameters = "SearchQuery=" + txtRecipeFilter.Text + "&RecipeSource=" + ddlRecipeSource.SelectedValue + "&RecipeType=" + ddlRecipeType.SelectedValue + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                        _recipeInserted++;
                        ShowRecipe2.IDRecipe = _foudRecipes[RowOffSet + 1];
                        ShowRecipe2.IDUser = _idUser.ToString();
                        ShowRecipe2.ShowEditRecipeButton = true;
                        ShowRecipe2.ShowDeleteButton = ShowDeleteButton;
                        ShowRecipe2.AdditionalQueryStringParameters = "SearchQuery=" + txtRecipeFilter.Text + "&RecipeSource=" + ddlRecipeSource.SelectedValue + "&RecipeType=" + ddlRecipeType.SelectedValue + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                        _recipeInserted++;
                        ShowRecipe3.IDRecipe = _foudRecipes[RowOffSet + 2];
                        ShowRecipe3.IDUser = _idUser.ToString();
                        ShowRecipe3.ShowEditRecipeButton = true;
                        ShowRecipe3.ShowDeleteButton = ShowDeleteButton;
                        ShowRecipe3.AdditionalQueryStringParameters = "SearchQuery=" + txtRecipeFilter.Text + "&RecipeSource=" + ddlRecipeSource.SelectedValue + "&RecipeType=" + ddlRecipeType.SelectedValue + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                        _recipeInserted++;
                        ShowRecipe4.IDRecipe = _foudRecipes[RowOffSet + 3];
                        ShowRecipe4.IDUser = _idUser.ToString();
                        ShowRecipe4.ShowEditRecipeButton = true;
                        ShowRecipe4.ShowDeleteButton = ShowDeleteButton;
                        ShowRecipe4.AdditionalQueryStringParameters = "SearchQuery=" + txtRecipeFilter.Text + "&RecipeSource=" + ddlRecipeSource.SelectedValue + "&RecipeType=" + ddlRecipeType.SelectedValue + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                        _recipeInserted++;
                        ShowRecipe5.IDRecipe = _foudRecipes[RowOffSet + 4];
                        ShowRecipe5.IDUser = _idUser.ToString();
                        ShowRecipe5.ShowEditRecipeButton = true;
                        ShowRecipe5.ShowDeleteButton = ShowDeleteButton;
                        ShowRecipe5.AdditionalQueryStringParameters = "SearchQuery=" + txtRecipeFilter.Text + "&RecipeSource=" + ddlRecipeSource.SelectedValue + "&RecipeType=" + ddlRecipeType.SelectedValue + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                        _recipeInserted++;
                        ShowRecipe6.IDRecipe = _foudRecipes[RowOffSet + 5];
                        ShowRecipe6.IDUser = _idUser.ToString();
                        ShowRecipe6.ShowEditRecipeButton = true;
                        ShowRecipe6.ShowDeleteButton = ShowDeleteButton;
                        ShowRecipe6.AdditionalQueryStringParameters = "SearchQuery=" + txtRecipeFilter.Text + "&RecipeSource=" + ddlRecipeSource.SelectedValue + "&RecipeType=" + ddlRecipeType.SelectedValue + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                        _recipeInserted++;
                        ShowRecipe7.IDRecipe = _foudRecipes[RowOffSet + 6];
                        ShowRecipe7.IDUser = _idUser.ToString();
                        ShowRecipe7.ShowEditRecipeButton = true;
                        ShowRecipe7.ShowDeleteButton = ShowDeleteButton;
                        ShowRecipe7.AdditionalQueryStringParameters = "SearchQuery=" + txtRecipeFilter.Text + "&RecipeSource=" + ddlRecipeSource.SelectedValue + "&RecipeType=" + ddlRecipeType.SelectedValue + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
                        _recipeInserted++;
                        ShowRecipe8.IDRecipe = _foudRecipes[RowOffSet + 7];
                        ShowRecipe8.IDUser = _idUser.ToString();
                        ShowRecipe8.ShowEditRecipeButton = true;
                        ShowRecipe8.ShowDeleteButton = ShowDeleteButton;
                        ShowRecipe8.AdditionalQueryStringParameters = "SearchQuery=" + txtRecipeFilter.Text + "&RecipeSource=" + ddlRecipeSource.SelectedValue + "&RecipeType=" + ddlRecipeType.SelectedValue + "&RowOffset=" + hfRowOffSet.Value + "&UrlReferrer=" + _path;
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
                                if (ShowDeleteButton && hfRowOffSet.Value == "0")
                                {
                                    pnlNoSearchResult.Visible = true;
                                }
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
                    //else
                    //{
                    //    pnlNextPage.Visible = false;
                    //    if (MyConvert.ToInt32(hfRowOffSet.Value, 0) == 0)
                    //    {
                    //        pnlPrevPage.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        hfRowOffSet.Value = (MyConvert.ToInt32(hfRowOffSet.Value, 0) - 8).ToString();
                    //    }
                    //    hfLoadRecipeError.Value = "true";
                    //}
                }
                else
                {
                    Response.Redirect("/default.aspx", false);
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in GetDataFromDatabase: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void btnPrevPage_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (MyConvert.ToInt32(hfRowOffSet.Value, 0) > 7 && !MyConvert.ToBoolean(hfLoadRecipeError.Value, false))
                {
                    pnlPrevPage.Visible = true;
                }
                else
                {
                    pnlPrevPage.Visible = false;
                }
                txtRecipeFilter.Focus();
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnPrevPage_Click: " + ex.Message, "", true, false);
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
                txtRecipeFilter.Focus();
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnNextPage_Click: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            Session["RecipeInRecipesBookList"] = "";
        }

    }
}
