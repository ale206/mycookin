using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.CityManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.SocialAction;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.ObjectManager.MyUserNotificationManager;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.Log;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.AI.Recipe;
using System.Web.UI.HtmlControls;
using MyCookinWeb.CustomControls;

namespace MyCookinWeb.RecipeWeb
{
    public partial class EditRecipes : MyCookinWeb.Form.MyPageBase
    {
        int IDLanguage;
        RecipeLanguage _recipe;
        string _idRecipe;
        string _idUser;
        bool _continueLoad = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            Form.DefaultButton = btnSaveRecipe.UniqueID;
            //This avoid issue on routed pages
            Page.Form.Action = Request.RawUrl;

            ibtnDelete.OnClientClick = GetLocalResourceObject("ibtnDelete.OnClientClick").ToString();
            hfRecipeInDraft.Value = GetLocalResourceObject("hfRecipeInDraft.Value").ToString();
            hfPublicRecipe.Value = GetLocalResourceObject("hfPublicRecipe.Value").ToString();

            try
            {
                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                hfIDLanguage.Value = IDLanguage.ToString();
                try
                {
                    hfRecipePercentageCompleteBase.Value = GetLocalResourceObject("hfRecipePercentageCompleteBase.Value").ToString();
                }
                catch
                {
                }

                acBeverage.MethodName = "/Beverage/SearchBeverage.asmx/SearchBeverages";
                acBeverage.LanguageCode = IDLanguage.ToString();
                acBeverage.ObjectLabelIdentifier = "IngredientSingular";
                acBeverage.ObjectIDIdentifier = "IDIngredient";
                acBeverage.ObjectLabelText = "";
                acBeverage.LangFieldLabel = "IDLanguage";
                acBeverage.WordFieldLabel = "words";
                acBeverage.MinLenght = "2";

                //Inizialize Uploader
                MediaUploadConfig _uploadConfigStep = new MediaUploadConfig(MediaType.RecipeStepPhoto);
                multiupSteps.SelectFilesText = GetLocalResourceObject("SelectFilesText.Text").ToString();
                multiupSteps.UploadFilesText = "";
                multiupSteps.UploadConfig = MediaType.RecipeStepPhoto;
                multiupSteps.UploadHandlerURL = "/Utilities/MultiUploadImageHandler.ashx";
                multiupSteps.MaxFileNumber = 3;
                multiupSteps.MaxFileSizeInMB = _uploadConfigStep.MaxSizeMegaByte;
                multiupSteps.AllowedFileTypes = "jpg,jpeg,png";
               
                multiupSteps.LoadControl = true;

                //Inizialize Uploader
                MediaUploadConfig _uploadConfig = new MediaUploadConfig(MediaType.RecipePhoto);
                multiup.SelectFilesText = GetLocalResourceObject("ChangeRecipePhoto.Text").ToString(); ;
                multiup.UploadFilesText = "";
                multiup.UploadConfig = MediaType.RecipePhoto;
                multiup.UploadHandlerURL = "/Utilities/MultiUploadImageHandler.ashx";
                multiup.MaxFileNumber = 1;
                multiup.MaxFileSizeInMB = _uploadConfig.MaxSizeMegaByte;
                multiup.AllowedFileTypes = "jpg,jpeg,png";
                
                multiup.LoadControl = true;
                //==============================
                //Inizialize AddRecipeIngredient Control
                ariIngredient.MethodName = "/Ingredient/SearchIngredient.asmx/SearchIngredients";
                ariIngredient.LanguageCode = IDLanguage.ToString();
                ariIngredient.ObjectLabelIdentifier = "IngredientSingular";
                ariIngredient.ObjectIDIdentifier = "IDIngredient";
                ariIngredient.ObjectLabelText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1), "RC-IN-0009");
                ariIngredient.LangFieldLabel = "IDLanguage";
                ariIngredient.WordFieldLabel = "words";
                ariIngredient.MinLenght = "2";
                ariIngredient.StatAutoComplete();
                //==============================
                //_idRecipe = "8803a450-021f-465b-b95f-9def5664569d";
                //_idUser = "8aa259a3-b131-4c48-b065-b10851c0765b";

