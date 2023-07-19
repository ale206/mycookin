using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using System.Dynamic;
using System.Net;
using System.IO;
using AuthPack;
using System.Web;
using MyCookin.Log;
using MyCookin.Common;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.DAL.UserBoard.UserBoardTableAdapters;
using System.Configuration;
using MyCookin.ObjectManager.UserManager;
using Twitterizer;
using MyCookin.ObjectManager.StatisticsManager;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ErrorAndMessage;


namespace MyCookin.ObjectManager.SocialAction
{
    public enum SocialNetwork : int
    {
        Google = 1,
        Facebook = 2,
        Twitter = 3
    }

    /// <summary>
    /// For Json Object with id of the post on fb.
    /// </summary>
    public class FacebookPost
    {
        public string id;
    }

    public class SocialAction
    {
        #region Privatefields
        private string _IDUserSocial;
        private string _AccessToken;
        private string _AccessTokenSecret;
        private string _Message;
        private string _LinkURL;
        private string _PictureURL;
        private string _SourcePathURL;
        private string _Title;
        private string _Subtitle;
        private string _Description;
        private string _Icon;
        private string _Actions;
        private string _Privacy;

        private string _IDPostOnWall;
        private string _ImagePath;

        private Guid? _IDActionRelatedObject;
        private ActionTypes _IDUserActionType;
        private Guid? _IDUserAction;
        #endregion

        #region PublicFields
        public string IDUserSocial
        {
        get { return _IDUserSocial;}
        set { _IDUserSocial = value;}
        }
        public string AccessToken
        {
        get { return _AccessToken;}
        set { _AccessToken = value;}
        }
        public string AccessTokenSecret
        {
            get { return _AccessTokenSecret; }
            set { _AccessTokenSecret = value; }
        }
        public string Message
        {
        get { return _Message;}
        set { _Message = value;}
        }
        public string LinkURL
        {
        get { return _LinkURL;}
        set { _LinkURL = value;}
        }
        public string PictureURL
        {
        get { return _PictureURL;}
        set { _PictureURL = value;}
        }
        public string SourcePathURL
        {
        get { return _SourcePathURL;}
        set { _SourcePathURL = value;}
        }
        public string Title
        {
        get { return _Title;}
        set { _Title = value;}
        }
        public string Subtitle
        {
        get { return _Subtitle;}
        set { _Subtitle = value;}
        }
        public string Description
        {
        get { return _Description;}
        set { _Description = value;}
        }
        public string Icon
        {
        get { return _Icon;}
        set { _Icon = value;}
        }
        public string Actions
        {
        get { return _Actions;}
        set { _Actions = value;}
        }
        public string Privacy
        {
        get { return _Privacy;}
        set { _Privacy = value;}
        }
        public string IDPostOnWall
        {
            get { return _IDPostOnWall; }
            set { _IDPostOnWall = value; }
        }
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public Guid? IDActionRelatedObject
        {
            get { return _IDActionRelatedObject; }
            set { _IDActionRelatedObject = value; }
        }
        public ActionTypes IDUserActionType
        {
            get { return _IDUserActionType; }
            set { _IDUserActionType = value; }
        }
        public Guid? IDUserAction
        {
            get { return _IDUserAction; }
            set { _IDUserAction = value; }
        }
        #endregion

        #region Constructors
        public SocialAction()
        { 
        }

        public SocialAction(string IDUserSocial, string AccessToken, string AccessTokenSecret, Guid? IDActionRelatedObject, ActionTypes IDUserActionType, Guid? IDUserAction)
        {
            _IDUserSocial = IDUserSocial;
            _AccessToken = AccessToken;
            _AccessTokenSecret = AccessTokenSecret;
            _IDActionRelatedObject = IDActionRelatedObject;
            _IDUserActionType = IDUserActionType;
            _IDUserAction = IDUserAction;
        }

        /// <summary>
        /// Constructor to insert a Like on FB
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="IDPostOnWall"></param>
        public SocialAction(string AccessToken, string IDPostOnWall)
        {
            _AccessToken = AccessToken;
            _IDPostOnWall = IDPostOnWall;
        }

