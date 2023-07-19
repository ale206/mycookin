using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MailChimp;
using MailChimp.Helper;
using MailChimp.Lists;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.MyAdmin
{
    public partial class MailchimpUpdateLists : System.Web.UI.Page
    {
        /*LINKS:
     * https://github.com/danesparza/MailChimp.NET
     * http://apidocs.mailchimp.com/api/downloads/
     * https://us5.admin.mailchimp.com/account/api/
     * */

        public class MyMergeVar : MergeVar
        {
            [System.Runtime.Serialization.DataMember(Name = "FNAME")]
            public string FNAME { get; set; }
            [System.Runtime.Serialization.DataMember(Name = "LNAME")]
            public string LNAME { get; set; }
        }

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

            if (!IsPostBack)
            {
                UpdateLists();
            }
        }

        #region GetLists
        void GetLists(MailChimpManager mc)
        {
            // Next, make any API call you'd like:
            ListResult lists = mc.GetLists();

            //Getting the first 100 users in each list:
            //  For each list
            foreach (var list in lists.Data)
            {
                //  Write out the list name:
                //Debug.WriteLine("Users for the list " + list.Name);

                //  Get the first 100 members of each list:
                MembersResult results = mc.GetAllMembersForList(list.Id, "subscribed", 0, 100);

                //  Write out each member's email address:
                foreach (var member in results.Data)
                {
                    //Debug.WriteLine(member.Email);
                }
            }
        }
        #endregion

        #region AddEmail
        void AddEmail(MailChimpManager mc, string IDLista, string email, string FirstName, string LastName)
        {
            //  Create the email parameter
            EmailParameter emailParameter = new EmailParameter()
            {
                Email = email
            };

            //Add First Name And Last Name
            MyMergeVar myMergeVars = new MyMergeVar()
            {
                FNAME = FirstName,
                LNAME = LastName
            };

            string IDListaTest = IDLista;

            string EmailType = "html";
            bool DoubleOptIn = false;
            bool UpdateExisting = true;
            bool ReplaceInterests = true;
            bool SendWelcome = false;

            EmailParameter EmailResults = mc.Subscribe(IDListaTest, emailParameter, myMergeVars, EmailType, DoubleOptIn, UpdateExisting, ReplaceInterests, SendWelcome);
        }
        #endregion

        #region UpdateLists
        public bool UpdateLists()
        {
            bool ExecutionResult = false;

            try
            {
                // Pass the API key on the constructor:
                string MailchimpKey = AppConfig.GetValue("MailchimpKey", AppDomain.CurrentDomain);

                MailChimpManager mc = new MailChimpManager(MailchimpKey);

                string IDList_IT = AppConfig.GetValue("List_It", AppDomain.CurrentDomain);
                string IDList_EN = AppConfig.GetValue("List_En", AppDomain.CurrentDomain);
                string IDList_ES = AppConfig.GetValue("List_Es", AppDomain.CurrentDomain);

                //The SP automatically get the last retrieve date and update the table on db

                //Get all new English Users
                MyUser user = new MyUser();
                List<MyUser> NewUsers = new List<MyUser>();

                NewUsers = user.GetNewUsersForMailchimp();

                foreach (MyUser us in NewUsers)
                {
                    if (us.IDLanguage == 1)
                    {
                        //AGGIORNA LISTA EN
                        AddEmail(mc, IDList_EN, us.eMail, us.Name, us.Surname);

                        lblResult.Text += "EN - Utente Inserito: " + us.eMail + " - " + us.Name + " " + us.Surname + "<br>";
                    }
                    else if (us.IDLanguage == 2)
                    {
                        //AGGIORNA LISTA IT
                        AddEmail(mc, IDList_IT, us.eMail, us.Name, us.Surname);

                        lblResult.Text += "IT - Utente Inserito: " + us.eMail + " - " + us.Name + " " + us.Surname + "<br>";
                    }
                    else if (us.IDLanguage == 3)
                    {
                        //AGGIORNA LISTA ES
                        AddEmail(mc, IDList_ES, us.eMail, us.Name, us.Surname);

                        lblResult.Text += "ES - Utente Inserito: " + us.eMail + " - " + us.Name + " " + us.Surname + "<br>";
                    }

                    ExecutionResult = true;
                }
            }
            catch(Exception ex)
            {
                lblResult.Text += "Errore nell'aggiornamento: " + ex.Message;

                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), ex.Message, "Error in Mailchimp Update Lists", Session["IDUser"].ToString(), true, false);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
            }

            lblResult.Text += " - Operazione Completata.";

            return ExecutionResult;
            
        }

        #endregion
    }
}