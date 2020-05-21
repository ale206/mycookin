/*
 * (Copyright (c) 2011, Shannon Whitley <swhitley@whitleymedia.com> http://voiceoftech.com/
 * 
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are permitted 
 * provided that the following conditions are met:
 * 
 * Redistributions of source code must retain the above copyright notice, this list of conditions 
 * and the following disclaimer.
 * 
 * Redistributions in binary form must reproduce the above copyright notice, this list of conditions
 * and the following disclaimer in the documentation and/or other materials provided with the distribution.

 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND 
 * FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS 
 * BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF
 * THE POSSIBILITY OF SUCH DAMAGE.
 * 
 * LEGGI QUESTO PER GOOGLE
 * https://developers.google.com/accounts/docs/OAuth2WebServer?hl=it
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;
using System.Collections.Specialized;
using System.Xml;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.SocialAction;
using Google.GData.Client;
using Google.Contacts;
using Google.GData.Extensions;
using Google.GData.Apps;
using MyCookin.Log;
using MyCookin.ObjectManager.SocialAction.Google;
using MyCookin.Common;
using MyCookin.ErrorAndMessage;
using System.Net;
using Facebook;
using Facebook.Web;
using MyCookinWeb.Auth;
using System.Text.RegularExpressions;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.ObjectManager.UserBoardManager;

namespace AuthPack
{
    public partial class Auth :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            //Notice: Twitter does not return email. So use connect with twitter ONLY when a user is logged to use Session["email"] and others info.

            #region Twitter
            //Twitter oAuth Start
            if (Request["twitterauth"] != null && Request["twitterauth"] == "true")
            {
                try
                {
                    oAuthTwitter oAuth = new oAuthTwitter();
                    oAuth.CallBackUrl = Request.Url.AbsoluteUri.Replace("twitterauth=true", "twitterauth=false");
                    //Redirect the user to Twitter for authorization.
                    Response.Redirect(oAuth.AuthorizationLinkGet(), true);
                }
                catch(Exception ex)
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0014", "Error in Auth Twitter", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                    }
                    catch { }

                    int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0014");
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                }
            }
            //Twitter Return
            if (Request["twitterauth"] != null && Request["twitterauth"] == "false")
            {
                try
                {
                    oAuthTwitter oAuth = new oAuthTwitter();
                    //Get the access token and secret.
                    oAuth.AccessTokenGet(Request["oauth_token"], Request["oauth_verifier"]);
                    if (oAuth.TokenSecret.Length > 0)
                    {
                        //STORE THESE TOKENS FOR LATER CALLS
                        //Subsequent calls can be made without the Twitter login screen.
                        //Move this code outside of this auth process if you already have the tokens.
                        //
                        //Example: 
                        //oAuthTwitter oAuth = new oAuthTwitter();
                        //oAuth.Token = Session["token"];
                        //oAuth.TokenSecret = Session["token_secret"];
                        //Then make the following Twitter call.

                        //SAMPLE TWITTER API CALL
                        string url = "https://api.twitter.com/1.1/account/verify_credentials.json";
                        TwitterUser user = Json.Deserialise<TwitterUser>(oAuth.oAuthWebRequest(oAuthTwitter.Method.GET, url, String.Empty));

                        if (user.id.Length > 0)
                        {
                            UserData userData = new UserData();

                            userData.accessToken = oAuth.Token;
                            userData.refreshToken = oAuth.TokenSecret;

                            userData.id = user.id;
                            userData.username = user.screen_name;
                            userData.name = user.name;
                            userData.serviceType = "twitter";

                            //ONLY FOR TWITTER, GET FROM SESSION
                            userData.email = Session["eMail"].ToString();

                            AuthSuccess(userData);

                            //Login Or Registration
                            LoginOrRegistration(userData, null);
                        }

                        //POST Test
                        //url = "http://api.twitter.com/1.1/statuses/update.xml";
                        //xml = oAuth.oAuthWebRequest(oAuthTwitter.Method.POST, url, "status=" + oAuth.UrlEncode("Hello @swhitley - Testing the .NET oAuth API"));
                        Response.Clear();
                        //Response.Write("<script>window.opener.location.reload();window.close();</script>");

                        //Redirect
                        string UrlRedirect = AppConfig.GetValue("PrincipalUserBoard", AppDomain.CurrentDomain);

                        //Redirect
                        try
                        {
                            //If Exist Session, overwrite current UrlRedirect
                            UrlRedirect = Session["requestedPage"].ToString();
                        }
                        catch
                        {
                        }


                        Response.Redirect(UrlRedirect, true);
                    }
                }
                catch
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0014", "Error in Auth Twitter Or User Denied Authorization", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);

                        //Redirect
                        string UrlRedirect = AppConfig.GetValue("PrincipalUserBoard", AppDomain.CurrentDomain);

                        //Redirect
                        try
                        {
                            //If Exist Session, overwrite current UrlRedirect
                            UrlRedirect = Session["requestedPage"].ToString();
                        }
                        catch
                        {
                        }

                        Response.Redirect(UrlRedirect, true);
                    }
                    catch { }
                }
            }
            
            #endregion

            #region Google
            //Google oAuth Start
            if (Request["googleauth"] != null && Request["googleauth"] == "true")
            {
                try
                {
                    string returl = Request.Url.AbsoluteUri.Replace("googleauth=true", "googleauth=false");
                    string url = "https://accounts.google.com/o/oauth2/auth?client_id=" 
                                 + System.Web.HttpUtility.UrlEncode(ConfigurationManager.AppSettings["google_clientid"].ToString()) 
                                 + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(returl)
                                 + "&scope=" + System.Web.HttpUtility.UrlEncode("https://www.googleapis.com/auth/userinfo#email https://www.google.com/m8/feeds/ https://apps-apis.google.com/a/feeds/groups/ https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/plus.me") 
                                 + "&response_type=code&access_type=offline";
                    Response.Redirect(url, true);
                }
                catch
                {
                    //Log here is not necessary..
                }
            }

            //Google Return
            if (Request["googleauth"] != null && Request["googleauth"] == "false")
            {
                try
                {
                    string code = Request["code"];
                    string returl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("&code="));
                    GoogleTokens tokens = GoogleAuth.GoogleTokensGet(code, null, returl);

                    //QUESTO SOTTO SERVE PER FARE LE CHIAMATE ALLE API DOPO AVER RICEVUTO L'AUTORIZZAZIONE.

                    //SAMPLE GOOGLE API CALL
                    //Set the access token in the header.  It expires, so prepare to use the refresh token to get a new access token (not shown).
                    List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("Authorization", "OAuth " + tokens.access_token) };
                    //string url = "https://www.googleapis.com/userinfo/email?alt=json";
                    string url = "https://www.googleapis.com/oauth2/v2/userinfo?alt=json";
                    GoogleData user = Json.Deserialise<GoogleData>(AuthUtilities.WebRequest(AuthUtilities.Method.GET, url, "", headers));

                    if (user.data != null && user.data.email.Length > 0)
                    {
                        UserData userData = new UserData();
                        userData.username = user.data.email;
                        userData.serviceType = "google";
                        AuthSuccess(userData);
                    }

                    if (user.email.Length > 0)
                    {
                        UserData userData = new UserData();

                        //Memorize this accessToken for future access
                        userData.accessToken = tokens.access_token;
                        userData.refreshToken = tokens.refresh_token;


                        userData.birthday = user.birthday;
                        userData.email = user.email;
                        userData.family_name = user.family_name;
                        userData.gender = user.gender;
                        userData.given_name = user.given_name;
                        userData.id = user.id;
                        userData.link = user.link;
                        userData.locale = user.locale;
                        userData.name = user.name;
                        userData.picture = user.picture;
                        userData.verified_email = user.verified_email;

                        //Questo serve dopo, in AuthSuccess(UserData userData )
                        userData.username = user.email;

                        userData.serviceType = "google";

                        //Login Or Registration
                        AuthSuccess(userData);          //Cookie... (?)
                        try
                        {
                            LoginOrRegistration(userData, tokens);
                        }
                        catch
                        { //Log not necessary here 
                        }
                    }

                    Response.Clear();
                    //Response.Write("<script>window.opener.location.reload();window.close();</script>");

                }
                catch (Exception ex)
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0016", "Error in Auth Facebook: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                    }
                    catch { }

                    int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0016");
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);
                }
                finally
                {
                    string UrlRedirect = AppConfig.GetValue("PrincipalUserBoard", AppDomain.CurrentDomain);

                    //Redirect
                    try
                    {
                        //If Exist Session, overwrite current UrlRedirect
                        UrlRedirect = Session["requestedPage"].ToString();
                    }
                    catch(Exception ex)
                    {
                    }

                    Response.Redirect(UrlRedirect, true);
                }
            }
            #endregion

            #region Facebook
            if (Request.QueryString["fbaccessToken"] != null)
            {
                try
                {
                    string access_token = Request.QueryString["fbaccessToken"];

                    //SAMPLE FACEBOOK API CALL
                    string url = "https://graph.facebook.com/me?access_token=%%access_token%%";
                    url = url.Replace("%%access_token%%", access_token);
                    FacebookMe fb_me = Json.Deserialise<FacebookMe>(AuthUtilities.WebRequest(AuthUtilities.Method.GET, url, ""));

                    if (fb_me.username.Length == 0)
                    {
                        fb_me.username = fb_me.name;
                    }
                    UserData userData = new UserData();

                    //Memorize this accessToken for future access
                    userData.accessToken = access_token;

                    userData.id = fb_me.id;
                    userData.username = fb_me.username;
                    userData.serviceType = "facebook";
                    userData.name = fb_me.name;
                    userData.birthday = fb_me.birthday;
                    userData.email = fb_me.email;
                    userData.family_name = fb_me.last_name;        //In FB family_name is last_name
                    userData.gender = fb_me.gender;
                    userData.given_name = fb_me.first_name;         //In FB given_name is first_name
                    userData.link = fb_me.link;
                    userData.locale = fb_me.locale;
                    userData.picture = fb_me.picture;
                    userData.verified_email = fb_me.email;

                    AuthSuccess(userData);

                    LoginOrRegistration(userData, null);

                    Response.Clear();
                    Response.Write("<script>location.href = '../';</script>");
                    

                }
                catch(Exception ex)
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0016", "Error in Auth Facebook: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                    }
                    catch { }

                    int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-ER-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0016");
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                }
                finally
                {
                    //Redirect
                    string UrlRedirect = AppConfig.GetValue("PrincipalUserBoard", AppDomain.CurrentDomain);

                    //Redirect
                    try
                    {
                        //If Exist Session, overwrite current UrlRedirect
                        UrlRedirect = Session["requestedPage"].ToString();
                    }
                    catch
                    {
                    }

                    Response.Redirect(UrlRedirect, true);
                }

            }

            if (Request["facebookauth"] == "false" && !User.Identity.IsAuthenticated)
            {
                try
                {
                    Response.Clear();
                    Response.Write("<script>location.href = '../';</script>");
                }
                catch
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0016", "Error in Auth Facebook", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                    }
                    catch { }
                }
            }
            #endregion

            #region LinkedIn
            //LinkedIn Return
            if (Request.Cookies["linkedin_oauth_" + ConfigurationManager.AppSettings["linkedin_consumer_key"].ToString()] != null)
            {
                //Cookie Json object
                LinkedIn_oAuth_Cookie cookie = Json.Deserialise<LinkedIn_oAuth_Cookie>(Server.UrlDecode(Request.Cookies["linkedin_oauth_" + ConfigurationManager.AppSettings["linkedin_consumer_key"].ToString()].Value));

                //Verify the signature
                oAuthLinkedIn oAuthLi = new oAuthLinkedIn();
                string sigBase = cookie.access_token+cookie.member_id;

                HMACSHA1 hmacsha1 = new HMACSHA1();
                hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}", oAuthLi.UrlEncode(ConfigurationManager.AppSettings["linkedin_consumer_secret"])));

                string sig = oAuthLi.GenerateSignatureUsingHash(sigBase, hmacsha1);

                //Retrieve the access token.
                if (sig == cookie.signature)
                {
                    string response = oAuthLi.oAuthWebRequest(oAuthLinkedIn.Method.POST, oAuthLi.ACCESS_TOKEN + "?xoauth_oauth2_access_token=" + oAuthLi.UrlEncode(cookie.access_token), "");
                    string[] tokens = response.Split('&');
                    string token = tokens[0].Split('=')[1];
                    string token_secret = tokens[1].Split('=')[1];

                    //STORE THESE TOKENS FOR LATER CALLS
                    oAuthLi.Token = token;
                    oAuthLi.TokenSecret = token_secret;

                    //SAMPLE LINKEDIN API CALL
                    string url = "http://api.linkedin.com/v1/people/id=%%id%%:("
                    + "id"
                    + ",first-name"
                    + ",last-name"
                    + ")";
                    url = url.Replace("%%id%%", cookie.member_id);
                    string xml = oAuthLi.oAuthWebRequest(oAuthLinkedIn.Method.GET, url, "");

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);
                    string id = "";
                    string name = "";
                    foreach (XmlElement person in xmlDoc.GetElementsByTagName("person"))
                    {
                        if (person["id"] != null)
                        {
                            id = person["id"].InnerText;
                        }
                        if (person["first-name"] != null)
                        {
                            name = person["first-name"].InnerText;
                        }
                        if (person["last-name"] != null)
                        {
                            if (name.Length > 0)
                            {
                                name += " ";
                            }
                            name += person["last-name"].InnerText;
                        }
                    }

                    if (id.Length > 0)
                    {
                        UserData userData = new UserData();
                        userData.id = id;
                        userData.username = name;
                        userData.name = name;
                        userData.serviceType = "linkedin";
                        AuthSuccess(userData);
                    }

                    Response.Clear();
                    Response.Write(Request["callback"].ToString() + "()");
                }
            }
            #endregion

            //TODO: Add Error Handling
        }

        #region AuthSuccess
        /// <summary>
        /// Generate the forms auth cookie.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userData"></param>
        public void AuthSuccess(UserData userData )
        {
            try
            {
                //Create Form Authentication ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1
                    , userData.username
                    , DateTime.UtcNow
                    , DateTime.UtcNow.AddHours(18)
                    , true
                    , Json.Serialize<UserData>(userData)
                    , FormsAuthentication.FormsCookiePath);

                string hashCookies = FormsAuthentication.Encrypt(ticket);
                HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies);

                Response.Cookies.Add(userCookie);
            }
            catch
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0017", "error in AuthSuccess()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }
        }
        #endregion

        #region LoginOrRegistration
        public void LoginOrRegistration(UserData userData, GoogleTokens tokens)
        {
            try
            {
                string IDUser = String.Empty;

                
                

                #region ConvertVerifiedEmail
                //Try to Convert VerifiedEmail
                bool verified_emailFromSocial_bool = false;

                switch (userData.serviceType)
                {
                    case ("google"):
                        try
                        {
                            verified_emailFromSocial_bool = Convert.ToBoolean(userData.verified_email);
                        }
                        catch
                        {
                        }
                        break;
                    case ("facebook"):
                        if (userData.verified_email.Equals(userData.email))
                        {
                            verified_emailFromSocial_bool = true;
                        }
                        break;
                    case ("twitter"):
                        verified_emailFromSocial_bool = true;
                        break;
                }

                #endregion

                #region GeneralCulture
                string cultureString = "en";
                //Get only first two chars of language code
                try
                {
                    if (userData.locale.Length > 1)
                    {
                        cultureString = userData.locale.Substring(0, 2);
                    }
                }
                catch
                {
                }
                #endregion

                #region FacebookPicture
                string PictureUrl = "";

                switch (userData.serviceType)
                {
                    case ("google"):
                        PictureUrl = userData.picture;
                        break;
                    case ("facebook"):
                        PictureUrl = "https://graph.facebook.com/" + userData.id + "/picture";
                        break;
                    //case ("twitter"):
                    //NO PICS!
                    //break;
                }
                #endregion

                MyUserSocial UserSocial = new MyUserSocial();
                MyUser User = new MyUser();

                #region CheckUserAlreadyRegistered
                //Check if user is already registered through Social Network (If exist in table SocialLogins)
                //If yes, return our IDUser
                switch (userData.serviceType)
                {
                    case ("google"):
                        IDUser = UserSocial.GetIDUserFromSocialLogins(userData.id, (int)SocialNetwork.Google);
                        break;
                    case ("facebook"):
                        IDUser = UserSocial.GetIDUserFromSocialLogins(userData.id, (int)SocialNetwork.Facebook);
                        break;
                    case ("twitter"):
                        IDUser = UserSocial.GetIDUserFromSocialLogins(userData.id, (int)SocialNetwork.Twitter);
                        break;
                }

                if (!String.IsNullOrEmpty(IDUser))      //IDUserSocial exist. Return our IDUser
                {
                    //UserSocial.IDUser = new Guid(IDUser);
                    User.IDUser = new Guid(IDUser);

                    //Update Tokens
                    switch (userData.serviceType)
                    {
                        case ("google"):
                            MyUserSocial.UpdateSocialTokens((int)SocialNetwork.Google, User.IDUser, userData.accessToken, userData.refreshToken);
                            break;
                        case ("facebook"):
                            MyUserSocial.UpdateSocialTokens((int)SocialNetwork.Facebook, User.IDUser, userData.accessToken, userData.refreshToken);
                            break;
                        case ("twitter"):
                            MyUserSocial.UpdateSocialTokens((int)SocialNetwork.Twitter, User.IDUser, userData.accessToken, userData.refreshToken);
                            break;
                    }
                    
                }
                else
                {
                    //If NOT registered through this Social Network, check if the user is already registered, but not through this Social Network, using his email.
                    IDUser = User.GetIDUserFromEmail(userData.email).ToString();

                    if (IDUser.Equals((new Guid()).ToString()))
                    {
                        IDUser = String.Empty;
                    }

                    //If already registered to MyCookin, associate id's and return our IDUser
                    if (!String.IsNullOrEmpty(IDUser))
                    {
                        UserSocial.IDUser = new Guid(IDUser);
                        UserSocial.IDUserSocial = userData.id;

                        //Memorize Social Network Informations for user already Registered in our Website.
                        bool InsertResult = false;

                        switch (userData.serviceType)
                        {
                            case ("google"):
                                InsertResult = UserSocial.MemorizeIDUserSocial((int)SocialNetwork.Google, userData.link, verified_emailFromSocial_bool, PictureUrl, cultureString, userData.accessToken, userData.refreshToken);
                                break;
                            case ("facebook"):
                                InsertResult = UserSocial.MemorizeIDUserSocial((int)SocialNetwork.Facebook, userData.link, verified_emailFromSocial_bool, PictureUrl, cultureString, userData.accessToken, userData.refreshToken);
                                break;
                            case ("twitter"):
                                InsertResult = UserSocial.MemorizeIDUserSocial((int)SocialNetwork.Twitter, null, verified_emailFromSocial_bool, "", cultureString, userData.accessToken, userData.refreshToken);
                                break;
                        }

                        if (InsertResult)
                        {

                            switch (userData.serviceType)
                            {
                                case ("google"):
                                    //WRITE A ROW IN STATISTICS DB
                                    try
                                    {
                                        MyStatistics NewStatisticUser = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_NewRegistrationThroughGoogle, "New Registration Through Google", Network.GetCurrentPageName(), "", Session.SessionID);
                                        NewStatisticUser.InsertNewRow();
                                    }
                                    catch { }
                                    break;
                                case ("facebook"):
                                    //WRITE A ROW IN STATISTICS DB
                                    try
                                    {
                                        MyStatistics NewStatisticUser2 = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_NewRegistrationThroughFacebook, "New Registration Through Facebook", Network.GetCurrentPageName(), "", Session.SessionID);
                                        NewStatisticUser2.InsertNewRow();
                                    }
                                    catch { }
                                    break;
                                case ("twitter"):
                                    //WRITE A ROW IN STATISTICS DB
                                    try
                                    {
                                        MyStatistics NewStatisticUser3 = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_NewRegistrationThroughTwitter, "New Registration Through Twitter", Network.GetCurrentPageName(), "", Session.SessionID);
                                        NewStatisticUser3.InsertNewRow();
                                    }
                                    catch { }
                                    break;
                            }


                        }
                        else //Error in Registration
                        {
                            //WRITE A ROW IN LOG FILE AND DB
                            try
                            {
                                LogRow NewRow2 = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "", "Error in Registration Through Social Network " + userData.serviceType + " For a User already Registered on our site", "", true, false);
                                LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow2);
                                LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow2);
                            }
                            catch { }
                        }

                    }
                }
                #endregion

                //If IDUser has been valorized by one of methods above, Login User.
                if (!String.IsNullOrEmpty(IDUser))
                {
                    /***********************
                    ** LOGIN
                    ***********************/

                    User.GetUserInfoAllByID();

                    //Check if Current Password is Valid (If Expired or Change Next Logon)
                    //If not, redirect to Reset Password
                    if (!User.IsValidPassword())
                    {
                        string urlRedirect = "/User/ResetPassword.aspx?ID=" + IDUser;
                        Response.Redirect(urlRedirect, true);
                    }

                    if (User.LoginUser())
                    {
                        switch (userData.serviceType)
                        {
                            case ("google"):
                                //WRITE A ROW IN STATISTICS DB
                                try
                                {
                                    MyStatistics NewStatisticUser = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_LoginThroughGoogle, "New Login Through Google", Network.GetCurrentPageName(), "", Session.SessionID);
                                    NewStatisticUser.InsertNewRow();
                                }
                                catch { }
                                break;
                            case ("facebook"):
                                //WRITE A ROW IN STATISTICS DB
                                try
                                {
                                    MyStatistics NewStatisticUser2 = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_LoginThroughFacebook, "New Login Through Facebook", Network.GetCurrentPageName(), "", Session.SessionID);
                                    NewStatisticUser2.InsertNewRow();
                                }
                                catch { }
                                break;
                            case ("twitter"):
                                //WRITE A ROW IN STATISTICS DB
                                try
                                {
                                    MyStatistics NewStatisticUser3 = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_LoginThroughTwitter, "New Login Through Twitter", Network.GetCurrentPageName(), "", Session.SessionID);
                                    NewStatisticUser3.InsertNewRow();
                                }
                                catch { }
                                break;
                        }
                    }
                    else
                    {
                        //Error in Login
                        //WRITE A ROW IN LOG FILE AND DB
                        try
                        {
                            LogRow NewRow1 = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Login Through Google After Registration", IDUser, true, false);
                            LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow1);
                            LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow1);
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
                    /***********************
                    ** NEW REGISTRATION
                    ***********************/

                    #region CheckBirthDate
                    //Check BirthDate
                    //Sometimes Social Networks give us a Date without the year.
                    //If so give one past year

                    DateTime? birthdateFromSocial = new DateTime();

                    birthdateFromSocial = null;
                    try
                    {
                        //Convert, if exist
                        if (userData.birthday.Length > 5)
                        {
                            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en");

                            try
                            {
                                birthdateFromSocial = Convert.ToDateTime(userData.birthday, culture);
                            }
                            catch
                            {

                            }
                        }
                    }
                    catch
                    {
                    }
                    #endregion

                    int? GenderId = null;
                    try
                    {
                        if (userData.gender.Length > 1)
                        {
                            //Socials always(?) return Gender in English, so set 1 for Language
                            GenderId = User.GetIDGenderByGenderNameAndIDLanguage(userData.gender, 1);
                        }
                    }
                    catch
                    {
                    }

                    //Informations given by Google
                    User.eMail = userData.email;
                    User.Name = userData.given_name;
                    User.Surname = userData.family_name;
                    User.IDGender = GenderId;
                    User.BirthDate = birthdateFromSocial;

                    User.IDUserSocial = userData.id;      //ID of User on Social Network

                    User.Offset = MyConvert.ToInt32(Session["Offset"].ToString(), 0);

                    //Create a Username
                    Random rdn = new Random();

                    if (userData.serviceType == "twitter")
                    {
                        //for twitter
                        User.UserName = userData.username.Replace(" ", "") + rdn.Next(100, 999);
                    }
                    else
                    {
                        User.UserName = userData.name.Replace(" ", "") + rdn.Next(100, 999);
                    }

                    //Create a Password
                    User.PasswordHash = Guid.NewGuid().ToString();

                    User.ContractSigned = true;
                    User.IDLanguage = MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1);
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
                    User.LastIpAddress = _ipAddress;

                    //For Registrations via Social let this null.
                    //User.LastPasswordChange = DateTime.UtcNow;
                    User.PasswordExpireOn = DateTime.UtcNow.AddYears(3);
                    User.ChangePasswordNextLogon = false;
                    User.UserEnabled = true;                                            //Already Enabled in this step because we register through Google
                    User.UserLocked = false;                                            //False in this step because we register through Google
                    User.MantainanceMode = false;
                    User.DateMembership = DateTime.UtcNow;
                    User.AccountExpireOn = DateTime.UtcNow.AddYears(1);                    //Already incremented by 1 year.

                    User.MailConfirmedOn = DateTime.UtcNow;                                //Just here, the user has already confirmed through Google

                    ManageUSPReturnValue result = User.InsertUser();

                    if (!result.IsError)
                    {
                        //User added correctly. Get ID from SP
                        IDUser = result.USPReturnValue;

                        //And add to current MyUser Object
                        User.IDUser = new Guid(IDUser);

                        UserSocial.IDUser = new Guid(IDUser);

                        #region NewStartActionOnTheUserBoard
                        
                        MyUser newUser = new MyUser(UserSocial.IDUser);

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

                        //INSERT IN TABLE SocialLogins
                        //Do this AFTER registration because need IDUser of the new user.
                        bool InsertResult = false;

                        switch (userData.serviceType)
                        {
                            case ("google"):
                                InsertResult = UserSocial.MemorizeIDUserSocial((int)SocialNetwork.Google, userData.link, verified_emailFromSocial_bool, PictureUrl, cultureString, userData.accessToken, userData.refreshToken);
                                break;
                            case ("facebook"):
                                InsertResult = UserSocial.MemorizeIDUserSocial((int)SocialNetwork.Facebook, userData.link, verified_emailFromSocial_bool, PictureUrl, cultureString, userData.accessToken, userData.refreshToken);
                                break;
                            case ("twitter"):
                                InsertResult = UserSocial.MemorizeIDUserSocial((int)SocialNetwork.Twitter, userData.link, verified_emailFromSocial_bool, PictureUrl, cultureString, userData.accessToken, userData.refreshToken);
                                break;
                        }

                        if (InsertResult)
                        {
                            switch (userData.serviceType)
                            {
                                case ("google"):
                                    //WRITE A ROW IN STATISTICS DB
                                    try
                                    {
                                        MyStatistics NewStatisticUser = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_NewRegistrationThroughGoogle, "New Registration Through Google", Network.GetCurrentPageName(), "", Session.SessionID);
                                        NewStatisticUser.InsertNewRow();
                                    }
                                    catch { }
                                    break;
                                case ("facebook"):
                                    //WRITE A ROW IN STATISTICS DB
                                    try
                                    {
                                        MyStatistics NewStatisticUser2 = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_NewRegistrationThroughFacebook, "New Registration Through Facebook", Network.GetCurrentPageName(), "", Session.SessionID);
                                        NewStatisticUser2.InsertNewRow();
                                    }
                                    catch { }
                                    break;
                                case ("twitter"):
                                    //WRITE A ROW IN STATISTICS DB
                                    try
                                    {
                                        MyStatistics NewStatisticUser3 = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_NewRegistrationThroughTwitter, "New Registration Through Twitter", Network.GetCurrentPageName(), "", Session.SessionID);
                                        NewStatisticUser3.InsertNewRow();
                                    }
                                    catch { }
                                    break;
                            }

                            //LOGIN
                            if (User.LoginUser())
                            {
                                switch (userData.serviceType)
                                {
                                    case ("google"):
                                        //WRITE A ROW IN STATISTICS DB
                                        try
                                        {
                                            MyStatistics NewStatisticUser = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_LoginThroughGoogle, "New Login Through Google", Network.GetCurrentPageName(), "", Session.SessionID);
                                            NewStatisticUser.InsertNewRow();
                                        }
                                        catch { }
                                        break;
                                    case ("facebook"):
                                        //WRITE A ROW IN STATISTICS DB
                                        try
                                        {
                                            MyStatistics NewStatisticUser2 = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_LoginThroughFacebook, "New Login Through Facebook", Network.GetCurrentPageName(), "", Session.SessionID);
                                            NewStatisticUser2.InsertNewRow();
                                        }
                                        catch { }
                                        break;
                                    case ("twitter"):
                                        //WRITE A ROW IN STATISTICS DB
                                        try
                                        {
                                            MyStatistics NewStatisticUser3 = new MyStatistics(new Guid(IDUser), null, StatisticsActionType.SC_LoginThroughTwitter, "New Login Through Twitter", Network.GetCurrentPageName(), "", Session.SessionID);
                                            NewStatisticUser3.InsertNewRow();
                                        }
                                        catch { }
                                        break;
                                }
                            }
                            else
                            {
                                //Error in Login
                                //WRITE A ROW IN LOG FILE AND DB
                                try
                                {
                                    LogRow NewRow1 = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), result.ResultExecutionCode, "Error in Login Through Google After Registration", IDUser, true, false);
                                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow1);
                                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow1);
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
                                LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                            }
                            catch { }
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
                            LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log not necessary here
            }
        }
        #endregion

        #region GetGoogleParameters

        public static OAuth2Parameters GetGoogleParameters(GoogleTokens tokens)
        {
            OAuth2Parameters parameters = null;

            try
            {
                SocialGoogleAuthentication GoogleAuthentication = new SocialGoogleAuthentication(tokens.access_token, tokens.refresh_token);    //tokens.refresh_token aggiunta ale
                GoogleAuthentication.Token = tokens.access_token;

                //aggiunta ale
                GoogleAuthentication.RefreshToken = tokens.refresh_token;

                parameters = GoogleAuthentication.GetParameters();
            }
            catch
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0017", "Error in GetGoogleParameters", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }
            return parameters;
        }

        #endregion

    }
}
