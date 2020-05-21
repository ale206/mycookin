using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.ObjectManager.UserBoardNotificationsManager;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using System.Web.Caching;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookin.WebServices.Recipe
{
    /// <summary>
    /// Summary description for RecipeFeedbacks
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class RecipeFeedbacks : System.Web.Services.WebService
    {

        [WebMethod]
        public string LikeRecipe(string IDRecipe, string IDUser, string IDRecipeOwner, string IDLanguage, string Username, string RecipeURL)
        {
            bool isUnLike = false;
            string _textResult = "";
            int _IDLanguage = MyConvert.ToInt32(IDLanguage, 1);
            string _return = "error - RC-ER-0011";
            try
            {
                Guid _IDRecipe = new Guid(IDRecipe);
                Guid _IDUser = new Guid(IDUser);
                RecipeFeedback _likeRecipe = new RecipeFeedback(_IDRecipe, _IDUser, RecipeFeedbackType.Like, "");
                ManageUSPReturnValue _result = _likeRecipe.Save();
                if(String.IsNullOrEmpty(_result.USPReturnValue))
                {
                    isUnLike = true;
                    _textResult = "--unlike--";
                }
                else
                {
                    isUnLike = false;
                    _textResult = "--like--";
                }
                if (!_result.IsError)
                {
                    _return = "200 OK " + _textResult;
                    try
                    {
                        if (!isUnLike && IDUser != IDRecipeOwner)
                        {
                            MyUser _user = null;
                            if (HttpRuntime.Cache[IDUser] != null)
                            {
                                _user = (MyUser)HttpRuntime.Cache[IDUser];
                            }
                            else
                            {
                                _user = new MyUser(_IDUser);
                                _user.GetUserBasicInfoByID();
                                HttpRuntime.Cache.Insert(IDUser, _user, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
                            }
                            Guid _IDRecipeOwner = new Guid(IDRecipeOwner);
                            UserBoard _ActionTemplate = new UserBoard(13, null);
                            _ActionTemplate.IDLanguage = _IDLanguage;
                            _ActionTemplate.GetTemplate();
                            RecipeURL = RecipeURL.Substring(3);
                            string _notificationText = "<a href=\"" + RecipeURL + "\">" + _ActionTemplate.NotificationTemplate.Replace("{0}", _user.Name + ' ' + _user.Surname) + "</a>";
                            UserBoardNotification newNotification = new UserBoardNotification(_IDRecipeOwner, ActionTypes.LikeForNewRecipe, RecipeURL, _IDRecipe, null, _notificationText, _IDRecipeOwner);
                            newNotification.InsertNotification();
                        }
                    }
                    catch
                    { }
                }
                else
                {
                    _return = "error - " + _result.ResultExecutionCode;
                }
                
            }
            catch
            {
                _return = "error - RC-ER-0011";
            }
            
            return _return;
        }
        [WebMethod]
        public List<string> LikesDetailsForRecipe(string IDRecipe, string OffsetRows, string FetchRows)
        {
            List<string> _return = new List<string>();
            
            int _OffsetRows = MyConvert.ToInt32(OffsetRows, 0);
            int _FetchRows = MyConvert.ToInt32(FetchRows, 0);

            Guid _IDRecipe = new Guid(IDRecipe);
            DataTable dtRecipeLikes = RecipeFeedback.GetLikesForRecipe(_IDRecipe, _OffsetRows, _FetchRows);
            MyUser _user = null;

            if (dtRecipeLikes.Rows.Count > 0)
            {
                MyUserConfigParameter _elementHTML = new MyUserConfigParameter("UserListNoImageHTML");

                foreach (DataRow _recipeLike in dtRecipeLikes.Rows)
                {
                    try
                    {
                        if (HttpRuntime.Cache[_recipeLike.Field<Guid>("IDUser").ToString()] != null)
                        {
                            _user = (MyUser)HttpRuntime.Cache[_recipeLike.Field<Guid>("IDUser").ToString()];
                        }
                        else
                        {
                            _user = new MyUser(_recipeLike.Field<Guid>("IDUser"));
                            _user.GetUserBasicInfoByID();
                            HttpRuntime.Cache.Insert(_recipeLike.Field<Guid>("IDUser").ToString(), _user, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
                        }
                        string userLink = ("/" + _user.UserName + "/").ToLower();
                        _return.Add(_elementHTML.ConfigurationValue.Replace("{UserLink}", userLink.ToLower()).Replace("{UserName}", _user.UserName).Replace("{UserCompleteName}", _user.Name + " " + _user.Surname));
                    }
                    catch
                    { }
                }
            }
            return _return;
        }

        [WebMethod]
        public string AddComment(string IDRecipe, string IDUser, string IDRecipeOwner, string IDLanguage, string Username, string RecipeURL, string Text)
        {
            string _textResult = "";
            int _IDLanguage = MyConvert.ToInt32(IDLanguage, 1);
            string _return = "error - RC-ER-0012";
            try
            {
                Guid _IDRecipe = new Guid(IDRecipe);
                Guid _IDUser = new Guid(IDUser);
                RecipeFeedback _recipeComment = new RecipeFeedback(_IDRecipe, _IDUser, RecipeFeedbackType.Comment, Text);
                ManageUSPReturnValue _result = _recipeComment.Save();
                if (!_result.IsError)
                {
                    _return = "200 OK " + _textResult;
                    if (IDUser != IDRecipeOwner)
                    {
                        try
                        {
                            MyUser _user = null;
                            if (HttpRuntime.Cache[IDUser] != null)
                            {
                                _user = (MyUser)HttpRuntime.Cache[IDUser];
                            }
                            else
                            {
                                _user = new MyUser(_IDUser);
                                _user.GetUserBasicInfoByID();
                                HttpRuntime.Cache.Insert(IDUser, _user, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
                            }
                            Guid _IDRecipeOwner = new Guid(IDRecipeOwner);
                            UserBoard _ActionTemplate = new UserBoard(4, null);
                            _ActionTemplate.IDLanguage = _IDLanguage;
                            _ActionTemplate.GetTemplate();
                            RecipeURL = RecipeURL.Substring(3);
                            string _notificationText = "<a href=\"" + RecipeURL + "\">" + _ActionTemplate.NotificationTemplate.Replace("{0}", _user.Name + ' ' + _user.Surname) + "</a>";
                            UserBoardNotification newNotification = new UserBoardNotification(_IDRecipeOwner, ActionTypes.LikeForNewRecipe, RecipeURL, _IDRecipe, null, _notificationText, _IDRecipeOwner);
                            newNotification.InsertNotification();
                        }
                        catch
                        { }
                    }
                }
                else
                {
                    _return = "error - " + _result.ResultExecutionCode;
                }

            }
            catch
            {
                _return = "error - RC-ER-0012";
            }

            return _return;
        }
        
        [WebMethod]
        public string DeleteComment(string IDRecipeFeedback, string IDRecipe, string IDUser)
        {
            string _textResult = "";
            string _return = "error - RC-ER-0012";
            try
            {
                Guid _IDRecipeFeedback = new Guid(IDRecipeFeedback);
                Guid _IDRecipe = new Guid(IDRecipe);
                Guid _IDUser = new Guid(IDUser);
                RecipeFeedback _recipeComment = new RecipeFeedback(_IDRecipeFeedback);
                _recipeComment.QueryFeedback();
                if(_recipeComment.User.IDUser == _IDUser)
                {
                    ManageUSPReturnValue _result = _recipeComment.Delete();
                    if (!_result.IsError)
                    {
                        _return = "200 OK " + _textResult;
                    }
                    else
                    {
                        _return = "error - " + _result.ResultExecutionCode;
                    }
                }
            }
            catch
            {
                _return = "error - RC-ER-0012";
            }

            return _return;
        }

        [WebMethod]
        public List<string> ListCommentsForRecipe(string IDRecipe, string OffsetRows, string FetchRows, string IDUserRequester)
        {
            List<string> _return = new List<string>();

            int _OffsetRows = MyConvert.ToInt32(OffsetRows, 0);
            int _FetchRows = MyConvert.ToInt32(FetchRows, 0);

            Guid _IDRecipe = new Guid(IDRecipe);
            DataTable dtRecipeComments = RecipeFeedback.GetCommentsForRecipe(_IDRecipe, _OffsetRows, _FetchRows);
            MyUser _user = null;

            if (dtRecipeComments.Rows.Count > 0)
            {
                DBRecipesConfigParameter _elementHTML = new DBRecipesConfigParameter(6);
            
                foreach (DataRow _recipeComment in dtRecipeComments.Rows)
                {
                    try
                    {
                        if (HttpRuntime.Cache[_recipeComment.Field<Guid>("IDUser").ToString()] != null)
                        {
                            _user = (MyUser)HttpRuntime.Cache[_recipeComment.Field<Guid>("IDUser").ToString()];
                        }
                        else
                        {
                            _user = new MyUser(_recipeComment.Field<Guid>("IDUser"));
                            _user.GetUserBasicInfoByID();
                            HttpRuntime.Cache.Insert(_recipeComment.Field<Guid>("IDUser").ToString(), _user, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
                        }
                        string _userPhoto = "";
                        #region GetPhoto
                        try
                        {
                            if (_user.IDProfilePhoto.IDMedia != null)
                            {
                                _user.IDProfilePhoto.QueryMediaInfo();
                                try
                                {
                                    _userPhoto = _user.IDProfilePhoto.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                                }
                                catch
                                {
                                }
                                if (_userPhoto == "")
                                {
                                    _userPhoto = _user.IDProfilePhoto.GetCompletePath(false, false, true);
                                }

                                if (_userPhoto == "")
                                {
                                    _userPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.ProfileImagePhoto);
                                }
                            }
                            else
                            {
                                _userPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.ProfileImagePhoto);
                            }
                        }
                        catch
                        {
                            _userPhoto = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.ProfileImagePhoto);
                        }
                        #endregion
                        string _dateFormat = "dd/MM/yyyy, HH:mm:ss";
                        try
                        {
                            _dateFormat = AppConfig.GetValue("DateTimeFormatCSharp", AppDomain.CurrentDomain);
                        }
                        catch
                        { }
                        string _dateComment = MyConvert.ToLocalTime(_recipeComment.Field<DateTime>("FeedbackDate"), _user.Offset).ToString(_dateFormat);
                        string userLink = ("/" + _user.UserName + "/").ToLower();
                        if (IDUserRequester == _recipeComment.Field<Guid>("IDUser").ToString())
                        {
                            _return.Add(_elementHTML.DBRecipeConfigParameterValue.Replace("{IDBox}", _recipeComment.Field<Guid>("IDRecipeFeedback").ToString()).Replace("{imgDelete}", "imgDelete").Replace("{IDComment}", _recipeComment.Field<Guid>("IDRecipeFeedback").ToString()).Replace("{ImagePath}", _userPhoto).Replace("{UserName}", _user.UserName).Replace("{UserLink}", userLink.ToLower()).Replace("{UserCompleteName}", _user.Name + " " + _user.Surname).Replace("{DateTimeComment}", _dateComment).Replace("{CommentText}", _recipeComment.Field<string>("FeedbackText").Replace("\n", "<br />")));
                        }
                        else
                        {
                            _return.Add(_elementHTML.DBRecipeConfigParameterValue.Replace("{IDBox}", "").Replace("{imgDelete}", "imgDeleteNo").Replace("{IDComment}", "").Replace("{ImagePath}", _userPhoto).Replace("{UserName}", _user.UserName).Replace("{UserLink}", userLink.ToLower()).Replace("{UserCompleteName}", _user.Name + " " + _user.Surname).Replace("{DateTimeComment}", _dateComment).Replace("{CommentText}", _recipeComment.Field<string>("FeedbackText").Replace("\n", "<br />")));
                        }
                    }
                    catch
                    { }
                }
            }
            return _return;
        }
    }
}
