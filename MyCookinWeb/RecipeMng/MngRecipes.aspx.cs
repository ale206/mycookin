using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.ObjectManager.AI.Recipe;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;
using System.Drawing;
using MyCookinWeb.CustomControls;
using MyCookin.ObjectManager.UserBoardManager;

namespace MyCookinWeb.RecipeWeb
{
    public partial class MngRecipes :  MyCookinWeb.Form.MyPageBase
    {
        int IDLanguage;
        RecipeLanguage _recipe;

        protected void Page_Load(object sender, EventArgs e)
        {
            IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            acBeverage.MethodName = "/Beverage/SearchBeverage.asmx/SearchBeverages";
            acBeverage.LanguageCode = "2";
            acBeverage.ObjectLabelIdentifier = "IngredientSingular";
            acBeverage.ObjectIDIdentifier = "IDIngredient";
            acBeverage.ObjectLabelText = "";
            acBeverage.LangFieldLabel = "IDLanguage";
            acBeverage.WordFieldLabel = "words";
            acBeverage.MinLenght = "2";

            if (!IsPostBack)
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

                //Check if user belong group authorized to view this page
                if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("5b6650ae-4430-4e64-98f5-0c08f78e2392") >= 0)
                {

                    //If id parameter for page not exist
                    if (Request.QueryString["IDRecipe"] == null)
                    {
                        Response.Redirect("RecipeDashBoard.aspx", true);
                    }



                    if (!Page.IsPostBack)
                    {
                        GetDataFromDatabase();
                        try
                        {
                            LogRow logRowOpenIngredient = new LogRow(DateTime.UtcNow, "0", "0", Network.GetCurrentPageName(),
                                "", "Open Ingredient " + _recipe.IDRecipe.ToString(), Session["IDUser"].ToString(), false, true);
                            LogManager.WriteDBLog(LogLevel.InfoMessages, logRowOpenIngredient);
                            LogManager.WriteFileLog(LogLevel.InfoMessages, false, logRowOpenIngredient);
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    pnlMainTab.Visible = false;
                    pnlNoAuth.Visible = true;
                }
                Page.Form.DefaultButton = null;

                //Inizialize Uploader
                upshImgRecipe.ImageMediaType = MediaType.RecipePhoto;
                upshImgRecipe.CropErrorBoxMsg = "Non hai croppato l'area della foto";
                upshImgRecipe.CropErrorBoxTitle = "Errore.";
                upshImgRecipe.CropAspectRatio = "1";

                upshImgRecipe.DeleteWarningMsg = "Sei sicuro di voler cancellare?";
                upshImgRecipe.DeleteUndo = "No";
                upshImgRecipe.BaseFileName = _recipe.RecipeName.ToString();
                upshImgRecipe.IDMediaOwner = new Guid(Session["IDUser"].ToString());
                upshImgRecipe.DeleteConfirm = "Sicuro?";
                upshImgRecipe.DeleteUndo = "Annulla";
                //==============================

                //Inizialize AddRecipeIngredient Control
                ariIngredient.MethodName = "/Ingredient/SearchIngredient.asmx/SearchIngredients";
                ariIngredient.LanguageCode = "2";
                ariIngredient.ObjectLabelIdentifier = "IngredientSingular";
                ariIngredient.ObjectIDIdentifier = "IDIngredient";
                ariIngredient.ObjectLabelText = "Aggiungi Ingrediente: ";
                ariIngredient.LangFieldLabel = "IDLanguage";
                ariIngredient.WordFieldLabel = "words";
                ariIngredient.MinLenght = "2";
                //==============================


                //Configure AutoSuggest parameter
                //asRecipeTags.WebServiceURL = "/Recipe/GetRecipeInfo.asmx/GetRecipeLangTags";
                //asRecipeTags.SelectedItemProp = "Tag";
                //asRecipeTags.SearchObjProps = "Tag";
                //asRecipeTags.SelectedValuesProp = "Tag";
                //asRecipeTags.QueryParam = "TagStartWord";
                //asRecipeTags.QueryIDLangParam = "IDLanguage";
                //asRecipeTags.QueryIDLangValue = "2";
                //asRecipeTags.MinChars = "3";
                //asRecipeTags.MaxAllowedValues = "20";
                //asRecipeTags.StartText = "Inserisci Tag per questa ricetta";
                //asRecipeTags.EmptyText = "Nessun Tag trovato con queste iniziali";
                //asRecipeTags.LimitAllowedValuesText = "Hai raggiunto il numero massimo di Tag inseribili";

                ////TEMPORARY MUST BE FILLED!!!
                //asRecipeTags.preFillValue = "";

            }
            else
            {
                CreateDynamicRecipeProperty();
            }
        }

