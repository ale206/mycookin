using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;
using System.Drawing;
using MyCookin.ObjectManager.UserBoardManager;

namespace MyCookinWeb.IngredientWeb
{
    public partial class MngIngredients :  MyCookinWeb.Form.MyPageBase
    {
        IngredientLanguage ingr;
        string strIngrPrepRecipe;
        string strIngrCategory;
        int IDLanguage;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    if (Request.QueryString["IDIngr"] == null)
                    {
                        Response.Redirect(("IngredientDashBoard.aspx").ToLower(), true);
                    }

                    IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                    if (!Page.IsPostBack)
                    {
                        GetDataFromDatabase();
                        try
                        {
                            LogRow logRowOpenIngredient = new LogRow(DateTime.UtcNow, "0", "0", Network.GetCurrentPageName(),
                                "", "Open Ingredient " + ingr.IDIngredient.ToString(), Session["IDUser"].ToString(), false, true);
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
                upshImgIngredient.ImageMediaType = MediaType.IngredientPhoto;
                upshImgIngredient.CropErrorBoxMsg = "Non hai croppato l'area della foto";
                upshImgIngredient.CropErrorBoxTitle = "Errore.";
                upshImgIngredient.CropAspectRatio = "1";

                upshImgIngredient.DeleteWarningMsg = "Sei sicuro di voler cancellare?";
                upshImgIngredient.DeleteUndo = "No";
                upshImgIngredient.BaseFileName = txtIngredientSingular.Text;
                upshImgIngredient.IDMediaOwner = new Guid(Session["IDUser"].ToString());
                upshImgIngredient.DeleteConfirm = "Sicuro?";
                upshImgIngredient.DeleteUndo = "Annulla";
                //==============================
            }

            acIngredient.MethodName = "/Ingredient/SearchIngredient.asmx/SearchIngredients";
            acIngredient.LanguageCode = "2";
            acIngredient.ObjectLabelIdentifier = "IngredientSingular";
            acIngredient.ObjectIDIdentifier = "IDIngredient";
            acIngredient.ObjectLabelText = "Ingrediente: ";
            acIngredient.LangFieldLabel = "IDLanguage";
            acIngredient.WordFieldLabel = "words";
            acIngredient.MinLenght = "3";

            GetAltIngrInfo();
           
        }

        #region AlternativeIngredient

        protected void GetAltIngrInfo()
        {
            Ingredient tstIngr = new Ingredient(Request.QueryString["IDIngr"].ToString());
            tstIngr.GetAlternativeIngredients(false);
            pnlIngrAlternative.Controls.Clear();
            if (tstIngr.AlternativeIngrediets != null && tstIngr.AlternativeIngrediets.Length > 0)
            {
                for (int i = 0; i < tstIngr.AlternativeIngrediets.Length; i++)
                {
                    Panel pnlAltIngr = new Panel();
                    Label lblAltIngr = new Label();
                    ImageButton btnAltIngrDelete = new ImageButton();
                    IngredientLanguage ing = new IngredientLanguage(tstIngr.AlternativeIngrediets[i].IngredientSlave, 2);
                    ing.QueryIngredientLanguageInfo();

                    pnlAltIngr.ID = "pnl" + tstIngr.AlternativeIngrediets[i].IngredientSlave.IDIngredient.ToString();
                    lblAltIngr.ID = "lbl" + tstIngr.AlternativeIngrediets[i].IngredientSlave.IDIngredient.ToString();
                    lblAltIngr.Text = ing.IngredientSingular;
                    btnAltIngrDelete.ID = "btn" + tstIngr.AlternativeIngrediets[i].IngredientSlave.IDIngredient.ToString();
                    btnAltIngrDelete.CommandArgument = tstIngr.AlternativeIngrediets[i].IDIngredientAlternative.ToString();
                    btnAltIngrDelete.ImageUrl = "/Images/deleteX.png";
                    btnAltIngrDelete.Height = 16;
                    btnAltIngrDelete.Width = 16;
                    btnAltIngrDelete.Click += new ImageClickEventHandler(btnAltIngrDelete_Click);
                    pnlAltIngr.Controls.Add(lblAltIngr);
                    pnlAltIngr.Controls.Add(btnAltIngrDelete);

                    pnlIngrAlternative.Controls.Add(pnlAltIngr);
                }
            }
            acIngredient.StatAutoComplete();
        }

        protected void btnAltIngrDelete_Click(object sender, EventArgs e)
        {
            ImageButton _btn = (ImageButton)sender;
            IngredientAlternative ingAlt = new IngredientAlternative(new Guid(_btn.CommandArgument));
            ingAlt.Delete();
            GetAltIngrInfo();
        }

