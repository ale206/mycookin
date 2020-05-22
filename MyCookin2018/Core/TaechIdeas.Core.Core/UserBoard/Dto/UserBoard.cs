using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class UserBoard
    {
        public Guid IdUserAction { get; set; }
        public Guid IdUser { get; set; }
        public Guid? IdUserActionFather { get; set; }
        public Guid? IdActionRelatedObject { get; set; }
        public string UserActionMessage { get; set; }
        public int? IdVisibility { get; set; }
        public DateTime UserActionDate { get; set; }
        public DateTime? DeletedOn { get; set; }

        public int IdUserActionTypeLanguage { get; set; }
        public int IdLanguage { get; set; }
        public string UserActionType { get; set; }
        public string UserActionTypeTemplate { get; set; }
        public string UserActionTypeTemplatePlural { get; set; }
        public string UserActionTypeToolTip { get; set; }
        public string NotificationTemplate { get; set; }

        public ActionTypes IdUserActionType { get; set; }
        public bool UserActionTypeEnabled { get; set; }
        public bool UserActionTypeSiteNotice { get; set; }
        public bool UserActionTypeMailNotice { get; set; }
        public bool UserActionTypeSmsNotice { get; set; }
        public int? UserActionTypeMessageMaxLength { get; set; }

        public string SortOrder { get; set; }
        public int NumberOfResults { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? PageCount { get; set; }

        public int TypeOfLike { get; set; }

        //#region Constructors

        //public UserBoard(IUserManager userManager)
        //{
        //    _userManager = userManager;
        //}

        ///// <summary>
        ///// Constructor for Delete or Update Action
        ///// </summary>
        ///// <param name="UserActionId"></param>
        ///// <param name="GetInformations">If set to true, launch GetInfoByIDUserAction and GetTemplate</param>
        //public UserBoard(Guid UserActionId, bool GetInformations, IUserManager userManager)
        //{
        //    _IDUserAction = UserActionId;
        //    _userManager = userManager;
        //    _IDLanguage = _myConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1);

        //    if (GetInformations)
        //    {
        //        GetInfoByIDUserAction();
        //        GetTemplate();
        //    }
        //}

        ///// <summary>
        ///// Constructor for generate Father or Sons of the UsersBoard
        ///// </summary>
        ///// <param name="IDUserActionType">If valorized, select only the type desired - Notice this can be null ONLY for FATHER!</param>
        ///// <param name="IDLanguage"></param>
        ///// <param name="IDUser"></param>
        ///// <param name="IDUserActionFather"></param>
        //public UserBoard(ActionTypes IDUserActionType, int IDLanguage, Guid IDUser, Guid? IDUserActionFather,
        //    string SortOrder, int NumberOfResults, IUserManager userManager)
        //{
        //    _IDUserActionType = IDUserActionType;
        //    _IDLanguage = IDLanguage;
        //    _IDUser = IDUser;
        //    _IDUserActionFather = IDUserActionFather;
        //    _SortOrder = SortOrder;
        //    _NumberOfResults = NumberOfResults;
        //    _userManager = userManager;

        //    //Call Method to get Template and TemplatePlural
        //    GetTemplate();
        //}

        ///// <summary>
        ///// Constructor for generate UsersBoard With Pagination
        ///// </summary>
        ///// <param name="IDLanguage"></param>
        ///// <param name="IDUser"></param>
        ///// <param name="SortOrder"></param>
        ///// <param name="NumberOfResults"></param>
        ///// <param name="PageIndex"></param>
        ///// <param name="PageSize"></param>
        ///// <param name="PageCount"></param>
        //public UserBoard(int IDLanguage, Guid IDUser, string SortOrder, int? PageIndex, int? PageSize, IUserManager userManager)
        //{
        //    _IDLanguage = IDLanguage;
        //    _IDUser = IDUser;
        //    _SortOrder = SortOrder;
        //    _PageIndex = PageIndex;
        //    _PageSize = PageSize;
        //    _userManager = userManager;

        //    //Call Method to get Template and TemplatePlural
        //    _IDUserActionType = 0;
        //    _IDUserActionFather = null;

        //    GetTemplate();
        //}

        ///// <summary>
        ///// Constructor to insert new action and write notifications
        ///// </summary>
        ///// <param name="IDUser"></param>
        ///// <param name="IDUserActionFather"></param>
        ///// <param name="IDUserActionType"></param>
        ///// <param name="IDActionRelatedObject"></param>
        ///// <param name="UserActionMessage"></param>
        ///// <param name="IDVisibility"></param>
        ///// <param name="UserActionDate"></param>
        ///// <param name="IDLanguage"></param>
        //public UserBoard(Guid IDUser, Guid? IDUserActionFather, ActionTypes IDUserActionType,
        //    Guid? IDActionRelatedObject,
        //    string UserActionMessage, int? IDVisibility, DateTime UserActionDate, int IDLanguage, IUserManager userManager)
        //{
        //    _IDUser = IDUser;
        //    _IDUserActionFather = IDUserActionFather;
        //    _IDUserActionType = IDUserActionType;
        //    _IDActionRelatedObject = IDActionRelatedObject;
        //    _UserActionMessage = UserActionMessage;
        //    _IDVisibility = IDVisibility;
        //    _UserActionDate = UserActionDate;
        //    _IDLanguage = IDLanguage;
        //    _userManager = userManager;

        //    GetTemplate();
        //}

        ///// <summary>
        ///// Constructor for Likes and Comments
        ///// </summary>
        ///// <param name="IDUserActionType"></param>
        ///// <param name="IDLanguage"></param>
        ///// <param name="IDUserActionFather"></param>
        //public UserBoard(ActionTypes IDUserActionType, int IDLanguage, Guid? IDUserActionFather, IUserManager userManager)
        //{
        //    _IDUserActionType = IDUserActionType;
        //    _IDLanguage = IDLanguage;
        //    _IDUserActionFather = IDUserActionFather;
        //    _userManager = userManager;

        //    //Call Method to get Template and TemplatePlural
        //    GetTemplate();
        //}

        ///// <summary>
        ///// Constructor for UsersLikes
        ///// </summary>
        ///// <param name="IDUserActionType"></param>
        ///// <param name="IDUserActionFather"></param>
        //public UserBoard(int IDUserActionType, Guid? IDUserActionFather, IUserManager userManager)
        //{
        //    _IDUserActionType = (ActionTypes) IDUserActionType;
        //    _IDUserActionFather = IDUserActionFather;
        //    _userManager = userManager;
        //}

        //#endregion
    }
}