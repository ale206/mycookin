using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MyCookin.Common;
using MyCookin.DAL.User.ds_UserFriendshipTableAdapters;
using System.Data;
using MyCookin.Log;
using System.Web;
using MyCookin.DAL.Audit.ds_AuditTableAdapters;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.DAL.User.ds_UserInfoTableAdapters;

namespace MyCookin.ObjectManager.UserManager
{
    /// <summary>
    /// CLASS MyUserFriendship
    /// </summary>
    public class MyUserFriendship
    {
        #region PrivateFields
        MyUser _IDUserFriend1;
        MyUser _IDUserFriend2;

        Guid _IDUserFriend;
        DateTime _friendshipCompletedDate;
        DateTime _followingDate;
        Guid _IDUserFollower;
        #endregion

        #region PublicFields
        public MyUser IDUserFriend1
        {
            get { return _IDUserFriend1; }
            //set { _IDUserFriend1 = value; }
        }
        public MyUser IDUserFriend2
        {
            get { return _IDUserFriend2; }
            //set { _IDUserFriend2 = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// UserFriend
        /// </summary>
        /// <param name="UserFriendID">ID of the user friend</param>
        public MyUserFriendship(MyUser IDUserFriend1, MyUser IDUserFriend2)
        {
            _IDUserFriend1 = IDUserFriend1;
            _IDUserFriend2 = IDUserFriend2;
        }

        public MyUserFriendship(MyUser IDUserFriend1)
        {
            _IDUserFriend1 = IDUserFriend1;
        }

        #endregion

        #region Methods

        //FRIENDSHIP

        #region ListOfUsersYouMayKnowByFollower
        /// <summary>
        /// List of users you can know according to follower.
        /// List of user followed by the user that YOU are following, ordered by number of repeats
        /// </summary>
        /// <returns>Datatable with id of users</returns>
        public DataTable ListOfUsersYouMayKnowByFollower()
        {
            DataTable DT_PeopleYouMayKnow = new DataTable();

            try
            {
                UsersFollowersTableAdapter TA_PeopleYouMayKnow = new UsersFollowersTableAdapter();

                int NumberOfResults = Convert.ToInt32(AppConfig.GetValue("RoutingUser", AppDomain.CurrentDomain).ToString());
                DT_PeopleYouMayKnow = TA_PeopleYouMayKnow.GetPeopleYouMayKnowByFollower(_IDUserFriend1.IDUser, NumberOfResults);
                
                //Before this, the SIMPLY qry was:
                //SELECT IDUser AS IDUser2 FROM Users where IDUser <> @IDUser AND 
                //IDUser NOT IN (SELECT Friend FROM vPotentialFriends WHERE Me=@IDUser AND UserBlocked IS NULL)

                //In DAL there was UsersFriendsTableAdapter -> GetUsersListForFriendship(_IDUserFriend1.IDUser);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;
            }

            return DT_PeopleYouMayKnow;

        }

        #endregion

        #region ListOfFriendshipRequests

        /// <summary>
        /// Create a list of all friendship requests
        /// </summary>
        public DataTable CreateListOfFriendshipRequests()
        {
            DataTable FriendshipRequestsList = new DataTable();

            try
            {
                UsersFriendsTableAdapter taRequestFriendship = new UsersFriendsTableAdapter();

                FriendshipRequestsList = taRequestFriendship.CreateListOfFriendshipRequests(_IDUserFriend1.IDUser);

                
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Log of error here or return something!
            }

            return FriendshipRequestsList;
        }

        #endregion

        #region ListOfFriends
        /// <summary>
        /// Create a list of Friends
        /// </summary>
        public DataTable CreateListOfFriends()
        {
            DataTable FriendsList = new DataTable();

            try
            {
                UsersFriendsTableAdapter taFriendsList = new UsersFriendsTableAdapter();
                //FriendsList = new DataTable();

                FriendsList = taFriendsList.ListOfFriends(_IDUserFriend1.IDUser);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Log of error here or return something!
            }

            return FriendsList;
        }

        #endregion

        #region ListOfCommonFriends
        /// <summary>
        /// Create a list of Friends in Common
        /// </summary>
        /// <returns>Datatable with friends in common</returns>
        public DataTable CreateListOfCommonFriends()
        {
            DataTable CommonFriendsList = new DataTable();

            try
            {
                UsersFriendsTableAdapter taCommonFriendsList = new UsersFriendsTableAdapter();
                CommonFriendsList = new DataTable();

                CommonFriendsList = taCommonFriendsList.ListOfCommonFriends(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Log of error here or return something!
            }

            return CommonFriendsList;
          }
        #endregion

        #region ListOfBlockedFriends
        /// <summary>
        /// Create a list of Blocked Friends
        /// </summary>
        public DataTable CreateListOfBlockedFriends()
        {
            DataTable BlockedFriendsList = new DataTable();

            try
            {
                UsersFriendsTableAdapter taBlockedFriendsList = new UsersFriendsTableAdapter();
                BlockedFriendsList = new DataTable();

                BlockedFriendsList = taBlockedFriendsList.ListOfBlockedFriends(_IDUserFriend1.IDUser);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Log of error here or return something!
            }

            return BlockedFriendsList;
        }

        #endregion

        #region USP_RequestOrAcceptFriendship
        /// <summary>
        /// Request Or Accept Friendship
        /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue RequestOrAcceptFriendship()
        {
            ManageUSPReturnValue RequestOrAcceptFriendshipResult;

            try
            {
                ManageFriendshipTableAdapter taRequestOrAcceptFriendship = new ManageFriendshipTableAdapter();

                DateTime _friendshipCompletedDate = DateTime.UtcNow;

                RequestOrAcceptFriendshipResult = new ManageUSPReturnValue(taRequestOrAcceptFriendship.RequestOrAcceptFriendship(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, _friendshipCompletedDate));
                //MyUser.UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, RequestOrAcceptFriendshipResult);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, StatisticsActionType.US_FriendshipAccepted, "Friendship Request Or Accept", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }

                //Automatically FOLLOW this user
                FollowUser();

                //INSERT ACTION IN USER BOARD - Already inserted in FollowUser()

                //Questa va rivista perchè andrebbe inserita una riga SOLO se si accetta l'amicizia, non anche quando si richiede...
                //UserBoard NewActionInUserBoard = new UserBoard(_IDUserFriend1.IDUser, null, ActionTypes.NewFollower, _IDUserFriend2.IDUser, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(),1));
                //NewActionInUserBoard.InsertAction();

            }
            catch (SqlException sqlEx)
            {
                RequestOrAcceptFriendshipResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, RequestOrAcceptFriendshipResult);
            }
            catch (Exception ex)
            {
                RequestOrAcceptFriendshipResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, RequestOrAcceptFriendshipResult);
            }

            return RequestOrAcceptFriendshipResult;
        }

