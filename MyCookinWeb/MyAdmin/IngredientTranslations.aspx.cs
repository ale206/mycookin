using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.ThirdPartAPI.MicrosoftTranslatorSdk;

namespace MyCookinWeb.MyAdmin
{
    public partial class IngredientTranslations : System.Web.UI.Page
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
        protected void btnStartTranslate_Click(object sender, EventArgs e)
        {
            if (txtFromIDLanguage.Text != "" || txtNumIngredients.Text != "" || txtToIDLanguage.Text != "")
            {
                try
                {
                    int _idLangFrom = MyConvert.ToInt32(txtFromIDLanguage.Text, 2);
                    int _idLangTo = MyConvert.ToInt32(txtToIDLanguage.Text, 1);
                    string _langFrom = GetLangLabel(_idLangFrom);
                    string _langTo = GetLangLabel(_idLangTo);
                    lblResult.Text = "";
                    DataTable dtIngrToTranslate = IngredientLanguage.GetIngredientToTranslate(_idLangFrom, _idLangTo, MyConvert.ToInt32(txtNumIngredients.Text, 10));

                    if (dtIngrToTranslate.Rows.Count > 0)
                    {
                        MicrosoftTranslationAPI NewTranslation = new MicrosoftTranslationAPI(_langFrom, _langTo);

                        MicrosoftTranslationAPI TranslationObj = new MicrosoftTranslationAPI();

                        foreach (DataRow drIngr in dtIngrToTranslate.Rows)
                        {
                            try
                            {
                                IngredientLanguage _orginalIngrLang = new IngredientLanguage(drIngr.Field<Guid>("IDIngredient"), drIngr.Field<int>("IDLanguage"));
                                _orginalIngrLang.QueryIngredientLanguageInfo();
                                IngredientLanguage _TranslatedIngrLang = new IngredientLanguage(drIngr.Field<Guid>("IDIngredient"), _idLangTo);

                                TranslationObj = NewTranslation.TranslateSentence(_orginalIngrLang.IngredientSingular);
                                _TranslatedIngrLang.IngredientSingular = TranslationObj.TranslatedSentence;

                                TranslationObj = NewTranslation.TranslateSentence(_orginalIngrLang.IngredientPlural);
                                _TranslatedIngrLang.IngredientPlural = TranslationObj.TranslatedSentence;

                                TranslationObj = NewTranslation.TranslateSentence(_orginalIngrLang.IngredientDescription);
                                _TranslatedIngrLang.IngredientDescription = TranslationObj.TranslatedSentence;

                                _TranslatedIngrLang.isAutoTranslate = true;

                                ManageUSPReturnValue _result = _TranslatedIngrLang.SaveIngredientLanguage();
                                if (!_result.IsError)
                                {
                                    lblResult.Text += _orginalIngrLang.IngredientSingular + " ==> " + _TranslatedIngrLang.IngredientSingular + "<br/>";
                                }
                                else
                                {
                                    lblResult.Text += "ERROR ==> " + _orginalIngrLang.IngredientSingular + " - " + _result.ResultExecutionCode + "<br/>";
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
                        lblResult.Text = "No Ingredient ready for translation found";
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