        protected void GetDataFromDatabase()
        {
            try
            {
                _recipe = new RecipeLanguage(new Guid(Request.QueryString["IDRecipe"].ToString()), IDLanguage);
                ariIngredient.IDRecipe = Request.QueryString["IDRecipe"].ToString();
            }
            catch
            {
                Response.Redirect("RecipeDashBoard.aspx", true);
            }

            try
            {
                rptRecipeBeverage.DataSource = BeverageRecipe.GetBeverageForRecipe(new Guid(Request.QueryString["IDRecipe"].ToString()));
                rptRecipeBeverage.DataBind();
            }
            catch
            {
            }
            //If selected ingredient not exist
            if (_recipe == new Guid() || _recipe == null)
            {
                Response.Redirect("RecipeDashBoard.aspx", true);
            }

            try
            {
                hfIDRecipe.Value = _recipe.IDRecipe.ToString();
                _recipe.QueryRecipeInfo();
                _recipe.QueryRecipeLanguageInfo();
                //_recipe.GetRecipeIngredients();
                _recipe.GetRecipePropertiesValue();
                _recipe.GetRecipeSteps();

                if (_recipe.RecipeImage != null && _recipe.RecipeImage.IDMedia!= new Guid())
                {
                    upshImgRecipe.IDMedia = _recipe.RecipeImage;
                    //_recipe.RecipeImage.QueryMediaInfo();
                    //upshImgRecipe.IDMediaOwner =_recipe.RecipeImage.MediaOwner;
                }
            }
            catch (Exception ex)
            {
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error load Recipe Info", ex.Message, "Recipe: " + _recipe.RecipeName, false, true);
                LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
            }

            #region RecipeFather
            //try
            //{
            //    ddlRecipeFather.DataSource = _recipe.FindRecipeFatherByRecipeNameContains();
            //    ddlRecipeFather.DataValueField = "IDRecipe";
            //    ddlRecipeFather.DataTextField = "RecipeName";
            //    ddlRecipeFather.DataBind();
                

            //    if (_recipe.RecipeFather != null && _recipe.RecipeFather.IDRecipe != new Guid())
            //    {
            //        ddlRecipeFather.Items.FindByValue(_recipe.RecipeFather.IDRecipe.ToString()).Selected = true;
            //    }
            //    else
            //    {
            //        ddlRecipeFather.Items.FindByValue("00000000-0000-0000-0000-000000000000").Selected = true;
            //    }
            //}
            //catch
            //{
            //    try
            //    {
            //        ddlRecipeFather.Items.FindByValue("00000000-0000-0000-0000-000000000000").Selected = true;
            //    }
            //    catch
            //    {
            //    }
            //}
            #endregion

            #region RecipeVote
            string readOnly = "false";
            try
            {
                //RecipeVote _vote = new RecipeVote(_recipe.IDRecipe, new Guid(Session["IDUser"].ToString()));
                //if (_vote.RecipeVoteValue > 0)
                //{
                //    readOnly = "true";
                //}
            }
            catch
            {
            }

            rtgRecipe1.StartScore = _recipe.AvgRecipeRating().ToString();
            rtgRecipe1.ImageOffPath = "/Images/Rating/star-off.png";
            rtgRecipe1.ImageOnPath = "/Images/Rating/star-on.png";
            rtgRecipe1.ImageHalfPath = "/Images/Rating/star-half.png";
            rtgRecipe1.StarNumber = "5";
            rtgRecipe1.ReadOnly = readOnly;
            rtgRecipe1.EnableCancel = "false";
            rtgRecipe1.CancelImageOffPath = "/Images/Rating/cancel-off.png";
            rtgRecipe1.CancelImageOnPath = "/Images/Rating/cancel-on.png";
            rtgRecipe1.Width = "150";
            rtgRecipe1.RatingWebMethodPath = "/Recipe/ManageRecipe.asmx/VoteRecipe";
            rtgRecipe1.IDObjectToRate = _recipe.IDRecipe.ToString();
            rtgRecipe1.IDUserVoter = Session["IDUser"].ToString();
            rtgRecipe1.MessageOnError = "Qualcosa non ha vunzionato durante il salvataggio del voto, prova più tardi.";
            rtgRecipe1.StartRaty();
            #endregion

            try
            {
                ddlRecipeDifficulties.DataSource = RecipeInfo.GetAllRecipeDifficultiesLang(2);
                ddlRecipeDifficulties.DataValueField = "value";
                ddlRecipeDifficulties.DataTextField = "viewText";
                ddlRecipeDifficulties.DataBind();

                ddlRecipeDifficulties.Items.FindByValue(((int)_recipe.RecipeDifficulties).ToString()).Selected=true;
            }
            catch
            {
            }

            try
            {
                Page.Title = _recipe.RecipeName;
                lblRecipeTitleValue.Text = _recipe.RecipeName;
                txtRecipeName.Text = _recipe.RecipeName;
                txtCookingTime.Text = _recipe.CookingTimeMinute.ToString();
                txtNumberOfPerson.Text = _recipe.NumberOfPerson.ToString();
                txtPreparationTime.Text = _recipe.PreparationTimeMinute.ToString();
                txtRecipeHistory.Text = _recipe.RecipeHistory;
                txtRecipeNote.Text = _recipe.RecipeNote;
                txtRecipeSuggestion.Text = _recipe.RecipeSuggestion;
                chkBaseRecipe.Checked = _recipe.BaseRecipe;
                chkRecipeEnabled.Checked = _recipe.RecipeEnabled;
                txtRecipeStep.Text = _recipe.RecipeSteps[0].Step;
                chkHotSpicy.Checked = MyConvert.ToBoolean(_recipe.HotSpicy,false);
            }
            catch
            {
            }

            CreateDynamicRecipeProperty();

            GetRecipeIngredient();
        }

