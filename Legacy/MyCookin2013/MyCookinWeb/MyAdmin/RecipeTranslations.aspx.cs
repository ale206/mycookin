using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ThirdPartAPI.MicrosoftTranslatorSdk;

namespace MyCookinWeb.MyAdmin
{
    public partial class RecipeTranslations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("292d13f2-738f-487b-b739-96c52b9e8d21") >= 0)
            {
            }
            else
            {
                Response.Redirect("/default.aspx", true);
            }
        }
        protected void btnStartTranslateRecipe_Click(object sender, EventArgs e)
        {
            if (txtFromIDLanguage.Text != "" || txtNumRecipes.Text != "" || txtToIDLanguage.Text != "")
            {
                try
                {
                    int _idLangFrom = MyConvert.ToInt32(txtFromIDLanguage.Text, 2);
                    int _idLangTo = MyConvert.ToInt32(txtToIDLanguage.Text, 1);
                    string _langFrom = GetLangLabel(_idLangFrom);
                    string _langTo = GetLangLabel(_idLangTo);
                    lblResult.Text = "";
                    DataTable dtRecipeToTranslate = RecipeLanguage.GetRecipeToTranslate(_idLangFrom, _idLangTo, MyConvert.ToInt32(txtNumRecipes.Text, 10));
                    
                    if (dtRecipeToTranslate.Rows.Count > 0)
                    {
                        MicrosoftTranslationAPI NewTranslation = new MicrosoftTranslationAPI(_langFrom, _langTo);

                        MicrosoftTranslationAPI TranslationObj = new MicrosoftTranslationAPI();

                        foreach (DataRow drRecipe in dtRecipeToTranslate.Rows)
                        {
                            try
                            {
                                RecipeLanguage _originalRecipe = new RecipeLanguage(drRecipe.Field<Guid>("IDRecipe"), drRecipe.Field<int>("IDLanguage"));
                                _originalRecipe.QueryRecipeLanguageInfo();

                                RecipeLanguage _translateRecipe = new RecipeLanguage(drRecipe.Field<Guid>("IDRecipe"), Guid.NewGuid(), _idLangTo);

                                TranslationObj = NewTranslation.TranslateSentence(_originalRecipe.RecipeName);
                                _translateRecipe.RecipeName = TranslationObj.TranslatedSentence;

                                _translateRecipe.RecipeLanguageAutoTranslate = true;

                                if (!String.IsNullOrEmpty(_originalRecipe.RecipeHistory))
                                {
                                    TranslationObj = NewTranslation.TranslateSentence(_originalRecipe.RecipeHistory);
                                    _translateRecipe.RecipeHistory = TranslationObj.TranslatedSentence;
                                }

                                if (!String.IsNullOrEmpty(_originalRecipe.RecipeNote))
                                {
                                    TranslationObj = NewTranslation.TranslateSentence(_originalRecipe.RecipeNote);
                                    _translateRecipe.RecipeNote = TranslationObj.TranslatedSentence;
                                }

                                if (!String.IsNullOrEmpty(_originalRecipe.RecipeSuggestion))
                                {
                                    TranslationObj = NewTranslation.TranslateSentence(_originalRecipe.RecipeSuggestion);
                                    _translateRecipe.RecipeSuggestion = TranslationObj.TranslatedSentence;
                                }

                                _translateRecipe.RecipeDisabled = false;

                                TranslationObj = NewTranslation.TranslateSentence(_originalRecipe.RecipeLanguageTags);
                                _translateRecipe.RecipeLanguageTags = TranslationObj.TranslatedSentence;

                                _translateRecipe.OriginalVersion = false;

                                ManageUSPReturnValue _result = _translateRecipe.SaveLanguageInfo();
                                if (!_result.IsError)
                                {
                                    lblResult.Text += _originalRecipe.RecipeName + " ==> " + _translateRecipe.RecipeName + "<br/>";
                                }
                                else
                                {
                                    lblResult.Text += "ERROR  ==> " + _translateRecipe.RecipeName + " - " + _result.ResultExecutionCode + "<br/>";
                                }

                            }
                            catch (Exception ex)
                            {
                                lblResult.Text = "ERROR ==> " + ex.Message;
                            }
                        }
                    }
                    else
                    {
                        lblResult.Text = "No Recipe ready for translation found";
                    }
                }
                catch (Exception ex)
                {
                    lblResult.Text = "ERROR ==> " + ex.Message;
                }
            }
            else
            {
                lblResult.Text = "ERROR ==> Configuration parameter missing, please specify IDLangFrom, IDLangTo and Number of object";
            }

        }

        protected void btnStartTranslateRecipeSteps_Click(object sender, EventArgs e)
        {
            if (txtFromIDLanguage.Text != "" || txtNumRecipes.Text != "" || txtToIDLanguage.Text != "")
            {
                try
                {
                    int _idLangFrom = MyConvert.ToInt32(txtFromIDLanguage.Text, 2);
                    int _idLangTo = MyConvert.ToInt32(txtToIDLanguage.Text, 1);
                    string _langFrom = GetLangLabel(_idLangFrom);
                    string _langTo = GetLangLabel(_idLangTo);
                    lblResult.Text = "";
                    DataTable dtRecipeSteps = RecipeStep.GetRecipeStepsToTranslate(_idLangFrom, _idLangTo, MyConvert.ToInt32(txtNumRecipes.Text, 10));

                    if (dtRecipeSteps.Rows.Count > 0)
                    {
                        MicrosoftTranslationAPI NewTranslation = new MicrosoftTranslationAPI(_langFrom, _langTo);

                        MicrosoftTranslationAPI TranslationObj = new MicrosoftTranslationAPI();

                        foreach (DataRow drStepRecipe in dtRecipeSteps.Rows)
                        {
                            try
                            {
                                RecipeStep _originalStep = new RecipeStep(drStepRecipe.Field<Guid>("IDRecipeStep"));

                                RecipeStep _translatedStep = new RecipeStep(drStepRecipe.Field<Guid>("IDRecipeLanguage"), _originalStep.StepNumber, "", _originalStep.IDRecipeStepImage);

                                TranslationObj = NewTranslation.TranslateSentence(_originalStep.StepGroup);
                                _translatedStep.StepGroup = TranslationObj.TranslatedSentence;

                                TranslationObj = NewTranslation.TranslateSentence(_originalStep.Step);
                                _translatedStep.Step = TranslationObj.TranslatedSentence;

                                _translatedStep.StepTimeMinute = _originalStep.StepTimeMinute;

                                ManageUSPReturnValue _result = _translatedStep.Save();

                                if (!_result.IsError)
                                {
                                    lblResult.Text += _originalStep.Step.Substring(0, 10) + "... ==> " + _translatedStep.Step.Substring(0, 10) + "...<br/>";
                                }
                                else
                                {
                                    lblResult.Text += "ERROR  ==> " + _originalStep.Step.Substring(0, 10) + "... - " + _result.ResultExecutionCode + "<br/>";
                                }
                            }
                            catch (Exception ex)
                            {
                                lblResult.Text = "ERROR ==> " + ex.Message;
                            }
                        }
                    }
                    else
                    {
                        lblResult.Text = "No Recipe Step ready for translation found";
                    }
                }
                catch (Exception ex)
                {
                    lblResult.Text = "ERROR ==> " + ex.Message;
                }
            }
            else
            {
                lblResult.Text = "ERROR ==> Configuration parameter missing, please specify IDLangFrom, IDLangTo and Number of object";
            }
        }

        protected void btnStartTranslateRecipeIngrNotes_Click(object sender, EventArgs e)
        {
            if (txtFromIDLanguage.Text != "" || txtNumRecipes.Text != "" || txtToIDLanguage.Text != "")
            {
                try
                {
                    int _idLangFrom = MyConvert.ToInt32(txtFromIDLanguage.Text, 2);
                    int _idLangTo = MyConvert.ToInt32(txtToIDLanguage.Text, 1);
                    string _langFrom = GetLangLabel(_idLangFrom);
                    string _langTo = GetLangLabel(_idLangTo);
                    lblResult.Text = "";
                    DataTable dtRecipeIngredientNotes = RecipeIngredientsLanguage.GetRecipeIngredientNotesToTranslate(_idLangFrom, _idLangTo, MyConvert.ToInt32(txtNumRecipes.Text, 10));
                    
                    if (dtRecipeIngredientNotes.Rows.Count > 0)
                    {
                        MicrosoftTranslationAPI NewTranslation = new MicrosoftTranslationAPI(_langFrom, _langTo);

                        MicrosoftTranslationAPI TranslationObj = new MicrosoftTranslationAPI();
                        foreach (DataRow drRecipeIngredientNotes in dtRecipeIngredientNotes.Rows)
                        {
                            try
                            {
                                RecipeIngredientsLanguage _ingrLangOriginal = new RecipeIngredientsLanguage(drRecipeIngredientNotes.Field<Guid>("IDRecipeIngredient"), _idLangFrom);

                                RecipeIngredientsLanguage _ingrLangTranslated = new RecipeIngredientsLanguage(Guid.NewGuid(), _ingrLangOriginal.IDRecipeIngredient, _idLangTo, "", "", true);

                                if (!String.IsNullOrEmpty(_ingrLangOriginal.RecipeIngredientGroupName))
                                {
                                    TranslationObj = NewTranslation.TranslateSentence(_ingrLangOriginal.RecipeIngredientGroupName);
                                    _ingrLangTranslated.RecipeIngredientGroupName = TranslationObj.TranslatedSentence;
                                }

                                if (!String.IsNullOrEmpty(_ingrLangOriginal.RecipeIngredientNote))
                                {
                                    TranslationObj = NewTranslation.TranslateSentence(_ingrLangOriginal.RecipeIngredientNote);
                                    _ingrLangTranslated.RecipeIngredientNote = TranslationObj.TranslatedSentence;
                                }

                                ManageUSPReturnValue _result = _ingrLangTranslated.Save();

                                if (!_result.IsError)
                                {
                                    lblResult.Text += _ingrLangOriginal.RecipeIngredientNote + " | " + _ingrLangOriginal.RecipeIngredientGroupName + "... ==> " + _ingrLangTranslated.RecipeIngredientNote + " | " + _ingrLangTranslated.RecipeIngredientGroupName + "...<br/>";
                                }
                                else
                                {
                                    lblResult.Text += "ERROR  ==> " + _ingrLangOriginal.RecipeIngredientNote + " | " + _ingrLangOriginal.RecipeIngredientGroupName + "... - " + _result.ResultExecutionCode + "<br/>";
                                }

                            }
                            catch (Exception ex)
                            {
                                lblResult.Text = "ERROR ==> " + ex.Message;
                            }
                        }
                    }
                    else
                    {
                        lblResult.Text = "No Recipe Ingredient Notes ready for translation found";
                    }
                }
                catch (Exception ex)
                {
                    lblResult.Text = "ERROR ==> " + ex.Message;
                }
            }
            else
            {
                lblResult.Text = "ERROR ==> Configuration parameter missing, please specify IDLangFrom, IDLangTo and Number of object";
            }
        }

        protected string GetLangLabel(int IDLanguage)
        {
            string _return = "";
            switch (IDLanguage)
            {
                case 1:
                    _return = "en";
                    break;
                case 2:
                    _return = "it";
                    break;
                case 3:
                    _return = "es";
                    break;
                default:
                    _return = "en";
                    break;
            }
            return _return;
        }
    }
}