        /// <summary>
        /// Constructor to Post Picture
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="IDUserSocial"></param>
        /// <param name="ImagePath"></param>
        public SocialAction(string AccessToken, string IDUserSocial, string ImagePath)
        {
            _AccessToken = AccessToken;
            _IDUserSocial = IDUserSocial;
            _ImagePath = ImagePath;
        }
        #endregion

        #region Methods

        #region TemplateForSharing
        /// <summary>
        /// Instantiate Variables According to Related Object to share on Social Network.
        /// After this you can Share with FB_PostOnWall()
        /// </summary>
        protected void TemplateForSharing()
        {
            //_IDActionRelatedObject = IDActionRelatedObject;

            switch (_IDUserActionType)
            { 
                case ActionTypes.UserProfileUpdated:    //TEST CON IMMAGINE
                    //_IDActionRelatedObject is null

                    _Message = "Ho aggiornato il mio profilo su MyCookin";

                    //Link al profilo utente
                    _LinkURL = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + "/" + HttpContext.Current.Session["Username"].ToString();
                    _PictureURL = "http://media.npr.org/assets/img/2011/09/28/93434191-einstein-tongue_custom-36fb0ce35776dc2d92eda90880022bf48a67e192-s6-c10.jpg";
                    //_SourcePathURL = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + AppConfig.GetValue("RoutingUser", AppDomain.CurrentDomain) + HttpContext.Current.Session["Username"].ToString();
                    _Title = "Titolo";
                    _Subtitle = "Sottotitolo";
                    _Description = "Descrizione";
                    break;
                
                case ActionTypes.NewRecipe:

                    int IDLanguage = MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1);

                    //Get Recipe Info according to _IDActionRelatedObject
                    RecipeLanguage RecipeObj = new RecipeLanguage((Guid)IDActionRelatedObject, IDLanguage);
                    RecipeObj.QueryRecipeLanguageInfo();
                    RecipeObj.QueryBaseRecipeInfo();
                    RecipeObj.GetRecipeSteps();

                    string ImageUrl = "";
                    try
                    {
                        ImageUrl = RecipeObj.RecipeImage.GetCompletePath(false, false, true);
                    }
                    catch
                    {
                        //No image! Set here one of default.
                    }

                    MyCulture CultureInfo = new MyCulture(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                    string LanguageCode = CultureInfo.GetCurrentLanguageCodeByID();

                    _Message = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0060");
                    //_LinkURL = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + AppConfig.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + RecipeObj.RecipeName + "/" + RecipeObj.IDRecipe.ToString();;        //Recipe Url on MyCookin
                    _LinkURL = (AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + "/" + LanguageCode + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + RecipeObj.RecipeName.Replace(" ","-") + "/" + RecipeObj.IDRecipe.ToString()).ToLower();        //Recipe Url on MyCookin

                    if (RecipeObj.RecipeImage.MediaOnCDN)
                    {
                        _PictureURL = ImageUrl;       //Recipe Picture
                    }
                    else
                    {
                        _PictureURL = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + ImageUrl;       //Recipe Picture
                    }

                    //_SourcePathURL = "";    //Recipe Url on MyCookin
                    _Title = RecipeObj.RecipeName;            //RecipeName
                    _Subtitle = "";         //Ingredients?
                    _Description = RecipeObj.RecipeSteps[0].Step;      //Recipe Preparation
                    //_Icon = "";
                    //_Actions = "";
                    //_Privacy = ""; 
                    break;
                //case ActionTypes.PersonalMessage:
                    //_IDActionRelatedObject null
                    //_Message
                    //break;
                //case ActionTypes.FotoUploaded:
                //    _Message = "Nuova foto";
                //    break;
                //case ActionTypes.NewIngredient:
                //    _Message = "Ho aggiunto un nuovo ingrediente";
                //    break;
            }
        }
        #endregion

