using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using MyCookin.DAL.User.ds_UserInfoTableAdapters;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;
using System.Web;
using MyCookin.DAL.ErrorAndMessage.ds_ErrorAndMessageTableAdapters;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.DAL.User.ds_SocialConnectionTableAdapters;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.DAL.Statistics;

namespace MyCookin.ObjectManager.UserManager
{
    /// <summary>
    /// CLASS MyUser
    /// </summary>
    public class MyUser
    {
        #region PrivateField
        protected Guid _IDUser;
        protected string _Name;
        protected string _Surname;
        protected string _UserName;
        protected int? _UserDomain;
        protected int? _UserType;
        protected string _PasswordHash;
        protected DateTime? _LastPasswordChange;
        protected DateTime? _PasswordExpireOn;
        protected bool? _ChangePasswordNextLogon;
        protected bool? _ContractSigned;
        protected DateTime? _BirthDate;
        protected string _eMail;
        protected DateTime? _MailConfirmedOn;
        protected string _Mobile;
        protected string _MobileConfirmationCode;
        protected DateTime? _MobileConfirmedOn;
        protected int? _IDLanguage;
        protected int? _IDCity;
        protected Photo _IDProfilePhoto;
        protected bool? _UserEnabled;
        protected bool? _UserLocked;
        protected bool? _MantainanceMode;
        protected int? _IDSecurityQuestion;
        protected string _SecurityAnswer;
        protected DateTime? _DateMembership;
        protected DateTime? _AccountExpireOn;
        protected DateTime? _LastLogon;
        protected DateTime? _LastLogout;
        protected int _Offset;

        protected DateTime? _LastProfileUpdate;
        protected bool? _UserIsOnLine;
        protected string _LastIpAddress;
        protected int? _IDVisibility;
        protected string _ConfirmationCode;
        protected string _SecurityQuestion;
        protected int? _IDGender;
        protected string _IDSecurityGroupList;
        protected DateTime? _AccountDeletedOn;

        protected bool? _isProfessionalCook;
        protected bool? _cookInRestaurant;
        protected bool? _cookAtHome;
        protected string _cookDescription;
        protected DateTime? _cookMembership;


        protected string _IDUserSocial;

        protected MyUserPropertyCompiled[] _PropertyCompiled;

        protected string _CompleteName;

        #endregion

        #region PublicField
        public Guid IDUser
        {
            get { return _IDUser; }
            set { _IDUser = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public int? UserDomain
        {
            get { return _UserDomain; }
            set { _UserDomain = value; }
        }
        public int? UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
        public string PasswordHash
        {
            get { return _PasswordHash; }
            set { _PasswordHash = value; }
        }
        public DateTime? LastPasswordChange
        {
            get { return _LastPasswordChange; }
            set { _LastPasswordChange = value; }
        }
        public DateTime? PasswordExpireOn
        {
            get { return _PasswordExpireOn; }
            set { _PasswordExpireOn = value; }
        }
        public bool? ChangePasswordNextLogon
        {
            get { return _ChangePasswordNextLogon; }
            set { _ChangePasswordNextLogon = value; }
        }
        public bool? ContractSigned
        {
            get { return _ContractSigned; }
            set { _ContractSigned = value; }
        }
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }
        public string eMail
        {
            get { return _eMail; }
            set { _eMail = value; }
        }
        public DateTime? MailConfirmedOn
        {
            get { return _MailConfirmedOn; }
            set { _MailConfirmedOn = value; }
        }
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public string MobileConfirmationCode
        {
            get { return _MobileConfirmationCode; }
            set { _MobileConfirmationCode = value; }
        }
        public DateTime? MobileConfirmedOn
        {
            get { return _MobileConfirmedOn; }
            set { _MobileConfirmedOn = value; }
        }
        public int? IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }
        public int? IDCity
        {
            get { return _IDCity; }
            set { _IDCity = value; }
        }
        public Photo IDProfilePhoto
        {
            get { return _IDProfilePhoto; }
            set { _IDProfilePhoto = value; }
        }
        public bool? UserEnabled
        {
            get { return _UserEnabled; }
            set { _UserEnabled = value; }
        }
        public bool? UserLocked
        {
            get { return _UserLocked; }
            set { _UserLocked = value; }
        }
        public bool? MantainanceMode
        {
            get { return _MantainanceMode; }
            set { _MantainanceMode = value; }
        }
        public int? IDSecurityQuestion
        {
            get { return _IDSecurityQuestion; }
            set { _IDSecurityQuestion = value; }
        }
        public string SecurityAnswer
        {
            get { return _SecurityAnswer; }
            set { _SecurityAnswer = value; }
        }
        public DateTime? DateMembership
        {
            get { return _DateMembership; }
            set { _DateMembership = value; }
        }
        public DateTime? AccountExpireOn
        {
            get { return _AccountExpireOn; }
            set { _AccountExpireOn = value; }
        }
        public DateTime? LastLogon
        {
            get { return _LastLogon; }
            set { _LastLogon = value; }
        }
        public DateTime? LastLogout
        {
            get { return _LastLogout; }
            set { _LastLogout = value; }
        }
        public int Offset
        {
            get { return _Offset; }
            set { _Offset = value; }
        }
        public DateTime? LastProfileUpdate
        {
            get { return _LastProfileUpdate; }
            set { _LastProfileUpdate = value; }
        }
        public DateTime? AccountDeletedOn
        {
            get { return _AccountDeletedOn; }
            set { _AccountDeletedOn = value; }
        }
        public bool? UserIsOnLine
        {
            get { return _UserIsOnLine; }
            set { _UserIsOnLine = value; }
        }
        public string LastIpAddress
        {
            get { return _LastIpAddress; }
            set { _LastIpAddress = value; }
        }
        public int? IDVisibility
        {
            get { return _IDVisibility; }
            set { _IDVisibility = value; }
        }
        public string ConfirmationCode
        {
            get { return _ConfirmationCode; }
            set { _ConfirmationCode = value; }
        }
        public string SecurityQuestion
        {
            get { return _SecurityQuestion; }
            set { _SecurityQuestion = value; }
        }
        public int? IDGender
        {
            get { return _IDGender; }
            set { _IDGender = value; }
        }

        public bool? IsProfessionalCook
        {
            get { return _isProfessionalCook; }
            set { _isProfessionalCook = value; }
        }
        public bool? CookInRestaurant
        {
            get { return _cookInRestaurant; }
            set { _cookInRestaurant = value; }
        }
        public bool? CookAtHome
        {
            get { return _cookAtHome; }
            set { _cookAtHome = value; }
        }
        public string CookDescription
        {
            get { return _cookDescription; }
            set { _cookDescription = value; }
        }
        public DateTime? CookMembership
        {
            get { return _cookMembership; }
            set { _cookMembership = value; }
        }

        public string IDUserSocial
        {
            get { return _IDUserSocial;}
            set { _IDUserSocial = value;}
        }

