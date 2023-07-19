using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.Common;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlShowRecipeIngredient : System.Web.UI.UserControl
    {
        #region PublicFields

        public string IDRecipeIngredient
        {
            get { return hfIDRecipeIngredient.Value; }
            set { hfIDRecipeIngredient.Value = value; }
        }

        public string IDRecipe
        {
            get { return hfIDRecipe.Value; }
            set { hfIDRecipe.Value = value; }
        }

        public string IDIngredient
        {
            get { return hfIDIngredient.Value; }
            set { hfIDIngredient.Value = value; }
        }

        public string IsPrincipal
        {
            get { return hfIsPrincipal.Value; }
            set { hfIsPrincipal.Value = value; }
        }

        public string QuantityNotStd
        {
            get { return hfQuantityNotStd.Value; }
            set { hfQuantityNotStd.Value = value; }
        }

        public string QuantityNotStdType
        {
            get { return hfQuantityNotStdType.Value; }
            set { hfQuantityNotStdType.Value = value; }
        }

        public string Quantity
        {
            get { return hfQuantity.Value; }
            set { hfQuantity.Value = value; }
        }

        public string QuantityType
        {
            get { return hfQuantityType.Value; }
            set { hfQuantityType.Value = value; }
        }

        public string QuantityNotSpecified
        {
            get { return hfQuantityNotSpecified.Value; }
            set { hfQuantityNotSpecified.Value = value; }
        }

        public string RecipeIngredientGroupNumber
        {
            get { return hfRecipeIngredientGroupNumber.Value; }
            set { hfRecipeIngredientGroupNumber.Value = value; }
        }

        public string RecipeIngredientGroupNumberChange
        {
            get { return hfRecipeIngredientGroupNumberChange.Value; }
            set { hfRecipeIngredientGroupNumberChange.Value = value; }
        }

        public string IDLanguage
        {
            get { return hfIDLanguage.Value; }
            set { hfIDLanguage.Value = value; }
        }

        public string ShowPrincipalIngr
        {
            get { return hfShowPrincipalIngr.Value; }
            set { hfShowPrincipalIngr.Value = value; }
        }

        public string ShowInvalidIngr
        {
            get { return hfShowInvalidIngr.Value; }
            set { hfShowInvalidIngr.Value = value; }
        }

        public string ShowEditButton
        {
            get { return hfShowEditButton.Value; }
            set { hfShowEditButton.Value = value; }
        }

        public string DeleteButtonToolTip
        {
            get { return btnDeleteIngredient.ToolTip; }
            set { btnDeleteIngredient.ToolTip = value; }
        }

        public string DeleteButtonOnClientClick
        {
            get { return btnDeleteIngredient.OnClientClick; }
            set { btnDeleteIngredient.OnClientClick = value; }
        }

        public bool ShowInfoPanel
        {
            get { return MyConvert.ToBoolean(hfShowInfoPanel.Value,false); }
            set { hfShowInfoPanel.Value = value.ToString(); }
        }

        public bool EditIngredientRelevance
        {
            get { return MyConvert.ToBoolean(hfEnableEditIngredientRelevance.Value, false); }
            set { hfEnableEditIngredientRelevance.Value = value.ToString(); }
        }

        public bool LoadInfoPanelAsync
        {
            get { return MyConvert.ToBoolean(hfLoadInfoPanelAsync.Value, false); }
            set { hfLoadInfoPanelAsync.Value = value.ToString(); }
        }

        #endregion
        QuantityNotStdType _qtaNotStdType;
        IngredientQuantityTypeLanguage _qtaType;
        int _idLanguage;
        protected void Page_Load(object sender, EventArgs e)
        {
            _idLanguage = 1;
        }

        protected override void OnInit(EventArgs e)
        {
            //LoadControl()
            if (ShowInfoPanel)
            {
                StartPopBox();
            }
        }

        protected override void DataBindChildren()
        {
            LoadControl();
            //StartPopBox();
        }

        private void LoadControl()
        {
            if (ShowInfoPanel)
            {
                
                lnkIngredient.NavigateUrl = "#";
                lnkIngredient.CssClass = "linkPopBox lnkIngredient";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                //                 "StartPopBox('#" + pnlIngredientInfoBox.ClientID + "','#" + lnkIngredient.ClientID + "','#" + pnlBoxInternal.ClientID + "');", true);
            }
            else
            {
                lnkIngredient.NavigateUrl = "";
                lnkIngredient.CssClass = "lnkIngredient";
            }

            if (hfControlLoaded.Value != "true")
            {
                IngredientLanguage _ingr;
                
                _idLanguage = MyConvert.ToInt32(IDLanguage, MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1));
                //Get Ingredient Information
                try
                {
                    _ingr = new IngredientLanguage(new Guid(IDIngredient), _idLanguage);

                    _ingr.QueryIngredientInfo();
                    _ingr.QueryIngredientLanguageInfo();
                    //_ingr.GetIngredientCategories();

                    _qtaNotStdType = new QuantityNotStdType(MyConvert.ToInt32(QuantityNotStdType, 0), _idLanguage);
                    _qtaNotStdType.QueryLanguageDbInfo();

                    _qtaType = new IngredientQuantityTypeLanguage(MyConvert.ToInt32(QuantityType, 0), _idLanguage);
                    _qtaType.QueryIngredientsQuantityTypesBaseInfo();

                    RecipeIngredientsLanguage _recipeIngrLang = new RecipeIngredientsLanguage(new Guid(hfIDRecipeIngredient.Value), _idLanguage);
                    _recipeIngrLang.QueryBaseRecipesIngredientsInfo();

                    lblKcal.Text = lblKcal.Text.Replace("{0}", Math.Round(Convert.ToDouble(_ingr.Kcal100gr), 0).ToString());
                    #region IngredientAlternative

                    RecipeIngrediets _ingrAlter = new RecipeIngrediets(new Guid(IDRecipeIngredient));
                    _ingrAlter.GetAlternativeIngredients();

                    if (_ingrAlter.RecipeIngredientAlternatives != null && _ingrAlter.RecipeIngredientAlternatives.Length > 0)
                    {
                        pnlAltIngredient.Visible = true;

                        for (int i = 0; i < _ingrAlter.RecipeIngredientAlternatives.Length; i++)
                        {
                            IngredientLanguage _ingrAlt = new IngredientLanguage(_ingrAlter.RecipeIngredientAlternatives[i].Ingredient.IDIngredient, _idLanguage);
                            _ingrAlt.QueryIngredientLanguageInfo();
                            lblAltIngr.Text += _ingrAlt.IngredientSingular + "<br/>";
                        }

                    }
                    else
                    {
                        pnlAltIngredient.Visible = false;
                    }

                    #endregion

                    #region IngredientGroup

                    try
                    {
                        if (Convert.ToBoolean(hfRecipeIngredientGroupNumberChange.Value) && !String.IsNullOrEmpty(_recipeIngrLang.RecipeIngredientGroupName) && _recipeIngrLang.RecipeIngredientGroupNumber!=0)
                        {
                            pnlIngrGroup.Visible = true;
                            lblIngredientGroup.Text = _recipeIngrLang.RecipeIngredientGroupName;
                        }
                        else
                        {
                            pnlIngrGroup.Visible = false;
                            lblIngredientGroup.Text = "";
                        }
                    }
                    catch
                    {
                    }

                    #endregion

                    #region EditButtons

                    try
                    {
                        btnDeleteIngredient.Visible = Convert.ToBoolean(hfShowEditButton.Value);
                    }
                    catch
                    {
                    }

                    #endregion

                    LoadIngredientInfoBox(_ingr);

                    double? _qta = null;
                    bool _showPrincipalIngr = false;
                    bool _showInvalidIngr = false;

                    try
                    {
                        _qta = MyConvert.ToDouble(Quantity, 0);
                        _showPrincipalIngr = Convert.ToBoolean(ShowPrincipalIngr);
                        _showInvalidIngr = Convert.ToBoolean(ShowInvalidIngr);
                    }
                    catch
                    {

                    }

                    if (!_ingr.IngredientEnabled && _showInvalidIngr)
                    {
                        imgInvalidIngr.Visible = true;
                    }
                    else
                    {
                        imgInvalidIngr.Visible = false;
                    }

                    lnkIngredient.Text = "";
                    lblIngrQta.Text = "";

                    if (!String.IsNullOrEmpty(Quantity))
                    {
                        if (("|218|151|").IndexOf("|" + _qtaType.IDIngredientQuantityType.ToString() + "|") == -1)
                        {
                            if (_qta >= 1000 && !String.IsNullOrEmpty(_qtaType.IngredientQuantityTypeX1000Singular))
                            {
                                lblIngrQta.Text = _qta / 1000 + " ";
                            }
                            else
                            {
                                lblIngrQta.Text = Quantity + " ";
                            }
                        }
                    }

                    if (_qtaNotStdType.IDQuantityNotStd != 0)
                    {
                        if (_qta > 1)
                        {
                            lblIngrQta.Text = _qtaNotStdType.QuantityNotStdPlural + " ";
                        }
                        else
                        {
                            lblIngrQta.Text = _qtaNotStdType.QuantityNotStdSingular + " ";
                        }
                    }

                    if (_qtaType.ShowInIngredientList)
                    {
                        if (_qta >= 1000 && !String.IsNullOrEmpty(_qtaType.IngredientQuantityTypeX1000Singular))
                        {
                            if ((_qta / 1000) > 1)
                            {
                                lblIngrQta.Text += _qtaType.IngredientQuantityTypeX1000Plural + " ";
                            }
                            else
                            {
                                lblIngrQta.Text += _qtaType.IngredientQuantityTypeX1000Singular + " ";
                            }
                        }
                        else
                        {
                            if (_qta > 1)
                            {
                                lblIngrQta.Text += _qtaType.IngredientQuantityTypePlural + " ";
                            }
                            else
                            {
                                lblIngrQta.Text += _qtaType.IngredientQuantityTypeSingular + " ";
                            }
                        }

                        if (!String.IsNullOrEmpty(_qtaType.IngredientQuantityTypeWordsShowBefore))
                        {
                            lblIngrQta.Text = _qtaType.IngredientQuantityTypeWordsShowBefore + " " + lblIngrQta.Text;
                        }
                        if (!String.IsNullOrEmpty(_qtaType.IngredientQuantityTypeWordsShowAfter))
                        {
                            lblIngrQta.Text += _qtaType.IngredientQuantityTypeWordsShowAfter + " ";
                        }
                    }

                    if (_qta > 1)
                    {
                        lnkIngredient.Text += _ingr.IngredientPlural;
                        //lblIngreadientName.Text = _ingr.IngredientPlural;
                    }
                    else
                    {
                        lnkIngredient.Text += _ingr.IngredientSingular;
                        //lblIngreadientName.Text = _ingr.IngredientSingular;
                    }
                    

                    if (_showPrincipalIngr)
                    {
                        chkPrincipal.Visible = true;
                       
                        try
                        {
                            chkPrincipal.Checked = Convert.ToBoolean(IsPrincipal);
                        }
                        catch
                        {
                            chkPrincipal.Checked = false;
                        }
                    }
                    else
                    {
                        chkPrincipal.Visible = false;
                    }

                    if (EditIngredientRelevance)
                    {
                        ddlIngredientRelevance.Visible = true;
                        try
                        {
                            ddlIngredientRelevance.DataSource = RecipeInfo.GetAllIngredientRelevancesLang(_idLanguage);
                            ddlIngredientRelevance.DataValueField = "value";
                            ddlIngredientRelevance.DataTextField = "viewText";
                            ddlIngredientRelevance.DataBind();
                            ddlIngredientRelevance.Items.FindByValue(((int)_recipeIngrLang.IngredientRelevance).ToString()).Selected = true;
                        }
                        catch
                        {
                            ddlIngredientRelevance.Visible = false;
                        }
                    }
                    else
                    {
                        ddlIngredientRelevance.Visible = false;
                    }

                    try
                    {
                        if (!String.IsNullOrEmpty(_recipeIngrLang.RecipeIngredientNote))
                        {
                            lblIngredientNote.Text = "(" + _recipeIngrLang.RecipeIngredientNote + ")";
                        }
                    }
                    catch
                    {
                    }
                    hfControlLoaded.Value = "true";
                }
                catch (Exception ex)
                {
                    lnkIngredient.Text = "ERROR " + ex.Message;
                    hfControlLoaded.Value = "false";
                }
            }
        }

        protected void LoadIngredientInfoBox(IngredientLanguage _ingr)
        {

            #region BoxIngredientInfo

            if (ShowInfoPanel)
            {

                try
                {
                    string _imgPath = "";

                    if (_ingr.IngredientImage != null && _ingr.IngredientImage.IDMedia != new Guid())
                    {
                        Photo _ingrPhoto = new Photo(_ingr.IngredientImage.IDMedia);
                        _ingrPhoto.QueryMediaInfo();


                        try
                        {
                            _imgPath = _ingrPhoto.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                        }
                        catch
                        {
                        }

                        if (String.IsNullOrEmpty(_imgPath))
                        {
                            _imgPath = _ingrPhoto.GetCompletePath(false, false, true);
                        }

                        imgIngredientPhoto.ImageUrl = _imgPath;
                    }
                    imgIngredientPhoto.AlternateText = _ingr.IngredientSingular;

                    //lnkGoToIngredient.Text = _ingr.IngredientSingular;
                    lblIngreadientName.Text = _ingr.IngredientPlural;
                    lnkGoToIngredient.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(_idLanguage) + AppConfig.GetValue("RoutingIngredient" + IDLanguage, AppDomain.CurrentDomain) + _ingr.IngredientSingular.Replace(" ","-") + "/" + _ingr.IDIngredient.ToString()).ToLower();
                    //lnkGoToIngredient.Target = "_blank";
                    lblIngreadientName.Attributes.Add("itemprop", "ingredients");

                    lblIngredientBasicInfo.Text = _ingr.IngredientDescription;

                    //if (_ingr.IsVegetarian)
                    //{
                    //    lblIngredientBasicInfo.Text += "Vegetariano <br/>";
                    //}
                    //if (_ingr.IsVegan)
                    //{
                    //    lblIngredientBasicInfo.Text += "Vegano <br/>";
                    //}
                    //if (_ingr.IsGlutenFree)
                    //{
                    //    lblIngredientBasicInfo.Text += "Senza Glutine <br/>";
                    //}
                    //if (_ingr.IsHotSpicy)
                    //{
                    //    lblIngredientBasicInfo.Text += "Piccante <br/>";
                    //}

                    //if (_ingr.IngredientCategories != null && _ingr.IngredientCategories[0] != null)
                    //{
                    //    lblIngredientBasicInfo.Text += IngredientCategoryLanguage.GetIngredientCategoryLang(_ingr.IngredientCategories[0].IDIngredientCategory, MyConvert.ToInt32(IDLanguage, 1));
                    //}

                }
                catch
                {

                }
            }

            #endregion

        }

        protected void btnDeleteIngredient_Click(object sender, ImageClickEventArgs e)
        {
            RecipeIngrediets _deleteRecipeIngr = new RecipeIngrediets(new Guid(hfIDRecipeIngredient.Value),
                                                    new Guid(hfIDRecipe.Value), new Guid(hfIDIngredient.Value),
                                                    Convert.ToBoolean(hfIsPrincipal.Value), hfQuantityNotStd.Value,
                                                    _qtaNotStdType, MyConvert.ToDouble(hfQuantity.Value,0), _qtaType,
                                                    Convert.ToBoolean(hfQuantityNotSpecified.Value), 
                                                    Convert.ToInt32(hfRecipeIngredientGroupNumber.Value),RecipeInfo.IngredientRelevances.OptionalIngredient);
            ManageUSPReturnValue delResult= _deleteRecipeIngr.Delete();

            if (!delResult.IsError)
            {
                pnlIngredientInfoBox.Visible = false;
            }
        }

        internal void StartPopBox()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                             "StartPopBox('#" + pnlIngredientInfoBox.ClientID + "','#" + lnkIngredient.ClientID + "','#" + pnlBoxInternal.ClientID + "');", true);
        }
    }
}