        protected void btnAddAltIngr_Click(object sender, EventArgs e)
        {
            Ingredient tstIngr = new Ingredient(Request.QueryString["IDIngr"].ToString());
            tstIngr.AddAlternativeIngredient(new Guid(acIngredient.SelectedObjectID), new Guid(Session["IDUser"].ToString()));
            GetAltIngrInfo();
            acIngredient.Clear();
        }
        #endregion

        /// <summary>
        /// Save ingredient info
        /// </summary>
        /// <returns></returns>
        protected string SaveIngredient()
        {
            string stgResult = "";
            
            upshImgIngredient.BaseFileName = txtIngredientSingular.Text;

            IngredientLanguage saveingr = new IngredientLanguage(lblIDIngredient.Text, MyConvert.ToInt32(lblIDLanguage.Text, 1));
            saveingr.QueryIngredientInfo();
            saveingr.QueryIngredientLanguageInfo();
            if (ddlIngredientPreparationRecipe.SelectedValue != "")
            {
                saveingr.IngredientPreparationRecipe = new Recipe(new Guid(ddlIngredientPreparationRecipe.SelectedValue));
            }
            else
            {
                saveingr.IngredientPreparationRecipe = null;
            }
            MyUser modUser = new MyUser(new Guid(Session["IDUser"].ToString()));
            modUser.GetUserBasicInfoByID();
            //MyUser modUser = new MyUser(Session["eMail"].ToString());
            saveingr.AverageWeightOfOnePiece = MyConvert.ToInt32(txtAverageWeightOfOnePiece.Text, 0);
            //IMPOSTARE A TRUE
            saveingr.Checked = true;
            saveingr.IngredientEnabled = chkIngrEnabled.Checked;
            saveingr.IngredientDescription = txtIngredientDescription.Text;
            saveingr.IngredientPlural = txtIngredientPlural.Text;
            saveingr.IngredientSingular = txtIngredientSingular.Text;
            saveingr.IsGlutenFree = chkIsGlutenFree.Checked;
            saveingr.IsHotSpicy = chkIsHotSpicy.Checked;
            saveingr.IsVegan = chkIsVegan.Checked;
            saveingr.IsVegetarian = chkIsVegetarian.Checked;
            saveingr.Kcal100gr = MyConvert.ToDouble(txtKcal100gr.Text, 0);
            saveingr.grAlcohol = MyConvert.ToDouble(txtPercKcalAlcohol.Text, 0);
            saveingr.grFats = MyConvert.ToDouble(txtPercKcalFats.Text, 0);
            saveingr.grProteins = MyConvert.ToDouble(txtPercKcalProteins.Text, 0);
            saveingr.grCarbohydrates = MyConvert.ToDouble(txtPerKcalCarbohydrates.Text, 0);
            saveingr.IngredientModifiedByUser = modUser;
            saveingr.IngredientLastMod = DateTime.UtcNow;
            saveingr.January = chkJanuary.Checked;
            saveingr.February = chkFebruary.Checked;
            saveingr.March = chkMarch.Checked;
            saveingr.April = chkApril.Checked;
            saveingr.May = chkMay.Checked;
            saveingr.June = chkJune.Checked;
            saveingr.July = chkJuly.Checked;
            saveingr.August = chkAugust.Checked;
            saveingr.September = chkSeptember.Checked;
            saveingr.October = chkOctober.Checked;
            saveingr.November = chkNovember.Checked;
            saveingr.December = chkDecember.Checked;


            if (ddlIngredientCategory.SelectedValue != "")
            {
                saveingr.IngredientCategories = new IngredientCategory[1];

                saveingr.IngredientCategories[0] = new IngredientCategory(MyConvert.ToInt32(ddlIngredientCategory.SelectedValue, 0));
            }

            Photo _ingrImage = null;

            try
            {
                if (upshImgIngredient.IDMedia != new Guid())
                {
                    upshImgIngredient.IDMediaOwner = modUser.IDUser;
                    upshImgIngredient.SaveImage();
                    _ingrImage = upshImgIngredient.IDMedia;
                }
            }
            catch
            {
            }

            if (_ingrImage != null)
            {
                saveingr.IngredientImage = _ingrImage;
            }

            try
            {
                saveingr.SaveIngredient();
                ManageUSPReturnValue result = saveingr.SaveIngredientLanguage();
                lblIngredientTitleValue.Text = saveingr.IngredientSingular;
                stgResult = RetrieveMessage.RetrieveDBMessage(IDLanguage, result.ResultExecutionCode);
            }
            catch (Exception ex)
            {
                try
                {
                    ManageUSPReturnValue retValue = new ManageUSPReturnValue("RC-ER-0001", "Error save ingredient: " + saveingr.IDIngredient + " | " + ex.Message, true);
                    saveingr.IngredientLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, retValue);
                    stgResult = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-ER-0001");
                }
                catch
                {

                }
            }

