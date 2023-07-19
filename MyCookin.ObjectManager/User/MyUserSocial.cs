using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.User.ds_SocialConnectionTableAdapters;
using System.Data;
using MyCookin.Common;
using System.Configuration;
using Google.GData.Client;
using MyCookin.ObjectManager.SocialAction.Google;

using Google.GData.Extensions;
using System.Text.RegularExpressions;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.SocialAction;
using MyCookin.Log;
using Google.GData.Apps;
using Facebook;
using Facebook.Web;
using MyCookinWeb.Auth;
using System.Data.SqlClient;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.StatisticsManager;
using System.Web;
using Google.Contacts;

namespace MyCookin.ObjectManager.UserManager
{
    public class MyUserSocial
    {
        #region PrivateFields
        protected Guid _IDUser;
        protected int _IDSocialNetwork;
        protected string _Link;
        protected bool _VerifiedEmail;
        protected string _PictureUrl;
        protected string _Locale;
        protected string _AccessToken;
        protected string _RefreshToken;
        protected DateTime? _FriendsRetrievedOn;
        protected string _IDUserSocial;
        protected Guid _IDSocialLogin;

        protected DateTime? _LastTimeContacted;
        protected bool? _ContactAgain;
        protected string _FullName;
        protected string _GivenName;
        protected string _FamilyName;
        protected string _Emails;
        protected string _Phones;
        protected string _PhotoUrl;
        protected string _IDUserOnSocial;

        #endregion

        #region PublicFields
        public Guid IDUser
        {
        get { return _IDUser;}
        set { _IDUser = value;}
        }
        public int IDSocialNetwork
        {
        get { return _IDSocialNetwork;}
        set { _IDSocialNetwork = value;}
        }
        public string Link
        {
        get { return _Link;}
        set { _Link = value;}
        }
        public bool VerifiedEmail
        {
        get { return _VerifiedEmail;}
        set { _VerifiedEmail = value;}
        }
        public string PictureUrl
        {
        get { return _PictureUrl;}
        set { _PictureUrl = value;}
        }
        public string Locale
        {
        get { return _Locale;}
        set { _Locale = value;}
        }
        public string AccessToken
        {
        get { return _AccessToken;}
        set { _AccessToken = value;}
        }
        public string RefreshToken
        {
            get { return _RefreshToken; }
            set { _RefreshToken = value; }
        }
        public DateTime? FriendsRetrievedOn
        {
        get { return _FriendsRetrievedOn;}
        set { _FriendsRetrievedOn = value;}
        }
        public string IDUserSocial
        {
        get { return _IDUserSocial;}
        set { _IDUserSocial = value;}
        }
        public Guid IDSocialLogin
        {
        get { return _IDSocialLogin;}
        set { _IDSocialLogin = value;}
        }

        public DateTime? LastTimeContacted
        {
            get { return _LastTimeContacted; }
            set { _LastTimeContacted = value; }
        }

        public bool? ContactAgain
        {
            get { return _ContactAgain; }
            set { _ContactAgain = value; }
        }

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public string GivenName
        {
            get { return _GivenName; }
            set { _GivenName = value; }
        }

        public string FamilyName
        {
            get { return _FamilyName; }
            set { _FamilyName = value; }
        }

        public string Emails
        {
            get { return _Emails; }
            set { _Emails = value; }
        }

        public string Phones
        {
            get { return _Phones; }
            set { _Phones = value; }
        }

        public string PhotoUrl
        {
            get { return _PhotoUrl; }
            set { _PhotoUrl = value; }
        }

        public string IDUserOnSocial
        {
            get { return _IDUserOnSocial; }
            set { _IDUserOnSocial = value; }
        }

        #endregion

        #region Constructors
        public MyUserSocial()
        {

        }

        public MyUserSocial(Guid IDUser, int IDSocialNetwork)
        {
            _IDUser = IDUser;
            _IDSocialNetwork = IDSocialNetwork;

            //Call method to get user information for social.
            GetUserSocialInformations();
        }

                /// <summary>
        /// MyUser For Social Informations
        /// </summary>
        /// <param name="UserID"></param>

