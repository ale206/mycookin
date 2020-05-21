using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Common;
using MyCookin.ErrorAndMessage;
using MyCookin.Log;

namespace MyCookinWeb.UserInfo
{
    public partial class ActivateUser :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Logout of any user actually connected
                //Destroy Session Variables
                Session["IDUser"] = "";
                Session.Abandon();

                //Destroy Cookie
                Network.DestroyCookie();

                Guid IDUser = new Guid(Request.QueryString["ID"]);
                string ConfirmationCode = Request.QueryString["ConfirmationCode"];

                //Get current User IP
                string CurrentIp = HttpContext.Current.Request.UserHostAddress;

                MyUser ActivateNewUser = new MyUser(IDUser, ConfirmationCode, CurrentIp);

                //In ActivateUser check if the ConfirmationCode is correct as well
                ManageUSPReturnValue result = ActivateNewUser.ActivateUser();

                

                if (!result.IsError)
                {
                    string url = AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain);

                    //AutoAutenticate User if set in WebConfig
                    try
                    {
                        if (Convert.ToBoolean(AppConfig.GetValue("AutologinAfterActivation", AppDomain.CurrentDomain)))
                        {
                            //Get Basic User Informations
                            ActivateNewUser.GetUserBasicInfoByID();
                            //Set Session Variables
                            ActivateNewUser.SetLoginSessionVariables();
                        }
                    }
                    catch (Exception ex)
                    {
                        //WRITE A ROW IN LOG FILE AND DB
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in AutoLogin After Activation: " + ex.Message, IDUser.ToString(), true, false);
                            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                            LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                        }
                        catch { }
                    }
                    
                    //USER ACTIVATED OR ALREADY ACTIVATED
                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0022");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), result.ResultExecutionCode);
                    string RedirectUrl = AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //The log for this action is in MyUser.cs
                }
                else
                {
                    //ERROR IN ACTIVATION
                    //Show JQueryUi BoxDialog With Redirect
                    string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
                    string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), result.ResultExecutionCode);
                    string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

                    //The log for this action is in MyUser.cs
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Activation User.", "", true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }

                //Redirect to HomePage
                string url = (ResolveClientUrl(AppConfig.GetValue("HomePage", AppDomain.CurrentDomain))).ToLower();
                
                Response.Redirect(url, true);
            }

        }
    }
}