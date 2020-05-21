using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.Common;
using MyCookin.ObjectManager.RecipeManager;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlAddRecipeIngredient : System.Web.UI.UserControl
    {
        #region PublicFields
        public bool isInError
        {
            get { return Convert.ToBoolean(txtIsError.Value); }
        }
        public string ReturnMessage
        {
            get { return txtErrorMessage.Value; }
        }
        public string SelectedObject
        {
            get { return txtObjectName.Text; }
            set { txtObjectName.Text = value; }
        }
        public string IDIngredient
        {
            get { return txtObjectID.Value; }
        }
        public string IDRecipe
        {
            get { return hfIDRecipe.Value; }
            set { hfIDRecipe.Value = value; }
        }
        public bool Enabled
        {
            get { return txtObjectName.Enabled; }
            set { txtObjectName.Enabled = value; }
        }
        public string LanguageCode
        {
            get { return hfLanguageCode.Value; }
            set { hfLanguageCode.Value = value; }
        }
        public string LangFieldLabel
        {
            get { return hfLangFieldLabel.Value; }
            set { hfLangFieldLabel.Value = value; }
        }
        public string WordFieldLabel
        {
            get { return hfWordFieldLabel.Value; }
            set { hfWordFieldLabel.Value = value; }
        }
        public string MethodName
        {
            get { return hfMethodName.Value; }
            set { hfMethodName.Value = value; }
        }
        public string MinLenght
        {
            get { return hfMinLenght.Value; }
            set { hfMinLenght.Value = value; }
        }
        public string ObjectLabelText
        {
            get { return hfObjectLabelText.Value; }
            set { hfObjectLabelText.Value = value; }
        }
        public string ObjectLabelIdentifier
        {
            get { return txtObjectLabelIdentifier.Value; }
            set { txtObjectLabelIdentifier.Value = value; }
        }
        public string ObjectIDIdentifier
        {
            get { return txtObjectIDIdentifier.Value; }
            set { txtObjectIDIdentifier.Value = value; }
        }
        public string QtaStd
        {
            get { return txtQtaStd.Text; }
        }
        public string RecipeIngredientNote
        {
            get { return txtIngrNote.Text; }
        }
        public bool isPrinciplaIngredient
        {
            get { return chkPrincipalForRecipe.Checked; }
        }
        public string QtaType
        {
            get { return ddlQtaType.SelectedValue ; }
        }
        public string QtaNotStd
        {
            get { return ddlQtaNotStd.SelectedValue; }
        }
        public string AlternativesIngredients
        {
            get { return hfAlternativeIngredient.Value; }
        }
        public string PrincipalIngredientLabel
        {
            get { return lblPrincipalForRecipe.Text; }
            set { lblPrincipalForRecipe.Text = value; }
        }
        public string AlternativeIngredientLabel
        {
            get { return lblIngredientAlternatives.Text; }
            set { lblIngredientAlternatives.Text = value; }
        }
        //public string ButtonAddIngrImageUrl
        //{
        //    get { return btnAddIngredient.ImageUrl; }
        //    set { btnAddIngredient.ImageUrl = value; }
        //}
        public string ButtonAddIngrOnClientClick
        {
            get { return btnAddIngredient.OnClientClick; }
            set { btnAddIngredient.OnClientClick = value; }
        }
        public string ButtonAddIngrToolTip
        {
            get { return btnAddIngredient.ToolTip; }
            set { btnAddIngredient.ToolTip = value; }
        }
        public bool ButtonAddIngrVisible
        {
            get { return btnAddIngredient.Visible; }
            set { btnAddIngredient.Visible = value; }
        }
        public int RecipeIngredientGroupNumber
        {
            get { return MyConvert.ToInt32(hfRecipeIngredientGroupNumber.Value,0); }
            set { hfRecipeIngredientGroupNumber.Value = value.ToString(); }
        }
        public string RecipeIngredientGroupName
        {
            get { return hfRecipeIngredientGroupName.Value; }
            set { hfRecipeIngredientGroupName.Value = value; }
        }
        public int IDLanguage
        {
            get { return MyConvert.ToInt32(hfLanguageCode.Value, 1); }
            set { hfLanguageCode.Value = value.ToString(); }
        }


        public bool InsertRecipeIngredientIsError
        {
            get { return MyConvert.ToBoolean(hfInsertRecipeIngredientIsError.Value,true); }
            set { hfInsertRecipeIngredientIsError.Value = value.ToString(); }
        }
        public string InsertRecipeIngredientMessage
        {
            get { return hfInsertRecipeIngredientMessage.Value; }
            set { hfInsertRecipeIngredientMessage.Value = value; }
        }
        public bool InsertAltRecipeIngredientIsError
        {
            get { return MyConvert.ToBoolean(hfInsertAltRecipeIngredientIsError.Value, true); }
            set { hfInsertAltRecipeIngredientIsError.Value = value.ToString(); }
        }
        public string InsertAltRecipeIngredientMessage
        {
            get { return hfInsertAltRecipeIngredientMessage.Value; }
            set { hfInsertAltRecipeIngredientMessage.Value = value; }
        }

        #endregion

        //Add Click event to control
        public event EventHandler IngredientAdded;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                txtIsError.Value = "false";
                try
                {
                    int intMinLenght;
                    intMinLenght = Convert.ToInt32(MinLenght);
                    if (intMinLenght < 3)
                    {
                        MinLenght = "3";
                    }
                    else
                    {
                        MinLenght = intMinLenght.ToString();
                    }
                }
                catch
                {
                    MinLenght = "3";
                }
                lblObjectName.Text = ObjectLabelText;
                if (String.IsNullOrEmpty(MethodName) ||
                        String.IsNullOrEmpty(txtObjectLabelIdentifier.Value) ||
                        String.IsNullOrEmpty(txtObjectIDIdentifier.Value) ||
                        String.IsNullOrEmpty(LanguageCode))
                {
                    txtObjectName.Visible = false;
                    throw new System.ArgumentException("You missed some configuration parameters.");
                }

                //this avoid the callback error!!!
                #region ddlPreCompile

                try
                {
                    ddlIngredientRelevance.DataSource = RecipeInfo.GetAllIngredientRelevancesLang(MyConvert.ToInt32(hfLanguageCode.Value, 1));
                    ddlIngredientRelevance.DataValueField = "value";
                    ddlIngredientRelevance.DataTextField = "viewText";
                    ddlIngredientRelevance.DataBind();
               

                DataTable _dtIngrQtaType  = IngredientQuantityTypeLanguage.GetAllGetIngredientsQuantityTypes(MyConvert.ToInt32(hfLanguageCode.Value, 1));

                foreach (DataRow _IngrQtaType in _dtIngrQtaType.Rows)
                {
                    ListItem _item = new ListItem(_IngrQtaType["IngredientQuantityTypeSingular"].ToString(), _IngrQtaType["IDIngredientQuantityType"].ToString());
                    ddlQtaType.Items.Add(_item);
                }

                DataTable _dtIngrQtaNotStd = QuantityNotStdType.GetQuantityNotStdList(MyConvert.ToInt32(hfLanguageCode.Value, 1));
                //empty value
                ListItem _empty = new ListItem(GetLocalResourceObject("ddlQtaNotStd.Empty").ToString(), "");
                ddlQtaNotStd.Items.Add(_empty);
                foreach (DataRow _drIngrQtaNotStd in _dtIngrQtaNotStd.Rows)
                {
                    ListItem _item = new ListItem(_drIngrQtaNotStd["QuantityNotStdSingular"].ToString(), _drIngrQtaNotStd["IDQuantityNotStd"].ToString());
                    ddlQtaNotStd.Items.Add(_item);
                }
                }
                catch
                { }
                #endregion
            }
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    foreach (ListItem _item in ddlQtaType.Items)
        //    {
        //        Page.ClientScript.RegisterForEventValidation(_item.Value);
        //    }

        //    base.Render(writer);
        //}

        internal void StatAutoComplete()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                         "AddRecipeIngredientStartAutoComplete();", true);
        }

        internal void Clear()
        {
            txtObjectID.Value = "";
            txtObjectName.Text = "";
            txtQtaStd.Text = "";
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(IDIngredient) && !String.IsNullOrEmpty(IDRecipe))
            {
                RecipeIngredientGroupName = ddlIngredientGroup.SelectedItem.Text;
                RecipeIngredientGroupNumber = MyConvert.ToInt32(ddlIngredientGroup.SelectedValue,0);
                try
                {
                    Guid NewIDRecipeIngredient = Guid.NewGuid();
                    Guid NewIDRecipeIngredientLanguage = Guid.NewGuid();
                    Recipe _recipe = new Recipe(new Guid(IDRecipe));
                    RecipeIngredientsLanguage newRecipeIngr = new RecipeIngredientsLanguage(NewIDRecipeIngredientLanguage,
                                                    NewIDRecipeIngredient, IDLanguage,
                                                    RecipeIngredientNote, RecipeIngredientGroupName, false, _recipe.IDRecipe,
                                                    new Guid(IDIngredient), isPrinciplaIngredient,
                                                    "", new QuantityNotStdType(MyConvert.ToInt32(QtaNotStd, 0)),
                                                    MyConvert.ToDouble(QtaStd, 1),
                                                    new IngredientQuantityType(MyConvert.ToInt32(QtaType, 0)),
                                                    false, RecipeIngredientGroupNumber, null, 
                                                    (RecipeInfo.IngredientRelevances)Enum.Parse(typeof(RecipeInfo.IngredientRelevances),ddlIngredientRelevance.SelectedValue));
                    ManageUSPReturnValue result = _recipe.AddIngredient(newRecipeIngr);

                    InsertRecipeIngredientIsError = result.IsError;
                    InsertRecipeIngredientMessage = result.ResultExecutionCode;

                    //Insert alternative ingredient, if present
                    if (!result.IsError && !String.IsNullOrEmpty(AlternativesIngredients))
                    {
                        try
                        {
                            string[] _altIngredients = AlternativesIngredients.Split(',');

                            foreach (string altIngredient in _altIngredients)
                            {
                                if (!string.IsNullOrEmpty(altIngredient))
                                {
                                    try
                                    {
                                        Guid _altIngrID = new Guid(altIngredient);
                                        RecipeIngredientsLanguage _newAltRecipeIngr = new RecipeIngredientsLanguage(Guid.NewGuid(),
                                                            Guid.NewGuid(), IDLanguage,
                                                            RecipeIngredientNote, RecipeIngredientGroupName, false, _recipe.IDRecipe,
                                                            _altIngrID, false,
                                                            "", new QuantityNotStdType(MyConvert.ToInt32(QtaNotStd, 0)),
                                                            MyConvert.ToDouble(QtaStd, 1),
                                                            new IngredientQuantityType(MyConvert.ToInt32(QtaType, 0)),
                                                            false, RecipeIngredientGroupNumber, NewIDRecipeIngredient,
                                                            (RecipeInfo.IngredientRelevances)Enum.Parse(typeof(RecipeInfo.IngredientRelevances), ddlIngredientRelevance.SelectedValue));

                                        ManageUSPReturnValue AltIngrResult = _recipe.AddIngredient(_newAltRecipeIngr);

                                        InsertAltRecipeIngredientIsError = AltIngrResult.IsError;
                                        InsertAltRecipeIngredientMessage = AltIngrResult.ResultExecutionCode;
                                    }
                                    catch
                                    {
                                        InsertAltRecipeIngredientIsError = true;
                                        InsertAltRecipeIngredientMessage = "RC-ER-0003";
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }

                    }
                }
                catch
                {
                    InsertRecipeIngredientIsError = true;
                    InsertRecipeIngredientMessage = "RC-ER-0002";
                }

                Clear();
                StatAutoComplete();

                //Fire Click event for control
                IngredientAdded(this, EventArgs.Empty);
            }
            else
            {
                Clear();
                StatAutoComplete();
            }

            
        }

    }
}