        public MyUserPropertyCompiled[] PropertyCompiled
        {
            get { return _PropertyCompiled; }
        }

        public string CompleteName
        {
            get { return _CompleteName; }
            set { _CompleteName = value; }
        }
        #endregion

        #region Constructors
        
        //For Inheritance
        //protected MyUser()
        //{
        //}

        /// <summary>
        /// Empty Constructor, generally utilized for get list of Users
        /// </summary>
        public MyUser()
        {
            
        }

        /// <summary>
        /// Get User info from user GUID
        /// </summary>
        /// <param name="UserID">User GUID - Uniqueidentifier format</param>
        public MyUser(Guid UserID)
        {
            _IDUser = UserID;
        }

        /// <summary>
        /// Get User info by Email or Username. Select Just one. Automatically it get the IDUser
        /// </summary>
        /// <param name="eMail">Email of the user</param>
        /// <param name="eMail">Username of the user</param>
        public MyUser(string eMail, string Username)
        {
            if (!String.IsNullOrEmpty(eMail))
            { 
                _eMail = eMail;

                //Automatically Get _IDUser
                _IDUser = GetIDUserFromEmail(_eMail);
            }
            else if (!String.IsNullOrEmpty(Username))
            { 
                _UserName = Username;

                //Automatically Get _IDUser
                _IDUser = GetIDUserFromUsername(_UserName);
            }
        }

        /// <summary>
        /// Activate User after Registration
        /// </summary>
        /// <param name="UserID">User GUID - Uniqueidentifier format</param>
        /// <param name="ConfirmationCode">Confirmation Code (Email And Password Hashed) received from User</param>
        public MyUser(Guid UserID, string ConfirmationCode, string ipAddress)
        {
            _ConfirmationCode = ConfirmationCode;
            _IDUser = UserID;
            _LastIpAddress = ipAddress;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="eMail">Email from login page</param>
        /// <param name="PasswordHash">Password Hashed from login page</param>
        /// <param name="LastLogon">Current DateTime</param>
        /// <param name="UserIsOnLine">Set true, because user is logging</param>
        /// <param name="LastIpAddress">User Ip Address</param>
        [Obsolete("This is without Offset")]
        public MyUser(string eMail, string PasswordHash, DateTime LastLogon, bool UserIsOnLine, string LastIpAddress)
        {
            _eMail = eMail;
            _PasswordHash = PasswordHash;
            _LastLogon = LastLogon;
            _UserIsOnLine = UserIsOnLine;
            _LastIpAddress = LastIpAddress;
        }

        public MyUser(string eMail, string PasswordHash, DateTime LastLogon, bool UserIsOnLine, string LastIpAddress, int Offset)
        {
            _eMail = eMail;
            _PasswordHash = PasswordHash;
            _LastLogon = LastLogon;
            _UserIsOnLine = UserIsOnLine;
            _LastIpAddress = LastIpAddress;
            _Offset = Offset;
        }

        /// <summary>
        /// MyUser For Cook Informations
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="IsProfessionalCook"></param>
        /// <param name="CookInRestaurant"></param>
        /// <param name="CookAtHome"></param>
        /// <param name="CookDescription"></param>
        public MyUser(Guid UserID, bool IsProfessionalCook, bool CookInRestaurant, bool CookAtHome, string CookDescription)
           {
               _IDUser = UserID;

               _isProfessionalCook = IsProfessionalCook;
               _cookAtHome = CookAtHome;
               _cookDescription = CookDescription;
               _cookInRestaurant = CookInRestaurant;
           }

        #endregion

        #region Methods

        #region CheckDBConnection
        /// <summary>
        /// Check DB Connection
        /// Use something like this in each ObjectManager Class.
        /// </summary>
        /// <returns>True if OK - False if error during connection</returns>
        public static bool CheckDBConnection()
        {
            try
            {
                GetUsersDAL UsersDAL = new GetUsersDAL();
                UsersDAL.CheckDBUserConnection();

                return true;
            }
            catch (SqlException sqlEx)
            {
                string sqlErrorMessage = sqlEx.Message;

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "", "Error in Connection DB: " + sqlErrorMessage, "", true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }

                return false;
            }
        }
        #endregion

        #region NumberOfUsers
        /// <summary>
        /// Return the number of Users in Db - Table Users
        /// </summary>
        /// <returns>Number of Users</returns>
        public static int NumberOfUsers()
        {
            try
            {
                GetUsersDAL UsersDAL = new GetUsersDAL();
                int numberOfUsers = Convert.ToInt32(UsersDAL.NumberOfUsers());

                return numberOfUsers;
            }
            catch (SqlException sqlEx)
            {
                string sqlErrorMessage = sqlEx.Message;

                return 0;
            }
        }

        #endregion

