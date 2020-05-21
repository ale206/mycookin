using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ErrorAndMessage;
using MyCookin.Log;
using MyCookin.ObjectManager.StatisticsManager;
using System.Net;
using System.IO;
using System.Text;

namespace MyCookinWeb.UserInfo
{
    public partial class ResetPassword :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //PAGE NOT PROTECTED


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
            Page.Form.DefaultButton = lbtnResetPsw.UniqueID.ToString();

            //Notice: Using Placeholder, if you use IE don't see the placeholder because it remove when textbox is selected
            //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "FocusOnLoad('" + txtPassword.ClientID + "');", true);

            //If we have not ID in this page, something has gone wrong
            //redirect to Forgot Password
            if (String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string urlRedirect = "forgotpassword.aspx";
                Response.Redirect(urlRedirect);
            }

            //Check if we are logged
            if (MyUser.CheckUserLogged())
            {
                //If logged notice consequent LogOut
                lblNote.Text = "* You'll need to login again";
            }

            //Take UserName to check Password Strenght
            Guid IDUser = new Guid(Request.QueryString["ID"]);

            //Get User Info by Id
            MyUser InfoUser = new MyUser(IDUser);
            InfoUser.GetUserBasicInfoByID();

            txtUserName.Text = InfoUser.UserName;
        }

        protected void lbtnResetPsw_Click(object sender, EventArgs e)
        {
            try
            {
                Guid IDUser = new Guid(Request.QueryString["ID"]);
                string ConfirmationCode = Request.QueryString["ConfirmationCode"];

                //Get User Info by Id
                MyUser InfoUser = new MyUser(IDUser);
                InfoUser.GetUserInfoAllByID();

                //Check ConfirmationCode - HASH[(SecurityAnswer + IDUser + PasswordHash)]
                string ConfirmationCodeVerify = MySecurity.GenerateSHA1Hash(InfoUser.SecurityAnswer + InfoUser.IDUser + InfoUser.PasswordHash);

                //Get IDLanguage From Browser Culture
                

                //Get Login Page Url
                string url = AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain);

                //Check ConfirmationCode - If The user is already Registered and is resetting his password, this will give error then set it to true
                bool CheckConfirmationCode = true;

                try
                {
                    //Eventually Set to FALSE the CheckConfirmationCode
                    CheckConfirmationCode = ConfirmationCode.Equals(ConfirmationCodeVerify);
                }
                catch
                {

                }

                if (CheckConfirmationCode)
                {
                    string NewPasswordHash = MySecurity.GenerateSHA1Hash(txtPassword.Text.ToString());

                    //Update new Password - Ok.
                    if (InfoUser.UpdatePassword(NewPasswordHash) != 0)
                    {
                        //If IDSecurityQuestion Is not set yet, delete ConfirmationCode Set on SecurityAnswer field.
                        if (InfoUser.IDSecurityQuestion == null || InfoUser.IDSecurityQuestion.Equals(""))    //IDSecurityQuestion NOT set
                        {
                            //UPDATE ConfirmationCode in User Table (column SecurityAnswer)
                            InfoUser.UpdateTemporarySecurityAnswer(null);
                        }

                        //Show JQueryUi BoxDialog With Redirect
                        string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-9999");
                        string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0008");
                        string RedirectUrl = AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                        //WRITE A ROW IN STATISTICS DB
                        try
                        {
                            MyStatistics NewStatisticUser = new MyStatistics(InfoUser.IDUser, null, StatisticsActionType.US_ChangePassword, "Password Changed", Network.GetCurrentPageName(), "", "");
                            NewStatisticUser.InsertNewRow();
                        }
                        catch { }

                        //Logout of user actually connected
                        //Destroy Session Variables
                        Session.Abandon();

                        //Destroy Cookie
                        Network.DestroyCookie();
                    }
                    else
                    {
                        //Error in Update new Password

                        //Show JQueryUi BoxDialog With Redirect
                        string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                        string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0006");
                        string RedirectUrl = AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                        //WRITE A ROW IN LOG FILE AND DB
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0006", "Change Password Error", InfoUser.IDUser.ToString(), true, false);
                            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                            LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                        }
                        catch { }
                    }
                }
                else
                {
                    //Error in ConfirmationCode

                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0002");
                    string RedirectUrl = url;
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0002", RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0002"), InfoUser.IDUser.ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                        LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Reset Password: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }

        #region btnFacebook_Click
        protected void btnFacebook_Click(object sender, ImageClickEventArgs e)
        {
            Session["Offset"] = hfOffset.Value;

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
            Session["Offset"] = hfOffset.Value;

            string urlRedirect = "/auth/auth.aspx?googleauth=true";

            Response.Redirect(urlRedirect, true);
        }
        #endregion

    }
}