        protected void GetRecipeIngredient()
        {
            RecipeLanguage _recipe = new RecipeLanguage(new Guid(Request.QueryString["IDRecipe"].ToString()), IDLanguage);
            _recipe.GetRecipeIngredients();

            repRecipeIngredient.DataSource = _recipe.RecipeIngredients;
            repRecipeIngredient.DataBind();
        }

        protected void btnAddBeverageRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                RecipeLanguage _recipe = new RecipeLanguage(new Guid(Request.QueryString["IDRecipe"].ToString()), 2);

                BeverageRecipe brNew = new BeverageRecipe(_recipe.IDRecipe, new Guid(acBeverage.SelectedObjectID), new Guid(Request.QueryString["IDRecipe"].ToString()));
                brNew.Save();

                rptRecipeBeverage.DataSource = BeverageRecipe.GetBeverageForRecipe(new Guid(Request.QueryString["IDRecipe"].ToString()));
                rptRecipeBeverage.DataBind();

                acBeverage.Clear();
                acBeverage.StatAutoComplete();
            }
            catch
            {
                acBeverage.Clear();
                acBeverage.StatAutoComplete();
            }
        }

        protected void CreateDynamicRecipeProperty()
        {
            foreach (RecipePropertyType recipePropType in RecipePropertyType.GetAllRecipePropertyTypeList(IDLanguage))
            {
                if (recipePropType.Enabled)
                {
                    Panel _panelContainer = new Panel();
                    _panelContainer.ID = "pnlContainerRecipePropType" + recipePropType.IDRecipePropertyType.ToString();
                    _panelContainer.CssClass = "pnlPropTypeContainer";

                    Label _newLabel = new Label();
                    Label _newLabelDesc = new Label();
                    string _baseValue = "";
                    Panel _panelLeft = new Panel();
                    _panelLeft.ID = "pnlLeftRecipePropType" + recipePropType.IDRecipePropertyType.ToString();
                    _panelLeft.CssClass = "pnlTableCol";

                    Panel _panelRight = new Panel();
                    _panelRight.ID = "pnlRightRecipePropType" + recipePropType.IDRecipePropertyType.ToString();
                    _panelRight.CssClass = "pnlTableCol";
                    _panelRight.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                    _newLabel.ID = "lblRecipePropType" + recipePropType.IDRecipePropertyType.ToString();
                    _newLabel.CssClass = "IngredientInfoFieldTitle";
                    _newLabel.Text = recipePropType.RecipePropType;

                    //_newLabelDesc.ID = "lblRecipePropTypeDesc" + recipePropType.IDRecipePropertyType.ToString();
                    //_newLabelDesc.CssClass = "IngredientInfoFieldTitle";
                    //_newLabelDesc.Width = 300;
                    //switch (recipePropType.IDRecipePropertyType)
                    //{
                    //    case 1:
                    //        _newLabelDesc.Text = "Bevande, Antipasto, Secondo, Salsa, Dessert, Primo, Contorno, Piatto Unico, Torta, Al cucchiaio, Rosticceria, Zuppa";
                    //        break;
                    //    case 4:
                    //        _newLabelDesc.Text = "Lesso, Al Vapore, In Tegame, Fritto, Al Forno, Alla Brace, Al Cartoccio, In padella, Al dente, Al sangue, Ben cotto, Media cottura, Scottato, In pentola, Gratinato, Arrosto, Affumicato, A bagnomaria, Brasato, Saltato, Stufato";
                    //        break;
                    //    case 5:
                    //        _newLabelDesc.Text = "Cucchiaio, Cucchiaino, Forchetta, Forchettina, Finger Food, Bacchette, Stuzzicadenti";
                    //        break;
                    //}
                    //AutoSuggestMultiValue _newAutoSuggest = (AutoSuggestMultiValue)Page.LoadControl("~/CustomControls/AutoSuggestMultiValue.ascx");
                    //_newAutoSuggest.ID = "asRecipeProp" + recipePropType.IDRecipePropertyType.ToString();

                    //_newAutoSuggest.WebServiceURL = "/Recipe/GetRecipeInfo.asmx/GetRecipePropertiesValues";
                    //_newAutoSuggest.SelectedItemProp = "RecipeProp";
                    //_newAutoSuggest.SearchObjProps = "RecipeProp";
                    //_newAutoSuggest.SelectedValuesProp = "IDRecipeProperty";
                    //_newAutoSuggest.QueryParam = "PropertyStartWord";
                    //_newAutoSuggest.QueryIDLangParam = "IDLanguage";
                    //_newAutoSuggest.QueryIDLangValue = "2";
                    //_newAutoSuggest.MinChars = "2";
                    //_newAutoSuggest.MaxAllowedValues = "4";
                    //_newAutoSuggest.StartText = recipePropType.RecipePropType;
                    //_newAutoSuggest.OtherQueryParameter = ",\\'IDRecipePropertyType\\':\\'" + recipePropType.IDRecipePropertyType.ToString()+"\\'";
                    //_newAutoSuggest.EmptyText = "Nessun valore trovato con queste iniziali";
                    //_newAutoSuggest.LimitAllowedValuesText = "Hai raggiunto il numero massimo di valori Inseribili";



                    foreach (RecipeProperty recipeProp in RecipeProperty.GetAllRecipePropertyListByType(recipePropType.IDRecipePropertyType, IDLanguage))
                    {
                        try
                        {
                            CheckBox _chkDynPrpValue = new CheckBox();
                            
                            _chkDynPrpValue.ID = "chk_propVal" + recipeProp.IDRecipeProperty.ToString();
                            _chkDynPrpValue.Text = recipeProp.RecipeProp;
                            _chkDynPrpValue.CssClass = "chkDynPrpValue";
                            _chkDynPrpValue.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            try
                            {
                                foreach (RecipePropertyValue recipePropValue in _recipe.PropertyValue)
                                {
                                    if (recipePropValue.IDRecipeProp == recipeProp && recipePropValue.Value)
                                    {
                                        //_baseValue += "{ IDRecipeProperty: \"" + recipeProp.IDRecipeProperty + "\", RecipeProp: \"" + recipeProp.RecipeProp + "\" },";
                                        _chkDynPrpValue.Checked = true;
                                    }
                                }
                            }
                            catch
                            {
                            }
                            _panelRight.Controls.Add(_chkDynPrpValue);
                        }
                        catch
                        {
                        }
                    }
                    //if (!String.IsNullOrEmpty(_baseValue))
                    //{
                    //    _baseValue = _baseValue.Substring(0, _baseValue.Length - 1);
                    //    _newAutoSuggest.preFillValue = _baseValue;
                    //}
                    _panelLeft.Controls.Add(_newLabel);
                    
                    //_panelRight.Controls.Add(_newAutoSuggest);
                   
                    _panelContainer.Controls.Add(_panelLeft);
                    _panelContainer.Controls.Add(_panelRight);
                    upnlDynamicProperty.ContentTemplateContainer.Controls.Add(_panelContainer);
                }
            }
        }

        protected void ariIngredient_IngredientAdded(object sender, EventArgs e)
        {
            GetRecipeIngredient();
            ariIngredient.Clear();
            ariIngredient.StatAutoComplete();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int _saveErrorCount = 0;
            MyUser _User = null;
            try
            {
                _recipe = new RecipeLanguage(new Guid(Request.QueryString["IDRecipe"].ToString()), IDLanguage);
                _recipe.QueryRecipeInfo();
                _recipe.QueryRecipeLanguageInfo();
                _recipe.GetRecipeSteps();
                _recipe.GetRecipeIngredients();
                _User = new MyUser(new Guid(Session["IDUser"].ToString()));
            }
            catch
            {
               
            }
            
           

            if (_recipe != null && _recipe.IDRecipe != new Guid())
            {
                #region SaveDynamicPropertyValues

                try
                {
                    //Clear old properties before get the new one
                    ManageUSPReturnValue _clearResult = _recipe.ClearRecipePropertiesVales();

                    if (!_clearResult.IsError)
                    {
                        CheckBox _chkDynPrpValue = new CheckBox();

                        //string _controlReturnProperty = "";
                        //string[] _controlReturnPropertyArray;
                        ManageUSPReturnValue _propSaveResult;
                        foreach (RecipePropertyType recipePropType in RecipePropertyType.GetAllRecipePropertyTypeList(IDLanguage))
                        {
                            if (recipePropType.Enabled)
                            {
                                foreach (RecipeProperty recipeProp in RecipeProperty.GetAllRecipePropertyListByType(recipePropType.IDRecipePropertyType, IDLanguage))
                                {
                                    try
                                    {
                                        _chkDynPrpValue = (CheckBox)upnlDynamicProperty.ContentTemplateContainer.FindControl("pnlRightRecipePropType" + recipePropType.IDRecipePropertyType.ToString()).FindControl("chk_propVal" + recipeProp.IDRecipeProperty.ToString());

                                        //if (!String.IsNullOrEmpty(_newAutoSuggest.ReturnValue))
                                        //{
                                        //    _controlReturnProperty = _newAutoSuggest.ReturnValue;
                                        //    _controlReturnProperty = _controlReturnProperty.Replace(",", "     ").Trim().Replace("     ", ",");
                                        //    _controlReturnPropertyArray = _controlReturnProperty.Split(',');

                                        //    foreach (string _string in _controlReturnPropertyArray)
                                        //    {
                                        //        int _int = MyConvert.ToInt32(_string, 0);
                                        //        RecipeProperty _recipeProp = new RecipeProperty(_int, IDLanguage);
                                        //        RecipePropertyValue _propValue = new RecipePropertyValue(_recipe.IDRecipe, _recipeProp, true);
                                        //        _propSaveResult = _propValue.Save();
                                        //        if (_propSaveResult.IsError)
                                        //        {
                                        //            _saveErrorCount++;
                                        //        }
                                        //    }
                                        //}
                                        if (_chkDynPrpValue.Checked)
                                        {
                                            RecipePropertyValue _propValue = new RecipePropertyValue(_recipe.IDRecipe, recipeProp, true);
                                            _propSaveResult = _propValue.Save();
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }

                            }
                        }
                    }
                    _recipe.GetRecipePropertiesValue();
                }
                catch (Exception ex)
                {
                    _saveErrorCount++;
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error save recipe Dynamic properties", ex.Message, "IDRecipe: " + _recipe.IDRecipe.ToString(), true, false);
                        LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    }
                    catch
                    {
                    }
                }
                #endregion

                #region SaveRecipeSteps
                try
                {
                    _recipe.RecipeSteps[0].Step = txtRecipeStep.Text;
                   ManageUSPReturnValue _result = _recipe.RecipeSteps[0].Save();
                }
                catch(Exception ex)
                {
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Unable to save recipe step", ex.Message, "IDRecipe: " + _recipe.IDRecipe.ToString() , true, false);
                        LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    }
                    catch
                    {
                    }
                }
                #endregion

                #region CalculateTagInfoWithRecipeAI
                #endregion

                #region SaveRecipeData
                string stgResult = "";
                
                try
                {
                    _recipe.RecipeName=txtRecipeName.Text;
                    _recipe.RecipeLanguageAutoTranslate=false;
                    _recipe.RecipeHistory=txtRecipeHistory.Text;
                    _recipe.RecipeHistoryDate=null;
                    _recipe.RecipeNote=txtRecipeNote.Text;
                    _recipe.RecipeSuggestion=txtRecipeSuggestion.Text;
                    _recipe.RecipeDisabled=false;
                    _recipe.RecipeFather=null;
                    _recipe.Owner = _User.IDUser;
                    _recipe.NumberOfPerson=MyConvert.ToInt32(txtNumberOfPerson.Text,4);
                    _recipe.PreparationTimeMinute=MyConvert.ToInt32(txtPreparationTime.Text,20);
                    _recipe.CookingTimeMinute=MyConvert.ToInt32(txtCookingTime.Text,20); 
                    _recipe.RecipeDifficulties=(RecipeInfo.Difficulties)MyConvert.ToInt32(ddlRecipeDifficulties.SelectedValue,2);
                    _recipe.RecipeImage=upshImgRecipe.IDMedia;
                    _recipe.RecipeVideo=null;
                    _recipe.IDCity=MyConvert.ToInt32(txtRecipeOrigin.Text,0);
                    _recipe.LastUpdate=DateTime.UtcNow;
                    _recipe.UpdatedByUser=new Guid(Session["IDUser"].ToString());
                    _recipe.RecipeConsulted++;
                    _recipe.isStarterRecipe=true;
                    _recipe.Draft = false;
                    _recipe.DeletedOn=null;
                    _recipe.BaseRecipe=chkBaseRecipe.Checked;
                    _recipe.RecipeEnabled=chkRecipeEnabled.Checked;
                    _recipe.HotSpicy = chkHotSpicy.Checked;
                    _recipe.OriginalVersion = true;
                    //IMPOSTARE A TRUE
                    _recipe.Checked=true;
                    Dictionary<string, string> CalcValue=null;
                    try
                    {
                        CalcValue = RecipeAI.CalculateRecipeNutritionalFacts(_recipe.IDRecipe);
                        _recipe.RecipePortionKcal = MyConvert.ToDouble(CalcValue["TotRecipeKcal"].ToString(), 0);
                        _recipe.RecipePortionProteins = MyConvert.ToDouble(CalcValue["TotRecipeProteins"].ToString(), 0);
                        _recipe.RecipePortionFats = MyConvert.ToDouble(CalcValue["TotRecipeFats"].ToString(), 0);
                        _recipe.RecipePortionCarbohydrates = MyConvert.ToDouble(CalcValue["TotRecipeCarbohydrates"].ToString(), 0);
                        _recipe.RecipePortionAlcohol = MyConvert.ToDouble(CalcValue["TotRecipeAlcohol"].ToString(), 0);
                        _recipe.RecipePortionQta = MyConvert.ToDouble(CalcValue["TotQta"].ToString(), 0);
                        _recipe.Vegetarian = MyConvert.ToBoolean(CalcValue["Vegetarian"], false);
                        _recipe.Vegan = MyConvert.ToBoolean(CalcValue["Vegan"], false);
                        _recipe.GlutenFree = MyConvert.ToBoolean(CalcValue["GlutenFree"], false);
                    }
                    catch
                    {
                    }

                    try
                    {
                         _recipe.RecipeLanguageTags=RecipeAI.CalculateRecipeTags(_recipe.IDRecipe,IDLanguage,
                                                                           MyConvert.ToBoolean(AppConfig.GetValue("IncludeIngredientList", AppDomain.CurrentDomain), true),
                                                                           MyConvert.ToBoolean(AppConfig.GetValue("IncludeDynamicProp", AppDomain.CurrentDomain), true),
                                                                           MyConvert.ToBoolean(AppConfig.GetValue("IncludeIngredientCategory", AppDomain.CurrentDomain), false));
                        RecipeLanguageTag _tag;
                        if (MyConvert.ToDouble(CalcValue["TotPercKalFromFat"], 0) > MyConvert.ToDouble(AppConfig.GetValue("FatRecipeThreshold", AppDomain.CurrentDomain), 0))
                        {
                            _tag = new RecipeLanguageTag(MyConvert.ToInt32(AppConfig.GetValue("FatRecipeTagID", AppDomain.CurrentDomain), 0), IDLanguage);
                        }
                        else
                        {
                            _tag = new RecipeLanguageTag(MyConvert.ToInt32(AppConfig.GetValue("NoFatRecipeTagID", AppDomain.CurrentDomain), 0), IDLanguage);
                        }
                        _tag.QueryDBInfo();
                        _recipe.RecipeLanguageTags += " ," + _tag.Tag;

                    }
                    catch
                    {
                    }
                    try
                    {
                        _recipe.RecipeCompletePerc = _recipe.PercentageComplete();
                    }
                    catch
                    {
                        _recipe.RecipeCompletePerc = 0;
                    }
                   ManageUSPReturnValue _result = _recipe.Save();

                   if (_result.IsError)
                   {
                       throw new System.ArgumentException(_result.ResultExecutionCode);
                   }
                   else
                   {
                       try
                       {
                           if (_recipe.RecipeEnabled)
                           {
                               //INSERT ACTION IN USER BOARD
                               UserBoard NewActionInUserBoard = new UserBoard(_recipe.UpdatedByUser.IDUser, null, ActionTypes.NewRecipe, _recipe.IDRecipe, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                               ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();

                              
                           }
                           stgResult = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-IN-0003");
                       }
                       catch
                       {
                       }
                   }
                }
                catch(Exception ex)
                {
                    try
                    {
                        stgResult = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-ER-0007");
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error saving recipe on DB", ex.Message, "IDRecipe: " + _recipe.IDRecipe.ToString(), true, false);
                        LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    }
                    catch
                    {
                    }
                }
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + txtRecipeName.Text.Replace("'", "\\'") + "','" + stgResult + "');", true);

                #endregion

            }
            else
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Unable to save recipe, data not availbe", "", "IDRecipe: " + Request.QueryString["IDRecipe"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                }
                catch
                {
                }
            }

            //try
            //{
            //    //INSERT ACTION IN USER BOARD
            //    UserBoard NewActionInUserBoard = new UserBoard(_User.IDUser, null, ActionTypes.NewRecipe, _recipe.IDRecipe, null, null, DateTime.UtcNow, IDLanguage);
            //    ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();
            //}
            //catch
            //{
            //}
        }


    }
}