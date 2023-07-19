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
    public partial class CreateRecipe :  MyCookinWeb.Form.MyPageBase
    {
        int IDLanguage;

        protected void Page_Load(object sender, EventArgs e)
        {
            IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            Form.DefaultButton = null;
            try
            {
                hfAddPhotoText.Value = GetLocalResourceObject("hfAddPhotoText.Value").ToString();
            }
            catch
            {
            }
            if (!IsPostBack)
            {
                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                Form.DefaultButton = null;

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

                    ////Inizialize Uploader
                    MediaUploadConfig _uploadConfig = new MediaUploadConfig(MediaType.RecipePhoto);
                    multiup.SelectFilesText = hfAddPhotoText.Value;
                    multiup.UploadFilesText = "";
                    multiup.UploadConfig = MediaType.RecipePhoto;
                    multiup.UploadHandlerURL = "/Utilities/MultiUploadImageHandler.ashx";
                    multiup.MaxFileNumber = 1;
                    multiup.MaxFileSizeInMB = _uploadConfig.MaxSizeByte / 1024 / 1024;
                    multiup.AllowedFileTypes = "jpg,jpeg,png";
                    multiup.IDMediaOwner = Session["IDUser"].ToString();
                    multiup.LoadControl = true;
                    ////==============================
                    if (Request.QueryString["RecipeName"] != null)
                    {
                        txtRecipeName.Text = Request.QueryString["RecipeName"].ToString();
                    }
                }
            }
        }

        protected void FileUploaded(object sender, EventArgs e)
        {
            try
            {
                Guid _newRecipeGuid = Guid.NewGuid();
                RecipeLanguage _newRecipe = new RecipeLanguage(_newRecipeGuid, Guid.NewGuid(), IDLanguage);
                _newRecipe.RecipeName = txtRecipeName.Text.Replace("/", "").Replace("\\", "").Replace("http", "").Replace("https", "");
                _newRecipe.RecipeImage = new Photo(new Guid(multiup.MediaCreatedIDs.Substring(0, multiup.MediaCreatedIDs.Length - 1)));
                _newRecipe.RecipeLanguageAutoTranslate = false;
                _newRecipe.RecipeHistory = "";
                _newRecipe.RecipeHistoryDate = null;
                _newRecipe.RecipeNote = "";
                _newRecipe.RecipeSuggestion = "";
                _newRecipe.RecipeDisabled = false;
                _newRecipe.RecipeFather = null;
                _newRecipe.Owner = new Guid(Session["IDUser"].ToString());
                _newRecipe.NumberOfPerson = 4;
                _newRecipe.PreparationTimeMinute = 0;
                _newRecipe.CookingTimeMinute = 0;
                _newRecipe.RecipeDifficulties = RecipeInfo.Difficulties.Medium;
                _newRecipe.RecipeVideo = null;
                _newRecipe.IDCity = 0;
                _newRecipe.LastUpdate = DateTime.UtcNow;
                _newRecipe.UpdatedByUser = new Guid(Session["IDUser"].ToString());
                _newRecipe.RecipeConsulted = 0;
                _newRecipe.isStarterRecipe = false;
                _newRecipe.CreationDate = DateTime.UtcNow;
                _newRecipe.DeletedOn = null;
                _newRecipe.BaseRecipe = false;
                _newRecipe.RecipeEnabled = true;
                _newRecipe.HotSpicy = false;
                _newRecipe.Checked = true;
                _newRecipe.RecipeCompletePerc = 10;
                _newRecipe.OriginalVersion = true;
                _newRecipe.Draft = true;
                _newRecipe.RecipeRated = 0;
                try
                {
                    ManageUSPReturnValue _reult = _newRecipe.Save();
                    if (!_reult.IsError)
                    {
                        try
                        {
                            RecipeStep _step = new RecipeStep(_newRecipe.IDRecipeLanguage, 0, "", new Photo(new Guid()));
                            _step.Save();
                        }
                        catch
                        {
                        }
                        //Create the first property
                        try
                        {
                            RecipeProperty _prop = new RecipeProperty(11, IDLanguage);
                            RecipePropertyValue _propValue = new RecipePropertyValue(_newRecipe.IDRecipe, _prop, true);
                            _propValue.Save();
                        }
                        catch
                        {
                        }
                        //try
                        //{
                        //    //INSERT ACTION IN USER BOARD
                        //    UserBoard NewActionInUserBoard = new UserBoard(_newRecipe.UpdatedByUser.IDUser, null, ActionTypes.NewRecipe, _newRecipe.IDRecipe, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                        //    ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();

                        //}
                        //catch
                        //{
                        //}
                        Response.Redirect(("/Utilities/ImageCrop.aspx?IDMedia=" + multiup.MediaCreatedIDs.Substring(0, multiup.MediaCreatedIDs.Length - 1) + "&ReturnURL=" + "/RecipeMng/EditRecipes.aspx?IDRecipe=" + _newRecipeGuid.ToString() + "&MediaType=" + MediaType.RecipePhoto.ToString()).ToLower(), false);
                    }
                    else
                    {
                        string stgResult = "";
                        try
                        {
                            stgResult = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-ER-0007");
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error saving recipe on DB", "", "IDRecipe: " + _newRecipeGuid.ToString(), true, false);
                            LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                            LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                        }
                        catch
                        {
                        }
                        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + txtRecipeName.Text.Replace("'", "\\'") + "','" + stgResult + "');", true);
                    }
                }
                catch (Exception ex)
                {
                    string stgResult = "";
                    try
                    {
                        stgResult = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-ER-0007");
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "Error saving recipe on DB", ex.Message, "IDRecipe: " + _newRecipeGuid.ToString(), true, false);
                        LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    }
                    catch
                    {
                    }
                    ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + txtRecipeName.Text.Replace("'", "\\'") + "','" + stgResult + "');", true);
                }
            }
            catch (Exception ex)
            {
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
    }
}
