using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyCookin.ObjectManager.ContactManager;
using MyCookin.Common;

namespace MyCookin.WebServices.Contact
{
    /// <summary>
    /// Summary description for ContactManager
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ContactManager : System.Web.Services.WebService
    {

        #region SendNewContactRequest
        [WebMethod]
        //Send new message
        public List<ContactRequest> SendNewContactRequest(int IDLanguage, string FirstName, string LastName, string Email, string RequestText, bool PrivacyAccept,
                                string IpAddress, int IDContactRequestType)
        {
            List<ContactRequest> ContactRequestList = new List<ContactRequest>();

            DateTime Requestdate = DateTime.UtcNow;

            try
            {
                ContactRequest NewContactRequest = new ContactRequest(IDLanguage, FirstName, LastName, Email, RequestText, PrivacyAccept,
                                Requestdate, IpAddress, IDContactRequestType);

                ContactRequestList = NewContactRequest.InsertNewRequest();

                //Send email to new user
                string From = AppConfig.GetValue("EmailFromProfileUser", AppDomain.CurrentDomain);
                string To = "alessio@mycookin.com";
                string Cc = "saverio@mycookin.com";
                string Subject = "New Message from MyCookin Contact Page - Type " + IDContactRequestType;
                //string url = "/PagesForEmail/WelcomeUser.aspx?link=" + link;
                string url = "";
                string Message = "<a href=\"mailto:" + Email + "\">" + FirstName + " " + LastName + "</a> scrive: <br>" + RequestText;

                Network Mail = new Network(From, To, Cc, "", Subject, Message, url);

                if (!Mail.SendEmail())
                {
                    ContactRequestList.Add(
                        new ContactRequest()
                        {
                            IsError = true,
                            ResultExecutionCode = "",
                            USPReturnValue = "Error in send email!"
                        }
                    );
                }


                return ContactRequestList;
            }
            catch
            {
                return ContactRequestList;
            }
        }
        #endregion
    }
}