        #region GetUserInfoAllByID
        /// Compile All Info for User By IDUser
        /// </summary>
        public void GetUserInfoAllByID()
        {
            GetUsersDAL UserDal = new GetUsersDAL();
            DataTable UserInfo = new DataTable();

            UserInfo = UserDal.GetUserByIDUser(_IDUser);

            if (UserInfo.Rows.Count > 0)
            {
                _IDUser = UserInfo.Rows[0].Field<Guid>("IDUser");
                _Name = UserInfo.Rows[0].Field<string>("Name");
                _Surname = UserInfo.Rows[0].Field<string>("Surname");
                _UserName = UserInfo.Rows[0].Field<string>("Username");
                _UserDomain = UserInfo.Rows[0].Field<int?>("UserDomain");
                _UserType = UserInfo.Rows[0].Field<int?>("UserType");
                _PasswordHash = UserInfo.Rows[0].Field<string>("PasswordHash");
                _LastPasswordChange = UserInfo.Rows[0].Field<DateTime?>("LastPasswordChange");
                _PasswordExpireOn = UserInfo.Rows[0].Field<DateTime?>("PasswordExpireOn");
                _ChangePasswordNextLogon = UserInfo.Rows[0].Field<bool?>("ChangePasswordNextLogon");
                _ContractSigned = UserInfo.Rows[0].Field<bool?>("ContractSigned");
                _BirthDate = UserInfo.Rows[0].Field<DateTime?>("BirthDate");
                _eMail = UserInfo.Rows[0].Field<string>("eMail");
                _MailConfirmedOn = UserInfo.Rows[0].Field<DateTime?>("MailConfirmedOn");
                _Mobile = UserInfo.Rows[0].Field<string>("Mobile");
                _MobileConfirmationCode = UserInfo.Rows[0].Field<string>("MobileConfirmationCode");
                _MobileConfirmedOn = UserInfo.Rows[0].Field<DateTime?>("MobileConfirmedOn");
                _IDLanguage = UserInfo.Rows[0].Field<int?>("IDLanguage");
                _IDCity = UserInfo.Rows[0].Field<int?>("IDCity");
                _IDProfilePhoto = UserInfo.Rows[0].Field<Guid?>("IDProfilePhoto");
                _UserEnabled = UserInfo.Rows[0].Field<bool?>("UserEnabled");
                _UserLocked = UserInfo.Rows[0].Field<bool?>("UserLocked");
                _MantainanceMode = UserInfo.Rows[0].Field<bool?>("MantainanceMode");
                _IDSecurityQuestion = UserInfo.Rows[0].Field<int?>("IDSecurityQuestion");
                _SecurityAnswer = UserInfo.Rows[0].Field<string>("SecurityAnswer");
                _DateMembership = UserInfo.Rows[0].Field<DateTime?>("DateMembership");
                _AccountExpireOn = UserInfo.Rows[0].Field<DateTime?>("AccountExpireOn");
                _LastLogon = UserInfo.Rows[0].Field<DateTime?>("LastLogon");
                _LastLogout = UserInfo.Rows[0].Field<DateTime?>("LastLogout");
                _LastProfileUpdate = UserInfo.Rows[0].Field<DateTime?>("LastProfileUpdate");
                _UserIsOnLine = UserInfo.Rows[0].Field<bool?>("UserIsOnLine");
                _LastIpAddress = UserInfo.Rows[0].Field<string>("LastIpAddress");
                _IDVisibility = UserInfo.Rows[0].Field<int?>("IDVisibility");
                _IDGender = UserInfo.Rows[0].Field<int?>("IDGender");
                _Offset = UserInfo.Rows[0].Field<int>("Offset");

                //From UserCook Table
                _isProfessionalCook = UserInfo.Rows[0].Field<bool?>("IsProfessionalCook");
                _cookInRestaurant = UserInfo.Rows[0].Field<bool?>("CookInRestaurant");
                _cookAtHome = UserInfo.Rows[0].Field<bool?>("CookAtHome");
                _cookDescription = UserInfo.Rows[0].Field<string>("CookDescription");
                _cookMembership = UserInfo.Rows[0].Field<DateTime?>("CookMembership");

                //Get a list of all Groups which user belongs
                //_IDSecurityGroupList
                DataTable dtGroup=GetSecurityUserGroupList();

                for (int i = 0; i < dtGroup.Rows.Count;i++ )
                {
                    _IDSecurityGroupList += dtGroup.Rows[i][1].ToString() + ";";
                }
            }
        }

        #endregion

        #region GetUserBasicInfoByID
        /// <summary>
        /// Compile All Info for User By IDUser - Return _Name, _Surname, _UserName, _PasswordHash, _BirthDate, _eMail, _Mobile, _IDLanguage, _IDCity, _IDProfilePhoto, _IDGender
        /// </summary>
        public void GetUserBasicInfoByID()
        {
            GetUsersDAL UserDal = new GetUsersDAL();
            DataTable UserInfo = new DataTable();

            UserInfo = UserDal.GetUserByIDUser(_IDUser);

            if (UserInfo.Rows.Count > 0)
            {
                _Name = UserInfo.Rows[0].Field<string>("Name");
                _Surname = UserInfo.Rows[0].Field<string>("Surname");
                _UserName = UserInfo.Rows[0].Field<string>("Username");
                _PasswordHash = UserInfo.Rows[0].Field<string>("PasswordHash");
                _BirthDate = UserInfo.Rows[0].Field<DateTime?>("BirthDate");
                _eMail = UserInfo.Rows[0].Field<string>("eMail");
                _Mobile = UserInfo.Rows[0].Field<string>("Mobile");
                _IDLanguage = UserInfo.Rows[0].Field<int?>("IDLanguage");
                _IDCity = UserInfo.Rows[0].Field<int?>("IDCity");
                _IDProfilePhoto = UserInfo.Rows[0].Field<Guid?>("IDProfilePhoto");
                _IDGender = UserInfo.Rows[0].Field<int?>("IDGender");
                _Offset = UserInfo.Rows[0].Field<int>("Offset");

                //From UserCook Table
                _isProfessionalCook = UserInfo.Rows[0].Field<bool?>("IsProfessionalCook");
                _cookInRestaurant = UserInfo.Rows[0].Field<bool?>("CookInRestaurant");
                _cookAtHome = UserInfo.Rows[0].Field<bool?>("CookAtHome");
                _cookDescription = UserInfo.Rows[0].Field<string>("CookDescription");
                _cookMembership = UserInfo.Rows[0].Field<DateTime?>("CookMembership");
            }
        }

        #endregion

        #region GetIDUserFromEmail
        /// <summary>
        /// Get IdUser from Email
        /// </summary>
        /// <param name="Email">Email of the user</param>
        /// <returns>IdUser - Guid</returns>
        public Guid GetIDUserFromEmail(string Email)
        { 
            GetUsersDAL UserDal = new GetUsersDAL();
            DataTable UserInfo = new DataTable();

            UserInfo = UserDal.GetIDUserFromEmail(Email);

            if (UserInfo.Rows.Count > 0)
            {
                _IDUser = UserInfo.Rows[0].Field<Guid>("IDUser");
            }
            else
            {   
                //Create an Empty Guid in case of error.
                _IDUser = new Guid();
            }

            return _IDUser;
        }

        #endregion

        #region GetIDUserFromUsername
        /// <summary>
        /// Get IdUser from Username
        /// </summary>
        /// <param name="Username">Username of the user</param>
        /// <returns>IdUser - Guid</returns>
        public Guid GetIDUserFromUsername(string Username)
        {
            GetUsersDAL UserDal = new GetUsersDAL();
            DataTable UserInfo = new DataTable();

            UserInfo = UserDal.GetIDUserFromUsername(_UserName);

            if (UserInfo.Rows.Count > 0)
            {
                _IDUser = UserInfo.Rows[0].Field<Guid>("IDUser");
            }
            else
            {
                //Create an Empty Guid in case of error.
                _IDUser = new Guid();
            }

            return _IDUser;
        }

        #endregion

        #region InsertNewUser
        /// <summary>
        /// Insert new User
        /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue InsertUser()
        {
            ManageUSPReturnValue InsertResult;

            try
            {
                ManageUserDAL NewUser = new ManageUserDAL();
                
                InsertResult = new ManageUSPReturnValue(NewUser.InsertNewUser(_Name, _Surname, _UserName, _eMail, _PasswordHash, _BirthDate, _LastPasswordChange, _PasswordExpireOn, _ChangePasswordNextLogon, _ContractSigned, _IDLanguage, _UserEnabled, _UserLocked, _MantainanceMode, _DateMembership, _AccountExpireOn, _LastIpAddress, _LastLogon, _UserDomain, _UserType, _MailConfirmedOn, _Mobile, _MobileConfirmedOn, _MobileConfirmationCode, _IDCity, _IDProfilePhoto, _IDSecurityQuestion, _SecurityAnswer, _LastProfileUpdate, _UserIsOnLine, _IDVisibility, _IDGender, _AccountDeletedOn, _LastLogout, _Offset));
                //UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, InsertResult);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(_IDUser, null, StatisticsActionType.US_NewRegistration, "New Registration", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }

            }
            catch (SqlException sqlEx)
            {
                InsertResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, InsertResult);
            }
            catch (Exception ex)
            {
                InsertResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, InsertResult);
            }
            