        public MyUserSocial(Guid IDUser, int IDSocialNetwork, DateTime? LastTimeContacted, bool? ContactAgain, string FullName, string GivenName, string FamilyName, string Emails, string Phones, string PhotoUrl, string IDUserOnSocial)
        {
            _IDUser = IDUser;

            _IDSocialNetwork = IDSocialNetwork;
            _LastTimeContacted = LastTimeContacted;
            _ContactAgain = ContactAgain;
            _FullName = FullName;
            _GivenName = GivenName;
            _FamilyName = FamilyName;
            _Emails = Emails;
            _Phones = Phones;
            _PhotoUrl = PhotoUrl;
            _IDUserOnSocial = IDUserOnSocial;

        }

        #endregion

        #region Methods
        #region GetUserSocialInformations
        /// <summary>
        /// Get User Social Informations - AccessToken, Link, Id On SocialNetwork, etc.
        /// </summary>
        public void GetUserSocialInformations()
        { 
            SocialLoginsTableAdapter SocialLoginDal = new SocialLoginsTableAdapter();

            DataTable DT_UserSocialInfo = new DataTable();

            DT_UserSocialInfo = SocialLoginDal.GetUserSocialInformations(_IDUser, _IDSocialNetwork);

            if (DT_UserSocialInfo.Rows.Count > 0)
            {
                _Link = DT_UserSocialInfo.Rows[0].Field<string>("Link");
                _VerifiedEmail = DT_UserSocialInfo.Rows[0].Field<bool>("VerifiedEmail");
                _PictureUrl = DT_UserSocialInfo.Rows[0].Field<string>("PictureUrl");
                _Locale = DT_UserSocialInfo.Rows[0].Field<string>("Locale");
                _AccessToken = DT_UserSocialInfo.Rows[0].Field<string>("AccessToken");
                _RefreshToken = DT_UserSocialInfo.Rows[0].Field<string>("RefreshToken");
                _FriendsRetrievedOn = DT_UserSocialInfo.Rows[0].Field<DateTime?>("FriendsRetrievedOn");
                _IDUserSocial = DT_UserSocialInfo.Rows[0].Field<string>("IDUserSocial");
                _IDSocialLogin = DT_UserSocialInfo.Rows[0].Field<Guid>("IDSocialLogin");
            }
        }
        #endregion

        #region GetIDSocialFriendsByIDUser
        /// <summary>
        /// Get all IDSocialFriends by IDUser
        /// </summary>
        /// <returns></returns>
        public DataTable GetIDSocialFriendsByIDUser()
        {
            SocialUserFriendsTableAdapter SocialUserFriendsDal = new SocialUserFriendsTableAdapter();

            DataTable DT_SocialUserFriends = new DataTable();

            DT_SocialUserFriends = SocialUserFriendsDal.GetIDSocialFriendsByIDUser(_IDUser);

            return DT_SocialUserFriends;
        }
        #endregion

        #region GetUserSocialFriends
        /// <summary>
        /// Get Friends informations by Social Network for this user
        /// </summary>
        /// <param name="SocialNetwork"></param>
        /// <returns>DataTable with all informations - In datatable</returns>
        public DataTable GetUserSocialFriends(int SocialNetwork)
        {
            SocialFriendsTableAdapter SocialFriendsDal = new SocialFriendsTableAdapter();

            DataTable DT_SocialUserFriendsInformation = new DataTable();

            DT_SocialUserFriendsInformation = SocialFriendsDal.GetFriendsByIDUserAndIDSocialNetwork(_IDUser, SocialNetwork);

            return DT_SocialUserFriendsInformation;
        }
        #endregion

        #region GetIDUsersWithOldFriendsRetrievedOn
        /// <summary>
        /// Get all IDSocialFriends by IDUser
        /// </summary>
        /// <returns></returns>
        public static DataTable GetIDUsersWithOldFriendsRetrievedOn()
        {

            int DaysOfLastRetrieveSocialFriends = MyConvert.ToInt32(AppConfig.GetValue("DaysOfLastRetrieveSocialFriends", AppDomain.CurrentDomain),7);
            DateTime DateToCheck = DateTime.UtcNow.AddDays(-DaysOfLastRetrieveSocialFriends);

            SocialLoginsTableAdapter SocialLoginsDal = new SocialLoginsTableAdapter();

            DataTable DT_SocialLogins = new DataTable();

            DT_SocialLogins = SocialLoginsDal.GetIDUsersWithOldFriendsRetrievedOn(DateToCheck);

            return DT_SocialLogins;
        }
        #endregion