        #region FB_PostOnWall
        public string FB_PostOnWall()
        {
            //Create Template
            TemplateForSharing();

            string IDPost = String.Empty;

            try
            {
                //Guida
                //http://csharpsdk.org/
                //https://developers.facebook.com/docs/reference/api/post/

                //string Actions = "{\"name\": \"View on Rate-It\", \"link\": \"http://www.rate-it.co.nz\"}";
                //string Privacy = "{\"value\": \"EVERYONE\"}";

                //New FacebookClient
                FacebookClient fbClient = new FacebookClient(_AccessToken);
                var args = new Dictionary<string, object>();

                //Set argoments if exist.
                if (!String.IsNullOrEmpty(_Message)) { args["message"] = _Message; }
                if (!String.IsNullOrEmpty(_LinkURL)) { args["link"] = _LinkURL; }
                if (!String.IsNullOrEmpty(_SourcePathURL)) { args["source"] = _SourcePathURL; }
                if (!String.IsNullOrEmpty(_Title)) { args["name"] = _Title; }
                if (!String.IsNullOrEmpty(_Subtitle)) { args["caption"] = _Subtitle; }
                if (!String.IsNullOrEmpty(_Description)) { args["description"] = _Description; }
                if (!String.IsNullOrEmpty(_Icon)) { args["icon"] = _Icon; }
                if (!String.IsNullOrEmpty(_PictureURL)) { args["picture"] = _PictureURL; }
                if (!String.IsNullOrEmpty(_Actions)) { args["Actions"] = _Actions; }
                if (!String.IsNullOrEmpty(_Privacy)) { args["Privacy"] = _Privacy; }

                //Post Action
                var Result = fbClient.Post(string.Format("{0}/feed", _IDUserSocial), args);

                //Return a JSON Object with parameter "id" that contain userid_postid
                string ResultPost = Result.ToString();


                //Deserialize to obtain the id

                FacebookPost FBPost = Json.Deserialise<FacebookPost>(ResultPost);
                string IDUserAndIDPost = FBPost.id;

                string[] words = IDUserAndIDPost.Split('_');
                string IDUser = words[0];

                IDPost = words[1];

                //Memorizza questo id
                if (InsertActionShared(IDPost, SocialNetwork.Facebook))
                {
                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatisticUser = new MyStatistics(new Guid(HttpContext.Current.Session["IDUser"].ToString()), null, StatisticsActionType.SC_ActionSharedOnFacebook, "Action Shared on Facebook", Network.GetCurrentPageName(), "", "");
                        NewStatisticUser.InsertNewRow();
                    }
                    catch { }
                }
                else
                {

                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0012", _IDActionRelatedObject.ToString(), HttpContext.Current.Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                    }
                    catch { }
                }


            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0012", "Can't share on Fb wall the Action " + _IDActionRelatedObject.ToString() + " - " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return IDPost;
        }
        #endregion

        #region FB_Like
        public bool FB_Like()
        {
            bool ResultLike = false;

            try
            {
                FacebookClient fbClient = new FacebookClient(_AccessToken);
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                var Result = fbClient.Post(string.Format("{0}/likes", _IDPostOnWall), parameters);

                ResultLike = Convert.ToBoolean(Result);
            }
            catch (Exception ex)
            {
                ResultLike = false;

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Can't set like on Fb wall object. " + ex.Message, "IDUserSocial: " + _IDUserSocial, true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }

            }

            return ResultLike;
        }
        #endregion

