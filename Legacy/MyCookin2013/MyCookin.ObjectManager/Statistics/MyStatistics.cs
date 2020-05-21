using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.Statistics;
using MyCookin.Log;

namespace MyCookin.ObjectManager.StatisticsManager
{
    #region Enum StatisticsActionType
    public enum StatisticsActionType:int
    {
        NotDefined = 0,
        //User
        US_Login = 100,
        US_Logout = 101,
        US_ProfileViewed = 102,
        US_FriendshipRequest = 103,
        US_FriendshipRemoved = 104,
        US_NewFriendship = 105,
        US_UserFollowed = 106,
        US_UserDefollowed = 107,
        US_UserBlocked = 108,
        US_UserBlockRemoved = 109,
        US_UserReportedAsSpammer = 110,
        US_UserBecomeCook = 111,
        US_UserNoMoreCook = 112,
        US_UserUpdateCookInformation = 113,
        US_UpdateProfile = 114,
        US_ChangePassword = 115,
        US_WrongPasswordInserted = 116,
        US_AccountDeleted = 117,
        US_EmailSent = 118,
        US_SessionDestroyed = 119,
        US_CookieDestroyed = 120,
        US_NewRegistration = 121,
        US_AccountRestored = 122,
        US_UserActivated = 123,
        US_FriendshipDeclined = 124,
        US_FriendshipAccepted = 125,
        US_NavigateExternalLink = 126,

        //SOCIAL
        SC_LoginThroughFacebook = 201,
        SC_LoginThroughGoogle = 202,
        SC_LoginThroughTwitter = 203,
        SC_NewRegistrationThroughGoogle = 204,
        SC_NewRegistrationThroughFacebook = 205,
        SC_NewRegistrationThroughTwitter = 206,
        SC_ActionSharedOnFacebook = 207,
        SC_ActionSharedOnTwitter = 208,
        SC_ActionSharedOnGoogle = 209,
        SC_Like = 210,
        SC_DontLikeMore = 211,
        SC_SocialFriendsRetrieved = 212,
        SC_ContactFriendsMemorized = 231,
        SC_NewActionOnUserBoard = 232,
        SC_NewComment = 233,
        SC_NewPersonalMessage = 234,
        SC_NewPostOnFriendUserBoard = 235,
        SC_LikeForComment = 236,
        SC_LikeForNewFollower = 237,
        SC_LikeForNewIngredient = 238,
        SC_LikeForNewRecipe = 239,
        SC_LikeForPersonalMessage = 240,
        SC_LikeForPostOnFriendUserBoard = 241,
        SC_LikeForProfileUpdate = 242,

        //Ingredients
        IN_NewIngredient = 300,
        IN_ViewIngredient = 301,

        //Recipes
        RC_NewRecipe = 400,
        RC_ViewRecipe = 401,
        RC_AddedToRecipeBook = 402,
        RC_CookingRecipe = 403,
        RC_RecipeSharedOnFacebookFromShowRecipe = 404,
        RC_RecipeSharedOnTwitterFromShowRecipe = 405,
        RC_RecipeSharedOnOwnWallFromShowRecipe = 406,
        RC_RecipeSharedOnFacebookFromWall = 407,
        RC_RecipeSharedOnTwitterFromWall = 408,
        RC_RecipeSharedOnOwnWallFromWall = 409

    }
    #endregion

    public class MyStatistics
    {
        #region Privatefields
        private Guid _IDUserActionStatistic;
        private Guid? _IDUser;
        private Guid? _IDRelatedObject;
        private StatisticsActionType _StatisticsActionType;
        private string _Comments;
        private DateTime _DateAction;
        private string _FileOrigin;
        private string _SearchString;
        private string _OtherInfo;
        #endregion

        #region PublicFields
        public Guid IDUserActionStatistic
        {
        get { return _IDUserActionStatistic;}
        set { _IDUserActionStatistic = value;}
        }
        public Guid? IDUser
        {
        get { return _IDUser;}
        set { _IDUser = value;}
        }
        public Guid? IDRelatedObject
        {
        get { return _IDRelatedObject;}
        set { _IDRelatedObject = value;}
        }
        public StatisticsActionType StatisticsActionType
        {
        get { return _StatisticsActionType;}
        set { _StatisticsActionType = value;}
        }
        public string Comments
        {
        get { return _Comments;}
        set { _Comments = value;}
        }
        public DateTime DateAction
        {
        get { return _DateAction;}
        set { _DateAction = value;}
        }
        public string FileOrigin
        {
            get { return _FileOrigin; }
            set { _FileOrigin = value; }
        }
        public string SearchString
        {
            get { return _SearchString; }
            set { _SearchString = value; }
        }
        public string OtherInfo
        {
            get { return _OtherInfo; }
            set { _OtherInfo = value; }
        }
        #endregion

        #region Constructors
        public MyStatistics()
        { 
        }

        public MyStatistics(Guid? IDUser, Guid? IDRelatedObject, StatisticsActionType StatisticsActionType, string Comments, 
                            string FileOrigin, string SearchString, string OtherInfo)
        {
            _IDUser = IDUser;
            _IDRelatedObject = IDRelatedObject;
            _StatisticsActionType = StatisticsActionType;
            _Comments = Comments;
            _FileOrigin = FileOrigin;
            _SearchString = SearchString;
            _OtherInfo = OtherInfo;
        }
        #endregion

        #region Methods
        public void InsertNewRow()
        {
            try
            {
                DBStatisticsEntities ent_Statatistic = new DBStatisticsEntities();
                
                ent_Statatistic.USP_InsertUserActionStatistic(_IDUser, _IDRelatedObject, (int)_StatisticsActionType, _Comments, DateTime.UtcNow, _FileOrigin,
                                                        _SearchString, _OtherInfo);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", "MyStatistics.cs", "US-ER-9999", "General Error in NewStatisticRow " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

    }
}