            pnlResult.Visible = true;

            if (saveingr.IngredientPreparationRecipe != null)
            {
                //pnlPreparationRecipeInfo.Style.Clear();
                pnlPreparationRecipeInfo.Style.Add("display", "");
                //pnlIngrNutrictionalInfo.Style.Clear();
                pnlIngrNutrictionalInfo.Style.Add("display", "none");

                RecipeLanguage PreparationRecipe = new RecipeLanguage(saveingr.IngredientPreparationRecipe, IDLanguage);

                txtPrepRecipeInfo.Text = PreparationRecipe.GetRecipeStep();
            }


            //INSERT ACTION IN USER BOARD
            UserBoard NewActionInUserBoard = new UserBoard(modUser.IDUser, null, ActionTypes.NewIngredient, saveingr.IDIngredient, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
            ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();


            return stgResult;
        }

        /// <summary>
        /// Call SaveIngredient() when primary save button was clicked, show an update message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditIngredient_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string result = SaveIngredient();
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + txtIngredientSingular.Text.Replace("'","\\'") + "','" + result + "');", true);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Load the data from database for the ingredient indicated in the querystring of the page
        /// </summary>
        protected void GetDataFromDatabase()
        {
            upshImgIngredient.IDMediaOwner = new Guid(Session["IDUser"].ToString());
            
            try
            {
                ingr = new IngredientLanguage(Request.QueryString["IDIngr"].ToString(), IDLanguage);
            }
            catch
            {
                Response.Redirect(("IngredientDashBoard.aspx").ToLower(), true);
            }
            
            //If selected ingredient not exist
            if (ingr == null)
            {
                Response.Redirect(("IngredientDashBoard.aspx").ToLower(), true);
            }

            try
            {
                ingr.QueryIngredientLanguageInfo();
                ingr.QueryIngredientInfo();
                ingr.GetIngredientCategories();
                ingr.GetIngredientQuantityTypes();
            }
            catch (Exception ex)
            {
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error load Ingredient Info", ex.Message, "Ingredient: " + lblIDIngredient.Text, false, true);
                LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
            }

            upshImgIngredient.BaseFileName = ingr.IngredientSingular;

            Page.Title = ingr.IngredientSingular;

            lblIDLanguage.Text = ingr.IDLanguage.ToString();
            lblIDIngredient.Text = ingr.IDIngredient.ToString();
            chkIngrEnabled.Checked = ingr.IngredientEnabled;
            lblIngredientTitleValue.Text = ingr.IngredientSingular;
            lblIngredientTitleValue2.Text = ingr.IngredientSingular;
            ddlIngredientPreparationRecipe.DataSource = RecipeLanguage.GetRecipeByName(ingr.IngredientSingular, IDLanguage);
            ddlIngredientPreparationRecipe.DataTextField = "RecipeName";
            ddlIngredientPreparationRecipe.DataValueField = "IDRecipe";
            ddlIngredientPreparationRecipe.DataBind();

            ddlIngredientQuantityType.DataSource = IngredientQuantityTypeLanguage.GetAllGetIngredientsQuantityTypes(IDLanguage);
            ddlIngredientQuantityType.DataTextField = "IngredientQuantityTypeSingular";
            ddlIngredientQuantityType.DataValueField = "IDIngredientQuantityType";
            ddlIngredientQuantityType.DataBind();

            DataTable dtAllowedQuantityType = ingr.ListIngredientAllowedQuantityTypes();
            gvIngredientAllowedQuantityType.DataSource = dtAllowedQuantityType;


            gvIngredientAllowedQuantityType.DataBind();
            
            if (ingr.IngredientPreparationRecipe == null)
            { strIngrPrepRecipe = ""; }
            else
            { strIngrPrepRecipe = ingr.IngredientPreparationRecipe.IDRecipe.ToString(); }


            if (ingr.IngredientImage != null)
            {
                upshImgIngredient.IDMedia = ingr.IngredientImage;
                ingr.IngredientImage.QueryMediaInfo();
                upshImgIngredient.IDMediaOwner = ingr.IngredientImage.MediaOwner;
            }

            try
            {
                ddlIngredientPreparationRecipe.Items.FindByValue(strIngrPrepRecipe).Selected = true;
            }
            catch
            {
            }

            if (ddlIngredientPreparationRecipe.SelectedValue != "")
            {
                //pnlPreparationRecipeInfo.Style.Clear();
                pnlPreparationRecipeInfo.Style.Add("display", "");
                //pnlIngrNutrictionalInfo.Style.Clear();
                pnlIngrNutrictionalInfo.Style.Add("display", "none");

                RecipeLanguage PreparationRecipe = new RecipeLanguage(ingr.IngredientPreparationRecipe, IDLanguage);

                txtPrepRecipeInfo.Text = PreparationRecipe.GetRecipeStep();
            }

            txtAverageWeightOfOnePiece.Text = ingr.AverageWeightOfOnePiece.ToString();
            txtKcal100gr.Text = ingr.Kcal100gr.ToString();
            txtPercKcalProteins.Text = ingr.grProteins.ToString();
            txtPercKcalFats.Text = ingr.grFats.ToString();
            txtPerKcalCarbohydrates.Text = ingr.grCarbohydrates.ToString();
            txtPercKcalAlcohol.Text = ingr.grAlcohol.ToString();
            chkIsVegetarian.Checked = ingr.IsVegetarian;
            chkIsVegan.Checked = ingr.IsVegan;
            chkIsGlutenFree.Checked = ingr.IsGlutenFree;
            chkIsHotSpicy.Checked = ingr.IsHotSpicy;
            txtIngredientSingular.Text = ingr.IngredientSingular;
            txtIngredientPlural.Text = ingr.IngredientPlural;
            txtIngredientDescription.Text = ingr.IngredientDescription;
            chkJanuary.Checked = ingr.January;
            chkFebruary.Checked = ingr.February;
            chkMarch.Checked = ingr.March;
            chkApril.Checked = ingr.April;
            chkMay.Checked = ingr.May;
            chkJune.Checked = ingr.June;
            chkJuly.Checked = ingr.July;
            chkAugust.Checked = ingr.August;
            chkSeptember.Checked = ingr.September;
            chkOctober.Checked = ingr.October;
            chkNovember.Checked = ingr.November;
            chkDecember.Checked = ingr.December;


            ddlIngredientCategory.DataSource = IngredientCategoryLanguage.GetCompleteIngedientCategoryTreeByLang(IDLanguage);
            ddlIngredientCategory.DataTextField = "IngredientCategoryLanguage";
            ddlIngredientCategory.DataValueField = "IDIngredientCategory";
            ddlIngredientCategory.DataBind();

             if (ingr.IngredientCategories == null)
            { strIngrCategory = ""; }
            else
            {
                strIngrCategory = IngredientCategoryLanguage.GetIngredientCategoryLang(ingr.IngredientCategories[0].IDIngredientCategory, IDLanguage); 
            }

             try
             {
                 ddlIngredientCategory.Items.FindByText(strIngrCategory).Selected = true;
             }
             catch
             {
             }
        }

        