        #region GetIDUserSocialFromIDUserAndIDSocialNetwork
        /// <summary>
        /// Get all IDSocialFriends by IDUser
        /// </summary>
        /// <returns></returns>
        public string GetIDUserSocialFromIDUserAndIDSocialNetwork(int IDSocialNetwork)
        {
            string IDUserSocial = String.Empty;

            try
            {
                SocialLoginsTableAdapter SocialLoginsDal = new SocialLoginsTableAdapter();

                DataTable DT_UserSocial = new DataTable();

                DT_UserSocial = SocialLoginsDal.GetIDUserSocialFromIDUserAndIDSocialNetwork(_IDUser, IDSocialNetwork);

                if (DT_UserSocial.Rows.Count > 0)
                {
                    IDUserSocial = DT_UserSocial.Rows[0].Field<string>("IDUserSocial");
                }
            }
            catch
            { 
            
            }
            
            return IDUserSocial;
        }
        #endregion

        #region UpdateFriendsRetrievedOn
        public bool UpdateFriendsRetrievedOn()
        {
            bool returnValue = false;

            try
            {
                SocialLoginsTableAdapter TASocialLogins = new SocialLoginsTableAdapter();
                TASocialLogins.UpdateFriendsRetrievedOn(DateTime.UtcNow, _IDUser, _IDSocialNetwork);

                returnValue = true;
            }
            catch
            {
                returnValue = false;
            }

            return returnValue;
        }
        #endregion

        #region GetIDUserFromSocialLogins
        /// <summary>
        /// Get IDUser from Social Logins Table
        /// </summary>
        /// <param name="IDUserSocial">User ID on social Network</param>
        /// <param name="SocialNetwork">Social Network ID</param>
        /// <returns></returns>
        public string GetIDUserFromSocialLogins(string IDUserSocial, int IDSocialNetwork)
        {
            SocialLoginsTableAdapter taSocialLogin = new SocialLoginsTableAdapter();

            DataTable dtSocialLogin = new DataTable();

            try
            {
                dtSocialLogin = taSocialLogin.GetIDUserFromSocialLogins(IDUserSocial, IDSocialNetwork);

                _IDUser = dtSocialLogin.Rows[0].Field<Guid>("IDUser");

                _IDUserSocial = _IDUser.ToString();
            }
            catch
            {
                _IDUserSocial = "";
            }

            return _IDUserSocial;
        }

        #endregion

        #region MemorizeIDUserSocial
        public bool MemorizeIDUserSocial(int IDSocialNetwork, string Link, bool? VerifiedEmail, string PictureUrl, string Locale, string AccessToken, string RefreshToken)
        {
            bool returnValue = false;

            SocialLoginsTableAdapter taSocialLogin = new SocialLoginsTableAdapter();

            Guid newId = Guid.NewGuid();

            try
            {
                taSocialLogin.MemorizeIDUserSocial(newId, IDSocialNetwork, _IDUser, _IDUserSocial, Link, VerifiedEmail, PictureUrl, Locale, AccessToken, RefreshToken);

                returnValue = true;
            }
            catch
            {
                returnValue = false;
            }

            return returnValue;
        }
        #endregion

        #region UpdateSocialTokens
        public static bool UpdateSocialTokens(int IDSocialNetwork, Guid IDUser, string AccessToken, string RefreshToken)
        {
            bool returnValue = false;

            SocialLoginsTableAdapter taSocialLogin = new SocialLoginsTableAdapter();

            Guid newId = Guid.NewGuid();

            try
            {
                taSocialLogin.UpdateSocialTokens(AccessToken, RefreshToken, IDUser, IDSocialNetwork);

                returnValue = true;
            }
            catch
            {
                returnValue = false;
            }

            return returnValue;
        }
        #endregion

