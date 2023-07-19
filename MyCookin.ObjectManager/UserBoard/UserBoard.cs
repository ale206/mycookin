using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.UserBoard.UserBoardTableAdapters;
using System.Data;
using MyCookin.Log;
using MyCookin.Common;
using System.Web;
using System.Data.SqlClient;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.ObjectManager.UserBoardNotificationsManager;
using MyCookin.ObjectManager.RecipeManager;

namespace MyCookin.ObjectManager.UserBoardManager
{
    public enum ActionTypes : int
    {
        NotSpecified = 0,
        NewFollower = 1,
        PersonalMessage = 2,
        LikeForComment = 3,
        Comment = 4,
        FotoUploaded = 5,
        NewRecipe = 6,
        NewIngredient = 7,
        UserProfileUpdated = 8,
        PostOnFriendUserBoard = 9,
        LikeForPostOnFriendUserBoard = 10,
        LikeForNewIngredient = 11,
        LikeForPersonalMessage = 12,
        LikeForNewRecipe = 13,
        LikeForNewFollower = 14,
        LikeForProfileUpdate = 15,
        NewMessageReceived = 16,
        NewRecipeShare = 17,
        RecipeAddedToRecipeBook = 19,
        RecipeCooked = 20,
        LikeForRecipeAddedToRecipeBook = 21,
        OwnRecipeShared = 22,
    }

    public enum LoadBoardDirection : int
    {
        NotSpecified = 0,
        Next = 1,
        Previous = -1
    }

    public class UserBoard
    {
        #region PrivateFields
        private Guid _IDUserAction;
        private Guid _IDUser;
        private Guid? _IDUserActionFather;
        private Guid? _IDActionRelatedObject;
        private string _UserActionMessage;
        private int? _IDVisibility;
        private DateTime _UserActionDate;
        private DateTime? _DeletedOn;
        
        private int _IDUserActionTypeLanguage;
        private int _IDLanguage;
        private string _UserActionType;
        private string _UserActionTypeTemplate;
        private string _UserActionTypeTemplatePlural;
        private string _UserActionTypeToolTip;
        private string _NotificationTemplate;

        private ActionTypes _IDUserActionType;
        private bool _UserActionTypeEnabled;
        private bool _UserActionTypeSiteNotice;
        private bool _UserActionTypeMailNotice;
        private bool _UserActionTypeSmsNotice;
        private int? _UserActionTypeMessageMaxLength;

        private string _SortOrder;
        private int _NumberOfResults;

        private int? _PageIndex;
        private int? _PageSize;
        private int? _PageCount;

        private int _TypeOfLike;
        #endregion

        #region PublicFields
        public Guid IDUserAction
        {
        get { return _IDUserAction;}
        set { _IDUserAction = value;}
        }
        public Guid IDUser
        {
        get { return _IDUser;}
        set { _IDUser = value;}
        }
        public Guid? IDUserActionFather
        {
        get { return _IDUserActionFather;}
        set { _IDUserActionFather = value;}
        }
        public Guid? IDActionRelatedObject
        {
        get { return _IDActionRelatedObject;}
        set { _IDActionRelatedObject = value;}
        }
        public string UserActionMessage
        {
        get { return _UserActionMessage;}
        set { _UserActionMessage = value;}
        }
        public int? IDVisibility
        {
        get { return _IDVisibility;}
        set { _IDVisibility = value;}
        }
        public DateTime UserActionDate
        {
            get { return MyConvert.ToLocalTime(_UserActionDate, MyConvert.ToInt32(HttpContext.Current.Session["Offset"].ToString(), 0)); }
            set { _UserActionDate = value;}
        }
        public DateTime? DeletedOn
        {
        get { return _DeletedOn;}
        set { _DeletedOn = value;}
        }
        public int IDUserActionTypeLanguage
        {
        get { return _IDUserActionTypeLanguage;}
        set { _IDUserActionTypeLanguage = value;}
        }
        public int IDLanguage
        {
        get { return _IDLanguage;}
        set { _IDLanguage = value;}
        }
        public string UserActionType
        {
        get { return _UserActionType;}
        set { _UserActionType = value;}
        }
        public string UserActionTypeTemplate
        {
        get { return _UserActionTypeTemplate;}
        set { _UserActionTypeTemplate = value;}
        }
        public string UserActionTypeTemplatePlural
        {
            get { return _UserActionTypeTemplatePlural; }
            set { _UserActionTypeTemplatePlural = value; }
        }
        public string UserActionTypeToolTip
        {
        get { return _UserActionTypeToolTip;}
        set { _UserActionTypeToolTip = value;}
        }
        public string NotificationTemplate
        {
            get { return _NotificationTemplate; }
            set { _NotificationTemplate = value; }
        }
        public ActionTypes IDUserActionType
        {
        get { return _IDUserActionType;}
        set { _IDUserActionType = value;}
        }
        public bool UserActionTypeEnabled
        {
        get { return _UserActionTypeEnabled;}
        set { _UserActionTypeEnabled = value;}
        }
        public bool UserActionTypeSiteNotice
        {
        get { return _UserActionTypeSiteNotice;}
        set { _UserActionTypeSiteNotice = value;}
        }
        public bool UserActionTypeMailNotice
        {
        get { return _UserActionTypeMailNotice;}
        set { _UserActionTypeMailNotice = value;}
        }
        public bool UserActionTypeSmsNotice
        {
        get { return _UserActionTypeSmsNotice;}
        set { _UserActionTypeSmsNotice = value;}
        }
        public int? UserActionTypeMessageMaxLength
        {
        get { return _UserActionTypeMessageMaxLength;}
        set { _UserActionTypeMessageMaxLength = value;}
        }
        public string SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        public int NumberOfResults
        {
            get { return _NumberOfResults; }
            set { _NumberOfResults = value; }
        }
        public int? PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        public int? PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        public int? PageCount
        {
            get { return _PageCount; }
            set { _PageCount = value; }
        }

        public int TypeOfLike
        {
            get { return _TypeOfLike; }
            set { _TypeOfLike = value; }
        }
        #endregion

        #region Constructors
        public UserBoard()
        { 
        
        }

        /// <summary>
        /// Constructor for Delete or Update Action
        /// </summary>
        /// <param name="IDUserAction"></param>
        /// <param name="GetInformations">If set to true, launch GetInfoByIDUserAction and GetTemplate</param>
        public UserBoard(Guid IDUserAction, bool GetInformations)
        {
            _IDUserAction = IDUserAction;
            _IDLanguage = MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(),1);

            if (GetInformations)
            {
                GetInfoByIDUserAction();
                GetTemplate();
            }
        }

