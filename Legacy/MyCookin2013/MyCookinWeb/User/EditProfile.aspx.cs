using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Log;
using MyCookinWeb.Form;
using System.Drawing;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.CityManager;
using MyCookin.ObjectManager.SocialAction;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.ObjectManager.MyUserNotificationManager;

namespace MyCookinWeb.User
{
    public partial class ProfileProperties :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid IDUserGuid = new Guid();
            int IDLanguage = 1;
            
            NavHistoryClear();
            //If not logged go to Login
            //*****************************
            if (!MyUser.CheckUserLogged())
            {
                Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl()).ToLower(), true);
            }
            else
            {
                IDUserGuid = new Guid(Session["IDUser"].ToString());

                IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
            }
            //*****************************

            //No default button
            Page.Form.DefaultButton = null;

            //Load Panel Notifications Settings
            LoadNotificationSettingPanel();
            
            
            //==============================
            
            if (!IsPostBack)
            {
                MediaUploadConfig _uploadConfig = new MediaUploadConfig(MediaType.ProfileImagePhoto);
                //Inizialize Uploader
                multiup.SelectFilesText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1), "US-IN-0069");
                multiup.UploadFilesText = "";
                multiup.UploadConfig = MediaType.ProfileImagePhoto;
                multiup.UploadHandlerURL = "/Utilities/MultiUploadImageHandler.ashx";
                multiup.MaxFileNumber = 1;
                multiup.MaxFileSizeInMB = _uploadConfig.MaxSizeMegaByte;
                multiup.AllowedFileTypes = "jpg,jpeg,png";
                multiup.IDMediaOwner = Session["IDUser"].ToString();
                multiup.LoadControl = true;
                //Show ValidationSummary in JQuery BoxDialog
                string SummaryBoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ValidationSummaryInBoxDialog('" + btnChangePassword.ClientID + "', '" + vsChangePassword.ClientID + "', '" + pnlResult.ClientID + "', '" + SummaryBoxTitle + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ValidationSummaryInBoxDialog('" + btnMyAccountUpdate.ClientID + "', '" + vsEditProfile.ClientID + "', '" + pnlResult.ClientID + "', '" + SummaryBoxTitle + "');", true);

                //Register JQueryUi Calendar
                MyCulture Culture = new MyCulture(IDLanguage);
                string LanguageCode = Culture.GetCurrentLanguageCodeByID();

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "AdultCalendar('" + txtBirthDate.ClientID + "', '" + LanguageCode + "');", true);

                #region FillFieldsForStaticForms
                try
                {
                    if (!IsPostBack)
                    {
                        //Fill Fields for UserInfo table
                        MyUser InfoUser = new MyUser(IDUserGuid);
                        InfoUser.GetUserInfoAllByID();

                        //Dynamic DropDownList For Security Questions From DB
                        ddlSecurityQuestion.DataSource = InfoUser.GetSecurityQuestions();

                        ddlSecurityQuestion.DataTextField = "SecurityQuestion";
                        ddlSecurityQuestion.DataValueField = "IDSecurityQuestion";

                        ddlSecurityQuestion.DataBind();

                        //If SecurityQuestion is set, select it
                        try
                        {
                            ddlSecurityQuestion.Items.FindByValue(InfoUser.IDSecurityQuestion.ToString()).Selected = true;
                        }
                        catch
                        {
                        }

                        //Will be visible on button click
                        pnlSecurityQuestionAndAnswer.Visible = false;

                        //Dynamic DropDownList For Gender From DB
                        #region DdlGender
                        ddlGender.DataSource = InfoUser.GetGender();

                        ddlGender.DataValueField = "IDGender";
                        ddlGender.DataTextField = "Gender";

                        ddlGender.DataBind();

                        //If Gender is set, select it
                        try
                        {
                            ddlGender.Items.FindByValue(InfoUser.IDGender.ToString()).Selected = true;
                        }
                        catch
                        {
                        }
                        #endregion

                        //Dynamic DropDownList For Gender From DB
                        #region DdlLanguage
                        ddlLanguage.DataSource = InfoUser.GetLanguageList();

                        ddlLanguage.DataValueField = "IDLanguage";
                        ddlLanguage.DataTextField = "Language";

                        ddlLanguage.DataBind();

                        //If Gender is set, select it
                        try
                        {
                            ddlLanguage.Items.FindByValue(InfoUser.IDLanguage.ToString()).Selected = true;
                        }
                        catch
                        {
                        }
                        #endregion

                        DateTime ShortBirthDay = Convert.ToDateTime(InfoUser.BirthDate);

                        txtBirthDate.Text = ShortBirthDay.ToShortDateString();
                        txtMobile.Text = InfoUser.Mobile;
                        txtName.Text = InfoUser.Name;
                        txtSurname.Text = InfoUser.Surname;
                        txtUserName.Text = InfoUser.UserName;
                        multiup.BaseFileName = InfoUser.UserName;
                        txtCurrentUsername.Text = InfoUser.UserName;

                        //Inizialize to Available textbox for check username existence
                        txtCheckUsername.Text = "available";

                        if (InfoUser.IDProfilePhoto != null)
                        {
                            imgProfile.IDMedia = InfoUser.IDProfilePhoto.IDMedia.ToString();
                            imgProfile.MediaType = MediaType.ProfileImagePhoto;
                            imgProfile.ImageHeight = 150;
                            imgProfile.ImageWidth = 150;
                            imgProfile.EnableEditing = true;
                        }

                        #region City

                        MyCulture UserCulture = new MyCulture((int)InfoUser.IDLanguage);
                        cacCity.MethodName = "/City/FindCity.asmx/SearchCities";
                        cacCity.LanguageCode = UserCulture.GetCurrentLanguageCodeByID();
                        cacCity.ObjectLabelIdentifier = "CityName";
                        cacCity.ObjectIDIdentifier = "IDCity";
                        cacCity.ObjectLabelText = "";
                        cacCity.LangFieldLabel = "LangCode";
                        cacCity.WordFieldLabel = "words";

                        if (InfoUser.IDCity != null)
                        {
                            City CityManager = new City((int)InfoUser.IDCity);

                            txtCityID.Text = InfoUser.IDCity.ToString();

                            cacCity.SelectedObject = CityManager.CityName;
                        }
                        #endregion

                        #region ChangePassword
                        //if user has set to null LastPasswordChange, the user has registered via social network and the password is a random psw.
                        //the user can't know the psw, so can't complete the field OldPassword.
                        //Then we hide it and we give the possibility to insert the first "real" password
                        if (InfoUser.LastPasswordChange == null)
                        {
                            lblOldPassword.Visible = false;
                            txtOldPassword.Visible = false;
                            rfvOldPassword.Visible = false;
                            rfvOldPassword.Enabled = false;
                        }
                        #endregion

                        #region Cooker - DISABLED FOR NOW
                        //If user is already a Cooker, hide Panel to Register him as cooker.
                        //if (InfoUser.IsACooker())
                        //{
                        //    pnlBecomeCookDescription.Visible = false;
                        //    pnlCookInformations.Visible = true;

                        //    //Fill Cooker Form
                        //    if (InfoUser.CookAtHome == true)
                        //    {
                        //        chkCookAtHome.Checked = true;
                        //    }

                        //    if (InfoUser.CookInRestaurant == true)
                        //    {
                        //        chkCookInRestaurant.Checked = true;
                        //    }

                        //    if (InfoUser.IsProfessionalCook == true)
                        //    {
                        //        chkProfessionalCook.Checked = true;
                        //    }

                        //    txtCookDescription.Text = InfoUser.CookDescription;

                        //}
                        //else //else show panel to update info as cooker 
                        //{
                        //    pnlBecomeCookDescription.Visible = true;
                        //    pnlCookInformations.Visible = false;
                        //}
                        #endregion

                        #region SocialPanel
                        //Check if the user has connected his social. If not, show the panel
                        if (MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Facebook))
                        {
                            facebook_login.Visible = false;
                        }

                        if (MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Google))
                        {
                            google_login.Visible = false;
                        }

                        if (MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Twitter))
                        {
                            twitter_login.Visible = false;
                        }

                        //If all button are NOT visible, hide lable Connect Social
                        if (!(facebook_login.Visible) && !(google_login.Visible) && !(twitter_login.Visible))
                        {
                            lblSocialConnect.Visible = false;
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    string ErrorMessage = ex.Message;

                    //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error Creating Form in Edit Profile: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                    }
                    catch { }
                }

                #endregion
            }
            else
            {
                multiup.ResetMultiUpload();
            }

            #region DynamicFormGeneratorAndButtonAddEvents
            try
            {
                #region DynamicFormGenerator
                //Fill the form dinamically
                DynamicFormGenerator PersonalInformationForm = new DynamicFormGenerator(this, IDLanguage, 1, upnPersonalInfo, "USER", Session["IDUser"].ToString());
                PersonalInformationForm.FillPanel();

                //DynamicFormGenerator OtherInformationForm = new DynamicFormGenerator(this, IDLanguage, 2, upnOtherInformations, "USER", Session["IDUser"].ToString());
                //OtherInformationForm.FillPanel();

                DynamicFormGenerator SocialIntegrationsForm = new DynamicFormGenerator(this, IDLanguage, 4, upnSocialIntegrations, "USER", Session["IDUser"].ToString());
                SocialIntegrationsForm.FillPanel();
                #endregion

                #region DynamicButtonAddEvents
                //Find Buttons to add Events
                //N.B.: First, add Methods like btn_[UpdatePanelID]_Click()
                Button PersonalInfoButton = (Button)upnPersonalInfo.FindControl("btn_upnPersonalInfo");
                PersonalInfoButton.Click += new EventHandler(btn_upnPersonalInfo_Click);

                //Button OtherInformationsButton = (Button)upnOtherInformations.FindControl("btn_upnOtherInformations");
                //OtherInformationsButton.Click += new EventHandler(btn_upnOtherInformations_Click);

                Button SocialIntegrationsButton = (Button)upnSocialIntegrations.FindControl("btn_upnSocialIntegrations");
                SocialIntegrationsButton.Click += new EventHandler(btn_upnSocialIntegrations_Click);
                #endregion
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error Filling the form dinamically in Edit Profile: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
            #endregion
        }

        #region LoadNotificationSettingPanel
        private void LoadNotificationSettingPanel()
        {
            try
            {
                MyUserNotification Notification = new MyUserNotification(new Guid(Session["IDUser"].ToString()), Convert.ToInt32(Session["IDLanguage"].ToString()));
                List<MyUserNotification> NotificationsList = new List<MyUserNotification>();

                NotificationsList = Notification.GetNotificationList();

                foreach (MyUserNotification not in NotificationsList)
                {
                    Panel NotificationContainer = new Panel();
                    NotificationContainer.ID = "pnlNotificationContainer_" + (int)not.IDUserNotificationType;
                    NotificationContainer.ClientIDMode = ClientIDMode.Static;

                    Label lblQuestion = new Label();
                    lblQuestion.Text = not.NotificationQuestion;
                    lblQuestion.ID = "lblQuestion_" + not.IDUserNotificationType;
                    lblQuestion.ClientIDMode = ClientIDMode.Static;

                    CheckBox chkNotificationSetting = new CheckBox();
                    chkNotificationSetting.Checked = not.IsEnabled;
                    chkNotificationSetting.ID = "chkNotificationSetting_" + not.IDUserNotificationType;
                    chkNotificationSetting.ClientIDMode = ClientIDMode.Static;

                    pnlNotifications.Controls.Add(NotificationContainer);
                    NotificationContainer.Controls.Add(chkNotificationSetting);
                    NotificationContainer.Controls.Add(lblQuestion);

                }
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on Load Notification Setting Panel: " + ex.Message, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
            
        }
        #endregion

        #region StaticButtons
        
        #region ButtonBecomeCooker
        /// <summary>
        /// Button to become Cooker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBecomeCook_Click(object sender, EventArgs e)
        {
            int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            try
            {
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                MyUser User = new MyUser(IDUserGuid);
                User.GetUserBasicInfoByID();

                if (User.RegisterAsCooker())
                {
                    //Show JQueryUi BoxDialog
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0012");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0012");
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //Disabled for now
                    //pnlBecomeCookDescription.Visible = false;
                    //pnlCookInformations.Visible = true;

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_UserBecomeCook, "User Become Cook", Network.GetCurrentPageName(), "", "");
                        NewStatisticUser.InsertNewRow();
                    }
                    catch { }
                }
                else
                {
                    //Show JQueryUi BoxDialog
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Insert New Cooker: check MyUser.cs -> RegisterAsCooker()", Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                    }
                    catch { }
                }

            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Show JQueryUi BoxDialog
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Insert New Cooker: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region MyAccountUpdate
        /// <summary>
        /// Update MyAccount User Informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMyAccountUpdate_Click(object sender, EventArgs e)
        {
            int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            try
            {
                cacCity.Enabled = false;

                //Guid Id User From Session
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                MyUser UserInfo = new MyUser(IDUserGuid);

                //Get All user informations
                UserInfo.GetUserInfoAllByID();

                //Change with modified fields - Override Some UserInformations 
                UserInfo.Name = txtName.Text;
                UserInfo.Surname = txtSurname.Text;
                UserInfo.BirthDate = Convert.ToDateTime(txtBirthDate.Text);
                UserInfo.Mobile = txtMobile.Text;
                
                UserInfo.UserName = txtUserName.Text;
                UserInfo.IDLanguage = MyConvert.ToInt32(ddlLanguage.SelectedValue, 1);

                Session["IDLanguage"] = UserInfo.IDLanguage;

                //If a user don't write city, the Convert will give error. Then, set to null.
                try
                {
                    UserInfo.IDCity = Convert.ToInt32(cacCity.SelectedObjectID);
                }
                catch
                {
                    UserInfo.IDCity = null;
                }

                //Memorize currently Question And Answer (To not rewrite to null in case of update but not selected)
                string CurrentAnswer = UserInfo.SecurityAnswer;

                try
                {
                    int CurrentIDQuestion = (int)UserInfo.IDSecurityQuestion;
                }
                catch
                { 
                }

                //txtSecurityAnswer And ddlSecurityQuestion could be hidden or empty. If not, overwrite with the new
                try
                {
                    if (!String.IsNullOrEmpty(txtSecurityAnswer.Text))
                    {
                        int NewIDSecurityQuestion = Convert.ToInt32(ddlSecurityQuestion.SelectedValue);

                        string SecurityAnswerHash = MySecurity.GenerateSHA1Hash(txtSecurityAnswer.Text);
                        UserInfo.UpdateSecurityAnswer(NewIDSecurityQuestion, SecurityAnswerHash);
                    }
                }
                catch
                {
                }
                
                //Gender Could be NULL, if so Convert.toint32 generates error.
                try
                {
                    UserInfo.IDGender = Convert.ToInt32(ddlGender.SelectedValue);
                }
                catch
                {
                    UserInfo.IDGender = null;
                }
                
                //Other User fields

                //UserInfo.eMail = txtMail.Text;
                //UserInfo.PasswordHash = MySecurity.GenerateSHA1Hash(txtPassword.Text.ToString());
                //UserInfo.BirthDate = Convert.ToDateTime(txtBirthDate.Text);
                //UserInfo.ContractSigned = chkContractSigned.Checked;
                //UserInfo.LastIpAddress = HttpContext.Current.Request.UserHostAddress;
                //UserInfo.LastPasswordChange = DateTime.UtcNow;
                //UserInfo.PasswordExpireOn = DateTime.UtcNow.AddYears(3);
                //UserInfo.ChangePasswordNextLogon = false;
                //UserInfo.UserEnabled = false;
                //UserInfo.UserLocked = true;
                //UserInfo.MantainanceMode = false;
                //UserInfo.DateMembership = DateTime.UtcNow;
                //UserInfo.AccountExpireOn = DateTime.UtcNow.AddMonths(1);   //Is set to 1 month until user confirm registration. Then, increase of years.
                //UserInfo.LastLogon = DateTime.UtcNow;
                //UserInfo.ContractSigned = null;
                //UserInfo.UserDomain = null;
                //UserInfo.UserType = null;
                //UserInfo.MailConfirmedOn = null;
                //UserInfo.MobileConfirmedOn = null;
                //UserInfo.MobileConfirmationCode = null;
                //UserInfo.IDCity = null;
                //UserInfo.IDProfilePhoto = null;
                //UserInfo.LastProfileUpdate = null;
                //UserInfo.UserIsOnLine = null;
                //UserInfo.IDVisibility = null;
                //UserInfo.AccountDeletedOn = null;

                //Update User Informations
                UserInfo.UpdateUserInfo();


                //INSERT ACTION IN USER BOARD
                UserBoard NewActionInUserBoard = new UserBoard(UserInfo.IDUser, null, ActionTypes.UserProfileUpdated, null, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();

                Guid IDUserAction = new Guid(InsertActionResult.USPReturnValue.ToString());

                /* 
                 * NON CANCELLARE!!!!!!!!!
                 * 
                 * 
                 * PER SAVERIO: DA INSERIRE NELLA PARTE DELLE RICETTE:
                 * quando ci sarà la parte per inserire una nuova ricetta, questa servirà per eventualmente condividere automaticamente sui social
                 * 
                 * 
                 * 
                #region AUTOSHARE
                MyUserPropertyCompiled propComp = new MyUserPropertyCompiled(new Guid(Session["IDUser"].ToString()), 5, IDLanguage);
                if (propComp.PropertyValue.IndexOf("Facebook") > -1)
                {
                    //Check if auto share on Facebook

                    if (MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Faceboook))
                    {
                        //Get IDUserSocial And AccessToken
                        //*****************************************
                        MyUserSocial UserSocialInfo = new MyUserSocial(IDUserGuid, (int)SocialNetwork.Faceboook);
                        UserSocialInfo.GetUserSocialInformations();

                        //UserSocialInfo.IDUserSocial;
                        //UserSocialInfo.AccessToken;
                        //*****************************************

                        //Get UserBoard Action Info - To know what kind of object is
                        UserBoard UserBoardActionInfo = new UserBoard(IDUserAction);
                        List<UserBoard> UserBoardActionInfoList = new List<UserBoard>();

                        UserBoardActionInfoList = UserBoardActionInfo.GetUserActionInfo();

                        Guid? IDActionRelatedObject = new Guid();
                        try
                        {
                            IDActionRelatedObject = new Guid(UserBoardActionInfoList[0].IDActionRelatedObject.ToString());
                        }
                        catch
                        {
                            IDActionRelatedObject = null;
                        }

                        //Get info according to IDUserAction
                        SocialAction NewFBAction = new SocialAction(UserSocialInfo.IDUserSocial, UserSocialInfo.AccessToken, UserSocialInfo.RefreshToken, IDActionRelatedObject, UserBoardActionInfoList[0].IDUserActionType, IDUserAction);

                        string IDPost = NewFBAction.FB_PostOnWall();

                        //In this case is better don't show error.

                        //Error in publish: user removed authorization
                        //if (String.IsNullOrEmpty(IDPost))
                        //{
                        //    int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                            //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                            //string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                            //string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-0012");
                            //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
                        //}
                    }
                }
                else if (propComp.PropertyValue.IndexOf("Twitter") > -1)
                {
                    if (MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Twitter))
                    {
                        //Check if auto share on Twitter
                        //Get IDUserSocial And AccessToken
                        //*****************************************
                        MyUserSocial UserSocialInfo = new MyUserSocial(IDUserGuid, (int)SocialNetwork.Twitter);
                        UserSocialInfo.GetUserSocialInformations();

                        //UserSocialInfo.IDUserSocial;
                        //UserSocialInfo.AccessToken;
                        //*****************************************

                        //Get UserBoard Action Info - To know what kind of object is
                        UserBoard UserBoardActionInfo = new UserBoard(IDUserAction);
                        List<UserBoard> UserBoardActionInfoList = new List<UserBoard>();

                        UserBoardActionInfoList = UserBoardActionInfo.GetUserActionInfo();

                        Guid? IDActionRelatedObject = new Guid();
                        try
                        {
                            IDActionRelatedObject = new Guid(UserBoardActionInfoList[0].IDActionRelatedObject.ToString());
                        }
                        catch
                        {
                            IDActionRelatedObject = null;
                        }

                        //Get info according to IDUserAction
                        SocialAction NewTWAction = new SocialAction(UserSocialInfo.IDUserSocial, UserSocialInfo.AccessToken, UserSocialInfo.RefreshToken, IDActionRelatedObject, UserBoardActionInfoList[0].IDUserActionType, IDUserAction);

                        NewTWAction.TW_tweet();
                    }
                }
                #endregion
                */
                


                //Reconfirm Mobile.....
                //if (!UserInfo.Mobile.Equals(txtMobile.Text))
                //{
                //    //.......................
                //}



                //Show JQueryUi BoxDialog
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0011");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_UpdateProfile, "User update his profile", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Show JQueryUi BoxDialog
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Update Profile: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ChangePassword
        /// <summary>
        /// Change User Password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                //Guid Id User From Session
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                MyUser User = new MyUser(IDUserGuid);
                User.GetUserInfoAllByID();

                //An user that registered via Social Network doesn't have a password set, it is random
                //LastUpdatePassword is set to null
                bool CanChangePassword = false;

                if (User.LastPasswordChange == null)
                {
                    //OldPasswordNOTExist
                    CanChangePassword = true;
                }
                else
                {
                    //OldPasswordExist
                    string OldPasswordHash = MySecurity.GenerateSHA1Hash(txtOldPassword.Text.ToString());

                    if (OldPasswordIsCorrect(OldPasswordHash, User, IDLanguage))
                    {
                        CanChangePassword = true;
                    }
                }

                //Change Password wether control above is ok
                if (CanChangePassword)
                {
                    try
                    {
                        //If correct, Generate Hash and Update Password
                        string NewPasswordHash = MySecurity.GenerateSHA1Hash(txtPassword1.Text.ToString());

                        User.UpdatePassword(NewPasswordHash);

                        //WRITE A ROW IN STATISTICS DB
                        try
                        {
                            MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_ChangePassword, "User changed his password", Network.GetCurrentPageName(), "", "");
                            NewStatisticUser.InsertNewRow();
                        }
                        catch { }

                        //Do Logout
                        Response.Redirect((AppConfig.GetValue("LogoutPage", AppDomain.CurrentDomain) + "?requestedPage=" + AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain)).ToLower(), true);

                    }
                    catch (Exception ex)
                    {
                        //Error during Update Password
                        string ErrorMessage = ex.Message;

                        //WRITE A ROW IN LOG FILE AND DB
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0006", "Error in Update Password: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
                            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                            LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                        }
                        catch { }

                        //Show JQueryUi BoxDialog
                        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-0006");
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Change Password: " + ex.Message, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }

                //Show JQueryUi BoxDialog
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-0001");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
            }
        }
        #endregion

        #region OldPasswordIsCorrect
        /// <summary>
        /// Check if the Old Password insered in textbox is correct with that memorized in db
        /// </summary>
        /// <param name="OldPasswordHash"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        protected bool OldPasswordIsCorrect(string OldPasswordHash, MyUser User, int IDLanguage)
        {
            if (OldPasswordHash.Equals(User.PasswordHash))
            {
                return true;
            }
            else
            {
                //Old Password is not correct

                //Show JQueryUi BoxDialog
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-0006");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-0006");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(User.IDUser, null, StatisticsActionType.US_WrongPasswordInserted, "User has Wrong writing Old Password", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }

                return false;
            }
        }
        #endregion

        #region FacebookButton
        protected void btnFacebook_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
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
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on FB Button: " + ex.Message, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region DeleteAccount
        /// <summary>
        /// Delete User Account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                //Guid Id User From Session
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
                MyUser User = new MyUser(IDUserGuid);

                if (User.DeleteAccount())
                {
                    //Account Deleted

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_AccountDeleted, "Account Deleted", Network.GetCurrentPageName(), "", "");
                        NewStatisticUser.InsertNewRow();
                    }
                    catch { }

                    //Do Logout
                    Response.Redirect((AppConfig.GetValue("LogoutPage", AppDomain.CurrentDomain) + "?requestedPage=" + AppConfig.GetValue("HomePage", AppDomain.CurrentDomain)).ToLower(), true);

                }
                else
                {
                    //Error in Account Delete

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0006", "Error in Account Delete", Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                        LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    }
                    catch { }

                    //Show JQueryUi BoxDialog
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-0001");
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on Delete Account: " + ex.Message, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnUpdateNotificationSetting
        protected void btnUpdateNotificationSetting_Click(object sender, EventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                //Guid Id User From Session
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());


                MyUserNotification Notification = new MyUserNotification(IDUserGuid, Convert.ToInt32(Session["IDLanguage"].ToString()));
                List<MyUserNotification> NotificationsList = new List<MyUserNotification>();

                NotificationsList = Notification.GetNotificationList();

                foreach (MyUserNotification not in NotificationsList)
                {

                    string IDPanel = "pnlNotificationContainer_" + (int)not.IDUserNotificationType;
                    Panel _updatePanelID = pnlNotifications.FindControl(IDPanel) as Panel;
                    
                    string IDCheckBox = "chkNotificationSetting_" + not.IDUserNotificationType;
                    CheckBox chkNotificationSetting = _updatePanelID.FindControl(IDCheckBox) as CheckBox;

                    MyUserNotification NotificationUpdate = new MyUserNotification(IDUserGuid, not.IDUserNotification, chkNotificationSetting.Checked);

                    NotificationUpdate.UpdateUserNotificationSetting();
                }

                //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0011");

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on Update Notification Setting: " + ex.Message, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #endregion

        #region DynamicButtons
        //Dynamic Button's ID will be something like btn_[UpdatePanelID]
        //Dynamic Button's EVENT will be something like btn_[UpdatePanelID]_Click() - Add this event on pageLoad (look at region #DynamicButtonAddEvents)

        #region ButtonPersonalInfo
        protected void btn_upnPersonalInfo_Click(object sender, EventArgs e)
        {
            int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            try
            {
                //Insert/Update PersonalInfo Panel
                DynamicFormGenerator PersonalInformationForm = new DynamicFormGenerator(this, IDLanguage, 1, upnPersonalInfo, "USER", Session["IDUser"].ToString());
                PersonalInformationForm.GetInfoUserFormElements(true);

                //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0011");
                
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Update Personal Info: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ButtonOtherInformations
        //protected void btn_upnOtherInformations_Click(object sender, EventArgs e)
        //{
        //    int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

        //    try
        //    {
        //        DynamicFormGenerator OtherInformationForm = new DynamicFormGenerator(this, IDLanguage, 2, upnOtherInformations, "USER", Session["IDUser"].ToString());
        //        OtherInformationForm.GetInfoUserFormElements(true);

        //        //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
        //        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
        //        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0011");
                
        //        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        string ErrorMessage = ex.Message;

        //        //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
        //        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
        //        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                
        //        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

        //        //WRITE A ROW IN LOG FILE AND DB
        //        try
        //        {
        //            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Update Profile Other Informations: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
        //            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //            LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
        //        }
        //        catch { }
        //    }
        //}
        #endregion

        #region ButtonSocialIntegrations
        protected void btn_upnSocialIntegrations_Click(object sender, EventArgs e)
        {
            int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

            try
            {
                DynamicFormGenerator SocialIntegrationsForm = new DynamicFormGenerator(this, IDLanguage, 4, upnSocialIntegrations, "USER", Session["IDUser"].ToString());
                SocialIntegrationsForm.GetInfoUserFormElements(true);

                //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0011");

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-WN-9999");
                string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Update Profile Other Informations: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ChangeAnswer
        protected void btnChangeAnswer_Click(object sender, ImageClickEventArgs e)
        {
            pnlSecurityQuestionAndAnswer.Visible = true; 
            btnChangeAnswer.Visible = false;
        }
        #endregion

        #endregion

        #region UpdateCookerInformations - DISABLED FOR NOW
        /// <summary>
        /// Update Cooker Informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnUpdateCookerInformations_Click(object sender, ImageClickEventArgs e)
        //{
        //    int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

        //    try
        //    {
        //        Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
        //        MyUser UserCook = new MyUser(IDUserGuid, chkProfessionalCook.Checked, chkCookInRestaurant.Checked, chkCookAtHome.Checked, txtCookDescription.Text.ToString());
        //        UserCook.GetUserBasicInfoByID();

        //        //Update Cooker Informations
        //        if (UserCook.UpdateCookerInformations())
        //        {
        //            //Show JQueryUi BoxDialog
        //            string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
        //            string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0011");
        //            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

        //            //WRITE A ROW IN STATISTICS DB
        //            try
        //            {
        //                MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_UserUpdateCookInformation, "User Update Cook Informations", Network.GetCurrentPageName(), "", "");
        //                NewStatisticUser.InsertNewRow();
        //            }
        //            catch { }
        //        }
        //        else
        //        {
        //            //Show JQueryUi BoxDialog
        //            string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
        //            string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
        //            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

        //            //WRITE A ROW IN LOG FILE AND DB
        //            try
        //            {
        //                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Update Cook Informations: check MyUser.cs -> UpdateCookerInformations()", Session["IDUser"].ToString(), true, false);
        //                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //                LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
        //            }
        //            catch { }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string ErrorMessage = ex.Message;

        //        //Show JQueryUi BoxDialog
        //        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
        //        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
        //        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

        //        //WRITE A ROW IN LOG FILE AND DB
        //        try
        //        {
        //            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Update Cook Informations: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
        //            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //            LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
        //        }
        //        catch { }
        //    }
        //}
        #endregion

        #region ButtonRemoveAsCooker - DISABLED FOR NOW
        /// <summary>
        /// Button to Remove User as Cooker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnNoMoreCook_Click(object sender, EventArgs e)
        //{
        //    int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

        //    try
        //    {
        //        Guid IDUserGuid = new Guid(Session["IDUser"].ToString());
        //        MyUser User = new MyUser(IDUserGuid);
        //        User.GetUserBasicInfoByID();

        //        if (User.RemoveAsCooker())
        //        {
        //            //WRITE A ROW IN STATISTICS DB
        //            MyStatistics NewStatisticUser = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_UserNoMoreCook, "User is not more a cook", Network.GetCurrentPageName(), "", "");
        //            NewStatisticUser.InsertNewRow();

        //            //Show JQueryUi BoxDialog With Redirect
        //            string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
        //            string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0013");
        //            string RedirectUrl = "EditProfile.aspx";
        //            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

        //            pnlBecomeCookDescription.Visible = true;
        //            pnlCookInformations.Visible = false;

        //        }
        //        else
        //        {
        //            //Show JQueryUi BoxDialog
        //            string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
        //            string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
        //            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

        //            //WRITE A ROW IN LOG FILE AND DB
        //            try
        //            {
        //                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Remove information that user is a Cooker: check MyUser.cs -> RemoveAsCooker()", Session["IDUser"].ToString(), true, false);
        //                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //                LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
        //            }
        //            catch { }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string ErrorMessage = ex.Message;

        //        //Show JQueryUi BoxDialog
        //        string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
        //        string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
        //        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);

        //        //WRITE A ROW IN LOG FILE AND DB
        //        try
        //        {
        //            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Remove Cook: " + ErrorMessage, Session["IDUser"].ToString(), true, false);
        //            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
        //            LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
        //        }
        //        catch { }
        //    }
        //}
        #endregion

        protected void FileUploaded(object sender, EventArgs e)
        {
            try
            {
                lblGeneralUploadError.Visible = false;
                MyUser _user = new MyUser(new Guid(Session["IDUser"].ToString()));
                _user.GetUserInfoAllByID();
                if (_user.IDProfilePhoto != null)
                {
                    _user.IDProfilePhoto.QueryMediaInfo();
                    _user.IDProfilePhoto.DeletePhoto();
                }
                _user.IDProfilePhoto = new Photo(new Guid(multiup.MediaCreatedIDs.Substring(0, multiup.MediaCreatedIDs.Length - 1)));
                _user.UpdateUserInfo();
                Response.Redirect(("/Utilities/ImageCrop.aspx?IDMedia=" + multiup.MediaCreatedIDs.Substring(0, multiup.MediaCreatedIDs.Length - 1) + "&ReturnURL=" + Request.RawUrl + "&MediaType=" + MediaType.ProfileImagePhoto.ToString()).ToLower(), true);
            }
            catch
            {
                lblGeneralUploadError.Visible = true;
            }
                
        }
    }
}
