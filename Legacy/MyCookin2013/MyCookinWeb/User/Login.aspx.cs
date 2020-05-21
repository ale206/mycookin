using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ErrorAndMessage;
using MyCookin.Log;
using System.Net;
using System.IO;
using System.Text;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb.UserInfo
{
    public partial class Login :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region FacebookRegistrationOrLogin

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

            //Set default button
            Page.Form.DefaultButton = lbtnUserLogin.UniqueID.ToString();
           
            txtEmail.Focus();

            if (!IsPostBack)
            {
                //Check DB Connection
                //If Db doesn't work, redirect to Error Page
                if (!MyUser.CheckDBConnection())
                {
                    Response.Redirect(AppConfig.GetValue("ErrorPage", AppDomain.CurrentDomain), true);
                }

                #region AcceptCookie?
                try
                {
                    //Checking Whether a Browser Accepts Cookies 
                    Session["AcceptsCookies"] = false;

                    if (!Convert.ToBoolean(Session["AcceptsCookies"]))
                    {
                        Response.Cookies["TestCookie"].Value = "ok";
                        Response.Cookies["TestCookie"].Expires = DateTime.UtcNow.AddMinutes(1);

                        if (Request.Cookies["TestCookie"] == null)
                        {
                            //Browser does not accept cookies.

                            Session["AcceptsCookies"] = false;

                            ////Alert User about Cookies Disabled
                            ////Show JQueryUi BoxDialog With Redirect On Close - JS: ShowJQuiBoxDialogWithRedirect(Title, Text, RedirectUrl)
                            //string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                            //string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-0005");
                            //string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                            //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                            chkRemember.Visible = false;

                            //Hide Login Panel
                            //pnlWrapperMainLoginContent.Visible = false;
                        }
                        else
                        {
                            //Browser Accepts Cookies

                            Session["AcceptsCookies"] = true;

                            // Delete test cookie.
                            Response.Cookies["TestCookie"].Expires = DateTime.UtcNow.AddDays(-1);
                        }
                    }
                }
                catch(Exception ex)
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error on Cookies Login User: " + ex.Message, "", true, false);
                        LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                        LogManager.WriteFileLog(LogLevel.CriticalErrors, true, NewRow);
                    }
                    catch { }
                }
                #endregion

                //Redirect to a Page when open Login Page and you are already logged
                if (MyUser.CheckUserLogged())
                {
                    //Redirect to UserProfile.aspx
                    //Response.Redirect(AppConfig.GetValue("RoutingUser", AppDomain.CurrentDomain) + Session["Username"], true);

                    //Redirect to the Principal UserBoard (MyNews.aspx)
                    //Response.Redirect((AppConfig.GetValue("PrincipalUserBoard", AppDomain.CurrentDomain)).ToLower(), true);
                    Response.Redirect(("/" + Session["UserName"].ToString() + "/news").ToLower(), true);
                }

                //Show ValidationSummary in JQuery BoxDialog
                //string SummaryBoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ValidationSummaryInBoxDialog('" + lbtnUserLogin.ClientID + "', '" + vsLogin.ClientID + "', '" + pnlResult.ClientID + "', '" + SummaryBoxTitle + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ValidationSummaryWithTipsyTooltip('" + lbtnUserLogin.ClientID + "', '" + vsLogin.ClientID + "', '" + pnlResult.ClientID + "', '" + SummaryBoxTitle + "');", true);

                //Focus on the first TextBox
                //Notice: Using Placeholder, if you use IE don't see the placeholder because it remove when textbox is selected
                //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "FocusOnLoad('" + txtEmail.ClientID + "');", true);

                //Hide Scrollbar
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "HideScrollbars();", true);
                
            }
            else
            {
                //Social Tabs 
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), "$('#pnlMainTab').tabs({ fx: { height: 'toggle', opacity: 'toggle' } }); ", true);
            }
        }

        protected void lbtnUserLogin_Click(object sender, EventArgs e)
        {
            //Redirect to page that user tried to visualize
            string requestedPage = String.Empty;

            try
            {
                

                string eMail = txtEmail.Text.ToString();
                string PasswordHash = MySecurity.GenerateSHA1Hash(txtPsw.Text.ToString());
                string _ipAddress = "";
                try
                {
                    if (Request.Headers[""] != null && !String.IsNullOrEmpty(Request.Headers["X-Forwarded-For"].ToString()))
                    {
                        _ipAddress = Request.Headers["X-Forwarded-For"].ToString();
                    }
                    else
                    {
                        _ipAddress = HttpContext.Current.Request.UserHostAddress;
                    }
                }
                catch
                {
                    _ipAddress = HttpContext.Current.Request.UserHostAddress;
                }


                MyUser User = new MyUser(eMail, PasswordHash, DateTime.UtcNow, true, _ipAddress, MyConvert.ToInt32(Session["Offset"].ToString(), 0));

                ManageUSPReturnValue result = User.LoginUserWithStoredProcedure();

                if (!result.IsError)
                {
                    //Log for statistics is in MyUser.

                    //GET LOGGED USER INFO
                    string UserLoggedId = result.USPReturnValue;
                    Guid IDUserLogged = new Guid(UserLoggedId);

                    MyUser UserLogged = new MyUser(IDUserLogged);
                    UserLogged.GetUserInfoAllByID();

                    //Check if Current Password is Valid (If Expired or Change Next Logon)
                    //If not, redirect to Reset Password
                    if (!UserLogged.IsValidPassword())
                    {
                        //Destroy Sessions
                        //Logout of user actually connected
                        //Destroy Session Variables
                        Session.Abandon();

                        //Destroy Cookie
                        Network.DestroyCookie();

                        string urlRedirect = "/user/resetpassword.aspx?ID=" + UserLoggedId;
                        Response.Redirect(urlRedirect, true);
                    }

                    if (chkRemember.Checked)
                    {
                        #region Cookies
                        //If User's Browser accepts Cookies
                        if (Convert.ToBoolean(Session["AcceptsCookies"]))
                        {
                            //SET COOKIE
                            HttpCookie myCookie = new HttpCookie(AppConfig.GetValue("CookieName", AppDomain.CurrentDomain));

                            // Set the cookie values.
                            myCookie.Values["IDUser"] = UserLogged.IDUser.ToString();

                            // Set the cookie expiration date.
                            myCookie.Expires = DateTime.UtcNow.AddDays(MyConvert.ToInt32(AppConfig.GetValue("CookieExpireTimeDay", AppDomain.CurrentDomain), 365));

                            // Add the cookie.
                            Response.Cookies.Add(myCookie);
                        }
                        #endregion
                    }

                    //Set Session Variables
                    UserLogged.SetLoginSessionVariables();

                    #region Redirect
                    
                    requestedPage = Request.QueryString["requestedPage"];

                    //If null, set requestedPage to default page (UserProfile or something else)
                    if (requestedPage == null)
                    {
                        //requestedPage = AppConfig.GetValue("RoutingUser", AppDomain.CurrentDomain) + "/" + UserLogged.UserName;

                        //Redirect to the Principal UserBoard (MyNews.aspx)
                        //requestedPage = AppConfig.GetValue("PrincipalUserBoard", AppDomain.CurrentDomain);
                        Response.Redirect(("/" + Session["UserName"].ToString() + "/news").ToLower(), true);
                    }

                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatisticUser = new MyStatistics(IDUserLogged, null, StatisticsActionType.US_Login, "Login", Network.GetCurrentPageName(), "", Session.SessionID);
                        NewStatisticUser.InsertNewRow();
                    }
                    catch { }

                    //Response Redirect moved at the end of the if, to avoid catch error (thread interrotto)
                    
                    #endregion
                }
                else
                {
                    //Error in Login - User is Locked or similar

                    //If User is locked, send again activation code by email
                    if (result.ResultExecutionCode == "US-WN-0002")
                    {
                        MyUser newUser = new MyUser(eMail, "");
                        newUser.GetUserBasicInfoByID();
                        /*********
                        Generate Activation Code and send it by email
                        ********/

                        //EmailPasswordHash will be send by email to new user.
                        //It will be use to confirm user registration as well.
                        string EmailPasswordHash = MySecurity.GenerateSHA1Hash(newUser.eMail + newUser.PasswordHash);

                        //Link to activate account sent by email
                        string host = HttpContext.Current.Request.Url.Host;
                        string link = Network.GetCurrentPathUrl() + Server.UrlEncode("ActivateUser.aspx?ID=" + HttpUtility.UrlEncode(newUser.IDUser.ToString()) + "&ConfirmationCode=" + HttpUtility.UrlEncode(EmailPasswordHash));

                        //Send email to new user
                        string From = AppConfig.GetValue("EmailFromProfileUser", AppDomain.CurrentDomain);
                        string To = eMail;
                        string Subject = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0022");
                        string url = "/PagesForEmail/WelcomeUser.aspx?link=" + link;

                        Network Mail = new Network(From, To, "", "", Subject, "", url);

                        if (!Mail.SendEmail())
                        {
                            //Show error dialog
                            //Show JQueryUi BoxDialogWithRedirect - JS: ShowJQuiBoxDialogWithRedirect(Title, Text)
                            string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                            string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0005"); ;
                            string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                            //WRITE A ROW IN LOG FILE AND DB
                            try
                            {
                                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Can't send email for activation via email", "", true, false);
                                LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                                LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                            }
                            catch { }
                        }
                        else
                        {
                            //Email With Activation Link Sent
                            //Show JQueryUi BoxDialog With Redirect
                            string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0022");
                            string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0007");
                            string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);
                        }
                    }
                    else
                    {
                        //Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
                        string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                        string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), result.ResultExecutionCode);
                        //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
                   
                        lblWrongUsnOrPsw.Text = BoxText;
                        lblWrongUsnOrPsw.Visible = true;

                        //WRITE A ROW IN LOG FILE AND DB
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), result.ResultExecutionCode), User.IDUser.ToString(), false, true);
                            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                            LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                        }
                        catch { }
                    }
                }
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Login User: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, true, NewRow);
                }
                catch { }

            }

            if (!String.IsNullOrEmpty(requestedPage))
            {
                Response.Redirect((requestedPage).ToLower(), true);
            }
        }

        #region btnFacebook_Click
        protected void btnFacebook_Click(object sender, ImageClickEventArgs e)
        {
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