           return InsertResult;
        }

        #endregion

        #region LoginWithStoredProcedure
        /// <summary>
        /// Login User
       /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue LoginUserWithStoredProcedure()
        {
           ManageUSPReturnValue LoginResult;

           try
           {
               ManageUserDAL LoginUser = new ManageUserDAL();
               LoginResult = new ManageUSPReturnValue(LoginUser.LoginUser(_eMail, _PasswordHash, _LastLogon, _Offset, _UserIsOnLine, _LastIpAddress));
               //UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, LoginResult);

               //WRITE A ROW IN STATISTICS DB - not necessary here
               //try
               //{
               //    MyStatistics NewStatisticUser = new MyStatistics(_IDUser, null, StatisticsActionType.US_Login, "Login", Network.GetCurrentPageName(), "", "");
               //    NewStatisticUser.InsertNewRow();
               //}
               //catch { }

           }
           catch (SqlException sqlEx)
           {
               LoginResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
               UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, LoginResult);
           }
           catch (Exception ex)
           {
               LoginResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
               UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, LoginResult);
           }

           return LoginResult;
        }
        #endregion

        #region LoginUser
        public bool LoginUser()
        {
            bool ExecutionResult = false;

            try
            {
                UpdateLastLogonAndSetUserAsOnLine();

                //Get User Informations
                GetUserInfoAllByID();

                _Offset = MyConvert.ToInt32(HttpContext.Current.Session["Offset"].ToString(), 0);

                //Login User
                LoginUserWithStoredProcedure();

                //Set Session Variables
                SetLoginSessionVariables();

                ExecutionResult = true;
            }
            catch
            {
            }

            return ExecutionResult;
        }
        #endregion

        #region Logout
        /// <summary>
        /// Logout User
        /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue LogoutUser()
        {
            ManageUSPReturnValue LogoutResult;

            try
            {
                ManageUserDAL LogoutUser = new ManageUserDAL();
                LogoutResult = new ManageUSPReturnValue(LogoutUser.LogoutUser(_IDUser, DateTime.UtcNow, _Offset, false));
                //UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, LogoutResult);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(_IDUser, null, StatisticsActionType.US_Logout, "Logout", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }
            }
            catch (SqlException sqlEx)
            {
                LogoutResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, LogoutResult);
            }
            catch (Exception ex)
            {
                LogoutResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, LogoutResult);
            }

            return LogoutResult;
        }
        #endregion

        #region UpdateLastLogonAndSetUserAsOnLine
        /// <summary>
        /// Update User Last Logon And Set User As OnLine
        /// </summary>
        public void UpdateLastLogonAndSetUserAsOnLine()
        {
            GetUsersDAL UserDAL = new GetUsersDAL();

            UserDAL.UpdateLastLogon(DateTime.UtcNow, _IDUser);
        }
        #endregion

        #region ActivateUser
        /// <summary>
        /// Activate New User
        /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue ActivateUser()
        {
           ManageUSPReturnValue ActivateResult;

           //Get Email + Password Hash according to IDUser
           GetUserInfoAllByID();

           string _emailPasswordHash = MySecurity.GenerateSHA1Hash(_eMail + _PasswordHash);
           
           if (_ConfirmationCode.Equals(_emailPasswordHash))
           {
               try
               {
                   //Try Activation

                   ManageUserDAL ActivateNewUser = new ManageUserDAL();

                   _MailConfirmedOn = DateTime.UtcNow;
                   _UserEnabled = true;
                   _UserLocked = false;
                   _AccountExpireOn = DateTime.UtcNow.AddYears(10);   //Is set to 1 month until user confirm registration. Then, increase of years.

                   ActivateResult = new ManageUSPReturnValue(ActivateNewUser.ActivateUser(_IDUser, _MailConfirmedOn, _UserEnabled, _UserLocked, _LastIpAddress, _AccountExpireOn));
                   //UserLogUSP(LogLevel.InfoMessages, LogLevel.InfoMessages, false, ActivateResult);

                   //WRITE A ROW IN STATISTICS DB
                   try
                   {
                       MyStatistics NewStatisticUser = new MyStatistics(_IDUser, null, StatisticsActionType.US_UserActivated, "User Activated", Network.GetCurrentPageName(), "", "");
                       NewStatisticUser.InsertNewRow();
                   }
                   catch { }
               }
               catch (SqlException sqlEx)
               {
                   ActivateResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                   UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, ActivateResult);
               }
               catch (Exception ex)
               {
                   ActivateResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                   UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, ActivateResult);
               }

               return ActivateResult;
           }
           else
           {
               //_ConfirmationCode is not equal to _emailPasswordHash
               ActivateResult = new ManageUSPReturnValue("US-ER-0004", "", true);
               UserLogUSP(LogLevel.Warnings, LogLevel.Warnings, false, ActivateResult);
               return ActivateResult;
           }
        }
        #endregion

        #region UpdatePassword
        /// <summary>
        /// Update User Password
        /// </summary>
        /// <param name="NewPasswordHash">New Password</param>
        /// <returns>returns 1 if success, 0 otherwise</returns>
        public int UpdatePassword(string NewPasswordHash)
        {
           int resultUpdate = 0;

           try
           {
               GetUsersDAL UpdatePsw = new GetUsersDAL();

               resultUpdate = UpdatePsw.UpdatePassword(NewPasswordHash, DateTime.UtcNow, DateTime.UtcNow.AddYears(3), _IDUser);
           }
           catch (Exception ex)
           {
               string ErrorMessage = ex.Message;
           }

           return resultUpdate;
        }
        #endregion

        #region GetSecurityQuestion
        /// <summary>
        /// Get the text of Security Question
        /// </summary>
        /// <param name="IDUser">ID of User</param>
        /// <param name="IDLanguage">Current Id Language</param>
        /// <returns>Text of Security Question</returns>
        public string GetSecurityQuestion() 
        {
           DataTable SecurityQuestion = new DataTable();
           SecurityQuestionsLanguagesTableAdapter SecurityQuestionTBL = new SecurityQuestionsLanguagesTableAdapter();

           SecurityQuestion = SecurityQuestionTBL.GetSecurityQuestionTextByIDUserAndLanguage(_IDUser, Convert.ToInt32(_IDLanguage));

           _SecurityQuestion = SecurityQuestion.Rows[0].Field<string>("SecurityQuestion");

           return _SecurityQuestion;
        }
        #endregion

        #region UpdateLastProfileUpdateDate
        /// <summary>
        /// Update Field LastProfileUpdate in User Table
        /// </summary>
        public void UpdateLastProfileUpdateDate()
        {
           GetUsersDAL UserDAL = new GetUsersDAL();
           
           UserDAL.UpdateLastProfileUpdateDate(DateTime.UtcNow, _IDUser);
        }
        #endregion

        #region IsACooker?
        /// <summary>
        /// Check if a user is a cooker as well
        /// </summary>
        /// <returns></returns>
        public bool IsACooker()
        {
           try
           {
               UsersCookTableAdapter UsersCookTA = new UsersCookTableAdapter();
               DataTable UserCookInfoDT = new DataTable();
               UserCookInfoDT = UsersCookTA.GetUserCookInformations(_IDUser);

               DataTableReader UserCookInfoReader = new DataTableReader(UserCookInfoDT);

               if (UserCookInfoReader.HasRows)
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
           catch (Exception ex)
           {
               string ErrorMessage = ex.Message;
               return false;
           }
        }
        #endregion

        #region GenerateNewTemporaryCode
        /// <summary>
        /// Generate a TemporaryCode - HASH[(new Guid + idUser + PasswordHash)]
        /// </summary>
        /// <returns>New ConfirmationCode</returns>
        public string GenerateNewTemporaryCode()
        {
           Guid _newGuid = new Guid();
           string _newGuidString = _newGuid.ToString();

           string _confirmationCode = MySecurity.GenerateSHA1Hash(_newGuidString + _IDUser + _PasswordHash);

           return _confirmationCode;
        }
        #endregion

        #region UpdateUserInfo
        /// <summary>
        /// Update User Information
        /// </summary>
        public void UpdateUserInfo()
        {
           try
           {
               GetUsersDAL UserDal = new GetUsersDAL();
               UserDal.UpdateUserInformation(_Name, _Surname, (DateTime)_BirthDate, _Mobile, DateTime.UtcNow, _IDGender, _IDProfilePhoto, _IDCity, (int)_IDLanguage, _eMail, _UserName, _IDUser);
               //UserDal.UpdateUserInformations(_Name, _Surname, _UserName, (int)_UserDomain, (int)_UserType, _PasswordHash, (DateTime)_LastPasswordChange, (DateTime)_PasswordExpireOn, (bool)_ChangePasswordNextLogon, (bool)_ContractSigned, (DateTime)_BirthDate, _eMail, (DateTime)_MailConfirmedOn, _Mobile, _MobileConfirmationCode, (DateTime)_MobileConfirmedOn, (int)_IDLanguage, (int)_IDCity, (Guid)_IDProfilePhoto, (bool)_UserEnabled, (bool)_UserLocked, (bool)_MantainanceMode, (int)_IDSecurityQuestion, _SecurityAnswer, (DateTime)_DateMembership, (DateTime)_AccountExpireOn, (DateTime)_LastLogon, (DateTime)_LastProfileUpdate, (bool)_UserIsOnLine, _LastIpAddress, (int)_IDVisibility, _IDUser);
           }
           catch(Exception ex)
           { 
                //error
               string ErrorMessage = ex.Message;
           }
        }
        #endregion

        #region UpdateSecurityAnswer
        /// <summary>
        /// Update Security Answer
        /// </summary>
        public void UpdateTemporarySecurityAnswer(string Temporarycode)
        {
            try
            {
                //UPDATE ConfirmationCode in User Table (column SecurityAnswer)
                GetUsersDAL SecurityAnswer = new GetUsersDAL();
                SecurityAnswer.UpdateTemporarySecurityAnswer(Temporarycode, _IDUser);
            }
            catch (Exception ex)
            {
                //error
                string ErrorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Update Security Answer
        /// </summary>
        public void UpdateSecurityAnswer(int IdSecurityQuestion, string SecurityAnswer)
        {
            try
            {
                //UPDATE ConfirmationCode in User Table (column SecurityAnswer)
                GetUsersDAL dalSecurityAnswer = new GetUsersDAL();
                dalSecurityAnswer.UpdateSecurityAnswer(IdSecurityQuestion, SecurityAnswer, _IDUser);
            }
            catch (Exception ex)
            {
                //error
                string ErrorMessage = ex.Message;
            }
        }


        #endregion

        #region SecurityQuestionsList
        /// <summary>
        /// Get Security question List according to language
        /// </summary>
        /// <returns>DataTable with IDSecurityQuestion, SecurityQuestion</returns>
        public DataTable GetSecurityQuestions()
        {
            DataTable SecurityQuestionsList = new DataTable();

            SecurityQuestionsLanguagesTableAdapter taSecurityQuestion = new SecurityQuestionsLanguagesTableAdapter();

            SecurityQuestionsList = taSecurityQuestion.GetAllSecurityQuestionsByLanguage(MyConvert.ToInt32(_IDLanguage.ToString(), 1));

            //Check if SecurityQuestionsList has rows for this language.
            //If not, get Questions in Default Language.
            if (SecurityQuestionsList.Rows.Count == 0)
            {
                SecurityQuestionsList = taSecurityQuestion.GetAllSecurityQuestionsByLanguage(1);
            }

            return SecurityQuestionsList;
        }
        #endregion

        #region GenderList
        /// <summary>
        /// Return Gender List according to language
        /// </summary>
        /// <returns>DataTable with IDGender, Gender</returns>
        public DataTable GetGender()
        {
            DataTable GenderList = new DataTable();

            GenderTableAdapter taGender = new GenderTableAdapter();

            GenderList = taGender.GetGenderByLanguage((int)_IDLanguage);

            //If does not exist Gender for this language, use English
            if (GenderList.Rows.Count == 0)
            {
                GenderList = taGender.GetGenderByLanguage(1);
            }

            return GenderList;
        }
        #endregion

        #region GetIDGenderByGenderName
        public int? GetIDGenderByGenderNameAndIDLanguage(string GenderName, int IDLanguage)
        {
            GenderTableAdapter taGender = new GenderTableAdapter();

            int? IDGender = null;
            DataTable dtGender = new DataTable();

            try
            {
                dtGender = taGender.GetIDGenderByGenderNameAndIDLanguage(GenderName, IDLanguage);

                IDGender = Convert.ToInt32(dtGender.Rows[0].Field<int>("IDGender"));
            }
            catch
            {
            }

            return IDGender;
        }
        #endregion

        #region LanguageList
        /// <summary>
        /// Return Gender List according to language
        /// </summary>
        /// <returns>DataTable with IDGender, Gender</returns>
        public DataTable GetLanguageList()
        {
            DataTable LanguageList = new DataTable();

            LanguagesTableAdapter taLanguageList = new LanguagesTableAdapter();

            LanguageList = taLanguageList.GetLanguageList();

            return LanguageList;
        }
        #endregion

        #region SecurityUserGroupList
        /// <summary>
        /// Return a list of all Groups which user belongs
        /// </summary>
        /// <returns>DataTable with IDSecurityGroup</returns>
        public DataTable GetSecurityUserGroupList()
        {
            DataTable SecurityUserGroupList = new DataTable();

            SecurityGroupsUserMembersTableAdapter taSecurityUserGroup = new SecurityGroupsUserMembersTableAdapter();

            SecurityUserGroupList = taSecurityUserGroup.GetUserGroupListByIDUser(_IDUser);

            return SecurityUserGroupList;
        }
        #endregion

        #region DeleteSecurityQuestionAndAnswer
        /// <summary>
        /// Delete (Update to NULL) Security Question and Answer of user that wrong answer too many times
        /// </summary>
        /// <returns>True if Update success</returns>
        public bool DeleteSecurityQuestionAndAnswer()
        {
            try
            {
                GetUsersDAL ConnectionToDal = new GetUsersDAL();
                ConnectionToDal.DeleteSecurityQuestionAndAnswer(_IDUser);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DeleteUserByEmail
        /// <summary>
        /// Delete User By Email
        /// </summary>
        /// <returns>True if Update success</returns>
        public bool DeleteUserByEmail()
        {
            try
            {
                GetUsersDAL ConnectionToDal = new GetUsersDAL();
                ConnectionToDal.DeleteUserByEmail(_eMail);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region SetLoginSessionVariables
        /// <summary>
        /// Set Session Variables on Login And when we have only IDUser from Cookie
        /// </summary>
        public void SetLoginSessionVariables()
        {
           //Notice: add ** Session["xxxxx"] = ""; ** in Global.asax

           HttpContext.Current.Session["IDUser"] = _IDUser;
           HttpContext.Current.Session["Name"] = _Name;
           HttpContext.Current.Session["Surname"] = _Surname;
           HttpContext.Current.Session["Username"] = _UserName;
           HttpContext.Current.Session["eMail"] = _eMail;
           HttpContext.Current.Session["IDLanguage"] = _IDLanguage;
           HttpContext.Current.Session["IDGender"] = _IDGender;
           HttpContext.Current.Session["Offset"] = _Offset;
           HttpContext.Current.Session["IDSecurityGroupList"] = _IDSecurityGroupList;   //Some ID separated by ;
        }
        #endregion

        #region CheckUserLogged
        /// <summary>
        /// Check if a user is logged
        /// </summary>
        /// <returns>True if User Logged</returns>
        public static bool CheckUserLogged()
        {
            bool IsLogged = false;

            try
            {
                if (HttpContext.Current.Session["IDUser"].ToString().Equals(""))
                {
                    //Check Cookie
                    string CookieName = AppConfig.GetValue("CookieName", AppDomain.CurrentDomain);

                    if (HttpContext.Current.Request.Cookies[CookieName] != null)
                    {
                        string IDUser;
                        if (HttpContext.Current.Request.Cookies[CookieName]["IDUser"] != null)
                        {
                            IDUser = HttpContext.Current.Request.Cookies[CookieName]["IDUser"];

                            MyUser UserLogged = new MyUser(new Guid(IDUser));
                            UserLogged.GetUserInfoAllByID();

                            //Login User
                            UserLogged.LastLogon = DateTime.UtcNow;
                            UserLogged.UserIsOnLine = true;
                            string _ipAddress = "";
                            try
                            {
                                if (HttpContext.Current.Request.Headers[""] != null && !String.IsNullOrEmpty(HttpContext.Current.Request.Headers["X-Forwarded-For"].ToString()))
                                {
                                    _ipAddress = HttpContext.Current.Request.Headers["X-Forwarded-For"].ToString();
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

                            UserLogged.LastIpAddress = _ipAddress;
                            UserLogged.Offset = MyConvert.ToInt32(HttpContext.Current.Session["Offset"].ToString(), 0);

                            ManageUSPReturnValue result = UserLogged.LoginUserWithStoredProcedure();

                            //Set Session Variables
                            UserLogged.SetLoginSessionVariables();

                            IsLogged = true;
                        }
                        else
                        {
                            IsLogged = false;
                        }
                    }
                }
                else
                {
                    IsLogged = true;
                }
            }
            catch
            {
                IsLogged = false;
            }

            return IsLogged;
        }
        #endregion

        #region LastTimeOnline
        /// <summary>
        /// Return User Last Login
        /// </summary>
        /// <returns></returns>
        public static string LastTimeOnline(Guid IDUser, int IDLanguage, int Offset)
        {
            MyUser UserRequested = new MyUser(IDUser);
            UserRequested.GetUserInfoAllByID();

            string LastTimeOnline = RetrieveMessage.RetrieveDBMessage(Convert.ToInt32(IDLanguage), "US-IN-0028");

            //Check if user is online
            //if (!CheckUserLogged())
            if (UserRequested.LastLogon < UserRequested.LastLogout)
            {
                if (String.IsNullOrEmpty(UserRequested.LastLogon.ToString()))
                {
                    LastTimeOnline += " " + String.Format("{0:dd/MM/yyyy HH:mm}", MyConvert.ToLocalTime(Convert.ToDateTime(UserRequested.DateMembership), Offset));
                }
                else
                {
                    LastTimeOnline += " " + String.Format("{0:dd/MM/yyyy HH:mm}", MyConvert.ToLocalTime(Convert.ToDateTime(UserRequested.LastLogout), Offset));
                }

                
            }
            else
            {
                LastTimeOnline = "Online";
            }

            return LastTimeOnline;
        }
        #endregion

        #region NumberOfProfileVisits
        public string NumberOfProfileVisits()
        {
            string NumberOfProfileVisits = RetrieveMessage.RetrieveDBMessage(Convert.ToInt32(_IDLanguage), "US-IN-0030");

            DBStatisticsEntities NewStat = new DBStatisticsEntities();
            
            System.Data.Objects.ObjectResult USP_Result = NewStat.USP_CountByIDRelatedObjectAndActionType(_IDUser, (int)StatisticsActionType.US_ProfileViewed);

            //DA CONTINUARE......

            NumberOfProfileVisits += USP_Result.ToString();     // ???????????

            return NumberOfProfileVisits;
        }

        #endregion

        #region UserLogUSP - NOTE: This is present in MyUserFriendship, MyUserSocial.
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
                    IDLanguageForLog = MyConvert.ToInt32(AppConfig.GetValue("IDLanguageForLog", AppDomain.CurrentDomain), 1);
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

        #region RegisterAsCooker
        /// <summary>
        /// Register the user as Cooker
        /// </summary>
        public bool RegisterAsCooker()
        {
            try
            {
                UsersCookTableAdapter UserCookTA = new UsersCookTableAdapter();
                UserCookTA.InsertCooker(_IDUser, false, false, false, "", DateTime.UtcNow);

                return true;
            }
            catch (Exception ex)
            {
                //Error
                string ErrorMessage = ex.Message;

                return false;
            }
        }
        #endregion

        #region RemoveAsCooker
        /// <summary>
        /// Remove information that the user is a Cooker
        /// </summary>
        public bool RemoveAsCooker()
        {
            try
            {
                UsersCookTableAdapter UserCookTA = new UsersCookTableAdapter();
                UserCookTA.DeleteCooker(_IDUser);

                return true;
            }
            catch (Exception ex)
            {
                //Error
                string ErrorMessage = ex.Message;

                return false;
            }
        }
        #endregion

        #region UpdateCookerInformations
        /// <summary>
        /// Update Cooker Informations
        /// </summary>
        /// <returns></returns>
        public bool UpdateCookerInformations()
        {
            try
            {
                UsersCookTableAdapter UserCookTA = new UsersCookTableAdapter();
                UserCookTA.UpdateCookerInformation((bool)_isProfessionalCook, (bool)_cookInRestaurant, (bool)_cookAtHome, _cookDescription, _IDUser);

                return true;
            }
            catch (Exception ex)
            {
                //error
                string ErrorMessage = ex.Message;

                return false;
            }
        }
        #endregion

        #region GetUsersListAccordingToWords
        /// <summary>
        /// Create a List of Users according to words written in a Search Box
        /// </summary>
        /// <param name="words">Words that must be searched in Email, Username, Name, ...</param>
        /// <returns>List of User and Values that have to be shown in the label of the Search Box</returns>
        public List<MyUser> GetUsersList(string words)
        {
            //QUI SERVE LA QRY CHE IN BASE ALLE PAROLE PASSATE TROVA EMAIL O NOME O COGNOME O USERNAME DI PERSONE..
            //IN BASE ALLE RIGHE CHE RITORNA SI CICLERA' PER AGGIUNGERE ALLA LISTA......

            List<MyUser> UserList = new List<MyUser>();

            DataTable dtUsers = new DataTable();

            GetUsersDAL dalUsers = new GetUsersDAL();

            int NumberOfResults = MyConvert.ToInt32(AppConfig.GetValue("UserFindResultsNumber", AppDomain.CurrentDomain).ToString(), 5);

            dtUsers = dalUsers.FindUserByWords(words, NumberOfResults);

            if (dtUsers.Rows.Count > 0)
            {
                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    UserList.Add(new MyUser()
                    {
                        IDUser = dtUsers.Rows[i].Field<Guid>("IDUser"),
                        Name = dtUsers.Rows[i].Field<String>("Name"),
                        Surname = dtUsers.Rows[i].Field<String>("Surname"),
                        CompleteName = dtUsers.Rows[i].Field<String>("Name") + " " + dtUsers.Rows[i].Field<String>("Surname")
                    });
                }
            }

            return UserList;
        }
        #endregion

        #region DeleteAccount
        /// <summary>
        /// Delete User Account (Update User Info Setting a date in DeleteAccountOn)
        /// </summary>
        /// <returns>returns true if success, false otherwise</returns>
        public bool DeleteAccount()
        {
            bool resultDelete = false;

            try
            {
                GetUsersDAL deleteAccount = new GetUsersDAL();

                deleteAccount.DeleteAccount(DateTime.UtcNow, _IDUser);

                resultDelete = true;
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;
            }

            return resultDelete;
        }
        #endregion

        #region IsValidPassword
        /// <summary>
        /// Check if Current Password is Valid (If Expired or Change Next Logon)
        /// </summary>
        /// <returns>True if Valid, False Otherwise</returns>
        public bool IsValidPassword()
        {
            bool IsValidPassword = false;

            try
            {
                //Check if is set ChangePasswordNextLogon
                if (!Convert.ToBoolean(_ChangePasswordNextLogon))
                {
                    IsValidPassword = true;
                }
                else
                {
                    IsValidPassword = false;
                }

                //Check if password is expired
                if (DateTime.Compare(DateTime.UtcNow, Convert.ToDateTime(_PasswordExpireOn)) <= 0)
                {
                    IsValidPassword = true;
                }
                else
                {
                    IsValidPassword = false;
                }

            }
            catch
            {
            }

            return IsValidPassword;
        }
        #endregion

        #region CheckUserNameExcludingCurrent
        /// <summary>
        /// Check if Username exist.
        /// </summary>
        /// <param name="Username">Username to check</param>
        /// <param name="CurrentUsername">Current Username</param>
        /// <returns>True if Valid, False Otherwise</returns>
        public static bool CheckUserNameExcludingCurrent(string Username, string CurrentUsername)
        {
            //If we are not logged, for ex. during registration, Session Username will be empty
            try
            {
                GetUsersDAL UserDal = new GetUsersDAL();

                if (Convert.ToBoolean(UserDal.CheckUserNameExcludingCurrent(Username, CurrentUsername)))
                {
                    //Exist
                    return true;
                }
                else
                {
                    //Doesn't exist
                    return false;
                }
            }
            catch
            { 
                return false;
            }
        }

        #endregion

        #region CheckUsername
        /// <summary>
        /// Check if Username exist.
        /// </summary>
        /// <param name="Username">Username to check</param>
        /// <returns>True if Valid, False Otherwise</returns>
        public static bool CheckUsername(string Username)
        {
            //If we are not logged, for ex. during registration, Session Username will be empty
            try
            {
                GetUsersDAL UserDal = new GetUsersDAL();

                if (Convert.ToBoolean(UserDal.CheckUserName(Username)))
                {
                    //Exist
                    return true;
                }
                else
                {
                    //Doesn't exist
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region CheckEmailExistenceExcludingCurrent
        /// <summary>
        /// Check if Username exist
        /// </summary>
        /// <param name="Email">Email to check</param>
        /// <param name="CurrentEmail">Current Email</param>
        /// <returns>True if Valid, False Otherwise</returns>
        public static bool CheckEmailExistenceExcludingCurrent(string Email, string CurrentEmail)
        {
            //If we are not logged, for ex. during registration, Session Username will be empty
            try
            {
                GetUsersDAL UserDal = new GetUsersDAL();

                if (Convert.ToBoolean(UserDal.CheckEmailExcludingCurrent(Email, CurrentEmail)))
                {
                    //Exist
                    return true;
                }
                else
                {
                    //Doesn't exist
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region CheckEmail
        /// <summary>
        /// Check if Username exist
        /// </summary>
        /// <param name="Email">Email to check</param>
        /// <returns>True if Valid, False Otherwise</returns>
        public static bool CheckEmail(string Email)
        {
            //If we are not logged, for ex. during registration, Session Username will be empty
            try
            {
                GetUsersDAL UserDal = new GetUsersDAL();

                if (Convert.ToBoolean(UserDal.CheckEmail(Email)))
                {
                    //Exist
                    return true;
                }
                else
                {
                    //Doesn't exist
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region GetPropertyCompiledValues

        public void GetPropertyCompiledValues()
        {
            DataTable dtPropertyCompiled = MyUserPropertyCompiled.GetAllPropertyCompiled(_IDUser);
            int x = 0;

            if (dtPropertyCompiled.Rows.Count > 0)
            {
                _PropertyCompiled = new MyUserPropertyCompiled[dtPropertyCompiled.Rows.Count];

                foreach (DataRow PropComp in dtPropertyCompiled.Rows)
                {
                    MyUserPropertyCompiled PropertyCompiled = new MyUserPropertyCompiled(PropComp.Field<Guid>("IDUserInfoPropertyCompiled"), MyConvert.ToInt32(_IDLanguage, 1));
                    _PropertyCompiled[x] = PropertyCompiled;
                    x++;
                }
            }

        }

        #endregion

        #region ProfileCompletePercentageCalc
        public double ProfileCompletePercentageCalc()
        {
            double Percentage = 0;
            int TotProperties = 0;
            int CompiledByUser = 0;

            try
            {
                string UserProfilePropertiesID = AppConfig.GetValue("UserProfilePropertiesID", AppDomain.CurrentDomain);

                string[] PropertiesID = UserProfilePropertiesID.Split(',');

                foreach (string PropertyId in PropertiesID)
                {
                    List<MyUserProperty> propList = MyUserProperty.GetAllMyUserPropertyByCategory(Convert.ToInt32(PropertyId), (int)_IDLanguage);

                    TotProperties += propList.Count;

                    CompiledByUser = MyUserPropertyCompiled.GetCountPropertyCompiledByUser(_IDUser).Rows.Count;
                }

                //Add the number of the fields of personal user info (of the table Users) 
                TotProperties += 7;
                if (!String.IsNullOrEmpty(_Name)) { CompiledByUser += 1; }
                if (!String.IsNullOrEmpty(_Surname)) { CompiledByUser += 1; }
                if (!String.IsNullOrEmpty(_UserName)) { CompiledByUser += 1; }
                if (!String.IsNullOrEmpty(_Mobile)) { CompiledByUser += 1; }
                if (!String.IsNullOrEmpty(_BirthDate.ToString())) { CompiledByUser += 1; }
                if (!String.IsNullOrEmpty(_IDGender.ToString())) { CompiledByUser += 1; }
                if (!String.IsNullOrEmpty(_IDCity.ToString())) { CompiledByUser += 1; }

                Percentage = ((Convert.ToDouble(CompiledByUser) / Convert.ToDouble(TotProperties)) * 100);
            }
            catch
            { 
                //Error calculating Percentage
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "ProfileCompletePercentageCalc", _IDUser.ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }

            return Percentage;
        }
        #endregion

        #region IsMySelf?
        /// <summary>
        /// Check if the user requested is ME - Check with Session[IDUser]
        /// </summary>
        /// <param name="IDUserRequested"></param>
        /// <returns></returns>
        public static bool IsMySelf(string IDUserRequested)
        {
            bool myself = false;

            try
            {
                if (IDUserRequested.ToLower().Equals(HttpContext.Current.Session["IDUser"].ToString().ToLower()))
                {
                    myself = true;
                }
            }
            catch
            {
            }

            return myself;
        }
        #endregion

        #region GetNewUsersForMailchimp
        /// <summary>
        /// Get New Users For Mailchimp by Language
        /// </summary>
        public List<MyUser> GetNewUsersForMailchimp()
        {
            List<MyUser> UserList = new List<MyUser>();

            DataTable dtUsers = new DataTable();

            USP_GetNewUsersForMailchimpTableAdapter dalUsers = new USP_GetNewUsersForMailchimpTableAdapter();

            dtUsers = dalUsers.GetNewUsersForMailchimp();

            if (dtUsers.Rows.Count > 0)
            {
                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    UserList.Add(new MyUser()
                    {
                        Name = dtUsers.Rows[i].Field<String>("Name"),
                        Surname = dtUsers.Rows[i].Field<String>("Surname"),
                        eMail = dtUsers.Rows[i].Field<String>("eMail"),
                        IDLanguage = dtUsers.Rows[i].Field<int>("IDLanguage")
                    });
                }
            }

            return UserList;
        }

        #endregion

        #region SuggestUsers Static Method
        public static List<MyUser> SuggestUsers(Guid IDRequester, int RowOffset, int FetchRows)
        {
            GetUsersDAL UserDal = new GetUsersDAL();
            List<MyUser> userSuggested = new List<MyUser>();

            DataTable dtUserSuggested = UserDal.USP_SuggestNewUser(IDRequester, RowOffset, FetchRows);
            if(dtUserSuggested.Rows.Count > 0)
            {
                for (int i = 0; i < dtUserSuggested.Rows.Count; i++)
                {
                    userSuggested.Add(new MyUser()
                    {
                        _IDUser = dtUserSuggested.Rows[i].Field<Guid>("IDUser"),
                        _UserName = dtUserSuggested.Rows[i].Field<string>("UserName"),
                        _IDProfilePhoto = dtUserSuggested.Rows[i].Field<Guid>("IDProfilePhoto"),
                        _Name = dtUserSuggested.Rows[i].Field<string>("Name"),
                        _Surname = dtUserSuggested.Rows[i].Field<string>("Surname")
                    });
                }
            }
            return userSuggested;
        }
        #endregion
        #endregion

        #region Operators

        public static implicit operator MyUser(Guid guid)
        {
            MyUser user = new MyUser(guid);
            return user;
        }

        public static implicit operator Guid(MyUser user)
        {
            Guid guid = new Guid();
            if (user == null)
            {
                return guid;
            }
            else
            {
                return user.IDUser;
            }
        }

        public static bool operator == (MyUser user1, MyUser user2)
        {
            if ((Object)user1 == null)
            {
                user1 = new MyUser(new Guid());
            }
            if ((Object)user2 == null)
            {
                user2 = new MyUser(new Guid());
            }
            if ((Object)user1 == null || (Object)user2 == null)
            {
                return (Object)user1 == (Object)user2;
            }
            else if (String.IsNullOrEmpty(user1.eMail) || String.IsNullOrEmpty(user2.eMail))
            {
                return user1.IDUser == user2.IDUser;
            }

            return user1.eMail == user2.eMail;
        }

        public static bool operator != (MyUser user1, MyUser user2)
        {
            return !(user1 == user2);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Recipe return false.
            MyUser user = obj as MyUser;
            if ((System.Object)user == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (eMail == user.eMail);
        }

        public bool Equals(MyUser user)
        {
            // If parameter is null return false:
            if ((object)user == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (eMail == user.eMail);
        }

        public override int GetHashCode()
        {
            return IDUser.GetHashCode();
        } 

        #endregion

    }

    /// <summary>
    /// CLASS UserCorporate
    /// </summary>
    public class UserCorporate : MyUser
    {
        public UserCorporate(string UserID)
        {

        }
    }
}