                //If id parameter for page not exist
                if (Request.QueryString["IDRecipe"] == null)
                {
                    Response.Redirect("/recipemng/createrecipe.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    _continueLoad = false;
                }
                else
                {
                    _idRecipe = Request.QueryString["IDRecipe"].ToString();
                    _idUser = Session["IDUser"].ToString();
                    multiup.IDMediaOwner = _idUser;
                    multiupSteps.IDMediaOwner = _idUser;
                }

                if (!IsPostBack)
                {
                    /*Check Authorization to Visualize this Page
                     * If not authorized, redirect to login.
                    //*****************************************/
                    PageSecurity SecurityPage = new PageSecurity(Session["IDUser"].ToString(), Network.GetCurrentPageName());

                    if (!SecurityPage.CheckAuthorization())
                    {
                        Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl()).ToLower(), false);
                        Context.ApplicationInstance.CompleteRequest();
                        _continueLoad = false;
                    }
                    //******************************************

                    GetRecipeBasicInfo();
                    if (_continueLoad)
                    {
                        GetDataFromDatabase();
                        Page.Form.DefaultButton = null;
                    }
                }
                else
                {
                    CreateDynamicRecipeProperty();
                }
                if (_continueLoad)
                {
                    CreateSortableSteps();
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in Loading Page EditRecipe: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void CreateSortableSteps()
        {
            try
            {
                sortableStep.Controls.Clear();
                if (_recipe == null)
                {
                    _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                    _recipe.QueryRecipeLanguageInfo();
                }
                _recipe.GetRecipeSteps();
                if (_recipe.RecipeSteps.Length >= MyConvert.ToInt32(AppConfig.GetValue("MaxRecipeStepsCount", AppDomain.CurrentDomain), 15))
                {
                    multiupSteps.Visible = false;
                    btnAddNoPhotoStep.Visible = false;
                }
                else
                {
                    multiupSteps.Visible = true;
                    btnAddNoPhotoStep.Visible = true;
                }
                foreach (RecipeStep _step in _recipe.RecipeSteps)
                {
                    try
                    {
                        Panel _pnlImageStep = new Panel();
                        _pnlImageStep.ID = "pnlStep" + _step.IDRecipeStep;
                        _pnlImageStep.CssClass = "pnlEditStepWithPhoto";

                        Panel _pnlEditImageStep = new Panel();
                        _pnlEditImageStep.ID = "pnlEditImageStep" + _step.IDRecipeStep;
                        _pnlEditImageStep.CssClass = "imgEditStepPnl";

                        Image _imgMove = new Image();
                        _imgMove.ID = "imgMove" + _step.IDRecipeStep;
                        _imgMove.CssClass = "imgMove ui-icon ui-icon-arrowthick-2-n-s MyTooltip";
                        _imgMove.ImageUrl = "/Images/icon-drag-and-drop.png";
                        _imgMove.ToolTip = GetLocalResourceObject("DragAndDrop.Tooltip").ToString();

                        ImageButton _btnDeleteStep = new ImageButton();
                        _btnDeleteStep.ID = "btnDeleteStep_" + _step.IDRecipeStep;
                        _btnDeleteStep.ImageUrl = "/Images/deleteX.png";
                        _btnDeleteStep.CssClass = "btnDeleteStep";
                        _btnDeleteStep.CommandArgument = _step.IDRecipeStep.ToString();
                        _btnDeleteStep.Click += new ImageClickEventHandler(btnDeleteStep_Click);
                        _btnDeleteStep.OnClientClick = GetLocalResourceObject("DeleteStep.OnClientClick").ToString();

                        var _li = new HtmlGenericControl("li");
                        _li.Attributes.Add("class", "ui-state-default");
                        _li.ID = "liStep" + _step.IDRecipeStep.ToString();
                        _li.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                        TextBox _txtStep = new TextBox();
                        _txtStep.ID = "txtStep" + _step.IDRecipeStep;
                        _txtStep.CssClass = "txtStepEditDesc";
                        _txtStep.Text = _step.Step;
                        _txtStep.Width = 400;
                        _txtStep.Height = 150;
                        _txtStep.TextMode = TextBoxMode.MultiLine;
                        _txtStep.Attributes.Add("onkeypress", "return isSpecialHTMLChar(event)");

                        if (_recipe.RecipeSteps.Length == 1)
                        {
                            _btnDeleteStep.Visible = false;
                            _imgMove.Visible = false;
                            _txtStep.Width = 610;
                        }
                        if (_step.IDRecipeStepImage == null)
                        {
                            _pnlEditImageStep.Visible = false;
                            _txtStep.Width = 560;
                        }

                        ctrlEditImage _imgStepEdit = (ctrlEditImage)LoadControl("~/CustomControls/ctrlEditImage.ascx");

                        if (_step.IDRecipeStepImage != null)
                        {
                            _imgStepEdit.ID = "imgStep" + _step.IDRecipeStep;
                            _imgStepEdit.ImageCssClass = "imgEditStep";
                            _imgStepEdit.IDMedia = _step.IDRecipeStepImage.IDMedia.ToString();
                            _imgStepEdit.MediaType = MediaType.RecipeStepPhoto;
                            _imgStepEdit.EnableEditing = true;
                            _imgStepEdit.Visible = true;
                            _imgStepEdit.ImageWidth = 150;
                            _imgStepEdit.ImageHeight = 150;
                        }

                        Panel _pnlSeparator = new Panel();
                        _pnlSeparator.ID = "pnlSeparato" + _step.IDRecipeStep;
                        _pnlSeparator.CssClass = "pnlStepSeparator";


                        _pnlEditImageStep.Controls.Add(_imgStepEdit);
                        _pnlImageStep.Controls.Add(_imgMove);
                        _pnlImageStep.Controls.Add(_pnlEditImageStep);
                        _pnlImageStep.Controls.Add(_txtStep);
                        _pnlImageStep.Controls.Add(_btnDeleteStep);
                        _pnlImageStep.Controls.Add(_pnlSeparator);
                        _li.Controls.Add(_pnlImageStep);
                        sortableStep.Controls.Add(_li);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        protected void btnDeleteStep_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton _btn = (ImageButton)sender;
                RecipeStep _step = new RecipeStep(new Guid(_btn.CommandArgument));
                _step.Delete();
                try
                {
                    if (_step.IDRecipeStepImage != null)
                    {
                        _step.IDRecipeStepImage.DeletePhoto();
                    }
                }
                catch
                {
                }
                _btn.Parent.Parent.Visible = false;
                if (_recipe == null)
                {
                    _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                    _recipe.QueryRecipeInfo();
                    _recipe.QueryRecipeLanguageInfo();
                }
                _recipe.GetRecipeSteps();
                CreateSortableSteps();
                if (_recipe.RecipeSteps.Length >= MyConvert.ToInt32(AppConfig.GetValue("MaxRecipeStepsCount", AppDomain.CurrentDomain), 15))
                {
                    multiupSteps.Visible = false;
                    btnAddNoPhotoStep.Visible = false;
                }
                else
                {
                    multiupSteps.Visible = true;
                    btnAddNoPhotoStep.Visible = true;
                }
            }
            catch
            {
            }
        }

        protected void btnAddNoPhotoStep_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSteps();
                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                _recipe.QueryRecipeInfo();
                _recipe.QueryRecipeLanguageInfo();
                _recipe.GetRecipeSteps();
                RecipeStep _step = new RecipeStep(_recipe.IDRecipeLanguage, _recipe.RecipeSteps.Length + 1, "", new Photo(new Guid()));
                _step.Save();
                CreateSortableSteps();
            }
            catch
            {
            }
        }

        protected void SaveSteps()
        {
            TextBox _recipeStep = new TextBox();
            //_recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
            //_recipe.QueryRecipeLanguageInfo();
            _recipe.GetRecipeSteps();
            string[] _stepsOrder = hfStepsOrder.Value.Split(',');

            foreach (RecipeStep _step in _recipe.RecipeSteps)
            {
                //_recipeStep = (TextBox)UpdatePanel2.ContentTemplateContainer.FindControl("txtStep" + _step.IDRecipeStep);
                _recipeStep = (TextBox)sortableStep.FindControl("liStep" + _step.IDRecipeStep.ToString()).FindControl("txtStep" + _step.IDRecipeStep);
                _step.Step = _recipeStep.Text;
                if (!String.IsNullOrEmpty(hfStepsOrder.Value))
                {
                    _step.StepNumber = Array.IndexOf(_stepsOrder, "liStep" + _step.IDRecipeStep.ToString()) + 1;
                }
                _step.Save();
            }
        }

        protected void StepsUploaded(object sender, EventArgs e)
        {
            try
            {
                SaveSteps();
                _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                _recipe.QueryRecipeLanguageInfo();
                _recipe.GetRecipeSteps();

                string[] _imageCreated = multiupSteps.MediaCreatedIDs.Substring(0, multiupSteps.MediaCreatedIDs.Length - 1).Split(',');
                int _numstep = _recipe.RecipeSteps.Length + 1;

                if (_imageCreated.Length > 0)
                {
                    for (int i = 0; i < _imageCreated.Length; i++)
                    {
                        try
                        {
                            _numstep++;
                            Guid _photo = new Guid(_imageCreated[i]);
                            RecipeStep _step = new RecipeStep(_recipe.IDRecipeLanguage, _numstep, "", new Photo(_photo));
                            _step.Save();
                        }
                        catch
                        {
                            _numstep--;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            multiupSteps.Clear();
            multiupSteps.ResetMultiUpload();
            CreateSortableSteps();
        }

        protected void GetRecipeBasicInfo()
        {
            try
            {
                _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                ariIngredient.IDRecipe = _idRecipe;
            }
            catch
            {
                Response.Redirect("/recipemng/createrecipe.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                _continueLoad = false;
            }
            
            //If selected ingredient not exist
            if (_recipe == new Guid() || _recipe == null)
            {
                Response.Redirect("/recipemng/createrecipe.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                _continueLoad = false;
            }
            _recipe.QueryRecipeInfo();
           
            bool _adminToken = false;
            try
            {
                if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("292d13f2-738f-487b-b739-96c52b9e8d21") >= 0)
                {
                    _adminToken = true;
                }
            }
            catch
            {
                _adminToken = false;
            }
            if (!_adminToken)
            {
                if (_recipe.Owner.IDUser.ToString() != _idUser)
                {
                    Response.Redirect("/recipemng/createrecipe.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    _continueLoad = false;
                }
            }
        }

        protected void GetDataFromDatabase()
        {
            try
            {
                try
                {
                    _recipe.QueryRecipeLanguageInfo();
                    _recipe.GetRecipePropertiesValue();
                    //_recipe.GetRecipeSteps();

                    multiup.BaseFileName = _recipe.RecipeName;
                    multiupSteps.BaseFileName = _recipe.RecipeName + "_Step";

                    multiup.IDMediaOwner = _idUser;
                    multiupSteps.IDMediaOwner = _idUser;

                    //multiup.ResetMultiUpload();
                    //multiupSteps.ResetMultiUpload();

                    if (_recipe.RecipeImage != null && _recipe.RecipeImage.IDMedia != new Guid())
                    {
                        _recipe.RecipeImage.QueryMediaInfo();
                        imgRecipe.IDMedia = _recipe.RecipeImage.IDMedia.ToString();
                        //image.MediaSizeType = MediaSizeTypes.Small;
                        imgRecipe.MediaType = MediaType.RecipePhoto;
                        imgRecipe.EnableEditing = true;
                        imgRecipe.Visible = true;
                        imgRecipe.ImageWidth = 150;
                        imgRecipe.ImageHeight = 150;
                        imgNoPhoto.Visible = false;
                    }
                    else
                    {
                        imgRecipe.EnableEditing = false;
                        imgRecipe.Visible = false;
                        imgNoPhoto.Visible = true;
                        imgNoPhoto.ImageUrl = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                    }
                }
                catch (Exception ex)
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error load Recipe Info", ex.Message, "Recipe: " + _recipe.RecipeName, false, true);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }

                try
                {
                    ddlRecipeDifficulties.DataSource = RecipeInfo.GetAllRecipeDifficultiesLang(2);
                    ddlRecipeDifficulties.DataValueField = "value";
                    ddlRecipeDifficulties.DataTextField = "viewText";
                    ddlRecipeDifficulties.DataBind();

                    ddlRecipeDifficulties.Items.FindByValue(((int)_recipe.RecipeDifficulties).ToString()).Selected = true;
                }
                catch
                {
                }

                try
                {
                    lblRecipeName.Text = _recipe.RecipeName;
                    lnkRecipeName.Text = _recipe.RecipeName;
                    lnkRecipeName.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + _recipe.RecipeName.Replace(" ","-") + "/" + _recipe.IDRecipe.ToString()).ToLower();

                    lblRecipeCompletePerc.Text = hfRecipePercentageCompleteBase.Value.Replace("{0}", _recipe.RecipeCompletePerc.ToString());
                    Page.Title = _recipe.RecipeName;
                    txtRecipeName.Text = _recipe.RecipeName;
                    int _CookingTimeMinute = MyConvert.ToInt32(_recipe.CookingTimeMinute,0);
                    if (_CookingTimeMinute > 0)
                    {
                        TimeSpan _timeCook = new TimeSpan(0, _CookingTimeMinute, 0);
                        txtCookingTimeHours.Text = _timeCook.Hours.ToString();
                        txtCookingTimeMinutes.Text = _timeCook.Minutes.ToString();
                    }
                    else
                    {
                        txtCookingTimeHours.Text = "0";
                        txtCookingTimeMinutes.Text = "0";
                    }
                    txtNumberOfPerson.Text = _recipe.NumberOfPerson.ToString();
                    int _PreparationTimeMinute = MyConvert.ToInt32(_recipe.PreparationTimeMinute, 0);
                    if (_PreparationTimeMinute > 0)
                    {
                        TimeSpan _timePreparation = new TimeSpan(0, _PreparationTimeMinute, 0);
                        txtPreparationTimeHours.Text = _timePreparation.Hours.ToString();
                        txtPreparationTimeMinutes.Text = _timePreparation.Minutes.ToString();
                    }
                    else
                    {
                        txtPreparationTimeHours.Text = "0";
                        txtPreparationTimeMinutes.Text = "0";
                    }
                    chkDraft.Checked = MyConvert.ToBoolean(_recipe.Draft, false);
                    if (chkDraft.Checked)
                    {
                        chkDraft.Text = hfRecipeInDraft.Value;
                        chkDraft.CssClass = "RecipeDraft";
                        chkDraft.Attributes.Add("onclick", "CheckDraftClick('RecipeDraft');");
                        chkDraft.Attributes.Add("onmouseover", "CheckDraft('over','RecipeDraft');");
                        chkDraft.Attributes.Add("onmouseout", "CheckDraft('out','RecipeDraft');");
                    }
                    else
                    {
                        chkDraft.Text = hfPublicRecipe.Value;
                        chkDraft.CssClass = "PublicRecipe";
                        chkDraft.Attributes.Add("onclick", "CheckDraftClick('PublicRecipe');");
                        chkDraft.Attributes.Add("onmouseover", "CheckDraft('over','PublicRecipe');");
                        chkDraft.Attributes.Add("onmouseout", "CheckDraft('out','PublicRecipe');");
                    }
                    txtRecipeHistory.Text = _recipe.RecipeHistory;
                    txtRecipeNote.Text = _recipe.RecipeNote;
                    txtRecipeSuggestion.Text = _recipe.RecipeSuggestion;
                    //txtRecipeStep.Text = _recipe.RecipeSteps[0].Step;
                    chkHotSpicy.Checked = MyConvert.ToBoolean(_recipe.HotSpicy, false);
                }
                catch
                {
                }
                try
                {
                    rptRecipeBeverage.DataSource = BeverageRecipe.GetBeverageForRecipe(new Guid(_idRecipe));
                    rptRecipeBeverage.DataBind();
                }
                catch
                {
                }
                CreateDynamicRecipeProperty();
                GetRecipeIngredient();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in GetDataFromDatabase(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void CreateDynamicRecipeProperty()
        {
            try
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
                        Panel _panelLeft = new Panel();
                        _panelLeft.ID = "pnlLeftRecipePropType" + recipePropType.IDRecipePropertyType.ToString();
                        _panelLeft.CssClass = "pnlTableCol pnlPropertyType";

                        Panel _panelRight = new Panel();
                        _panelRight.ID = "pnlRightRecipePropType" + recipePropType.IDRecipePropertyType.ToString();
                        _panelRight.CssClass = "pnlTableCol";
                        _panelRight.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                        _newLabel.ID = "lblRecipePropType" + recipePropType.IDRecipePropertyType.ToString();
                        _newLabel.CssClass = "IngredientInfoFieldTitle";
                        _newLabel.Text = recipePropType.RecipePropType;

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

                        _panelLeft.Controls.Add(_newLabel);
                        _panelContainer.Controls.Add(_panelLeft);
                        _panelContainer.Controls.Add(_panelRight);
                        upnlDynamicProperty.ContentTemplateContainer.Controls.Add(_panelContainer);
                    }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in CreateDynamicRecipeProperty(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void GetRecipeIngredient()
        {
            try
            {
                RecipeLanguage _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                _recipe.GetRecipeIngredients();

                repRecipeIngredient.DataSource = _recipe.RecipeIngredients;
                repRecipeIngredient.DataBind();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in GetRecipeIngredient(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void ariIngredient_IngredientAdded(object sender, EventArgs e)
        {
            try
            {
                GetRecipeIngredient();
                ariIngredient.Clear();
                ariIngredient.StatAutoComplete();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in ariIngredient_IngredientAdded(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void FileUploaded(object sender, EventArgs e)
        {
            try
            {
                lblGeneralUploadError.Visible = false;
                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                _recipe.QueryBaseRecipeInfo();
                if (_recipe.RecipeImage != null)
                {
                    _recipe.RecipeImage.QueryMediaInfo();
                    _recipe.RecipeImage.DeletePhoto();
                }
                _recipe.RecipeImage = new Photo(new Guid(multiup.MediaCreatedIDs.Substring(0, multiup.MediaCreatedIDs.Length - 1)));
                _recipe.Save();
                Response.Redirect(("/Utilities/ImageCrop.aspx?IDMedia=" + multiup.MediaCreatedIDs.Substring(0, multiup.MediaCreatedIDs.Length - 1) + "&ReturnURL=" + Request.RawUrl + "&MediaType=" + MediaType.RecipePhoto.ToString()).ToLower(), false);
                Context.ApplicationInstance.CompleteRequest();
                _continueLoad = false;
            }
            catch(Exception ex)
            {
                lblGeneralUploadError.Visible = true;

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in FileUploaded(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }

        protected void btnAddBeverageRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(acBeverage.SelectedObjectID))
                {
                    RecipeLanguage _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);

                    BeverageRecipe brNew = new BeverageRecipe(_recipe.IDRecipe, new Guid(acBeverage.SelectedObjectID), new Guid(_idRecipe));
                    brNew.Save();

                    rptRecipeBeverage.DataSource = BeverageRecipe.GetBeverageForRecipe(new Guid(_idRecipe));
                    rptRecipeBeverage.DataBind();

                    acBeverage.Clear();
                    acBeverage.StatAutoComplete();
                }
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnAddBeverageRecipe_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }

                acBeverage.Clear();
                acBeverage.StatAutoComplete();
            }
        }

        protected void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                int _saveErrorCount = 0;
                MyUser _User = null;
                try
                {
                    _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                    _recipe.QueryRecipeInfo();
                    _recipe.QueryRecipeLanguageInfo();
                    //_recipe.GetRecipeSteps();
                    _recipe.GetRecipeIngredients();
                    _User = new MyUser(new Guid(_idUser));
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

                    #region SaveRecipeData
                    string stgResult = "";

                    try
                    {
                        bool _numPersonChange = false;
                        if (_recipe.NumberOfPerson != MyConvert.ToInt32(txtNumberOfPerson.Text, 4))
                        {
                            _numPersonChange = true;
                        }
                        _recipe.RecipeName = txtRecipeName.Text.Replace("/", "").Replace("\\", "").Replace("http", "").Replace("https", ""); 
                        _recipe.RecipeLanguageAutoTranslate = false;
                        _recipe.RecipeHistory = txtRecipeHistory.Text;
                        _recipe.RecipeHistoryDate = null;
                        _recipe.RecipeNote = txtRecipeNote.Text;
                        _recipe.RecipeSuggestion = txtRecipeSuggestion.Text;
                        _recipe.RecipeDisabled = false;
                        _recipe.RecipeFather = null;
                        _recipe.Owner = _recipe.Owner;
                        _recipe.NumberOfPerson = MyConvert.ToInt32(txtNumberOfPerson.Text, 4);
                        _recipe.PreparationTimeMinute = (MyConvert.ToInt32(txtPreparationTimeHours.Text, 0) * 60) + MyConvert.ToInt32(txtPreparationTimeMinutes.Text, 0);
                        _recipe.CookingTimeMinute = (MyConvert.ToInt32(txtCookingTimeHours.Text, 0) * 60) + MyConvert.ToInt32(txtCookingTimeMinutes.Text, 0);
                        _recipe.RecipeDifficulties = (RecipeInfo.Difficulties)MyConvert.ToInt32(ddlRecipeDifficulties.SelectedValue, 2);
                        _recipe.RecipeVideo = null;
                        _recipe.IDCity = 0;
                        _recipe.LastUpdate = DateTime.UtcNow;
                        _recipe.UpdatedByUser = new Guid(_idUser);
                        //_recipe.isStarterRecipe = false;
                        _recipe.DeletedOn = null;
                        _recipe.BaseRecipe = false;
                        _recipe.RecipeEnabled = true;
                        _recipe.HotSpicy = chkHotSpicy.Checked;
                        _recipe.OriginalVersion = true;
                        _recipe.Checked = true;
                        _recipe.Draft = chkDraft.Checked;
                        Dictionary<string, string> CalcValue = null;
                        try
                        {
                            //If number of person change the recipe info was saved before calculate the nutrictional info
                            if (_numPersonChange)
                            {
                                _recipe.Save();
                            }
                        }
                        catch
                        {
                        }
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
                            _recipe.RecipeLanguageTags = RecipeAI.CalculateRecipeTags(_recipe.IDRecipe, IDLanguage,
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
                            _recipe.RecipeCompletePerc = 10;
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
                                try
                                {
                                    SaveSteps();
                                }
                                catch
                                {
                                }
                                lblRecipeName.Text = _recipe.RecipeName;
                                lnkRecipeName.Text = _recipe.RecipeName;
                                try
                                {
                                    hfRecipePercentageCompleteBase.Value = GetLocalResourceObject("hfRecipePercentageCompleteBase.Value").ToString();
                                }
                                catch
                                {
                                }
                                lblRecipeCompletePerc.Text = hfRecipePercentageCompleteBase.Value.Replace("{0}", _recipe.RecipeCompletePerc.ToString());

                            }
                            catch
                            {
                            }
                            try
                            {
                                if (_recipe.RecipeEnabled && !chkDraft.Checked)
                                {
                                    //INSERT ACTION IN USER BOARD
                                    UserBoard NewActionInUserBoard = new UserBoard(_recipe.UpdatedByUser.IDUser, null, ActionTypes.NewRecipe, _recipe.IDRecipe, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                                    ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();
                                }
                                stgResult = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-IN-0003");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                    "noty({text: '" + stgResult + "'});", true);
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch (Exception ex)
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
                    //ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + txtRecipeName.Text.Replace("'", "\\'") + "','" + stgResult + "');", true);
                    acBeverage.StatAutoComplete();
                    #endregion

                }
                else
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Unable to save recipe, data not available", "", "IDRecipe: " + Request.QueryString["IDRecipe"].ToString(), true, false);
                        LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    LogRow NewRow = new LogRow(DateTime.Now, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "RC-ER-9999", "Error in btnSaveRecipe_Click(): " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }
        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _recipe = new RecipeLanguage(new Guid(_idRecipe), IDLanguage);
                _recipe.QueryRecipeInfo();
                _recipe.QueryRecipeLanguageInfo();
                _recipe.Delete();
                Response.Redirect("/", true);
            }
            catch
            {

            }
        }
    }
    
}