        #endregion

        #region DeclineFriendship
        /// <summary>
        /// Decline Friendship - Update table with today date on UserBlocked
        /// </summary>
        /// <param name="IDUser1"></param>
        /// <param name="IDUser2"></param>
        /// <returns></returns>
        public bool DeclineFriendship()
        {
            try
            {
                UsersFriendsTableAdapter taDeclineFriendship = new UsersFriendsTableAdapter();

                taDeclineFriendship.DeclineFriendship(DateTime.UtcNow, _IDUserFriend1.IDUser, _IDUserFriend2.IDUser);

                return true;
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                return false;
            }

        }
        #endregion

        #region USP_BlockUser
        /// <summary>
        /// Block User (and tell him to fuck off) - Insert or Update table with today date on UserBlocked
        /// </summary>
        /// <param name="IDUser1">Me</param>
        /// <param name="IDUser2">User to block</param>
        /// <returns></returns>
        public ManageUSPReturnValue BlockUser()
        {
            ManageUSPReturnValue BlockUserResult;

            try
            {
                ManageFriendshipTableAdapter taBlockUser = new ManageFriendshipTableAdapter();

                DateTime _UserBlocked = DateTime.UtcNow;

                BlockUserResult = new ManageUSPReturnValue(taBlockUser.BlockUser(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, _UserBlocked));
                //MyUser.UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, BlockUserResult);

                //Automatically DEFOLLOW this user - Statistics already wirtten in this method
                DefollowUser();
                
            }
            catch (SqlException sqlEx)
            {
                BlockUserResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                //UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, RequestOrAcceptFriendshipResult);
            }
            catch (Exception ex)
            {
                BlockUserResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                //UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, RequestOrAcceptFriendshipResult);
            }

            return BlockUserResult;
        }
        #endregion