        /// <summary>
        /// Add Allowed quantity to an ingredient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddIngredientAllowedQuantityType_Click(object sender, EventArgs e)
        {
            try
            {
                IngredientLanguage saveingrQtaType = new IngredientLanguage(lblIDIngredient.Text, MyConvert.ToInt32(lblIDLanguage.Text, 1));
                saveingrQtaType.AddOrDeleteAllowedQuantityType(MyConvert.ToInt32(ddlIngredientQuantityType.SelectedValue, 0), false);
                saveingrQtaType.GetIngredientQuantityTypes();
                gvIngredientAllowedQuantityType.DataSource = saveingrQtaType.ListIngredientAllowedQuantityTypes();
                gvIngredientAllowedQuantityType.DataBind();
            }
            catch(Exception ex)
            {
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error insert AllowedQuantityType", ex.Message, "Ingredient: " + lblIDIngredient.Text, false, true);
                LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
            }
        }

        /// <summary>
        /// Delete Allowed quantity to an ingredient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
            IngredientLanguage saveingrQtaType = new IngredientLanguage(lblIDIngredient.Text, MyConvert.ToInt32(lblIDLanguage.Text,1));
            Button _btnDelete = (Button)sender;
            saveingrQtaType.AddOrDeleteAllowedQuantityType(MyConvert.ToInt32(_btnDelete.CommandArgument,0), true);
            saveingrQtaType.GetIngredientQuantityTypes();
            gvIngredientAllowedQuantityType.DataSource = saveingrQtaType.ListIngredientAllowedQuantityTypes();
            gvIngredientAllowedQuantityType.DataBind();
            }
            catch (Exception ex)
            {
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error delete AllowedQuantityType", ex.Message, "Ingredient: " + lblIDIngredient.Text, false, true);
                LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
            }
        }

    }
}