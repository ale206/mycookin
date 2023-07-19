using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;

namespace MyCookin.WebServices.User
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class CheckUser : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string CheckUsername()
        {
            string username = this.Context.Request.QueryString["username"];
            string currentUsername = this.Context.Request.QueryString["currentUsername"];

            string returnValue = string.Empty;

            if (String.IsNullOrEmpty(currentUsername))
            {
                if (MyUser.CheckUsername(username))
                {
                    //Exist
                    returnValue = "not_available";
                }
                else
                {
                    returnValue = "available";
                }
            }
            else
            {
                if (MyUser.CheckUserNameExcludingCurrent(username, currentUsername))
                {
                    //Exist
                    returnValue = "not_available";
                }
                else
                {
                    returnValue = "available";
                }
            }

            return returnValue;
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string CheckEmail()
        {
            string email = this.Context.Request.QueryString["email"];
            string currentEmail = this.Context.Request.QueryString["currentEmail"];

            string returnValue = string.Empty;

            if (String.IsNullOrEmpty(currentEmail))
            {
                if (MyUser.CheckEmail(email))
                {
                    //Exist
                    returnValue = "not_available";
                }
                else
                {
                    returnValue = "available";
                }
            }
            else
            {
                if (MyUser.CheckEmailExistenceExcludingCurrent(email, currentEmail))
                {
                    //Exist
                    returnValue = "not_available";
                }
                else
                {
                    returnValue = "available";
                }
            }
            return returnValue;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string CheckPasswordStrenght()
        {
            string password = this.Context.Request.QueryString["password"];
            string usn = this.Context.Request.QueryString["usn"];

            string returnValue = string.Empty;
            try
            {
                int success = Convert.ToInt32(MySecurity.PasswordAdvisor.CheckPasswordStrength(password, usn));

                if (success <= 2) // Password Not Secure
                {
                    returnValue = "not_secure";

                }
                else //Password Ok
                {
                    returnValue = "secure";
                }

            }
            catch
            {
                //Handle Error
            }
            finally
            {
                //sqlConn.Close();
            }

            return returnValue;
        }
    }
}
