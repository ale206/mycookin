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
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.ObjectManager.SocialAction;

namespace MyCookinWeb.RecipeWeb
{
    public partial class ShowRecipe : MyCookinWeb.Form.MyPageBase
    {
        int IDLanguage;
        RecipeLanguage _recipe;
        bool _readOnly = true;
        string _searchString = "";
        bool _continueLoad = true;
        MyUser _User = null;

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
            if(_readOnly)
            {
                btnLike.Visible = false;
                pnlAddComment.Visible = false;
            }
            else
            {
                btnLike.Visible = true;
                pnlAddComment.Visible = true;
            }
            try
            {
                if (Request.QueryString["RowOffset"] != null)
                {
                    hfRowOffSet.Value = MyConvert.ToInt32(Request.QueryString["RowOffset"].ToString(), 0).ToString();
                }
            }
            catch
            {
            }

            if (Request.QueryString["IDLanguage"] != null)
            {
                try
                {
                    IDLanguage = MyConvert.ToInt32(Request.QueryString["IDLanguage"].ToString(), 1);
                }
                catch
                {
                    IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                }
            }
            else
            {
                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
            }
            try
            {
                hfRecipeOf.Value = GetLocalResourceObject("hfRecipeOf.Value").ToString();
                hfRecipeOf2.Value = GetLocalResourceObject("hfRecipeOf2.Value").ToString();
                hfLikeDetailBaseText.Value = GetLocalResourceObject("hfLikeDetailBaseText.Value").ToString();
            }
            catch
            {

            }
            hfIDLanguage.Value = IDLanguage.ToString();
            hfLanguageCode.Value = MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage);
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
                lnkOtherRecipes.NavigateUrl = (AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + _allValue).ToLower();
            }
            catch
            {
                lnkOtherRecipes.NavigateUrl = "/recipemng/AllRecipes.aspx";
            }
            //This avoid issue on routed pages
            Page.Form.Action = Request.RawUrl;
            //btnGoBack.PostBackUrl = Request.UrlReferrer.ToString();

            pnlContent.Attributes.Add("itemscope", "");
            pnlContent.Attributes.Add("itemtype", "http://schema.org/Recipe");

            if (!IsPostBack)
            {
                string _checkNav = "";
                if (Request.QueryString["Nav"] != null)
                {
                    _checkNav = Request.QueryString["Nav"].ToString();
                }
                if (String.IsNullOrEmpty(_checkNav))
                {
                    if (Request.UrlReferrer != null)
                    {
                        btnGoBack.Visible = true;

                        hfReferrerURL.Value = NavHistoryGetPrevUrl(Request.RawUrl);
                        NavHistoryAddUrl(Request.RawUrl);

                    }
                    else
                    {
                        btnGoBack.Visible = false;
                        hfReferrerURL.Value = "";
                    }
                }
                else
                {
                    string _back = NavHistoryGetPrevUrl(Request.RawUrl);
                    if (!String.IsNullOrEmpty(_back))
                    {
                        btnGoBack.Visible = true;
                        hfReferrerURL.Value = _back;
                    }
                    else
                    {
                        btnGoBack.Visible = false;
                        hfReferrerURL.Value = "";
                    }
                }
                GetRecipeBaseInfo();
                if (_continueLoad)
                {
                    GetDataFromDatabase();
                    GetRecipeSteps();
                    //CalculatePrevAndNextRecipe();
                    StartRateControl();
                }
            }
            else
            {
                try
                {
                    if (_continueLoad)
                    {
                        GetRecipeBaseInfo();
                        StartRateControl();
                        GetRecipeSteps();
                    }
                }
                catch
                {
                }
            }
        }

        protected void StartRateControl()
        {
            if (_recipe == null)
            {
                _recipe = new RecipeLanguage(new Guid(Request.QueryString["IDRecipe"].ToString()), IDLanguage);
                _recipe.QueryBaseRecipeInfo();
            }
            rtgRecipe1.StartScore = _recipe.RecipeAvgRating.ToString();
            rtgRecipe1.ImageOffPath = "/Images/Rating/star-off.png";
            rtgRecipe1.ImageOnPath = "/Images/Rating/star-on.png";
            rtgRecipe1.ImageHalfPath = "/Images/Rating/star-half.png";
            rtgRecipe1.StarNumber = "5";
            rtgRecipe1.ReadOnly = _readOnly.ToString();
            rtgRecipe1.EnableCancel = "false";
            rtgRecipe1.CancelImageOffPath = "/Images/Rating/cancel-off.png";
            rtgRecipe1.CancelImageOnPath = "/Images/Rating/cancel-on.png";
            rtgRecipe1.Width = "150";
            rtgRecipe1.RatingWebMethodPath = "/Recipe/ManageRecipe.asmx/VoteRecipe";
            rtgRecipe1.IDObjectToRate = _recipe.IDRecipe.ToString();
            rtgRecipe1.IDUserVoter = Session["IDUser"].ToString();
            rtgRecipe1.MessageOnError = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-ER-0009");
            rtgRecipe1.NoRateMessage = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-IN-0004");
            rtgRecipe1.StartRaty();
        }
       
        protected void CalculatePrevAndNextRecipe()
        {
            try
            {
                if(!String.IsNullOrEmpty(Session["FoudRecipeList"].ToString()))
                {
                    string[] _recipesArray = Session["FoudRecipeList"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    int _recipeArrayPosition = Array.IndexOf(_recipesArray, _recipe.IDRecipe.ToString());
                    if (_recipeArrayPosition < _recipesArray.Length-1)
                    {
                        try 
                        {
                            RecipeLanguage _recipeNext = new RecipeLanguage(new Guid(_recipesArray[_recipeArrayPosition + 1]), IDLanguage);
                            _recipeNext.QueryRecipeLanguageInfo();
                            lnkNavigateNextRecipe.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + _recipeNext.RecipeName.Replace(" ", "-") + "/" + _recipeNext.IDRecipe.ToString()).ToLower();
                            lnkNavigateNextRecipe.Visible = true;
                        }
                        catch
                        {
                            lnkNavigateNextRecipe.Visible = false;
                        }
                    }
                    else
                    {
                        lnkNavigateNextRecipe.Visible = false;
                    }
                    if (_recipeArrayPosition > 0)
                    {
                        try 
                        {
                            RecipeLanguage _recipePrev = new RecipeLanguage(new Guid(_recipesArray[_recipeArrayPosition - 1]), IDLanguage);
                            _recipePrev.QueryRecipeLanguageInfo();
                            lnkNavigatePrevRecipe.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + _recipePrev.RecipeName.Replace(" ", "-") + "/" + _recipePrev.IDRecipe.ToString()).ToLower();
                            lnkNavigatePrevRecipe.Visible = true;
                        }
                        catch
                        {
                            lnkNavigatePrevRecipe.Visible = false;
                        }
                    }
                    else
                    {
                        lnkNavigatePrevRecipe.Visible = false;
                    }
                }
                else
                {
                    lnkNavigateNextRecipe.Visible = false;
                    lnkNavigatePrevRecipe.Visible = false;
                }
            }
            catch
            {
            }
        }

        protected void GetRecipeBaseInfo()
        {
            try
            {
                if (Request.QueryString["IDRecipe"] == null)
                {
                    _continueLoad = false;
                    Context.ApplicationInstance.CompleteRequest();
                    Response.Redirect("/default.aspx", false);
                }
                if (_continueLoad)
                {
                    _recipe = new RecipeLanguage(new Guid(Request.QueryString["IDRecipe"].ToString()), IDLanguage);
                    if (Request.QueryString["SearchQuery"] != null)
                    {
                        _searchString = Request.QueryString["SearchQuery"].ToString();
                    }
                    _recipe.QueryRecipeInfo();
                    _recipe.QueryRecipeLanguageInfo();
                    hfRecipeName.Value = _recipe.RecipeName.Replace("'","\\'");
                    hfIDRecipe.Value = _recipe.IDRecipe.ToString();


                    if(String.IsNullOrEmpty(_recipe.RecipeName))
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
                        Response.Redirect((AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + _allValue).ToLower(), false);
                    }
                }
            }
            catch
            {
            }
        }

        protected void GetDataFromDatabase()
        {
            try
            {

                hfOgpUrl.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + Request.RawUrl;
                hfOgpFbAppID.Value = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                //Create recipe istance
               // _recipe = new RecipeLanguage(new Guid("37192E7D-73E3-402E-8B89-00051E0B6652"), IDLanguage);
                
                try
                {
                    _User = new MyUser(new Guid(Session["IDUser"].ToString()));
                    hfIDUser.Value = _User.IDUser.ToString();
                    hfCurrentUsername.Value = Session["Username"].ToString();
                    hfCurrentRecipeUrl.Value = Request.RawUrl;
                }
                catch
                {
                }
                //inizialize Like control
                if (!_readOnly)
                {
                    try
                    {
                        RecipeFeedback _checkLike = new RecipeFeedback();
                        _checkLike.QueryFeedback(_recipe.IDRecipe, _User.IDUser);
                        if (_checkLike.FeedbackType == RecipeFeedbackType.Like)
                        {
                            btnLike.ImageUrl = btnLike.ImageUrl.Replace("-off", "-on");
                        }
                    }
                    catch
                    {

                    }
                }

                //Query Recipe Info
                _recipe.GetRecipeIngredients();
                _recipe.GetRecipePropertiesValue();

                
                try
                {
                    Guid _recipeGuid = _recipe.Owner;
                    if (!_readOnly && _User.IDUser != _recipeGuid)
                    {
                        //pnlRecipeBookManage.Visible = true;
                        if (RecipeBook.CheckRecipeIsInBook(_User.IDUser, _recipe.IDRecipe))
                        {
                            btnRemoveFromRecipeBook.Visible = true;
                            btnAddToRecipeBook.Visible = false;
                        }
                        else
                        {
                            btnRemoveFromRecipeBook.Visible = false;
                            btnAddToRecipeBook.Visible = true;
                        }
                        btnShareFacebook.Visible = false;
                        btnShareTwitter.Visible = false;
                        btnUserCooking.Visible = true;
                        btnAddYourRecipe.Visible = true;
                    }
                    else if (!_readOnly)
                    {
                        btnShareFacebook.Visible = false;
                        btnShareTwitter.Visible = false;
                        btnUserCooking.Visible = true;
                        btnAddYourRecipe.Visible = false;
                    }
                    else
                    {
                        //pnlRecipeBookManage.Visible = false;
                        btnAddYourRecipe.Visible = false;
                        btnRemoveFromRecipeBook.Visible = false;
                        btnAddToRecipeBook.Visible = true;
                        btnShareFacebook.Visible = false;
                        btnShareTwitter.Visible = false;
                        btnUserCooking.Visible = false;
                    }
                }
                catch
                {
                }

                if (_recipe.RecipeLanguageAutoTranslate)
                {
                    lblAutoTranslate.Visible = true;
                }
                else
                {
                    lblAutoTranslate.Visible = false;
                }

                #region Nutritional Facts
                pnlVegan.Visible = MyConvert.ToBoolean(_recipe.Vegan, false);
                hfVegan.Value = pnlVegan.Visible.ToString();
                pnlVegetarian.Visible = MyConvert.ToBoolean(_recipe.Vegetarian, false);
                hfVegetarian.Value = pnlVegetarian.Visible.ToString();
                pnlGlutenFree.Visible = MyConvert.ToBoolean(_recipe.GlutenFree, false);
                hfGlutenFree.Value = pnlGlutenFree.Visible.ToString();
                pnlHotSpicy.Visible = MyConvert.ToBoolean(_recipe.HotSpicy, false);

                lblProteins.Text = Math.Round(Convert.ToDouble(_recipe.RecipePortionProteins),0).ToString();
                lblCarbohydrates.Text = Math.Round(Convert.ToDouble(_recipe.RecipePortionCarbohydrates),0).ToString();
                lblFats.Text = Math.Round(Convert.ToDouble(_recipe.RecipePortionFats),0).ToString();
                lblAlcohol.Text = Math.Round(Convert.ToDouble(_recipe.RecipePortionAlcohol), 0).ToString();
                lblKcal.Text = Math.Round(Convert.ToDouble(_recipe.RecipePortionKcal), 0).ToString();
                pnlPortionKcal.Attributes.Add("itemprop", "nutrition");
                pnlPortionKcal.Attributes.Add("itemscope", "");
                pnlPortionKcal.Attributes.Add("itemype", "http://schema.org/NutritionInformation");
                lblKcal.Attributes.Add("itemprop", "calories");

                pnlRecipeNutritionalInfo.Attributes.Add("itemprop", "nutrition");
                pnlRecipeNutritionalInfo.Attributes.Add("itemscope", "");
                pnlRecipeNutritionalInfo.Attributes.Add("itemype", "http://schema.org/NutritionInformation");
                lblProteins.Attributes.Add("itemprop", "proteinContent");
                lblCarbohydrates.Attributes.Add("itemprop", "carbohydrateContent");
                lblFats.Attributes.Add("itemprop", "fatContent");


                int _fatLimit = MyConvert.ToInt32(AppConfig.GetValue("FatRecipeThreshold", AppDomain.CurrentDomain), 100);
                int _proteinstLimit = MyConvert.ToInt32(AppConfig.GetValue("ProteinsRecipeThreshold", AppDomain.CurrentDomain), 100);
                int _carbohydratesLimit = MyConvert.ToInt32(AppConfig.GetValue("CarbohydratesRecipeThreshold", AppDomain.CurrentDomain), 100);
                int _alcoholLimit = MyConvert.ToInt32(AppConfig.GetValue("AlcoholRecipeThreshold", AppDomain.CurrentDomain), 100);

                if (_recipe.RecipePortionFats >= _fatLimit)
                {
                    lblFats.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Orange", AppDomain.CurrentDomain));
                }
                else
                {
                    lblFats.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Green", AppDomain.CurrentDomain));
                }

                if (_recipe.RecipePortionProteins >= _proteinstLimit)
                {
                    lblProteins.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Orange", AppDomain.CurrentDomain));
                }
                else
                {
                    lblProteins.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Green", AppDomain.CurrentDomain));
                }

                if (_recipe.RecipePortionCarbohydrates >= _carbohydratesLimit)
                {
                    lblCarbohydrates.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Orange", AppDomain.CurrentDomain));
                }
                else
                {
                    lblCarbohydrates.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Green", AppDomain.CurrentDomain));
                }

                if (_recipe.RecipePortionAlcohol >= _alcoholLimit)
                {
                    lblAlcohol.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Orange", AppDomain.CurrentDomain));
                }
                else
                {
                    lblAlcohol.ForeColor = System.Drawing.ColorTranslator.FromHtml(AppConfig.GetValue("Green", AppDomain.CurrentDomain));
                }
                #endregion
                //Visualize Recipe Info
                lblRecipeName.Text = _recipe.RecipeName;
                lblRecipeName.Attributes.Add("itemprop", "name");
                
                hfKeywords.Value = _recipe.RecipeName + " " + GetLocalResourceObject("KcalSEO.Value").ToString() + ", " + _recipe.RecipeLanguageTags;
                hfCreationDate.Value = _recipe.LastUpdate.ToString();

                if (_recipe.RecipeImage != null && _recipe.RecipeImage.IDMedia != new Guid())
                {
                    imgRecipe.ImageUrl = _recipe.RecipeImage.GetCompletePath(false, false, true);
                    imgRecipe.Attributes.Add("itemprop", "image");

                    if (imgRecipe.ImageUrl == "")
                    {
                        imgRecipe.ImageUrl = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                    }
                    
                    if (imgRecipe.ImageUrl.IndexOf("http://") > -1 || imgRecipe.ImageUrl.IndexOf("https://") > -1)
                    {
                        hfOgpImage.Value = imgRecipe.ImageUrl;
                    }
                    else
                    {
                        hfOgpImage.Value = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + imgRecipe.ImageUrl;
                    }
                    imgRecipe.CssClass = "imgRecipe";
                    try
                    {
                        hfThumbnailUrl.Value = _recipe.RecipeImage.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    imgRecipe.ImageUrl = "/Images/icon-recipe-big.png";
                    imgRecipe.CssClass = "imgRecipeNoPhoto";
                }
                imgRecipe.AlternateText = _recipe.RecipeName;
                repRecipeIngredient.DataSource = _recipe.RecipeIngredients;
                repRecipeIngredient.DataBind();

                #region Recipe Difficulties
                rcRecipe.LoadControl = true;
                rcRecipe.IDLanguage = IDLanguage;
                rcRecipe.ComplexityLevel = (int)_recipe.RecipeDifficulties;

                switch ((int)_recipe.RecipeDifficulties)
                {
                    case 1:
                        lblComplexity.Text = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0001");
                        break;
                    case 2:
                        lblComplexity.Text = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0002");
                        break;
                    case 3:
                        lblComplexity.Text = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0003");
                        break;
                    default:
                        lblComplexity.Text = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0002");
                        break;
                }
                #endregion
                try
                {
                    if (_recipe.Owner != null)
                    {
                        pnlRecipeOwner.Visible = true;
                        _recipe.Owner.GetUserBasicInfoByID();
                        lnkRecipeOwner.Text = _recipe.Owner.Name + " " + _recipe.Owner.Surname;
                        
                        Page.Title = _recipe.RecipeName + " - " + _recipe.Owner.UserName + " - MyCookin";
                        hfOgpTitle.Value = _recipe.RecipeName + " - " + _recipe.Owner.UserName;
                        hfRecipeOwner.Value = _recipe.Owner.UserName;
                        hfIDRecipeOwner.Value = _recipe.Owner.IDUser.ToString();

                        lnkRecipeOwner.Attributes.Add("itemprop", "author");
                        lnkRecipeOwner.NavigateUrl = ("/" + _recipe.Owner.UserName + "/").ToLower();
                        imgRecipeOwner.AlternateText = _recipe.Owner.UserName;
                        try
                        {
                            if (_recipe.Owner.IDProfilePhoto != null)
                            {
                                _recipe.Owner.IDProfilePhoto.QueryMediaInfo();
                                string _profilePhoto = "";
                                try
                                {
                                    _profilePhoto = _recipe.Owner.IDProfilePhoto.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                                }
                                catch
                                {
                                }
                                if (_profilePhoto == "")
                                {
                                    _profilePhoto = _recipe.Owner.IDProfilePhoto.GetCompletePath(false, false, true);
                                }

                                if (_profilePhoto != "")
                                {
                                    imgRecipeOwner.ImageUrl = _profilePhoto;
                                    imgRecipeOwner.CssClass = "imgUserIconInfo";
                                }
                                else
                                {
                                    imgRecipeOwner.CssClass = "imgUserIconInfoNoPhoto";
                                }
                            }
                            else
                            {
                                imgRecipeOwner.CssClass = "imgUserIconInfoNoPhoto";
                            }
                        }
                        catch
                        {
                            pnlRecipeOwner.Visible = false;
                        }
                    }
                    else
                    {
                        pnlRecipeOwner.Visible = false;
                    }
                }
                catch
                {
                }

                

                try
                {
                    if (_recipe.NumberOfPerson > 1)
                    {
                        lblNumPeoplePlural.Text = lblNumPeoplePlural.Text.Replace("{0}", _recipe.NumberOfPerson.ToString());
                        lblNumPeoplePlural.Visible = true;
                        lblNumPeopleSingular.Visible = false;
                        lblNumPeoplePlural.Attributes.Add("itemprop", "recipeYield");
                    }
                    else
                    {
                        lblNumPeopleSingular.Text = lblNumPeopleSingular.Text.Replace("{0}", _recipe.NumberOfPerson.ToString());
                        lblNumPeopleSingular.Visible = true;
                        lblNumPeoplePlural.Visible = false;
                        lblNumPeopleSingular.Attributes.Add("itemprop", "recipeYield");
                    }
                }
                catch
                {
                    lblNumPeopleSingular.Visible = false;
                    lblNumPeoplePlural.Visible = false;
                }
                if (_recipe.PreparationTimeMinute > 0)
                {
                    if (_recipe.PreparationTimeMinute <= 60)
                    {
                        lblPreparation.Text = lblPreparation.Text.Replace("{0}", _recipe.PreparationTimeMinute.ToString());
                        hfPrepTime.Value = ("PT{0}M").Replace("{0}", _recipe.PreparationTimeMinute.ToString());
                    }
                    else
                    {
                        TimeSpan _timePrep = new TimeSpan(0, MyConvert.ToInt32(_recipe.PreparationTimeMinute, 0), 0);
                        lblPreparation.Text = string.Format("{0:D2}:{1:D2}", _timePrep.Hours, _timePrep.Minutes);
                    }
                    pnlPreparationTime.Visible = true;
                }
                else
                {
                    pnlPreparationTime.Visible = false;
                }
                if (_recipe.CookingTimeMinute > 0)
                {
                    if (_recipe.CookingTimeMinute <= 60)
                    {
                        lblCookingTime.Text = lblCookingTime.Text.Replace("{0}", _recipe.CookingTimeMinute.ToString());
                        hfCookTime.Value = ("PT{0}M").Replace("{0}", _recipe.CookingTimeMinute.ToString());
                    }
                    else
                    {
                        TimeSpan _timeCook = new TimeSpan(0, MyConvert.ToInt32(_recipe.CookingTimeMinute, 0), 0);
                        lblCookingTime.Text = string.Format("{0:D2}:{1:D2}", _timeCook.Hours, _timeCook.Minutes);
                    }
                    pnlCookingTime.Visible = true;
                }
                else
                {
                    pnlCookingTime.Visible = false;
                }

                if (_recipe.IDCity != null && _recipe.IDCity != 0)
                {
                    City _cityRecipe = new City((int)_recipe.IDCity);
                    lblRegion.Text = _cityRecipe.CityName;
                    pnlRecipeRegion.Visible = true;
                }
                else
                {
                    pnlRecipeRegion.Visible = false;
                }

                string _cookingType = "";
                int _countCookingType = 0;
                foreach (RecipeProperty recipeProp in RecipeProperty.GetAllRecipePropertyListByType(4, IDLanguage))
                {
                    try
                    {
                        foreach (RecipePropertyValue recipePropValue in _recipe.PropertyValue)
                        {
                            if (recipePropValue.IDRecipeProp == recipeProp && recipePropValue.Value)
                            {
                                _cookingType += recipeProp.RecipeProp + "<br/>";
                                _countCookingType++;
                                if (_countCookingType >= 3)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (_countCookingType >= 3)
                    {
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(_cookingType))
                {
                    lblCookingType.Text = _cookingType.Substring(0, _cookingType.Length - 5);
                    lblCookingType.Attributes.Add("itemprop", "cookingMethod");
                    pnlCookingType.Visible = true;
                }
                else
                {
                    pnlCookingType.Visible = false;
                }

                string _foodPreservation = "";
                int _countFoodPreservation = 0;
                foreach (RecipeProperty recipeProp in RecipeProperty.GetAllRecipePropertyListByType(6, IDLanguage))
                {
                    try
                    {
                        foreach (RecipePropertyValue recipePropValue in _recipe.PropertyValue)
                        {
                            if (recipePropValue.IDRecipeProp == recipeProp && recipePropValue.Value)
                            {
                                _foodPreservation += recipeProp.RecipeProp + "<br/>";
                                _countFoodPreservation++;
                                if (_countFoodPreservation >= 2)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (_countFoodPreservation >= 2)
                    {
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(_foodPreservation))
                {
                    lblPreservation.Text = _foodPreservation.Substring(0, _foodPreservation.Length - 5); ;
                    pnlPreservation.Visible = true;
                }
                else
                {
                    pnlPreservation.Visible = false;
                }

                try
                {
                    List<BeverageRecipe> _beverages = BeverageRecipe.GetBeverageForRecipe(_recipe.IDRecipe);
                    if (_beverages.Count > 0)
                    {
                        pnlRecipeBeverage.Visible = true;
                        rptRecipeBeverage.DataSource = _beverages;
                        rptRecipeBeverage.DataBind();
                    }
                    else
                    {
                        pnlRecipeBeverage.Visible = false;
                    }
                }
                catch
                {
                }

                
                if (String.IsNullOrEmpty(_recipe.RecipeSuggestion))
                {
                    pnlRecipeSuggestion.Visible = false;
                }
                else
                {
                    pnlRecipeSuggestion.Visible = true;
                    lblRecipeSuggestion.Text = _recipe.RecipeSuggestion.Replace("\n", "<br />");
                }

                if (String.IsNullOrEmpty(_recipe.RecipeHistory))
                {
                    pnlRecipeHistory.Visible = false;
                }
                else
                {
                    pnlRecipeHistory.Visible = true;
                    lblRecipeHistory.Text = _recipe.RecipeHistory.Replace("\n", "<br />");
                }

                if (String.IsNullOrEmpty(_recipe.RecipeNote))
                {
                    pnlSpecialNote.Visible = false;
                }
                else
                {
                    pnlSpecialNote.Visible = true;
                    lblSpecialNote.Text = _recipe.RecipeNote.Replace("\n", "<br />");
                }

            }
            catch (Exception ex)
            {
               //WRITE A ROW IN LOG FILE AND DB
                try
                {
	                //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in GetDataFromDatabase(): " + ex.Message, "", true, false);
	                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
	                LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);}
                catch { }
            }

            try
            {
                _recipe.RecipeConsulted++;
                _recipe.Save();
            }
            catch
            {
            }
            //Insert Statistics for sharing
            try
            {
                string _refer = "";
                Guid _guidUser = new Guid();
                if (!String.IsNullOrEmpty(Session["IDUser"].ToString()))
                {
                    _guidUser = new Guid(Session["IDUser"].ToString());
                }
                if(Request.UrlReferrer!=null)
                {
                    _refer = Request.UrlReferrer.PathAndQuery;
                }
                MyStatistics NewStatistic = new MyStatistics(_guidUser, _recipe.IDRecipe, StatisticsActionType.RC_ViewRecipe, "Recipe Viewed", Network.GetCurrentPageName(), _refer, Session.SessionID);
                NewStatistic.InsertNewRow();
            }
            catch
            { }
        }

        protected void GetRecipeSteps()
        {
            try
            {
                _recipe.GetRecipeSteps();
                string _imgStepPath = "";
                foreach (RecipeStep _step in _recipe.RecipeSteps)
                {
                    Panel _pnlImageStep = new Panel();
                    _pnlImageStep.ID = "pnlStep" + _step.IDRecipeStep;
                    _pnlImageStep.CssClass = "pnlRightInternal";

                    Label _lblStep = new Label();
                    _lblStep.ID = "lblStep" + _step.IDRecipeStep;
                    _lblStep.CssClass = "lblStepDesc";
                    _lblStep.Text = _step.Step.Replace("\n", "<br />");
                    _lblStep.Attributes.Add("itemprop", "recipeInstructions");
                    
                    if (String.IsNullOrEmpty(hfOgpDescription.Value) && !String.IsNullOrEmpty(_step.Step))
                    {
                        if (_step.Step.Length > 247)
                        {
                            hfOgpDescription.Value = _step.Step.Substring(0, 247) + "...";
                        }
                        else
                        {
                            hfOgpDescription.Value = _step.Step;
                        }
                    }

                    Image _imgStep = new Image();
                    _imgStep.ID = "imgStep" + _step.IDRecipeStep;
                    _imgStep.CssClass = "imgStep";
                    _imgStep.AlternateText = _recipe.RecipeName;

                    Label _lblStepNumber = new Label();
                    _lblStepNumber.ID = "lblStepNum" + _step.IDRecipeStep;
                    _lblStepNumber.CssClass = "lblStepNum";
                    _lblStepNumber.Text = _step.StepNumber.ToString();

                    //Label _lblStepNumberNoPhoto = new Label();
                    //_lblStepNumberNoPhoto.ID = "lblStepNumNoPhoto" + _step.IDRecipeStep;
                    //_lblStepNumberNoPhoto.CssClass = "lblStepNumNoPhoto";
                    //_lblStepNumberNoPhoto.Text = _step.StepNumber.ToString();
                   
                    try
                    {
                        _imgStep.Visible = true;
                        _step.IDRecipeStepImage.QueryMediaInfo();
                        _imgStepPath = _step.IDRecipeStepImage.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                        if (_imgStepPath == "")
                        {
                            _imgStepPath = _step.IDRecipeStepImage.GetCompletePath(false, false, true);
                        }
                        if (_imgStepPath != "")
                        {
                            _imgStep.ImageUrl = _imgStepPath;
                        }
                        else
                        {
                            _imgStep.Visible = false;
                            _lblStep.CssClass = "lblStepDescNoPhoto";
                            _lblStepNumber.CssClass = "lblStepNumNoPhoto";
                        }
                    }
                    catch
                    {
                        _imgStep.Visible = false;
                        _lblStep.CssClass = "lblStepDescNoPhoto";
                        _lblStepNumber.CssClass = "lblStepNumNoPhoto";
                    }
                    if (_step.StepNumber == 0)
                    {
                        _lblStepNumber.Visible = false;
                    }



                    Panel _pnlSeparator = new Panel();
                    _pnlSeparator.ID = "pnlSeparato" + _step.IDRecipeStep;
                    _pnlSeparator.CssClass = "pnlStepSeparator";
                    _pnlImageStep.Controls.Add(_lblStepNumber);
                    _pnlImageStep.Controls.Add(_imgStep);
                    _pnlImageStep.Controls.Add(_lblStep);
                    _pnlImageStep.Controls.Add(_pnlSeparator);
                    pnlPreparationSteps.Controls.Add(_pnlImageStep);
                    
                }
            }
            catch
            {
            }
        }

        protected void btnUserCooking_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int _idlang = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                //INSERT ACTION IN USER BOARD
                UserBoard NewActionInUserBoard = new UserBoard(new Guid(Session["IDUser"].ToString()), null, ActionTypes.RecipeCooked, new Guid(Request.QueryString["IDRecipe"].ToString()), null, null, DateTime.UtcNow, _idlang);
                ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();

                string NotificationText = RetrieveMessage.RetrieveDBMessage(_idlang, "US-IN-0064");

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                    "noty({text: '" + NotificationText + "'});", true);

                //Insert Statistics for sharing
                try
                {
                    MyStatistics NewStatistic = new MyStatistics(new Guid(Session["IDUser"].ToString()), new Guid(Request.QueryString["IDRecipe"].ToString()), StatisticsActionType.RC_RecipeSharedOnOwnWallFromShowRecipe, "Recipe Shared on own Wall From Show Recipe", Network.GetCurrentPageName(), "", Session.SessionID);
                    NewStatistic.InsertNewRow();
                }
                catch
                { }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnUserCookin_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }
        protected void btnAddToRecipeBook_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Session["IDUser"].ToString() != "")
                {
                    RecipeBook _recipeBook = new RecipeBook(new Guid(Session["IDUser"].ToString()), new Guid(Request.QueryString["IDRecipe"].ToString()), 1);
                    ManageUSPReturnValue _result = _recipeBook.SaveRecipe();
                    if (!_result.IsError)
                    {
                        btnAddToRecipeBook.Visible = false;
                        btnRemoveFromRecipeBook.Visible = true;
                        try
                        {
                            //INSERT ACTION IN USER BOARD
                            UserBoard NewActionInUserBoard = new UserBoard(new Guid(Session["IDUser"].ToString()), null, ActionTypes.RecipeAddedToRecipeBook, new Guid(Request.QueryString["IDRecipe"].ToString()), null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                            ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    Response.Redirect("/user/login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnAddToRecipeBook_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void btnRemoveFromRecipeBook_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RecipeBook _recipeBook = new RecipeBook(new Guid(Session["IDUser"].ToString()), new Guid(Request.QueryString["IDRecipe"].ToString()), 1);
                ManageUSPReturnValue _result = _recipeBook.DeleteRecipe();
                if (!_result.IsError)
                {
                    btnAddToRecipeBook.Visible = true;
                    btnRemoveFromRecipeBook.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnRemoveFromRecipeBook_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void btnAddYourRecipe_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect(("/RecipeMng/CreateRecipe.aspx?RecipeName=" + lblRecipeName.Text).ToLower(), true);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnAddYourRecipe_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void btnGoBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //if (hfReferrerURL.Value.IndexOf("/FindRecipes.aspx") > -1 || hfReferrerURL.Value.IndexOf("/MyRecipesBook.aspx") > -1)
                //{
                //    Response.Redirect(hfReferrerURL.Value + "?SearchQuery=" + Request.QueryString["SearchQuery"].ToString() + "&Vegan=" + Request.QueryString["Vegan"].ToString() + "&Vegetarian=" + Request.QueryString["Vegetarian"].ToString() + "&GlutenFree=" + Request.QueryString["GlutenFree"].ToString() + "&FrigoMix=" + Request.QueryString["FrigoMix"].ToString() + "&Light=" + Request.QueryString["Light"].ToString() + "&Quick=" + Request.QueryString["Quick"].ToString() + "&RowOffset=" + hfRowOffSet.Value, true);
                //}
                //else if (hfReferrerURL.Value.IndexOf("/RecipeBook") > -1 || hfReferrerURL.Value.IndexOf("/Ricettario") > -1)
                //{
                //    string _redirectUrl = "";
                //    try
                //    {
                //        if (hfReferrerURL.Value.IndexOf("?") > -1)
                //        {
                //            hfReferrerURL.Value=hfReferrerURL.Value.Substring(0,hfReferrerURL.Value.IndexOf("?"));
                //        }
                //        _redirectUrl = hfReferrerURL.Value + "?SearchQuery=" + Request.QueryString["SearchQuery"].ToString() + "&RecipeSource=" + Request.QueryString["RecipeSource"].ToString() + "&RecipeType=" + Request.QueryString["RecipeType"].ToString() + "&RowOffset=" + hfRowOffSet.Value;
                //    }
                //    catch
                //    {
                //        _redirectUrl = hfReferrerURL.Value;
                //    }
                //    Response.Redirect(_redirectUrl, true);
                //}
                //else
                //{
                //    Response.Redirect(hfReferrerURL.Value, true);
                //}
                NavHistoryRemoveUrlFrom(Request.RawUrl);

                if (!String.IsNullOrEmpty(hfRowOffSet.Value) && hfReferrerURL.Value.IndexOf("?") > -1)
                {
                    Response.Redirect((hfReferrerURL.Value.Replace("RowOffset=0", "RowOffset=" + hfRowOffSet.Value)).ToLower(), true);
                }
                else
                {
                    Response.Redirect((hfReferrerURL.Value).ToLower(), true);
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnGoBack_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void btnShareFacebook_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Guid IDActionRelatedObject = _recipe.IDRecipe;

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                //Insert Statistics for sharing
                try
                {
                    MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDActionRelatedObject, StatisticsActionType.RC_RecipeSharedOnFacebookFromShowRecipe, "Recipe Shared on Facebook From Show Recipe", Network.GetCurrentPageName(), "", Session.SessionID);
                    NewStatistic.InsertNewRow();
                }
                catch
                { }

                //Check if the user is registered to this Social Network.
                //If not, ask for autorization
                if (!MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Facebook))
                {
                    #region AuthorizeFacebook
                    //Your Website Url which needs to Redirected
                    string callBackUrl = "";
                    if (!String.IsNullOrEmpty(Request.Url.Query))
                    {
                        callBackUrl = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "");
                    }
                    else
                    {
                        callBackUrl = Request.Url.AbsoluteUri;
                    }

                    string FacebookClientId = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                    string FacebookScopes = AppConfig.GetValue("facebook_scopes", AppDomain.CurrentDomain);

                    Response.Redirect(string.Format("https://graph.facebook.com/oauth/" +
                      "authorize?client_id={0}&redirect_uri={1}&scope={2}",
                      FacebookClientId, callBackUrl, FacebookScopes));
                    #endregion
                }
                else
                {
                    //Get IDUserSocial And AccessToken
                    //*****************************************
                    MyUserSocial UserSocialInfo = new MyUserSocial(IDUserGuid, (int)SocialNetwork.Facebook);
                    UserSocialInfo.GetUserSocialInformations();
                    //*****************************************

                    //Get info according to IDUserAction
                    SocialAction NewFBAction = new SocialAction(UserSocialInfo.IDUserSocial, UserSocialInfo.AccessToken, UserSocialInfo.RefreshToken, IDActionRelatedObject, ActionTypes.NewRecipe, null);

                    string IDPost = NewFBAction.FB_PostOnWall();

                    //Error in publish: user removed authorization
                    if (String.IsNullOrEmpty(IDPost))
                    {
                        #region AskForAuthorizations
                        //Ask again for authorizations..
                        //Your Website Url which needs to Redirected
                        string callBackUrl = "";
                        if (!String.IsNullOrEmpty(Request.Url.Query))
                        {
                            callBackUrl = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "");
                        }
                        else
                        {
                            callBackUrl = Request.Url.AbsoluteUri;
                        }

                        string FacebookClientId = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                        string FacebookScopes = AppConfig.GetValue("facebook_scopes", AppDomain.CurrentDomain);

                        Response.Redirect(string.Format("https://graph.facebook.com/oauth/" +
                          "authorize?client_id={0}&redirect_uri={1}&scope={2}",
                          FacebookClientId, callBackUrl, FacebookScopes));
                        #endregion
                    }
                    else
                    {
                        //OK! SHARED :)
                        btnShareFacebook.Visible = false;

                        //Noty Alert
                        string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0061");

                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                            "noty({text: '" + NotificationText + "'});", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //Noty Alert
                    string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0012");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "noty({text: '" + NotificationText + "'});", true);

                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> ibtnShareFacebook_Click: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }

        protected void btnShareTwitter_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Guid IDActionRelatedObject = _recipe.IDRecipe;

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                //Insert Statistics for sharing
                try
                {
                    MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDActionRelatedObject, StatisticsActionType.RC_RecipeSharedOnTwitterFromShowRecipe, "Recipe Shared on Twitter From Show Recipe", Network.GetCurrentPageName(), "", Session.SessionID);
                    NewStatistic.InsertNewRow();
                }
                catch
                { }

                //Check if the user is registered to this Social Network.
                //If not, ask for autorization
                if (!MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Twitter))
                {
                    string url = "/auth/auth.aspx?twitterauth=true";
                    Response.Redirect(url, true);
                }
                else
                {
                    //Get IDUserSocial And AccessToken
                    //*****************************************
                    MyUserSocial UserSocialInfo = new MyUserSocial(IDUserGuid, (int)SocialNetwork.Twitter);
                    UserSocialInfo.GetUserSocialInformations();
                    //*****************************************

                    //Get info according to IDUserAction
                    SocialAction NewTWAction = new SocialAction(UserSocialInfo.IDUserSocial, UserSocialInfo.AccessToken, UserSocialInfo.RefreshToken, IDActionRelatedObject, ActionTypes.NewRecipe, null);

                    NewTWAction.TW_tweet();

                    //OK. SHARED :)
                    btnShareTwitter.Visible = false;

                    //Noty Alert
                    string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0062");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "noty({text: '" + NotificationText + "'});", true);

                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //Noty Alert
                    string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0013");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "noty({text: '" + NotificationText + "'});", true);

                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> ibtnShareTwitter_Click: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
    }
}
