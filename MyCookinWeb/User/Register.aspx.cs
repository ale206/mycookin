using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ErrorAndMessage;
using MyCookin.Log;
using System.Net;
using System.IO;
using System.Text;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.ObjectManager.UserBoardManager;

namespace MyCookinWeb.UserInfo
{
    public partial class Register :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //If we are logged, do not enter in this page
                if (MyUser.CheckUserLogged())
                {
                    Response.Redirect((AppConfig.GetValue("HomePage", AppDomain.CurrentDomain)).ToLower(), true);
                }

                //Set default button
                Page.Form.DefaultButton = lbtnRegister.UniqueID.ToString();

                #region FacebookRegistrationOrLogin
                //if (Request.QueryString["code"] != null)
                if (!String.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string code = Request.QueryString["code"];

                    string callBackUrl = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "");

                    //Client Id Which YOU Got when you Register You Application
                    string FacebookClientId = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);

                    WebRequest request = WebRequest.Create(string.Format("https://graph.facebook.com/oauth/" +
                    "access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                    FacebookClientId, callBackUrl, AppConfig.GetValue("facebook_appsecret", AppDomain.CurrentDomain), code));

                    WebResponse response = request.GetResponse();

                    Stream stream = response.GetResponseStream();
                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader streamReader = new StreamReader(stream, encode);
                    string accessToken = streamReader.ReadToEnd().Replace("access_token=", "");
                    streamReader.Close();
                    response.Close();

                    string url = "/Auth/Auth.aspx?fbaccessToken=" + accessToken;
                    Response.Redirect(url, true);
                }
                #endregion

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowSocialButtons();", true);

                hfIDLanguage.Value = Session["IDLanguage"].ToString();

                if (!IsPostBack)
                {
                    //Show ValidationSummary in JQuery BoxDialog
                    //string SummaryBoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ValidationSummaryInBoxDialog('" + lbtnRegister.ClientID + "', '" + vsRegister.ClientID + "', '" + pnlResult.ClientID + "', '" + SummaryBoxTitle + "');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ValidationSummaryWithTipsyTooltip('" + lbtnRegister.ClientID + "', '" + vsRegister.ClientID + "', '" + pnlResult.ClientID + "', '" + SummaryBoxTitle + "');", true);
                    
                    //Register JQueryUi Calendar
                    MyCulture SessionCulture = new MyCulture(MyConvert.ToInt32(Session["IDLanguage"].ToString(),1));
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "AdultCalendar('" + txtBirthdate.ClientID + "', '" + SessionCulture.GetCurrentLanguageCodeByID() + "');", true);

                    //Focus on the first TextBox
                    txtName.Focus();

                    //Hide Scrollbar
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "HideScrollbars();", true);

                    //To don't lose password written in the field
                    if (IsPostBack && txtPassword.Text != string.Empty)
                    {
                        txtPassword.Attributes["value"] = txtPassword.Text;
                    }
                }
                else
                {
                    //Social Tabs 
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), "$('#pnlMainTab').tabs({ fx: { height: 'toggle', opacity: 'toggle' } }); ", true);
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on Register User Page: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, true, NewRow);
                }
                catch { }
            }
        }

        #region btnRegister_Click
        protected void lbtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                //Disable Button to avoid double click.
                lbtnRegister.Enabled = false;

                //Get current User IP
                string CurrentIp = HttpContext.Current.Request.UserHostAddress;

                MyUser newUser = new MyUser();

                newUser.Name = txtName.Text;
                newUser.Surname = txtSurname.Text;
                newUser.UserName = txtUsername.Text;
                newUser.eMail = txtEmail.Text;
                newUser.PasswordHash = MySecurity.GenerateSHA1Hash(txtPassword.Text.ToString());
                newUser.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                    
                //Use this if enable checkbox for Accept Conditions
                //newUser.ContractSigned = chkContractSigned.Checked;
                newUser.ContractSigned = true;

                newUser.IDLanguage = MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1);
                newUser.LastIpAddress = HttpContext.Current.Request.UserHostAddress;

                //In this case we set this date to know that the user know his psw. 
                //For Registrations via social, we can not memorize user password, so we will let this empty.
                newUser.LastPasswordChange = DateTime.UtcNow;

                newUser.PasswordExpireOn = DateTime.UtcNow.AddYears(3);
                newUser.ChangePasswordNextLogon = false;
                newUser.UserEnabled = false;
                newUser.UserLocked = true;
                newUser.MantainanceMode = false;
                newUser.DateMembership = DateTime.UtcNow;
                newUser.AccountExpireOn = DateTime.UtcNow.AddMonths(1);   //Is set to 1 month until user confirm registration. Then, increase of years.
                newUser.LastLogon = null;
                newUser.LastLogout = null;
                newUser.Offset = MyConvert.ToInt32(hfOffset.Value, 0);

                //Other Fields
                //newUser.UserDomain = null;
                //newUser.UserType = null;
                //newUser.MailConfirmedOn = null;
                //newUser.Mobile = null;
                //newUser.MobileConfirmedOn = null;
                //newUser.MobileConfirmationCode = null;
                //newUser.IDCity = null;
                //newUser.IDProfilePhoto = null;
                //newUser.IDSecurityQuestion = null;
                //newUser.SecurityAnswer = null;
                //newUser.LastProfileUpdate = null;
                //newUser.UserIsOnLine = null;
                //newUser.IDVisibility = null;
                //newUser.IDGender = null;
                //newUser.AccountDeletedOn = null;

                ManageUSPReturnValue result = newUser.InsertUser();

                if (!result.IsError)
                {
                    //Log for statistics is in MyUser

                    //User added correctly. Get ID from SP
                    string IDUser = result.USPReturnValue;
                    newUser.IDUser = new Guid(IDUser);

                    #region NewStartActionOnTheUserBoard
                    //At the first registration, follow MyCookin and insert a PostOnFriendUserBoard from MyCookin User to say welcome
                    string IDUserMyCookin = String.Empty;

                    //Follow MyCookin
                    try
                    {
                        IDUserMyCookin = AppConfig.GetValue("IDUserMyCookin", AppDomain.CurrentDomain);

                        //SP to Request or Accept Friendship
                        MyUser MyCookinUser = new MyUser(new Guid(IDUserMyCookin));

                        MyUserFriendship ManageFriendship = new MyUserFriendship(newUser, MyCookinUser);
                        ManageUSPReturnValue resultFollow = ManageFriendship.RequestOrAcceptFriendship();
                    }
                    catch
                    { }

                    //Insert Message
                    try
                    {
                        string Message = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1), "US-IN-0071");

                        //INSERT ACTION IN USER BOARD
                        UserBoard NewUserBoardAction = new UserBoard(new Guid(IDUserMyCookin), null, ActionTypes.PostOnFriendUserBoard, new Guid(IDUser), Message, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                        NewUserBoardAction.InsertAction();
                    }
                    catch
                    { }
                    #endregion

                    //Get the SP ResultExecutionCode
                    string ResultExecutionCode = result.ResultExecutionCode;

                    //If the user has simply restored his account, after he deleted it, resultExecutionCode will be US-IN-0037
                    //So, don't send email, just Login

                    if (ResultExecutionCode.Equals("US-IN-0037"))
                    {
                        //WRITE A ROW IN STATISTICS DB
                        try
                        {
                            MyStatistics NewStatisticUser = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.US_AccountRestored, "Account Restored", Network.GetCurrentPageName(), "", Session.SessionID);
                            NewStatisticUser.InsertNewRow();
                        }
                        catch { }

                        Guid IDUserGuid = new Guid(IDUser);

                        MyUser User = new MyUser(IDUserGuid);

                        //LOGIN
                        if (User.LoginUser())
                        {
                            //WRITE A ROW IN STATISTICS DB
                            try
                            {
                                MyStatistics NewStatisticUser2 = new MyStatistics(IDUserGuid, null, StatisticsActionType.US_Login, "Login After Account Restored", Network.GetCurrentPageName(), "", Session.SessionID);
                                NewStatisticUser2.InsertNewRow();
                            }
                            catch { }

                            //string UrlRedirect = "/User/UserProfile.aspx?IDUserRequested=" + IDUser;
                            string UrlRedirect = (AppConfig.GetValue("RoutingUser", AppDomain.CurrentDomain) + "/" + User.UserName).ToLower();

                            Response.Redirect(UrlRedirect, true);
                        }
                        else
                        {
                            //Error in Login
                            //WRITE A ROW IN LOG FILE AND DB
                            try
                            {
                                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, "Error in Login After Restored Profile", IDUser, true, false);
                                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                                LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                            }
                            catch { }

                            //Show error dialog
                            //Show JQueryUi BoxDialogWithRedirect - JS: ShowJQuiBoxDialogWithRedirect(Title, Text)
                            string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                            string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0001"); ;
                            string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);
                        }
                    }
                    else
                    {
                        //User Registration Number
                        int NumberOfUsers = MyUser.NumberOfUsers();

                        /*********
                            Generate Activation Code and send it by email
                            ********/

                        //EmailPasswordHash will be send by email to new user.
                        //It will be use to confirm user registration as well.
                        string EmailPasswordHash = MySecurity.GenerateSHA1Hash(newUser.eMail + newUser.PasswordHash);

                        //Link to activate account sent by email
                        string host = HttpContext.Current.Request.Url.Host;
                        string link = Network.GetCurrentPathUrl() + Server.UrlEncode("ActivateUser.aspx?ID=" + HttpUtility.UrlEncode(IDUser) + "&ConfirmationCode=" + HttpUtility.UrlEncode(EmailPasswordHash));

                        //Send email to new user
                        string From = AppConfig.GetValue("EmailFromProfileUser", AppDomain.CurrentDomain);
                        string To = txtEmail.Text.ToString();
                        string Subject = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0022");
                        string url = "/PagesForEmail/WelcomeUser.aspx?link=" + link;

                        Network Mail = new Network(From, To, "", "", Subject, "", url);

                        if (!Mail.SendEmail())
                        {
                            //Error in sending email - User MUST confirm email

                            //WRITE A ROW IN LOG FILE AND DB
                            try
                            {
                                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0005", "Send Email Error in User Registration Page", IDUser, true, false);
                                LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                                LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                            }
                            catch { }

                            //Se l'email non è stata inviata per errore l'utente visualizzerà lo stesso un messaggio di benvenuto,
                            //quando farà il login, il sistema troverà l'utente come Locked,
                            //quindi proverà a reinviare l'email per la conferma dell'indirizzo email.

                            //Show error dialog
                            //Show JQueryUi BoxDialogWithRedirect - JS: ShowJQuiBoxDialogWithRedirect(Title, Text)
                            string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                            string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0011"); ;
                            string RedirectUrl = AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain);
                            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);
                        }
                        else
                        {
                            //Email Sent - Show Welcome Message
                            //WRITE A ROW IN STATISTICS DB
                            try
                            {
                                MyStatistics NewStatisticUser = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.US_EmailSent, "Email Sent for Registration", Network.GetCurrentPageName(), "", Session.SessionID);
                                NewStatisticUser.InsertNewRow();
                            }
                            catch { }

                            //Show JQueryUi BoxDialog With Redirect
                            string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0022");
                            string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0007");
                            string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);
                        }
                    }
                }
                else //Error in Registration
                {
                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), result.ResultExecutionCode);
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), result.ResultExecutionCode), "", true, false);
                        LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                        LogManager.WriteFileLog(LogLevel.CriticalErrors, true, NewRow);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on Page Register User: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region cvContractSigned_ServerValidate - DISABILITATO
        /// <summary>
        /// Validate Checkbox to Accept Contract
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        //protected void cvContractSigned_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    //determine if chkContractSigned is checked or not, if it is validate it, else don't
        //    args.IsValid = (chkContractSigned.Checked == true);
        //}
        #endregion

        #region btnFacebook_Click
        protected void btnFacebook_Click(object sender, ImageClickEventArgs e)
        {
            Session["Offset"] = hfOffset.Value;

            //To redirect to the requested page after login via Social Networks
            string requestedPage = Request.QueryString["requestedPage"];

            if (!String.IsNullOrEmpty(requestedPage))
            {
                Session["requestedPage"] = requestedPage;
            }

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

            //Client Id Which u Got when you Register You Application
            string FacebookClientId = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
            string FacebookScopes = AppConfig.GetValue("facebook_scopes", AppDomain.CurrentDomain);

            Response.Redirect(string.Format("https://graph.facebook.com/oauth/" +
              "authorize?client_id={0}&redirect_uri={1}&scope={2}",
              FacebookClientId, callBackUrl, FacebookScopes));
        }
        #endregion

        #region btnGoogle_Click
        protected void btnGoogle_Click(object sender, ImageClickEventArgs e)
        {
            Session["Offset"] = hfOffset.Value;

            //To redirect to the requested page after login via Social Networks
            string requestedPage = Request.QueryString["requestedPage"];

            if (!String.IsNullOrEmpty(requestedPage))
            {
                Session["requestedPage"] = requestedPage;
            }

            string urlRedirect = "/auth/auth.aspx?googleauth=true";

            Response.Redirect(urlRedirect, true);

        }
        #endregion

    }
}