        /// <summary>
        /// Constructor for generate Father or Sons of the UsersBoard
        /// </summary>
        /// <param name="IDUserActionType">If valorized, select only the type desired - Notice this can be null ONLY for FATHER!</param>
        /// <param name="IDLanguage"></param>
        /// <param name="IDUser"></param>
        /// <param name="IDUserActionFather"></param>
        public UserBoard(ActionTypes IDUserActionType, int IDLanguage, Guid IDUser, Guid? IDUserActionFather, string SortOrder, int NumberOfResults)
        {
            _IDUserActionType = IDUserActionType;
            _IDLanguage = IDLanguage;
            _IDUser = IDUser;
            _IDUserActionFather = IDUserActionFather;
            _SortOrder = SortOrder;
            _NumberOfResults = NumberOfResults;

            //Call Method to get Template and TemplatePlural
            GetTemplate();
        }

        /// <summary>
        /// Constructor for generate UsersBoard With Pagination
        /// </summary>
        /// <param name="IDLanguage"></param>
        /// <param name="IDUser"></param>
        /// <param name="SortOrder"></param>
        /// <param name="NumberOfResults"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCount"></param>
        public UserBoard(int IDLanguage, Guid IDUser, string SortOrder, int? PageIndex, int? PageSize)
        {
            
            _IDLanguage = IDLanguage;
            _IDUser = IDUser;
            _SortOrder = SortOrder;
            _PageIndex = PageIndex;
            _PageSize = PageSize;
   
            //Call Method to get Template and TemplatePlural
            _IDUserActionType = 0;
            _IDUserActionFather = null;

            GetTemplate();
        }
        
        /// <summary>
        /// Constructor to insert new action and write notifications
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="IDUserActionFather"></param>
        /// <param name="IDUserActionType"></param>
        /// <param name="IDActionRelatedObject"></param>
        /// <param name="UserActionMessage"></param>
        /// <param name="IDVisibility"></param>
        /// <param name="UserActionDate"></param>
        /// <param name="IDLanguage"></param>
        public UserBoard(Guid IDUser, Guid? IDUserActionFather, ActionTypes IDUserActionType, Guid? IDActionRelatedObject,
                         string UserActionMessage, int? IDVisibility, DateTime UserActionDate, int IDLanguage)
        {
            _IDUser = IDUser;
            _IDUserActionFather = IDUserActionFather;
            _IDUserActionType = IDUserActionType;
            _IDActionRelatedObject = IDActionRelatedObject;
            _UserActionMessage = UserActionMessage;
            _IDVisibility = IDVisibility;
            _UserActionDate = UserActionDate;
            _IDLanguage = IDLanguage;

            GetTemplate();
        }

        /// <summary>
        /// Constructor for Likes and Comments
        /// </summary>
        /// <param name="IDUserActionType"></param>
        /// <param name="IDLanguage"></param>
        /// <param name="IDUserActionFather"></param>
        public UserBoard(ActionTypes IDUserActionType, int IDLanguage, Guid? IDUserActionFather)
        {
            _IDUserActionType = IDUserActionType;
            _IDLanguage = IDLanguage;
            _IDUserActionFather = IDUserActionFather;

            //Call Method to get Template and TemplatePlural
            GetTemplate();
        }

        /// <summary>
        /// Constructor for UsersLikes
        /// </summary>
        /// <param name="IDUserActionType"></param>
        /// <param name="IDUserActionFather"></param>
        public UserBoard(int IDUserActionType, Guid? IDUserActionFather)
        {
            _IDUserActionType = (ActionTypes)IDUserActionType;
            _IDUserActionFather = IDUserActionFather;
        }

        #endregion

        #region Methods

        #region UsersBoardBlockLoad
        /// <summary>
        /// Create a List of Blocks for MyProfile Board
        /// </summary>
        /// <returns></returns>
        public List<UserBoard> UsersBoardBlockLoad()
        {
            List<UserBoard> UserBoardList = new List<UserBoard>();

            UserActionsSPTableAdapter TA_UserAction = new UserActionsSPTableAdapter();

            DataTable DT_UserAction = new DataTable();

            try
            {
                string OtherIDActionsToShowOnUserBoard = AppConfig.GetValue("OtherIDActionsToShowOnUserBoard", AppDomain.CurrentDomain);

                DT_UserAction = TA_UserAction.UsersBoardBlockLoad(_IDUser, _SortOrder, _NumberOfResults, OtherIDActionsToShowOnUserBoard, _IDLanguage);

                if (DT_UserAction.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_UserAction.Rows.Count; i++)
                    {
                        UserBoardList.Add(new UserBoard()
                        {
                            _IDUser = DT_UserAction.Rows[i].Field<Guid>("IDUser"),
                            _IDUserActionType = (ActionTypes)DT_UserAction.Rows[i].Field<int>("IDUserActionType"),
                            _UserActionTypeEnabled = DT_UserAction.Rows[i].Field<bool>("UserActionTypeEnabled"),
                            _UserActionTypeMailNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeMailNotice"),
                            _UserActionTypeMessageMaxLength = DT_UserAction.Rows[i].Field<int?>("UserActionTypeMessageMaxLength"),
                            _UserActionTypeSiteNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSiteNotice"),
                            _UserActionTypeSmsNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSmsNotice"),
                            _IDUserActionTypeLanguage = DT_UserAction.Rows[i].Field<int>("IDUserActionTypeLanguage"),
                            _UserActionType = DT_UserAction.Rows[i].Field<string>("UserActionType"),
                            _UserActionTypeTemplate = DT_UserAction.Rows[i].Field<string>("UserActionTypeTemplate"),
                            _UserActionTypeToolTip = DT_UserAction.Rows[i].Field<string>("UserActionTypeToolTip"),
                            _IDUserAction = DT_UserAction.Rows[i].Field<Guid>("IDUserAction"),
                            _IDActionRelatedObject = DT_UserAction.Rows[i].Field<Guid?>("IDActionRelatedObject"),
                            _UserActionMessage = DT_UserAction.Rows[i].Field<string>("UserActionMessage"),
                            _IDVisibility = DT_UserAction.Rows[i].Field<int?>("IDVisibility"),
                            _UserActionDate = DT_UserAction.Rows[i].Field<DateTime>("UserActionDate")
                        });
                    }
                }
            }
            catch
            {
                //ERROR LOG
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List for Board: UsersBoardFatherOrSons()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return UserBoardList;

        }
        #endregion