        #region USP_RemoveBlockUser
        /// <summary>
        /// Remove Block User
        /// </summary>
        /// <param name="IDUser1">Me</param>
        /// <param name="IDUser2">User that we want to remove block</param>
        /// <returns></returns>
        public ManageUSPReturnValue RemoveBlockUser()
        {
            ManageUSPReturnValue RemoveBlockUserResult;

            try
            {
                ManageFriendshipTableAdapter taRemoveBlockUser = new ManageFriendshipTableAdapter();

                RemoveBlockUserResult = new ManageUSPReturnValue(taRemoveBlockUser.RemoveBlock(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser));
                //MyUser.UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, RemoveBlockUserResult);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, StatisticsActionType.US_UserBlockRemoved, "Block Removed", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }

                //Automatically FOLLOW this user
                FollowUser();

            }
            catch (SqlException sqlEx)
            {
                RemoveBlockUserResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, RemoveBlockUserResult);
            }
            catch (Exception ex)
            {
                RemoveBlockUserResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, RemoveBlockUserResult);
            }

            return RemoveBlockUserResult;
        }
        #endregion

        #region USP_RemoveFriendship
        /// <summary>
        /// Remove Friendship
        /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue RemoveFriendship()
        {
            ManageUSPReturnValue RemoveFriendshipResult;

            try
            {
                ManageFriendshipTableAdapter taRemoveFriendship = new ManageFriendshipTableAdapter();

                RemoveFriendshipResult = new ManageUSPReturnValue(taRemoveFriendship.RemoveFriendship(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser));
                //MyUser.UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, RemoveFriendshipResult);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, StatisticsActionType.US_FriendshipRemoved, "Friendship Removed", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }

                //Automatically DEFOLLOW this user - DISABLED
                //DefollowUser();

            }
            catch (SqlException sqlEx)
            {
                RemoveFriendshipResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                MyUser.UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, RemoveFriendshipResult);
            }
            catch (Exception ex)
            {
                RemoveFriendshipResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                MyUser.UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, RemoveFriendshipResult);
            }

            return RemoveFriendshipResult;
        }

        #endregion

        #region USP_RemoveFriendshipForUseWithFollow
        /// <summary>
        /// Remove Friendship - Use this if you are managing friendship with delete follow button
        /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue RemoveFriendshipForUseWithFollow()
        {
            ManageUSPReturnValue RemoveFriendshipResult;

            try
            {
                ManageFriendshipTableAdapter taRemoveFriendship = new ManageFriendshipTableAdapter();

                RemoveFriendshipResult = new ManageUSPReturnValue(taRemoveFriendship.RemoveFriendshipForUseWithFollow(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser));
                //MyUser.UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, RemoveFriendshipResult);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, StatisticsActionType.US_FriendshipRemoved, "Friendship Removed (One direction)", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }

                //Automatically DEFOLLOW this user - DISABLED
                //DefollowUser();

            }
            catch (SqlException sqlEx)
            {
                RemoveFriendshipResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                MyUser.UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, RemoveFriendshipResult);
            }
            catch (Exception ex)
            {
                RemoveFriendshipResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                MyUser.UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, RemoveFriendshipResult);
            }

            return RemoveFriendshipResult;
        }

        #endregion

        #region CheckFriendship
        /// <summary>
        /// Check if two User are friends
        /// </summary>
        /// <returns>True if Friends.</returns>
        public bool CheckFriendship()
        {
            UsersFriendsTableAdapter taFriendshipCheck = new UsersFriendsTableAdapter();
            DataTable dtFriendshipCheck = new DataTable();

            dtFriendshipCheck = taFriendshipCheck.CheckFriendship(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser);

            //DataTable anyway give a row. If IDUserFriend is null, .Field<Guid> gives Error.
            try
            {
                _IDUserFriend = dtFriendshipCheck.Rows[0].Field<Guid>("IDUserFriend");
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region CheckUserBlocked
        /// <summary>
        /// Check if a User is Blocked for another user
        /// </summary>
        /// <returns>True if Friends.</returns>
        public bool CheckUserBlocked()
        {
            UsersFriendsTableAdapter taUserBlockedCheck = new UsersFriendsTableAdapter();
            DataTable dtUserBlockedCheck = new DataTable();

            dtUserBlockedCheck = taUserBlockedCheck.CheckUserBlocked(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser);

            //DataTable anyway give a row. If IDUserFriend is null, .Field<Guid> gives Error.
            try
            {
                _IDUserFriend = dtUserBlockedCheck.Rows[0].Field<Guid>("IDUserFriend");
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region CheckUserSpamReported
        /// <summary>
        /// Check if a User is already reported as spammer
        /// </summary>
        /// <returns>True if Friends.</returns>
        public bool CheckUserSpamReported()
        {
            GetAuditEventDAL taAuditEvent = new GetAuditEventDAL();

            DataTable dtUserSpamReportedCheck = new DataTable();

            dtUserSpamReportedCheck = taAuditEvent.CheckUserSpamReported(_IDUserFriend1.IDUser.ToString(), _IDUserFriend2.IDUser);

            //DataTable anyway give a row. If IDUserFriend is null, .Field<Guid> gives Error.
            try
            {
                _IDUserFriend = dtUserSpamReportedCheck.Rows[0].Field<Guid>("IDAuditEvent");
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region GetFriendshipCompletedDate
        /// <summary>
        /// Get Friendship Completed Date
        /// </summary>
        /// <returns></returns>
        public DateTime GetFriendshipCompletedDate()
        { 
            //Check if Users are friends
            try
            {
                UsersFriendsTableAdapter taFriendshipCompletedDate = new UsersFriendsTableAdapter();
                DataTable dtFriendshipCompletedDate = new DataTable();

                dtFriendshipCompletedDate = taFriendshipCompletedDate.GetFriendshipCompletedDate(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser);

                //DataTable anyway give a row. If FriendshipCompletedDate is null, .Field<FriendshipCompletedDate> gives Error.
                try
                {
                    _friendshipCompletedDate = dtFriendshipCompletedDate.Rows[0].Field<DateTime>("FriendshipCompletedDate");
                    return _friendshipCompletedDate;
                }
                catch
                {
                    return DateTime.UtcNow.AddYears(1000);
                }

            }
            catch
            {
                //ERROR LOG
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in GetFriendshipCompletedDate()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }

                return DateTime.UtcNow.AddYears(1000);
            }
        }
        #endregion

        #region NumberOfYearsAccordingToDays
        /// <summary>
        /// Number of years according to Days
        /// </summary>
        /// <param name="Days">Number of Days</param>
        /// <returns>Number of Years</returns>
        private int DaysToYears(int Days)
        {
            int years = 0;

            if (Days >= 365)
            {
                years = Days / 365;  
            }

            return years;
        }
        #endregion

        #region NumberOfDaysAfter365
        /// <summary>
        /// Number of days that remaining after that 365days are 1year
        /// </summary>
        /// <param name="Days">Number of Days</param>
        /// <returns>Number of Days</returns>
        private int DaysWithoutYears(int Days)
        {
            int numberOfDays = Days;

            if (Days >= 365)
            {
                numberOfDays = Days % 365;
            }

            return numberOfDays;
        }
        #endregion

        #region FriendshipTime
        /// <summary>
        /// Friendship Duration Time
        /// </summary>
        /// <returns>A TimeSpan to get Days, Hours, Minutes, Seconds - For Year call DaysToYear()</returns>
        private TimeSpan FriendshipTime()
        {
            TimeSpan IntervalTime = new TimeSpan(0,0,0);

            //Check if Users are friends
            try
            {
                _friendshipCompletedDate = GetFriendshipCompletedDate();

                IntervalTime = DateTime.UtcNow.Subtract(_friendshipCompletedDate);
            }
            catch
            {
                //ERROR LOG
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in FriendshipTime()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return IntervalTime;
        }
        #endregion

        #region FriendShipTimeString
        public string FriendShipTimeString()
        {
            string FriendShipTimeString = "";

            //Check if Users are friends
            if (CheckFriendship())
            {
                TimeSpan FTProp = FriendshipTime();

                FriendShipTimeString = " (Friends for " + DaysToYears(FTProp.Days).ToString() + "y " + DaysWithoutYears(FTProp.Days).ToString() + "d " + FTProp.Hours + "h " + FTProp.Minutes + "m)";
            }

            return FriendShipTimeString;
        }


        #endregion

        #region NumberOfFriends
        public int NumberOfFriends()
        {
            UsersFriendsTableAdapter taNumberOfFriends = new UsersFriendsTableAdapter();

            int numberOfFriends = (int)taNumberOfFriends.NumberOfFriends(_IDUserFriend1.IDUser);

            return numberOfFriends;
        }

        #endregion

        #region FindFriendsByWords
        /// <summary>
        /// Create a List of Friends according to words written in a Search Box
        /// </summary>
        /// <param name="words">Words that must be searched in Email, Username, Name, ...</param>
        /// <returns>List of User and Values that have to be shown in the label of the Search Box</returns>
        public List<MyUser> FindFriendsByWords(string words)
        {
            //QUI SERVE LA QRY CHE IN BASE ALLE PAROLE PASSATE TROVA EMAIL O NOME O COGNOME O USERNAME DI PERSONE..
            //IN BASE ALLE RIGHE CHE RITORNA SI CICLERA' PER AGGIUNGERE ALLA LISTA......

            List<MyUser> FriendsList = new List<MyUser>();

            DataTable dtFriendsList = new DataTable();

            try
            {
                GetUsersDAL dalUsers = new GetUsersDAL();

                int NumberOfResults = MyConvert.ToInt32(AppConfig.GetValue("UserFindResultsNumber", AppDomain.CurrentDomain).ToString(), 5);

                dtFriendsList = dalUsers.FindFriendsByWords(_IDUserFriend1.IDUser, words, NumberOfResults);

                if (dtFriendsList.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFriendsList.Rows.Count; i++)
                    {
                        FriendsList.Add(new MyUser()
                        {
                            IDUser = dtFriendsList.Rows[i].Field<Guid>("IDUser"),
                            Name = dtFriendsList.Rows[i].Field<String>("Name"),
                            Surname = dtFriendsList.Rows[i].Field<String>("Surname"),
                            CompleteName = dtFriendsList.Rows[i].Field<String>("Name") + " " + dtFriendsList.Rows[i].Field<String>("Surname")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Log of error here or return something!
            }

            return FriendsList;
        }
        #endregion


    //FOLLOWING

        #region USP_FollowUser
        /// <summary>
        /// Follow a user
        /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue FollowUser()
        {
            ManageUSPReturnValue FollowUserResult;

            try
            {
                ManageFriendshipTableAdapter taFollowUser = new ManageFriendshipTableAdapter();

                _followingDate = DateTime.UtcNow;

                FollowUserResult = new ManageUSPReturnValue(taFollowUser.FollowUser(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, _followingDate));
                //MyUser.UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, FollowUserResult);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, StatisticsActionType.US_UserFollowed, "User Followed", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }

                //INSERT ACTION IN USER BOARD
                UserBoard NewActionInUserBoard = new UserBoard(_IDUserFriend1.IDUser, null, ActionTypes.NewFollower, _IDUserFriend2.IDUser, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                NewActionInUserBoard.InsertAction();

            }
            catch (SqlException sqlEx)
            {
                FollowUserResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, FollowUserResult);
            }
            catch (Exception ex)
            {
                FollowUserResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, FollowUserResult);
            }

            return FollowUserResult;
        }

        #endregion

        #region USP_DefollowUser
        /// <summary>
        /// Defollow a user
        /// </summary>
        /// <returns>MyCookin SP Standard Output - ResultExecutionCode, USPReturnValue, isError</returns>
        public ManageUSPReturnValue DefollowUser()
        {
            ManageUSPReturnValue DefollowUserResult;

            try
            {
                ManageFriendshipTableAdapter taDefollowUser = new ManageFriendshipTableAdapter();

                DefollowUserResult = new ManageUSPReturnValue(taDefollowUser.DefollowUser(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser));
                //MyUser.UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, DefollowUserResult);

                //WRITE A ROW IN STATISTICS DB
                try
                {
                    MyStatistics NewStatisticUser = new MyStatistics(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser, StatisticsActionType.US_UserDefollowed, "User Defollowed", Network.GetCurrentPageName(), "", "");
                    NewStatisticUser.InsertNewRow();
                }
                catch { }
            }
            catch (SqlException sqlEx)
            {
                DefollowUserResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, DefollowUserResult);
            }
            catch (Exception ex)
            {
                DefollowUserResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, DefollowUserResult);
            }

            return DefollowUserResult;
        }

        #endregion

        #region ListOfUserFollowed
        /// <summary>
        /// Create a list of User Followed (that you are following)
        /// </summary>
        public DataTable CreateListOfUserFollowed()
        {
            DataTable UserFollowedList = new DataTable();

            try
            {
                UsersFollowersTableAdapter taUserFollowed = new UsersFollowersTableAdapter();

                UserFollowedList = taUserFollowed.ListOfUserFollowed(_IDUserFriend1.IDUser);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Log of error here or return something!
            }

            return UserFollowedList;
        }

        #endregion

        #region ListOfFollower
        /// <summary>
        /// Create a list of Follower (people that are following me)
        /// </summary>
        public DataTable CreateListOfFollower()
        {
            DataTable FollowerList = new DataTable();

            try
            {
                UsersFollowersTableAdapter taFollower = new UsersFollowersTableAdapter();

                FollowerList = taFollower.ListOfFollower(_IDUserFriend1.IDUser);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //Log of error here or return something!
            }

            return FollowerList;
        }

        #endregion

        #region CheckFollowing
        /// <summary>
        /// Check if one User is following another
        /// </summary>
        /// <returns>True if Following.</returns>
        public bool CheckFollowing()
        {
            UsersFollowersTableAdapter taFollowingCheck = new UsersFollowersTableAdapter();
            DataTable dtFollowingCheck = new DataTable();

            dtFollowingCheck = taFollowingCheck.CheckFollowing(_IDUserFriend1.IDUser, _IDUserFriend2.IDUser);

            //DataTable anyway give a row. If IDUserFollower is null, .Field<Guid> gives Error.
            try
            {
                _IDUserFollower = dtFollowingCheck.Rows[0].Field<Guid>("IDUserFollower");
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region NumberOfFollowers
        /// <summary>
        /// How many users are following us
        /// </summary>
        /// <returns>Number of Follower</returns>
        public int NumberOfFollowers()
        {
            UsersFollowersTableAdapter taNumberOfFollowers = new UsersFollowersTableAdapter();

            int NumberOfFollowers = (int)taNumberOfFollowers.NumberOfFollowers(_IDUserFriend1.IDUser);

            return NumberOfFollowers;
        }

        #endregion

        #region NumberOfFollowing
        /// <summary>
        /// How many users are following us
        /// </summary>
        /// <returns>Number of people followed</returns>
        public int NumberOfFollowing()
        {
            UsersFollowersTableAdapter taNumberOfFollowing = new UsersFollowersTableAdapter();

            int NumberOfFollowing = (int)taNumberOfFollowing.NumberOfFollowing(_IDUserFriend1.IDUser);

            return NumberOfFollowing;
        }

        #endregion

        //USP LOG

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

        #endregion
    }
}