        #region FB_PostPicture
        public bool FB_PostPicture()
        {
            try
            {
                FacebookClient fb = new FacebookClient(_AccessToken);

                var imgstream = File.OpenRead(_ImagePath);

                dynamic res = fb.Post("/"+ _IDUserSocial +"/photos", new
                {

                    message = "",
                    file = new FacebookMediaStream
                    {
                        ContentType = "image/jpg",
                        FileName = Path.GetFileName(_ImagePath)

                    }.SetValue(imgstream)
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region InsertActionShared
        public bool InsertActionShared(string IDShareOnSocial, SocialNetwork IDSocialNetwork)
        {
            bool ExecuteResult = false;

            try
            {
                Guid IDUserGuid = new Guid(HttpContext.Current.Session["IDUser"].ToString());

                Guid newGuid = Guid.NewGuid();

                UserActionsSharedTableAdapter TA_ActionShared = new UserActionsSharedTableAdapter();

                TA_ActionShared.InsertActionShared(newGuid, (Guid)_IDUserAction, IDUserGuid, (int)IDSocialNetwork, DateTime.UtcNow, IDShareOnSocial);

                ExecuteResult = true;
            }
            catch
            { 
            }

            return ExecuteResult;
        }
        #endregion

        #region CheckIfShared
        public static bool CheckIfShared(Guid IDUserAction, Guid IDUser, SocialNetwork IDSocialNetwork)
        {
            bool IsShared = false;

            int? shared = 0;

            try
            {
                UserActionsSharedTableAdapter TA_ActionShared = new UserActionsSharedTableAdapter();

                shared = TA_ActionShared.CheckIfShared(IDUserAction, IDUser, (int)IDSocialNetwork);

                if (shared > 0)
                {
                    IsShared = true;
                }
            }
            catch
            {
            }

            return IsShared;
        }
        #endregion

        #region CheckIfSharedOnPersonalBoard
        public static bool CheckIfSharedOnPersonalBoard(Guid IDActionRelatedObject, Guid IDUser, ActionTypes ActionType)
        {
            bool IsShared = false;

            int? shared = 0;

            try
            {
                UsersActionsTableAdapter TA_ActionShared = new UsersActionsTableAdapter();

                shared = TA_ActionShared.CheckIfSharedOnPersonalBoard(IDActionRelatedObject, (int)ActionType, IDUser);

                if (shared > 0)
                {
                    IsShared = true;
                }
            }
            catch
            {
            }

            return IsShared;
        }
        #endregion

        public void TW_tweet()
        {
            //Create Template
            TemplateForSharing();

            OAuthTokens tokens = new OAuthTokens();
            tokens.AccessToken = _AccessToken;                      //Of The User
            tokens.AccessTokenSecret = _AccessTokenSecret;          //Of the User
            tokens.ConsumerKey = ConfigurationManager.AppSettings["twitter_consumer_key"];
            tokens.ConsumerSecret = ConfigurationManager.AppSettings["twitter_consumer_secret"];

            string CompleteMessage = "#MyCookin " + _LinkURL + " #" + _Title;
            //string CompleteMessage = "#MyCookin tweet di prova.";

            if (CompleteMessage.Length > 137)
            {
                CompleteMessage = CompleteMessage.Substring(0, 137) + "...";
            }

            TwitterResponse<TwitterStatus> tweetResponse = TwitterStatus.Update(tokens, CompleteMessage);
            if (tweetResponse.Result == RequestResult.Success)
            {
                // Tweet posted successfully!

                //We have return to memorize on db??
                string IDPost = "";


                if (InsertActionShared(IDPost, SocialNetwork.Twitter))
                {
                    //WRITE A ROW IN STATISTICS DB
                    try
                    {
                        MyStatistics NewStatisticUser = new MyStatistics(new Guid(HttpContext.Current.Session["IDUser"].ToString()), null, StatisticsActionType.SC_ActionSharedOnTwitter, "Action Shared on Twitter", Network.GetCurrentPageName(), "", "");
                        NewStatisticUser.InsertNewRow();
                    }
                    catch { }
                }
                else
                {
                    //WRITE A ROW IN LOG FILE AND DB
                    try
                    {
                        LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0012", _IDActionRelatedObject.ToString(), HttpContext.Current.Session["IDUser"].ToString(), true, false);
                        LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                        LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                    }
                    catch { }
                }
            }
            else
            {
                // Something bad happened

                if (tweetResponse.Result == RequestResult.Unauthorized)
                {
                    string url = "/auth/auth.aspx?twitterauth=true";
                    HttpContext.Current.Response.Redirect(url, true);
                }
            }
        }
           
        #endregion

    }
}