        #region GetUserFriendsFromSocialNetwork
        public bool GetUserFriendsFromSocialNetwork(string AccessToken, string RefreshToken)
        {
            bool resultExecute = false;

            try
            {
                switch (_IDSocialNetwork)
                {
                    case 1:
                        //Google

                        //Memorize All User Contacts Informations
                        string applicationName = ConfigurationManager.AppSettings["google_applicationName"].ToString();

                        SocialGoogleAuthentication GoogleAuthentication = new SocialGoogleAuthentication(AccessToken, RefreshToken);
                        GoogleAuthentication.Token = AccessToken;

                        GoogleAuthentication.RefreshToken = RefreshToken;

                        OAuth2Parameters parameters = GoogleAuthentication.GetParameters();

                        //Memorize All User Contacts Informations
                        MemorizeGoogleContacts(applicationName, parameters);

                        resultExecute = true;
                        break;

                    case 2:
                        //Facebook
                        MemorizeFacebookContact(AccessToken);

                        resultExecute = true;
                        break;
                }
            }
            catch
            { 
            
            }

            return resultExecute;
        }
        #endregion

        #region MemorizeAllUserContactsInformations

        #region Google
        protected void MemorizeGoogleContacts(string applicationName, OAuth2Parameters parameters)
        {
            //Get All User Contacts Informations
            try
            {
                RequestSettings settings = new RequestSettings(applicationName, parameters);

                ContactsRequest cr = new ContactsRequest(settings);     //Request all contacts
                settings.AutoPaging = true;                             //Allow autopaging - IMPORTANT!
                Feed<Contact> f = cr.GetContacts();                     //Get all contacts

                //Get All Contacts
                foreach (Contact cc in f.Entries)
                {
                    Name n = cc.Name;
                    _FamilyName = n.FamilyName;
                    _FullName = n.FullName;
                    _GivenName = n.GivenName;

                    //string IdUserOnSocial = cc.Id;
                    _Emails = "";
                    _Phones = "";

                    foreach (EMail email in cc.Emails)
                    {
                        _Emails += email.Address + ";";
                    }

                    //Extract Phone Numbers
                    foreach (PhoneNumber ph in cc.Phonenumbers)
                    {
                        _Phones += ph.Value + ";";
                    }

                    //INSERT ONLY IF THE CONTACT HAS AN EMAIL
                    //Is possible to have a contact with the phone only and no email.
                    try
                    {
                        _IDUserOnSocial = cc.Emails[0].Address;  //We can not have ID friend user on Google because is simply our Address Book, so we get just the first email

                        Regex regex = new Regex(@"^[\w\-\.]*[\w\.]\@[\w\.]*[\w\-\.]+[\w\-]+[\w]\.+[\w]+[\w $]");

                        Match match = regex.Match(_IDUserOnSocial);    
                        if (match.Success)
                        {
                            //MyUser UserForSocial = new MyUser(_IDUser, (int)SocialNetworks.Google, null, null, FullName, GivenName, FamilyName, emails, phones, "", IdUserOnSocial);

                            ManageUSPReturnValue result = MemorizeSocialContactFriend();

                            if (!result.IsError)
                            {
                                //Memorize Contact OK
                            }
                        }
                    }
                    catch
                    {
                        //string message = "this User Contact has not an email.";
                    }
                }

                //Update Data of this friends retrieve
                //User.IDSocialNetwork = (int)SocialNetworks.Google;
                UpdateFriendsRetrievedOn();

            }
            catch (AppsException a)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Memorize Social Friends" + "Error code: {0}" + a.ErrorCode + "Invalid input: {0}" + a.InvalidInput + "Reason: {0}" + a.Reason, _IDUser.ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                    LogManager.WriteFileLog(LogLevel.Warnings, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region Facebook
        protected void MemorizeFacebookContact(string accessToken)
        {
            try
            {
                string IDUserSocial = GetIDUserSocialFromIDUserAndIDSocialNetwork((int)SocialNetwork.Facebook);

                Facebook.FacebookClient app = new Facebook.FacebookClient(accessToken);
                var result = (Facebook.JsonObject)app.Get("/" + IDUserSocial + "/friends");
                List<FbUserInfo> model = new List<FbUserInfo>();  //model = friendlist array

                foreach (var friend in (Facebook.JsonArray)result["data"])
                {
                    model.Add(new FbUserInfo()
                    {
                        ID = (string)(((Facebook.JsonObject)friend)["id"]),
                        name = (string)(((Facebook.JsonObject)friend)["name"])
                    });
                }

                foreach (FbUserInfo res in model)
                {
                    _FullName = res.name;
                    _IDUserOnSocial = res.ID;

                    ManageUSPReturnValue resultFBInsert = MemorizeSocialContactFriend();

                    if (!resultFBInsert.IsError)
                    {
                        //Memorize Contact OK
                    }
                }

                //Update Data of this friends retrieve
                //User.IDSocialNetwork = (int)SocialNetworks.Faceboook;
                UpdateFriendsRetrievedOn();
            }
            catch
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Memorize Social Friends" + "Facebook Retrieve", _IDUser.ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #endregion

        #region MemorizeSocialContactFriend
        /// <summary>
        /// Insert or Update Social Contact Informations
        /// </summary>
        /// <returns></returns>
        public ManageUSPReturnValue MemorizeSocialContactFriend()
        {
            ManageUSPReturnValue MemorizeSocialContactFriendResult;

            try
            {
                ManageSocialTableAdapter MemorizeSocialContactsFriend = new ManageSocialTableAdapter();
                MemorizeSocialContactFriendResult = new ManageUSPReturnValue(MemorizeSocialContactsFriend.MemorizeSocialContactFriends(Convert.ToInt32(_IDSocialNetwork), _IDUser, _FullName, _GivenName, _FamilyName, _Emails, _Phones, _PhotoUrl, _IDUserOnSocial));
                //UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, MemorizeSocialContactFriendResult);

                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(new Guid(HttpContext.Current.Session["IDUser"].ToString()), null, StatisticsActionType.SC_ContactFriendsMemorized, "Social Contatct Friends Memorized", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }
            }
            catch (SqlException sqlEx)
            {
                MemorizeSocialContactFriendResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                UserLogUSP(LogLevel.Debug, LogLevel.Debug, true, MemorizeSocialContactFriendResult);
            }
            catch (Exception ex)
            {
                MemorizeSocialContactFriendResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                UserLogUSP(LogLevel.Debug, LogLevel.Debug, true, MemorizeSocialContactFriendResult);
            }

            return MemorizeSocialContactFriendResult;
        }

        #endregion

        #region IsUserRegisteredToThisSocial?

        public static bool IsUserRegisteredToThisSocial(Guid IDUser, int IDSocialNetwork)
        {
            bool IsRegistered = false;

            try
            {
                SocialLoginsTableAdapter taSocialLogin = new SocialLoginsTableAdapter();

                IsRegistered = Convert.ToBoolean(taSocialLogin.IsUserRegisteredToThisSocialNetwork(IDUser, IDSocialNetwork));
            }
            catch
            {
            
            }

            return IsRegistered;

            //IsUserRegisteredToThisSocialNetwork
        }
        #endregion

        #region UserLogUSP - NOTE: This is present in MyUserFriendship, MyUser.
        /// <summary>
        /// Call WriteLog class for stored Procedure result
        /// </summary>
        /// <param name="LogDbLevel">Log Level for write on Database Log Table</param>
        /// <param name="LogFsLevel">Log Level for write log file</param>
        /// <param name="SendEmail">Send Email if true</param>
        /// <param name="USPReturn">The return class of the Stored Procedure called</param>
        public static void UserLogUSP(LogLevel LogDbLevel, LogLevel LogFsLevel, bool SendEmail, ManageUSPReturnValue USPReturn)
        {
            try
            {
                //Note: For All Users StoredProcedure, USPReturn.USPReturnValue is IDUser.

                int IDLanguageForLog;

                try
                {
                    IDLanguageForLog = Convert.ToInt32(AppConfig.GetValue("IDLanguageForLog", AppDomain.CurrentDomain));
                }
                catch
                {
                    IDLanguageForLog = 1;
                }

                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogDbLevel.ToString(), "", Network.GetCurrentPageName(), USPReturn.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguageForLog, USPReturn.ResultExecutionCode), USPReturn.USPReturnValue, false, true);
                    LogManager.WriteFileLog(LogFsLevel, SendEmail, NewRow);
                    LogManager.WriteDBLog(LogDbLevel, NewRow);
                }
                catch { }
            }
            catch
            {

            }
        }
        #endregion

        #endregion //End region Methods
    }
}
