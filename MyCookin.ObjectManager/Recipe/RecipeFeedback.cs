using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Web;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.UserManager;
using MyCookin.DAL.Recipe.ds_RecipeTableAdapters;
using MyCookin.Common;
using MyCookin.ObjectManager;

namespace MyCookin.ObjectManager.RecipeManager
{
    public enum RecipeFeedbackType : int
    {
        Like = 1,
        Comment = 2
    }
    public class RecipeFeedback
    {
        #region PrivateFields

        private Guid _IDRecipeFeedback;
        private Recipe _Recipe;
        private MyUser _User;
        private RecipeFeedbackType _FeedbackType;
        private string _FeedbackText;
        private DateTime _FeedbackDate;

        #endregion

        #region PublicProterties

        public Guid IDRecipeFeedback
        {
            get { return _IDRecipeFeedback; }
        }
        public Recipe Recipe
        {
            get { return _Recipe; }
        }
        public MyUser User
        {
            get { return _User; }
        }
        public RecipeFeedbackType FeedbackType
        {
            get { return _FeedbackType; }
        }
        public string FeedbackText
        {
            get { return _FeedbackText; }
            set { _FeedbackText = value; }
        }
        public DateTime FeedbackDate
        {
            get { return _FeedbackDate; }
            set { _FeedbackDate = value; }
        }

        #endregion

        #region Costructors

        public RecipeFeedback()
        {
            
        }
        /// <summary>
        /// Get a specific RecipeFeedback, for read or delete operation
        /// </summary>
        /// <param name="IDRecipeFeedback">ID of feedback to retrive</param>
        public RecipeFeedback(Guid IDRecipeFeedback)
        {
            _IDRecipeFeedback = IDRecipeFeedback;
        }
        /// <summary>
        /// Create a new feedback for a recipe
        /// </summary>
        /// <param name="IDRecipe"></param>
        /// <param name="IDUser"></param>
        /// <param name="FeedbackType"></param>
        /// <param name="FeedbackText"></param>
        public RecipeFeedback(Guid IDRecipe, Guid IDUser, RecipeFeedbackType FeedbackType, string FeedbackText)
        {
            _IDRecipeFeedback = Guid.NewGuid();
            _Recipe = IDRecipe;
            _User = IDUser;
            _FeedbackType = FeedbackType;
            _FeedbackText = FeedbackText;
        }


        #endregion

        #region Methods
        /// <summary>
        /// Query Feedback by IDFeedback
        /// </summary>
        public void QueryFeedback()
        {
            GetRecipesFeedbacksDAL recipeFeedbackDAL = new GetRecipesFeedbacksDAL();
            DataTable dtRecipeFeedback = recipeFeedbackDAL.GetFeedbackByID(_IDRecipeFeedback);
            if (dtRecipeFeedback.Rows.Count > 0)
            {
                _Recipe = dtRecipeFeedback.Rows[0].Field<Guid>("IDRecipe");
                _User = dtRecipeFeedback.Rows[0].Field<Guid>("IDUser");
                _FeedbackType = (RecipeFeedbackType)dtRecipeFeedback.Rows[0].Field<int>("IDFeedbackType");
                _FeedbackText = dtRecipeFeedback.Rows[0].Field<string>("FeedbackText");
                _FeedbackDate = dtRecipeFeedback.Rows[0].Field<DateTime>("FeedbackDate");
            }
        }
        /// <summary>
        /// Query Feedback for an user
        /// </summary>
        /// <param name="IDRecipe">Recipe viewed</param>
        /// <param name="IDUser">User</param>
        public void QueryFeedback(Guid IDRecipe, Guid IDUser)
        {
            GetRecipesFeedbacksDAL recipeFeedbackDAL = new GetRecipesFeedbacksDAL();
            DataTable dtRecipeFeedback = recipeFeedbackDAL.USP_GetRecipeLikeForUser(IDRecipe, IDUser);
            if (dtRecipeFeedback.Rows.Count > 0)
            {
                _IDRecipeFeedback = dtRecipeFeedback.Rows[0].Field<Guid>("IDRecipeFeedback");
                _Recipe = dtRecipeFeedback.Rows[0].Field<Guid>("IDRecipe");
                _User = dtRecipeFeedback.Rows[0].Field<Guid>("IDUser");
                _FeedbackType = (RecipeFeedbackType)dtRecipeFeedback.Rows[0].Field<int>("IDFeedbackType");
                _FeedbackText = dtRecipeFeedback.Rows[0].Field<string>("FeedbackText");
                _FeedbackDate = dtRecipeFeedback.Rows[0].Field<DateTime>("FeedbackDate");
            }
        }
        public ManageUSPReturnValue Save()
        {
            ManageRecipesDAL _manageDAL = new ManageRecipesDAL();
            DataTable _dtResult = _manageDAL.USP_ManageRecipeFeedback(_IDRecipeFeedback, _Recipe, _User, (int)_FeedbackType, _FeedbackText, DateTime.UtcNow,false);
            ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtResult);

            return _result;
        }

        public ManageUSPReturnValue Delete()
        {
            ManageRecipesDAL _manageDAL = new ManageRecipesDAL();
            DataTable _dtResult = _manageDAL.USP_ManageRecipeFeedback(_IDRecipeFeedback, null, null, (int)RecipeFeedbackType.Comment, null, null, true);
            ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtResult);

            return _result;
        }

        public static DataTable GetLikesForRecipe(Guid IDRecipe,int RowOffset, int FetchRows)
        {
            GetRecipesFeedbacksDAL recipeFeedbackDAL = new GetRecipesFeedbacksDAL();
            return recipeFeedbackDAL.USP_GetRecipeFeedbacks(IDRecipe, 1, RowOffset, FetchRows);
        }

        public static DataTable GetCommentsForRecipe(Guid IDRecipe, int RowOffset, int FetchRows)
        {
            GetRecipesFeedbacksDAL recipeFeedbackDAL = new GetRecipesFeedbacksDAL();
            return recipeFeedbackDAL.USP_GetRecipeFeedbacks(IDRecipe, 2, RowOffset, FetchRows);
        }
        #endregion
    }
}