        #region UsersBoardMyNewsBlockLoad
        /// <summary>
        /// Create a List of Blocks for MyNews Board
        /// </summary>
        /// <returns></returns>
        public List<UserBoard> UsersBoardMyNewsBlockLoad()
        {
            List<UserBoard> UserBoardList = new List<UserBoard>();

            UserActionsSPTableAdapter TA_UserAction = new UserActionsSPTableAdapter();

            DataTable DT_UserAction = new DataTable();

            try
            {
                string OtherIDActionsToShowOnUserBoard = AppConfig.GetValue("OtherIDActionsToShowOnUserBoard", AppDomain.CurrentDomain);

                DT_UserAction = TA_UserAction.UsersBoardMyNewsBlockLoad(_IDUser, _SortOrder, _NumberOfResults, OtherIDActionsToShowOnUserBoard, _IDLanguage);

                if (DT_UserAction.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_UserAction.Rows.Count; i++)
                    {
                        UserBoardList.Add(new UserBoard()
                        {
                            _IDUser = DT_UserAction.Rows[i].Field<Guid>("IDUser"),
                            _IDUserActionType = (ActionTypes)DT_UserAction.Rows[i].Field<int>("IDUserActionType"),
                            _UserActionTypeEnabled = DT_UserAction.Rows[i].Field<bool>("UserActionTypeEnabled"),
                            _UserActionTypeMailNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeMailNotice"),
                            _UserActionTypeMessageMaxLength = DT_UserAction.Rows[i].Field<int?>("UserActionTypeMessageMaxLength"),
                            _UserActionTypeSiteNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSiteNotice"),
                            _UserActionTypeSmsNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSmsNotice"),
                            _IDUserActionTypeLanguage = DT_UserAction.Rows[i].Field<int>("IDUserActionTypeLanguage"),
                            _UserActionType = DT_UserAction.Rows[i].Field<string>("UserActionType"),
                            _UserActionTypeTemplate = DT_UserAction.Rows[i].Field<string>("UserActionTypeTemplate"),
                            _UserActionTypeToolTip = DT_UserAction.Rows[i].Field<string>("UserActionTypeToolTip"),
                            _IDUserAction = DT_UserAction.Rows[i].Field<Guid>("IDUserAction"),
                            _IDActionRelatedObject = DT_UserAction.Rows[i].Field<Guid?>("IDActionRelatedObject"),
                            _UserActionMessage = DT_UserAction.Rows[i].Field<string>("UserActionMessage"),
                            _IDVisibility = DT_UserAction.Rows[i].Field<int?>("IDVisibility"),
                            _UserActionDate = DT_UserAction.Rows[i].Field<DateTime>("UserActionDate"),
                            _TypeOfLike = GetTypeOfLike((ActionTypes)DT_UserAction.Rows[i].Field<int>("IDUserActionType"))
                            //_TypeOfLike = DT_UserAction.Rows[i].Field<int>("IDUserActionType")
                        });
                    }
                }
            }
            catch
            {
                try
                {
                //ERROR LOG
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List for MyNews Board: UsersBoardFatherOrSons()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return UserBoardList;
        }
        #endregion

        #region GetTypeOfLike
        public static int GetTypeOfLike(ActionTypes TypeOfAction)
        {
            int correctLike = 0;

            switch (TypeOfAction)
            {
                case ActionTypes.Comment:
                    correctLike = (int)ActionTypes.LikeForComment;
                    break;
                case ActionTypes.NewFollower:
                    correctLike = (int)ActionTypes.LikeForNewFollower;
                    break;
                case ActionTypes.NewIngredient:
                    correctLike = (int)ActionTypes.LikeForNewIngredient;
                    break;
                case ActionTypes.NewRecipe:
                    correctLike = (int)ActionTypes.LikeForNewRecipe;
                    break;
                case ActionTypes.PersonalMessage:
                    correctLike = (int)ActionTypes.LikeForPersonalMessage;
                    break;
                case ActionTypes.PostOnFriendUserBoard:
                    correctLike = (int)ActionTypes.LikeForPostOnFriendUserBoard;
                    break;
                case ActionTypes.UserProfileUpdated:
                    correctLike = (int)ActionTypes.LikeForProfileUpdate;
                    break;
                case ActionTypes.RecipeAddedToRecipeBook:
                    correctLike = (int)ActionTypes.LikeForRecipeAddedToRecipeBook;
                    break;
            }

            return correctLike;
        }
        #endregion

        #region GetTypeOfLikeFromAction
        public static int GetTypeOfLikeFromAction(ActionTypes TypeOfAction)
        {
            int correctLikeFromAction = 0;

            switch (TypeOfAction)
            {
                case ActionTypes.LikeForComment:
                    correctLikeFromAction = (int)ActionTypes.Comment;
                    break;
                case ActionTypes.LikeForNewFollower:
                    correctLikeFromAction = (int)ActionTypes.NewFollower;
                    break;
                case ActionTypes.LikeForNewIngredient:
                    correctLikeFromAction = (int)ActionTypes.NewIngredient;
                    break;
                case ActionTypes.LikeForNewRecipe:
                    correctLikeFromAction = (int)ActionTypes.NewRecipe;
                    break;
                case ActionTypes.LikeForPersonalMessage:
                    correctLikeFromAction = (int)ActionTypes.PersonalMessage;
                    break;
                case ActionTypes.LikeForPostOnFriendUserBoard:
                    correctLikeFromAction = (int)ActionTypes.PostOnFriendUserBoard;
                    break;
                case ActionTypes.LikeForProfileUpdate:
                    correctLikeFromAction = (int)ActionTypes.UserProfileUpdated;
                    break;
                case ActionTypes.LikeForRecipeAddedToRecipeBook:
                    correctLikeFromAction = (int)ActionTypes.RecipeAddedToRecipeBook;
                    break;
            }

            return correctLikeFromAction;
        }
        #endregion

        #region GetUsersBoardWithPagination
        /// <summary>
        /// Create a List for "Father" UserBoard or "Sons" Userboard
        /// </summary>
        /// <returns></returns>
        public List<UserBoard> GetUsersBoardWithPagination()
        {
            List<UserBoard> UserBoardList = new List<UserBoard>();

            UserActionsSPTableAdapter TA_UserAction = new UserActionsSPTableAdapter();

            DataTable DT_UserAction = new DataTable();

            try
            {

                DT_UserAction = TA_UserAction.GetUsersBoardWithPagination(_IDUser, _SortOrder, _PageIndex, _PageSize, out _PageCount);

                if (DT_UserAction.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_UserAction.Rows.Count; i++)
                    {
                        UserBoardList.Add(new UserBoard()
                        {
                            _IDUser = DT_UserAction.Rows[i].Field<Guid>("IDUser"),
                            _IDUserActionType = (ActionTypes)DT_UserAction.Rows[i].Field<int>("IDUserActionType"),
                            _UserActionTypeEnabled = DT_UserAction.Rows[i].Field<bool>("UserActionTypeEnabled"),
                            _UserActionTypeMailNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeMailNotice"),
                            _UserActionTypeMessageMaxLength = DT_UserAction.Rows[i].Field<int?>("UserActionTypeMessageMaxLength"),
                            _UserActionTypeSiteNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSiteNotice"),
                            _UserActionTypeSmsNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSmsNotice"),
                            _IDUserActionTypeLanguage = DT_UserAction.Rows[i].Field<int>("IDUserActionTypeLanguage"),
                            _UserActionType = DT_UserAction.Rows[i].Field<string>("UserActionType"),
                            _UserActionTypeTemplate = DT_UserAction.Rows[i].Field<string>("UserActionTypeTemplate"),
                            _UserActionTypeToolTip = DT_UserAction.Rows[i].Field<string>("UserActionTypeToolTip"),
                            _IDUserAction = DT_UserAction.Rows[i].Field<Guid>("IDUserAction"),
                            _IDActionRelatedObject = DT_UserAction.Rows[i].Field<Guid?>("IDActionRelatedObject"),
                            _UserActionMessage = DT_UserAction.Rows[i].Field<string>("UserActionMessage"),
                            _IDVisibility = DT_UserAction.Rows[i].Field<int?>("IDVisibility"),
                            _UserActionDate = DT_UserAction.Rows[i].Field<DateTime>("UserActionDate")
                        });
                    }
                }
            }
            catch
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List for Board: UsersBoardFatherOrSons()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return UserBoardList;

        }
        #endregion

        #region GetUsersBoardFatherOrSons
        /// <summary>
        /// Create a List for "Father" UserBoard or "Sons" Userboard
        /// </summary>
        /// <returns></returns>
        public List<UserBoard> GetUsersBoardFatherOrSons()
        {
            List<UserBoard> UserBoardList = new List<UserBoard>();

            UserActionsSPTableAdapter TA_UserAction = new UserActionsSPTableAdapter();

            DataTable DT_UserAction = new DataTable();

            try
            {
                _IDLanguage = MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1);

                DT_UserAction = TA_UserAction.GetUsersBoardFatherOrSon((int)_IDUserActionType, _IDUser, _IDUserActionFather, _NumberOfResults, _SortOrder, _IDLanguage);

                if (DT_UserAction.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_UserAction.Rows.Count; i++)
                    {
                        UserBoardList.Add(new UserBoard()
                        {
                            _IDUser = DT_UserAction.Rows[i].Field<Guid>("IDUser"),
                            _IDUserActionType = (ActionTypes)DT_UserAction.Rows[i].Field<int>("IDUserActionType"),
                            _UserActionTypeEnabled = DT_UserAction.Rows[i].Field<bool>("UserActionTypeEnabled"),
                            _UserActionTypeMailNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeMailNotice"),
                            _UserActionTypeMessageMaxLength = DT_UserAction.Rows[i].Field<int?>("UserActionTypeMessageMaxLength"),
                            _UserActionTypeSiteNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSiteNotice"),
                            _UserActionTypeSmsNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSmsNotice"),
                            _IDUserActionTypeLanguage = DT_UserAction.Rows[i].Field<int>("IDUserActionTypeLanguage"),
                            _UserActionType = DT_UserAction.Rows[i].Field<string>("UserActionType"),
                            _UserActionTypeTemplate = DT_UserAction.Rows[i].Field<string>("UserActionTypeTemplate"),
                            _UserActionTypeToolTip = DT_UserAction.Rows[i].Field<string>("UserActionTypeToolTip"),
                            _IDUserAction = DT_UserAction.Rows[i].Field<Guid>("IDUserAction"),
                            _IDActionRelatedObject = DT_UserAction.Rows[i].Field<Guid?>("IDActionRelatedObject"),
                            _UserActionMessage = DT_UserAction.Rows[i].Field<string>("UserActionMessage"),
                            _IDVisibility = DT_UserAction.Rows[i].Field<int?>("IDVisibility"),
                            _UserActionDate = DT_UserAction.Rows[i].Field<DateTime>("UserActionDate")
                        });
                    }
                }
            }
            catch
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List for Board: UsersBoardFatherOrSons()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return UserBoardList;

        }
        #endregion

        #region GetUsersBoardBlockElement
        public List<UserBoard> GetUsersBoardBlockElement()
        {
            List<UserBoard> GetUserBoardBlockElementList = new List<UserBoard>();

            UserActionsSPTableAdapter TA_UserAction = new UserActionsSPTableAdapter();

            DataTable DT_UserAction = new DataTable();

            try
            {
                DT_UserAction = TA_UserAction.GetUsersBoardBlockElement(_IDUserAction, _IDLanguage);

                if (DT_UserAction.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_UserAction.Rows.Count; i++)
                    {
                        GetUserBoardBlockElementList.Add(new UserBoard()
                        {
                            _IDUserAction = _IDUserAction,

                            _IDUserActionType = (ActionTypes)DT_UserAction.Rows[i].Field<int>("IDUserActionType"),
                            _UserActionTypeMailNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeMailNotice"),
                            _UserActionTypeMessageMaxLength = DT_UserAction.Rows[i].Field<int?>("UserActionTypeMessageMaxLength"),
                            _UserActionTypeSiteNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSiteNotice"),
                            _UserActionTypeSmsNotice = DT_UserAction.Rows[i].Field<bool>("UserActionTypeSmsNotice"),
                            _IDUserActionTypeLanguage = DT_UserAction.Rows[i].Field<int>("IDUserActionTypeLanguage"),
                            _UserActionType = DT_UserAction.Rows[i].Field<string>("UserActionType"),
                            _UserActionTypeTemplate = DT_UserAction.Rows[i].Field<string>("UserActionTypeTemplate"),
                            _UserActionTypeToolTip = DT_UserAction.Rows[i].Field<string>("UserActionTypeToolTip"),
                            _IDActionRelatedObject = DT_UserAction.Rows[i].Field<Guid?>("IDActionRelatedObject"),
                            _UserActionMessage = DT_UserAction.Rows[i].Field<string>("UserActionMessage"),
                            _IDVisibility = DT_UserAction.Rows[i].Field<int?>("IDVisibility"),
                            _UserActionDate = DT_UserAction.Rows[i].Field<DateTime>("UserActionDate"),
                            _IDLanguage = DT_UserAction.Rows[i].Field<int>("IDLanguage"),
                            _IDUser = DT_UserAction.Rows[i].Field<Guid>("IDUser"),
                            _IDUserActionFather = DT_UserAction.Rows[i].Field<Guid?>("IDUserActionFather")
                        });
                    }
                }
            }
            catch
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Create List for Board: UsersBoardFatherOrSons()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return GetUserBoardBlockElementList;
        }
        #endregion

        #region InsertAction & InsertNotification
        /// <summary>
        /// Insert new Action - New Comment, new like, etc.
        /// </summary>
        /// <returns></returns>
        public ManageUSPReturnValue InsertAction()
        {
            ManageUSPReturnValue InsertActionResult;

            try
            {
                InsertActionSPTableAdapter TA_InsertAction = new InsertActionSPTableAdapter();

                DateTime ActionInsertedOn = DateTime.UtcNow;

                InsertActionResult = new ManageUSPReturnValue(TA_InsertAction.InsertActionOnUsersBoard(_IDUser, _IDUserActionFather, (int)_IDUserActionType, _IDActionRelatedObject, _UserActionMessage, _IDVisibility, _UserActionDate));

                try
                {
                    //Get Correct Template
                    GetTemplate();

                    MyUser UserInAction = new MyUser(_IDUser);
                    UserInAction.GetUserBasicInfoByID();

                    List<UserBoard> OwnerActionFatherList = new List<UserBoard>();

                    Guid IDUser = new Guid();
                    ActionTypes CorrectActionType = ActionTypes.NotSpecified;
                    string URLNotification = null;
                    Guid? IDRelatedObject = null;
                    Guid? NotificationImage = null;
                    Guid? IDUserOwnerRelatedObject = null;
                    string UserNotification = String.Empty;

                    //For some Types, notification is not necessary.
                    //For example when a user post a new profile status, it should be notificated to ALL is following him. Then disactivate for cases like this.
                    //Set Notification Insert method active by default.
                    bool NotificationActive = true;

                    switch (_IDUserActionType)
                    {
                        //Someone is following someone else - Actions WITHOUT _IDUserActionFather valorized
                        case ActionTypes.NewFollower:
                            IDUser = (Guid)_IDActionRelatedObject;
                            CorrectActionType = (ActionTypes)_IDUserActionType;
                            URLNotification = "/" + UserInAction.UserName + "/";
                            IDRelatedObject = _IDUser;
                            NotificationImage = null;
                            IDUserOwnerRelatedObject = (Guid)_IDActionRelatedObject;

                            //Create Notification String
                            UserNotification = string.Format("<a href=\"" + URLNotification + "\">" + _NotificationTemplate + "</a>", UserInAction.Name + " " + UserInAction.Surname);

                            break;

                        case ActionTypes.NewRecipeShare:

                            UserBoard UserBoardAction = new UserBoard((Guid)IDActionRelatedObject, true);
                            UserBoardAction.GetUserActionInfo();

                            RecipeLanguage RecipeObj = new RecipeLanguage((Guid)UserBoardAction.IDActionRelatedObject, IDLanguage);
                            RecipeObj.QueryRecipeLanguageInfo();

                            IDUser = _IDUser;
                            CorrectActionType = (ActionTypes)_IDUserActionType;
                            URLNotification = AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + RecipeObj.RecipeName + "/" + RecipeObj.IDRecipe.ToString();
                            IDRelatedObject = UserBoardAction.IDActionRelatedObject;
                            NotificationImage = null;
                            IDUserOwnerRelatedObject = UserBoardAction.IDUser;

                            //Create Notification String
                            UserNotification = string.Format("<a href=\"" + URLNotification + "\">" + _NotificationTemplate + "</a>", UserInAction.Name + " " + UserInAction.Surname);

                            break;
                       
                        case ActionTypes.RecipeAddedToRecipeBook:
                            //IDActionRelatedObject is IDRecipe 

                            RecipeLanguage RecipeObj2 = new RecipeLanguage(_IDActionRelatedObject, IDLanguage);
                            RecipeObj2.QueryRecipeLanguageInfo();
                            RecipeObj2.QueryBaseRecipeInfo();

                            Guid IDUserOwner = RecipeObj2.Owner.IDUser;

                            IDUser = _IDUser;
                            CorrectActionType = (ActionTypes)_IDUserActionType;
                            URLNotification = AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + RecipeObj2.RecipeName + "/" + RecipeObj2.IDRecipe.ToString();
                            IDRelatedObject = _IDActionRelatedObject;
                            NotificationImage = null;
                            IDUserOwnerRelatedObject = IDUserOwner;

                            //Create Notification String
                            UserNotification = string.Format("<a href=\"" + URLNotification + "\">" + _NotificationTemplate + "</a>", UserInAction.Name + " " + UserInAction.Surname);

                            break;

                        case ActionTypes.RecipeCooked:
                            //IDActionRelatedObject is IDRecipe 

                            RecipeLanguage RecipeObj3 = new RecipeLanguage(_IDActionRelatedObject, IDLanguage);
                            RecipeObj3.QueryRecipeLanguageInfo();
                            RecipeObj3.QueryBaseRecipeInfo();

                            Guid IDUserRecipeOwner = RecipeObj3.Owner.IDUser;

                            IDUser = _IDUser;
                            CorrectActionType = (ActionTypes)_IDUserActionType;
                            URLNotification = AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + RecipeObj3.RecipeName + "/" + RecipeObj3.IDRecipe.ToString();
                            IDRelatedObject = _IDActionRelatedObject;
                            NotificationImage = null;
                            IDUserOwnerRelatedObject = IDUserRecipeOwner;

                            //Create Notification String: {0} sta cucinando la tua ricetta {1}
                            UserNotification = string.Format("<a href=\"" + URLNotification + "\">" + _NotificationTemplate + "</a>", UserInAction.Name + " " + UserInAction.Surname, RecipeObj3.RecipeName);

                            break;
                        case ActionTypes.PostOnFriendUserBoard:
                            IDUser = _IDUser;
                            CorrectActionType = (ActionTypes)_IDUserActionType;
                            URLNotification = "/" + UserInAction.UserName + "/";
                            IDRelatedObject = (Guid)_IDActionRelatedObject;
                            NotificationImage = null;
                            IDUserOwnerRelatedObject = (Guid)_IDActionRelatedObject;

                            //Create Notification String
                            UserNotification = string.Format("<a href=\"" + URLNotification + "\">" + _NotificationTemplate + "</a>", UserInAction.Name + " " + UserInAction.Surname);

                            break;
                        case ActionTypes.PersonalMessage:
                        case ActionTypes.UserProfileUpdated:
                        case ActionTypes.NewRecipe:
                            IDUser = _IDUser;
                            CorrectActionType = (ActionTypes)_IDUserActionType;
                            URLNotification = "/" + UserInAction.UserName + "/";
                            NotificationImage = null;

                            //Create Notification String
                            UserNotification = string.Format("<a href=\"" + URLNotification + "\">" + _NotificationTemplate + "</a>", UserInAction.Name + " " + UserInAction.Surname);

                            //Disable Notification if NOT present IDUserOwnerRelatedObject like in this case.
                            NotificationActive = false;

                            break;
                        default:
                            //All Actions WITH _IDUserActionFather valorized
                            OwnerActionFatherList = GetActionOwnerByFatherAction();

                            IDUser = new Guid(HttpContext.Current.Session["IDUser"].ToString());
                            CorrectActionType = (ActionTypes)_IDUserActionType;
                            URLNotification = "/User/UserBoardPost.aspx?IDUserAction=" + _IDUserActionFather;
                            IDRelatedObject = OwnerActionFatherList[0].IDActionRelatedObject;
                            NotificationImage = null;
                            //IDUserOwnerRelatedObject = OwnerActionFatherList[0]._IDActionRelatedObject;
                            IDUserOwnerRelatedObject = OwnerActionFatherList[0].IDUser;

                            //Create Notification String
                            UserNotification = string.Format("<a href=\"" + URLNotification + "\">" + _NotificationTemplate + "</a>", UserInAction.Name + " " + UserInAction.Surname);

                            //Disable Notification for some Actions
                            switch (_IDUserActionType)
                            {
                                case ActionTypes.LikeForRecipeAddedToRecipeBook:
                                    NotificationActive = false;
                                    break;                                
                            }

                            break;
                    }
                            
                    //Insert Notification
                    //This "if" avoid notifications for action on own Types (Ex.: notification for a comment on our post)
                    if (NotificationActive)
                    {
                        if (IDUserOwnerRelatedObject.ToString() != HttpContext.Current.Session["IDUser"].ToString())
                        {
                            UserBoardNotification newNotification = new UserBoardNotification(IDUser, CorrectActionType, URLNotification, IDRelatedObject, NotificationImage, UserNotification, IDUserOwnerRelatedObject);
                            newNotification.InsertNotification();
                        }
                        try
                        {
                            //JUST A LOG
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Debug.ToString(), "", Network.GetCurrentPageName(), "US-IN-0055", "Notification Inserted (UserBoard.cs)", HttpContext.Current.Session["IDUser"].ToString(), false, true);
                            LogManager.WriteFileLog(LogLevel.Debug, false, NewRow);
                            LogManager.WriteDBLog(LogLevel.Debug, NewRow);
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        //ERROR LOG
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in InsertAction -> InsertNotification: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                        LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    }
                    catch { }
                }

                //UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, InsertActionResult);
                //WRITE A ROW IN STATISTICS DB
                try
                {
                    //This is for EVERY Action
                    //MyStatistics NewStatistic = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_NewActionOnUserBoard, "New Action on UserBoard", Network.GetCurrentPageName(), "", "");
                    //NewStatistic.InsertNewRow();

                    //This is for SPECIFIC Action
                    #region StatisticsForCorrectType
                    switch (_IDUserActionType)
                    { 
                        case ActionTypes.Comment:
                            MyStatistics NewStatisticForComment = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_NewComment, "New Comment on UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticForComment.InsertNewRow();
                            break;
                        case ActionTypes.PersonalMessage:
                            MyStatistics NewStatisticForNewPersonalMessage = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_NewPersonalMessage, "New Personal Message on UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticForNewPersonalMessage.InsertNewRow();
                            break;
                        case ActionTypes.PostOnFriendUserBoard:
                            MyStatistics NewStatisticForNewPost = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_NewPostOnFriendUserBoard, "New Post on Friend UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticForNewPost.InsertNewRow();
                            break;
                        case ActionTypes.UserProfileUpdated:
                            MyStatistics NewStatisticUPU = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.US_UpdateProfile, "Profile Updated", Network.GetCurrentPageName(), "", "");
                            NewStatisticUPU.InsertNewRow();
                            break;

                        case ActionTypes.LikeForComment:
                            MyStatistics NewStatisticLFC = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_LikeForComment, "New Like on a Comment of the UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticLFC.InsertNewRow();
                            break;
                        case ActionTypes.LikeForNewFollower:
                            MyStatistics NewStatisticLFNF = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_LikeForNewFollower, "New Like on a New Follower News of the UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticLFNF.InsertNewRow();
                            break;
                        case ActionTypes.LikeForNewIngredient:
                            MyStatistics NewStatisticLFNI = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_LikeForNewIngredient, "New Like on a New Ingredient on the UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticLFNI.InsertNewRow();
                            break;
                        case ActionTypes.LikeForNewRecipe:
                            MyStatistics NewStatisticLFNR = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_LikeForNewRecipe, "New Like on a New Recipe on the UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticLFNR.InsertNewRow();
                            break;
                        case ActionTypes.LikeForPersonalMessage:
                            MyStatistics NewStatisticLFPM = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_LikeForPersonalMessage, "New Like on a Personal Message on the UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticLFPM.InsertNewRow();
                            break;
                        case ActionTypes.LikeForPostOnFriendUserBoard:
                            MyStatistics NewStatisticLFPF = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_LikeForPostOnFriendUserBoard, "New Like on a Post on Friend UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticLFPF.InsertNewRow();
                            break;
                        case ActionTypes.LikeForProfileUpdate:
                            MyStatistics NewStatisticLFPU = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.SC_LikeForProfileUpdate, "New Like on a Profile Update on the UserBoard", Network.GetCurrentPageName(), "", "");
                            NewStatisticLFPU.InsertNewRow();
                            break;
                        case ActionTypes.RecipeAddedToRecipeBook:
                            MyStatistics NewStatisticRATRB = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.RC_AddedToRecipeBook, "Recipe added to Recipe Book", Network.GetCurrentPageName(), "", "");
                            NewStatisticRATRB.InsertNewRow();
                            break;
                        case ActionTypes.RecipeCooked:
                            MyStatistics NewStatisticCR = new MyStatistics(_IDUser, _IDUserActionFather, StatisticsActionType.RC_CookingRecipe, "Recipe cooked", Network.GetCurrentPageName(), "", "");
                            NewStatisticCR.InsertNewRow();
                            break;
                    }
                    #endregion

                }
                catch { }
            }
            catch (SqlException sqlEx)
            {
                InsertActionResult = new ManageUSPReturnValue("US-ER-9999", sqlEx.Message, true);
                UserLogUSP(LogLevel.Errors, LogLevel.Errors, true, InsertActionResult);
            }
            catch (Exception ex)
            {
                InsertActionResult = new ManageUSPReturnValue("US-ER-9999", ex.Message, true);
                UserLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, InsertActionResult);
            }

            return InsertActionResult;
        }
        #endregion

        #region DeleteLike
        public bool DeleteLike()
        {
            bool ExecutionResult = false;

            try
            {
                UsersActionsTableAdapter TA_UsersActions = new UsersActionsTableAdapter();

                TA_UsersActions.DeleteLike(_IDUserActionFather, _IDUser, (int)_IDUserActionType);

                ExecutionResult = true;
            }
            catch(Exception ex)
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Delete Like: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return ExecutionResult;
        }
        #endregion

        #region UpdateActionDeletedOn
        public bool UpdateActionDeletedOn()
        {
            bool ExecutionResult = false;

            try
            {
                UsersActionsTableAdapter TA_UsersActions = new UsersActionsTableAdapter();

                TA_UsersActions.UpdateActionDeletedOn(DateTime.UtcNow, _IDUserAction);

                ExecutionResult = true;
            }
            catch (Exception ex)
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Update Action Deleted On: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return ExecutionResult;
        }
        #endregion

        #region GetInfoByIDUserAction
        public void GetInfoByIDUserAction()
        {
            UserActionsSPTableAdapter TA_UserAction = new UserActionsSPTableAdapter();

            DataTable DT_UserAction = new DataTable();

            try
            {
                DT_UserAction = TA_UserAction.GetUsersBoardBlockElement(_IDUserAction, _IDLanguage);

                if (DT_UserAction.Rows.Count > 0)
                {
                    _IDUserActionType = (ActionTypes)DT_UserAction.Rows[0].Field<int>("IDUserActionType");
                    _UserActionTypeMailNotice = DT_UserAction.Rows[0].Field<bool>("UserActionTypeMailNotice");
                    _UserActionTypeMessageMaxLength = DT_UserAction.Rows[0].Field<int?>("UserActionTypeMessageMaxLength");
                    _UserActionTypeSiteNotice = DT_UserAction.Rows[0].Field<bool>("UserActionTypeSiteNotice");
                    _UserActionTypeSmsNotice = DT_UserAction.Rows[0].Field<bool>("UserActionTypeSmsNotice");
                    _IDUserActionTypeLanguage = DT_UserAction.Rows[0].Field<int>("IDUserActionTypeLanguage");
                    _UserActionType = DT_UserAction.Rows[0].Field<string>("UserActionType");
                    _UserActionTypeTemplate = DT_UserAction.Rows[0].Field<string>("UserActionTypeTemplate");
                    _UserActionTypeToolTip = DT_UserAction.Rows[0].Field<string>("UserActionTypeToolTip");
                    _IDActionRelatedObject = DT_UserAction.Rows[0].Field<Guid?>("IDActionRelatedObject");
                    _UserActionMessage = DT_UserAction.Rows[0].Field<string>("UserActionMessage");
                    _IDVisibility = DT_UserAction.Rows[0].Field<int?>("IDVisibility");
                    _UserActionDate = DT_UserAction.Rows[0].Field<DateTime>("UserActionDate");
                    _IDLanguage = DT_UserAction.Rows[0].Field<int>("IDLanguage");
                    _IDUser = DT_UserAction.Rows[0].Field<Guid>("IDUser");
                    _IDUserActionFather = DT_UserAction.Rows[0].Field<Guid?>("IDUserActionFather");
                }
            }
            catch
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Get Information by IdAction: GetUserActionInfo()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

        }
        #endregion

        #region GetTemplate
        /// <summary>
        /// Get The Template for a specified profile
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Language"></param>
        public void GetTemplate()
        {
            try
            {
                UserActionsTypesLanguagesTableAdapter TA_UserActionTypesLang = new UserActionsTypesLanguagesTableAdapter();

                DataTable DT_Template = new DataTable();
            
                DT_Template = TA_UserActionTypesLang.GetTemplateAccordingToTypeAndLanguage((int)_IDUserActionType, _IDLanguage);

                _UserActionTypeTemplate = DT_Template.Rows[0].Field<string>("UserActionTypeTemplate");
                _UserActionTypeTemplatePlural = DT_Template.Rows[0].Field<string>("UserActionTypeTemplatePlural");
                _NotificationTemplate = DT_Template.Rows[0].Field<string>("NotificationTemplate");
            }
            catch (Exception ex)
            {
                //Activate this only if the entire Table UserActionsTypeLanguage is complete.
                //try
                //{
                //    //ERROR LOG
                //    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Get Template: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                //    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                //    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                //}
                //catch { }
            }
        }
        #endregion

        #region CountLikesOrComments
        public int CountLikesOrComments()
        {
            int ResultCount = 0;

            try
            {
                //int CorrectAction = 0;
               
                //switch (_IDUserActionType)
                //{
                //    //If Like
                //    case ActionTypes.LikeForComment:
                //    case ActionTypes.LikeForNewFollower:
                //    case ActionTypes.LikeForNewIngredient:
                //    case ActionTypes.LikeForNewRecipe:
                //    case ActionTypes.LikeForPersonalMessage:
                //    case ActionTypes.LikeForPostOnFriendUserBoard:
                //    case ActionTypes.LikeForProfileUpdate:
                //        CorrectAction = GetTypeOfLikeFromAction((ActionTypes)Convert.ToInt32(_IDUserActionType));
                //        break;
                //    default:
                //        //If Comment
                //        CorrectAction = (int)_IDUserActionType;
                //        break;
                //}
   
                UsersActionsTableAdapter TA_UserAction = new UsersActionsTableAdapter();

                ResultCount = Convert.ToInt32(TA_UserAction.CountLikesOrComments((int)_IDUserActionType, _IDUserActionFather));
                //ResultCount = Convert.ToInt32(TA_UserAction.CountLikesOrComments(CorrectAction, _IDUserActionFather));
            }
            catch (Exception ex)
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Count Likes Or Comments: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return ResultCount;
        }
        #endregion

        #region UsersByActionTypeAndActionFather
        public List<MyUser> UsersByActionTypeAndActionFather()
        {
            
            List<MyUser> UserList = new List<MyUser>();

            try
            {
                DataTable dtUsers = new DataTable();

                UsersActionsTableAdapter taUsersAction = new UsersActionsTableAdapter();

                int NumberOfResults = MyConvert.ToInt32(AppConfig.GetValue("UserLikesResultsNumber", AppDomain.CurrentDomain), 5); 

                dtUsers = taUsersAction.GetIDUsersByActionTypeAndActionFather(NumberOfResults, (int)_IDUserActionType, _IDUserActionFather);

                if (dtUsers.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUsers.Rows.Count; i++)
                    {
                        IDUser = dtUsers.Rows[i].Field<Guid>("IDUser");

                        MyUser UserInfo = new MyUser(IDUser);
                        UserInfo.GetUserBasicInfoByID();

                        UserList.Add(new MyUser()
                        {
                            IDUser = UserInfo.IDUser,
                            Name = UserInfo.Name,
                            Surname = UserInfo.Surname,
                            UserName = UserInfo.UserName
                        });
                    }
                }
            }
            catch
            { 
            
            }
            return UserList;
        }

        #endregion

        #region CountNumberOfActionsByUserAndType
        public int CountNumberOfActionsByUserAndType()
        {
            int ResultCount = 0;

            try
            {
                UsersActionsTableAdapter TA_UserAction = new UsersActionsTableAdapter();

                ResultCount = Convert.ToInt32(TA_UserAction.CountNumberOfActionsByUserAndType(_IDUserActionFather, (int)_IDUserActionType, _IDUser));
            }
            catch (Exception ex)
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Count Number Of Actions: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return ResultCount;

        }
        #endregion

        #region GetIDUserFromIDUserAction
        public Guid GetIDUserFromIDUserAction()
        {
            try
            {
                UsersActionsTableAdapter TA_UserAction = new UsersActionsTableAdapter();

                DataTable DT_IDUser = new DataTable();

                DT_IDUser = TA_UserAction.GetIDUserFromIDUserAction(_IDUserAction);

                _IDUser = DT_IDUser.Rows[0].Field<Guid>("IDUser");
            }
            catch (Exception ex)
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in Get IDUserFromIDUserAction: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return _IDUser;
        }
        #endregion

        #region GetUserActionInfo
        public List<UserBoard> GetUserActionInfo()
        {
            List<UserBoard> GetUserActionInfoList = new List<UserBoard>();

            UsersActionsTableAdapter TA_UserActionInfo = new UsersActionsTableAdapter();

            DataTable DT_UserActionInfo = new DataTable();

            try
            {
                DT_UserActionInfo = TA_UserActionInfo.GetUserActionInfoByID(_IDUserAction);

                if (DT_UserActionInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_UserActionInfo.Rows.Count; i++)
                    {
                        GetUserActionInfoList.Add(new UserBoard()
                        {
                            _IDUserAction = DT_UserActionInfo.Rows[i].Field<Guid>("IDUserAction"),
                            _IDUser = DT_UserActionInfo.Rows[i].Field<Guid>("IDUser"),
                            _IDUserActionFather = DT_UserActionInfo.Rows[i].Field<Guid?>("IDUserActionFather"),
                            _IDUserActionType = (ActionTypes)DT_UserActionInfo.Rows[i].Field<int>("IDUserActionType"),
                            _IDActionRelatedObject = DT_UserActionInfo.Rows[i].Field<Guid?>("IDActionRelatedObject"),
                            _UserActionMessage = DT_UserActionInfo.Rows[i].Field<string>("UserActionMessage"),
                            _IDVisibility = DT_UserActionInfo.Rows[i].Field<int?>("IDVisibility"),
                            _UserActionDate = DT_UserActionInfo.Rows[i].Field<DateTime>("UserActionDate"),
                        });
                    }
                }
            }
            catch
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in GetUserActionInfoList", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return GetUserActionInfoList;
        }
        #endregion

        #region GetObjectYouLike
        public List<UserBoard> GetObjectYouLike()
        {
            List<UserBoard> GetObjectYouLikeList = new List<UserBoard>();

            UsersActionsTableAdapter TA_UserAction = new UsersActionsTableAdapter();

            DataTable DT_UserAction = new DataTable();

            try
            {
                DT_UserAction = TA_UserAction.GetObjectYouLike((Guid)_IDUserActionFather);

                if (DT_UserAction.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_UserAction.Rows.Count; i++)
                    {
                        GetObjectYouLikeList.Add(new UserBoard()
                        {
                            _IDUser = DT_UserAction.Rows[i].Field<Guid>("IDUserOwner"),
                            _IDUserActionType = (ActionTypes)DT_UserAction.Rows[i].Field<int>("IDUserActionType"),
                            _IDActionRelatedObject = DT_UserAction.Rows[i].Field<Guid?>("IDActionRelatedObject")
                        });
                    }
                }
            }
            catch
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in GetObjectYouLike()", HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return GetObjectYouLikeList;
        }
        #endregion

        #region GetActionOwnerByFatherAction
        /// <summary>
        /// Return the owner of the action according to ActionFather, ActionType and IDUser different from who has just inserted action
        /// </summary>
        /// <returns></returns>
        private List<UserBoard> GetActionOwnerByFatherAction()
        {
            List<UserBoard> GetUserActionInfoList = new List<UserBoard>();

            UsersActionsTableAdapter TA_UserActionInfo = new UsersActionsTableAdapter();

            DataTable DT_UserActionInfo = new DataTable();

            try
            {
                DT_UserActionInfo = TA_UserActionInfo.GetActionOwnerByFather((Guid)_IDUserActionFather);

                if (DT_UserActionInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_UserActionInfo.Rows.Count; i++)
                    {
                        GetUserActionInfoList.Add(new UserBoard()
                        {
                            _IDUserAction = DT_UserActionInfo.Rows[i].Field<Guid>("IDUserAction"),
                            _IDUser = DT_UserActionInfo.Rows[i].Field<Guid>("IDUser"),
                            _IDUserActionFather = DT_UserActionInfo.Rows[i].Field<Guid?>("IDUserActionFather"),
                            _IDUserActionType = (ActionTypes)DT_UserActionInfo.Rows[i].Field<int>("IDUserActionType"),
                            _IDActionRelatedObject = DT_UserActionInfo.Rows[i].Field<Guid?>("IDActionRelatedObject"),
                            _UserActionMessage = DT_UserActionInfo.Rows[i].Field<string>("UserActionMessage"),
                            _IDVisibility = DT_UserActionInfo.Rows[i].Field<int?>("IDVisibility"),
                            _UserActionDate = DT_UserActionInfo.Rows[i].Field<DateTime>("UserActionDate"),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    //ERROR LOG
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in GetUserBoardInfoAccordingToIDUserActionFather: " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch { }
            }

            return GetUserActionInfoList;
        }
        #endregion

        #region UserLogUSP
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

                LogRow NewRow = new LogRow(DateTime.UtcNow, LogDbLevel.ToString(), "", Network.GetCurrentPageName(), USPReturn.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguageForLog, USPReturn.ResultExecutionCode), USPReturn.USPReturnValue, false, true);
                LogManager.WriteFileLog(LogFsLevel, SendEmail, NewRow);
                LogManager.WriteDBLog(LogDbLevel, NewRow);
            }
            catch
            {

            }
        }
        #endregion

        #endregion

    }
}
