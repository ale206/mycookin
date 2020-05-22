using System;
using System.Collections.Generic;
using System.IO;
using Facebook;
using Newtonsoft.Json;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Media;
using TaechIdeas.Core.Core.Social;
using TaechIdeas.Core.Core.Social.Dto;
using TaechIdeas.Core.Core.Statistic;
using TaechIdeas.Core.Core.Statistic.Dto;

namespace TaechIdeas.Core.BusinessLogic.Social
{
    public class SocialActionManager : ISocialActionManager
    {
        private readonly IRetrieveMessageManager _retrieveMessageManager;
        private readonly IMediaManager _mediaManager;
        private readonly IStatisticManager _statisticManager;
        private readonly INetworkConfig _networkConfig;
        private readonly IMyConvertManager _myConvertManager;
        private readonly IMyCultureManager _myCultureManager;

        public SocialActionManager(IRetrieveMessageManager retrieveMessageManager, IMediaManager mediaManager,
            IStatisticManager statisticManager, IMyConvertManager myConvertManager, INetworkConfig networkConfig, IMyCultureManager myCultureManager)
        {
            _retrieveMessageManager = retrieveMessageManager;
            _mediaManager = mediaManager;
            _statisticManager = statisticManager;
            _myConvertManager = myConvertManager;
            _networkConfig = networkConfig;
            _myCultureManager = myCultureManager;
        }

        #region TemplateForSharing

        //TODO: MOVE TO MYCOOKIN
        /// <summary>
        ///     Instantiate Variables According to Related Object to share on Social Network.
        ///     After this you can Share with FB_PostOnWall()
        /// </summary>
        /// <param name="idUserActionType"></param>
        /// <param name="idActionRelatedObject"></param>
        //public SocialAction TemplateForSharing(ActionTypes idUserActionType, Guid idActionRelatedObject)
        //{
        //    var socialAction = new SocialAction();

        //    //_IDActionRelatedObject = IDActionRelatedObject;

        //    switch (idUserActionType)
        //    {
        //        case ActionTypes.UserProfileUpdated: //TEST CON IMMAGINE
        //            //_IDActionRelatedObject is null

        //            socialAction.Message = "Ho aggiornato il mio profilo su MyCookin"; //TODO: Put in configuration

        //            //Link al profilo utente
        //            //socialAction.LinkUrl = Path.Combine(_networkConfig.WebUrl, HttpContext.Current.Session["Username"].ToString());
        //            socialAction.PictureUrl =
        //                "http://media.npr.org/assets/img/2011/09/28/93434191-einstein-tongue_custom-36fb0ce35776dc2d92eda90880022bf48a67e192-s6-c10.jpg";
        //            //_SourcePathURL = _appConfigManager.GetValue("WebUrl", AppDomain.CurrentDomain) + _appConfigManager.GetValue("RoutingUser", AppDomain.CurrentDomain) + HttpContext.Current.Session["Username"].ToString();
        //            socialAction.Title = "Titolo";
        //            socialAction.Subtitle = "Sottotitolo";
        //            socialAction.Description = "Descrizione";
        //            break;

        //        case ActionTypes.NewRecipe:

        //            //var idLanguage = _myConvertManager.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1);

        //            //Get Recipe Info according to _IDActionRelatedObject
        //            //var RecipeObj = new RecipeByIdAndLanguageOutput((Guid)IDActionRelatedObject, IDLanguage);

        //            var recipeByLanguageInput = new RecipeByIdAndLanguageInput()
        //            {
        //                //LanguageId = idLanguage,
        //                RecipeId = (Guid) idActionRelatedObject
        //            };

        //            //TODO: UseByFriendlyId
        //            // var recipeObj = _recipeManager.RecipeByIdAndLanguage(recipeByLanguageInput);
        //            var recipeObj = new RecipeByIdAndLanguageOutput();

        //            //RecipeObj.RecipeByIdAndLanguage();
        //            //RecipeObj.QueryBaseRecipeInfo();
        //            //RecipeObj.StepsByIdRecipeAndLanguage();

        //            var imageUrl = "";
        //            MediaByIdOutput image = null;

        //            try
        //            {
        //                //var imageId = recipeObj.IdRecipeImage;
        //                //Media.Media image = null;
        //                //if (imageId != null)
        //                //{
        //                //    image = _mediaManager.MediaById(new MediaByIdInput() {MediaId = (Guid) imageId});
        //                //}
        //                ////ImageUrl = _mediaManager.GetCompletePath(false, false, true,) RecipeObj.RecipeImage.GetCompletePath(false, false, true);
        //                //imageUrl = image.MediaPath;
        //            }
        //            catch
        //            {
        //                //No image! Set here one of default.
        //            }

        //            //var languageCode = _myCultureManager.LanguageCodeByLanguageId(_myConvertManager.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));

        //            //socialAction.Message = _retrieveMessageManager.RetrieveDbMessage(idLanguage, "US-IN-0060");
        //            //_LinkURL = _appConfigManager.GetValue("WebUrl", AppDomain.CurrentDomain) + _appConfigManager.GetValue("RoutingRecipe" + IDLanguage, AppDomain.CurrentDomain) + RecipeObj.RecipeName + "/" + RecipeObj.IDRecipe.ToString();;        //Recipe Url on MyCookin

        //            string routingRecipe;
        //            //TODO: Create method
        //            //switch (idLanguage)
        //            //{
        //            //    case 1:
        //            //        routingRecipe = _networkConfig.RoutingRecipeEn;
        //            //        break;
        //            //    case 2:
        //            //        routingRecipe = _networkConfig.RoutingRecipeIt;
        //            //        break;
        //            //    case 3:
        //            //        routingRecipe = _networkConfig.RoutingRecipeEs;
        //            //        break;
        //            //    default:
        //            //        routingRecipe = _networkConfig.RoutingRecipeEn;
        //            //        break;
        //            //}

        //            //TODO: Remove this Guid at the end
        //            //socialAction.LinkUrl = Path.Combine(_networkConfig.WebUrl, languageCode, routingRecipe, recipeObj.RecipeName.Replace(" ", "-"), recipeObj.RecipeId.ToString());
        //            //(_appConfigManager.GetValue("WebUrl", AppDomain.CurrentDomain) + "/" + languageCode +
        //            // _appConfigManager.GetValue("RoutingRecipe" + idLanguage, AppDomain.CurrentDomain) +
        //            // recipeObj.RecipeName.Replace(" ", "-") + "/" + recipeObj.RecipeId).ToLower();
        //            //Recipe Url on MyCookin

        //            if (image != null && image.MediaOnCdn)
        //            {
        //                socialAction.PictureUrl = imageUrl; //Recipe Picture
        //            }
        //            else
        //            {
        //                socialAction.PictureUrl = Path.Combine(_networkConfig.WebUrl, imageUrl); //Recipe Picture
        //            }

        //            //_SourcePathURL = "";    //Recipe Url on MyCookin
        //            socialAction.Title = recipeObj.RecipeName; //RecipeName
        //            socialAction.Subtitle = ""; //Ingredients?
        //            //socialAction.Description = recipeObj.StepsForRecipeOutput.First().RecipeStep; //Recipe Preparation
        //            //_Icon = "";
        //            //_Actions = "";
        //            //_Privacy = ""; 
        //            break;
        //        //case ActionTypes.PersonalMessage:
        //        //_IDActionRelatedObject null
        //        //_Message
        //        //break;
        //        //case ActionTypes.FotoUploaded:
        //        //    _Message = "Nuova foto";
        //        //    break;
        //        //case ActionTypes.NewIngredient:
        //        //    _Message = "Ho aggiunto un nuovo ingrediente";
        //        //    break;
        //    }

        //    return socialAction;
        //}

        #endregion

        #region FB_PostOnWall

        public string FB_PostOnWall(SocialAction socialAction)
        {
            //Create Template
            //TemplateForSharing();

            var idPost = string.Empty;

            //    Guida
            //    http://csharpsdk.org/
            //https://developers.facebook.com/docs/reference/api/post/

            //var actions = "{\"name\": \"View on Rate-It\", \"link\": \"http://www.rate-it.co.nz\"}";
            //var privacy = "{\"value\": \"EVERYONE\"}";

            //New FacebookClient
            var fbClient = new FacebookClient(socialAction.AccessToken);
            var args = new Dictionary<string, object>();

            //Set argoments if exist.
            if (!string.IsNullOrEmpty(socialAction.Message))
            {
                args["message"] = socialAction.Message;
            }

            if (!string.IsNullOrEmpty(socialAction.LinkUrl))
            {
                args["link"] = socialAction.LinkUrl;
            }

            if (!string.IsNullOrEmpty(socialAction.SourcePathUrl))
            {
                args["source"] = socialAction.SourcePathUrl;
            }

            if (!string.IsNullOrEmpty(socialAction.Title))
            {
                args["name"] = socialAction.Title;
            }

            if (!string.IsNullOrEmpty(socialAction.Subtitle))
            {
                args["caption"] = socialAction.Subtitle;
            }

            if (!string.IsNullOrEmpty(socialAction.Description))
            {
                args["description"] = socialAction.Description;
            }

            if (!string.IsNullOrEmpty(socialAction.Icon))
            {
                args["icon"] = socialAction.Icon;
            }

            if (!string.IsNullOrEmpty(socialAction.PictureUrl))
            {
                args["picture"] = socialAction.PictureUrl;
            }

            if (!string.IsNullOrEmpty(socialAction.Actions))
            {
                args["Actions"] = socialAction.Actions;
            }

            if (!string.IsNullOrEmpty(socialAction.Privacy))
            {
                args["Privacy"] = socialAction.Privacy;
            }

            //Post Action
            var result = fbClient.PostTaskAsync($"{socialAction.IdUserSocial}/feed", args);

            //Return a JSON Object with parameter "id" that contain userid_postid
            var resultPost = result.ToString();

            //Deserialize to obtain the id

            //RootObject oRootObject = new RootObject();
            var fbPost = JsonConvert.DeserializeObject<FacebookPost>(resultPost);

            //var fbPost = ThirdPartAPI.SocialNetworks.Json.Deserialise<FacebookPost>(resultPost);
            var idUserAndIdPost = fbPost.Id;

            var words = idUserAndIdPost.Split('_');
            var idUser = words[0];

            idPost = words[1];

            //Memorizza questo id
            if (socialAction.IdUserAction != null && InsertActionShared(idPost, SocialNetwork.Facebook, (Guid) socialAction.IdUserAction))
            {
                //WRITE A ROW IN STATISTICS DB
                try
                {
                    var newStatistic = new NewStatisticInput
                    {
                        Comments = "Action Shared on Facebook",
                        FileOrigin = "",
                        IdRelatedObject = null,
                        //IdUser = new Guid(HttpContext.Current.Session["IDUser"].ToString()),  //TODO
                        OtherInfo = "",
                        SearchString = "",
                        StatisticsActionType = StatisticsActionType.SC_ActionSharedOnFacebook
                    };

                    _statisticManager.NewStatistic(newStatistic);
                }
                catch
                {
                }
            }

            return idPost;
        }

        #endregion

        #region FB_Like

        public bool FB_Like(SocialAction socialAction)
        {
            var resultLike = false;

            var fbClient = new FacebookClient(socialAction.AccessToken);
            var parameters = new Dictionary<string, object>();

            var result = fbClient.PostTaskAsync($"{socialAction.IdPostOnWall}/likes", parameters);

            resultLike = Convert.ToBoolean(result);

            return resultLike;
        }

        #endregion

        #region FB_PostPicture

        public bool FB_PostPicture(SocialAction socialAction)
        {
            try
            {
                var fb = new FacebookClient(socialAction.AccessToken);

                var imgstream = File.OpenRead(socialAction.ImagePath);

                dynamic res = fb.PostTaskAsync("/" + socialAction.IdUserSocial + "/photos", new
                {
                    message = "",
                    file = new FacebookMediaStream
                    {
                        ContentType = "image/jpg",
                        FileName = Path.GetFileName(socialAction.ImagePath)
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

        public bool InsertActionShared(string idShareOnSocial, SocialNetwork idSocialNetwork, Guid idUserAction)
        {
            var executeResult = false;

            try
            {
                //var idUserGuid = new Guid(HttpContext.Current.Session["IDUser"].ToString());

                var newGuid = Guid.NewGuid();

                //TODO RESTORE: now in _userboardmanager
                //var taActionShared = new UserActionsSharedTableAdapter();

                //taActionShared.InsertActionShared(newGuid, (Guid) idUserAction, idUserGuid, (int) idSocialNetwork,
                //    DateTime.UtcNow, idShareOnSocial);

                executeResult = true;
            }
            catch
            {
            }

            return executeResult;
        }

        #endregion

        #region CheckIfShared

        public bool CheckIfShared(Guid idUserAction, Guid idUser, SocialNetwork idSocialNetwork)
        {
            var isShared = false;

            try
            {
                //TODO RESTORE: now in _userboardmanager
                //var taActionShared = new UserActionsSharedTableAdapter();

                //var shared = taActionShared.CheckIfShared(idUserAction, idUser, (int) idSocialNetwork);

                //if (shared > 0)
                //{
                //    isShared = true;
                //}
            }
            catch
            {
            }

            return isShared;
        }

        #endregion

        #region CheckIfSharedOnPersonalBoard

        public bool CheckIfSharedOnPersonalBoard(Guid idActionRelatedObject, Guid idUser, ActionTypes actionType)
        {
            var isShared = false;

            try
            {
                //TODO RESTORE: now in _userboardmanager
                //var taActionShared = new UsersActionsTableAdapter();

                //var shared = taActionShared.CheckIfSharedOnPersonalBoard(idActionRelatedObject, (int) actionType, idUser);

                //if (shared > 0)
                //{
                //    isShared = true;
                //}
            }
            catch
            {
            }

            return isShared;
        }

        #endregion

        #region TW_tweet

        public void TW_tweet(SocialAction socialAction)
        {
            //TODO: RESTORE

            throw new NotImplementedException();
            //Create Template
            //if (socialAction.IdActionRelatedObject != null)
            //{
            //    TemplateForSharing(socialAction.IdUserActionType, (Guid) socialAction.IdActionRelatedObject);

            //    var tokens = new OAuthTokens
            //    {
            //        AccessToken = socialAction.AccessToken,
            //        AccessTokenSecret = socialAction.AccessTokenSecret,
            //        ConsumerKey = ConfigurationManager.AppSettings["twitter_consumer_key"],
            //        ConsumerSecret = ConfigurationManager.AppSettings["twitter_consumer_secret"]
            //    };
            //    //Of The User
            //    //Of the User

            //    //TODO: Set in Configuration
            //    var completeMessage = "#MyCookin " + socialAction.LinkUrl + " #" + socialAction.Title;
            //    //string CompleteMessage = "#MyCookin tweet di prova.";

            //    if (completeMessage.Length > 137)
            //    {
            //        completeMessage = completeMessage.Substring(0, 137) + "...";
            //    }

            //    var tweetResponse = TwitterStatus.Update(tokens, completeMessage);

            //    switch (tweetResponse.Result)
            //    {
            //        case RequestResult.Success:
            //            // Tweet posted successfully!

            //            //We have return to memorize on db??
            //            const string idPost = "";

            //            if (socialAction.IdUserAction != null && InsertActionShared(idPost, SocialNetwork.Twitter, (Guid) socialAction.IdUserAction))
            //            {
            //                //WRITE A ROW IN STATISTICS DB
            //                try
            //                {
            //                    var newStatistic = new NewStatisticInput
            //                    {
            //                        Comments = "Action Shared on Twitter",
            //                        FileOrigin = _networkManager.GetCurrentPageName(),
            //                        IdRelatedObject = null,
            //                        IdUser = new Guid(HttpContext.Current.Session["IDUser"].ToString()),
            //                        OtherInfo = "",
            //                        SearchString = "",
            //                        StatisticsActionType = StatisticsActionType.SC_ActionSharedOnTwitter
            //                    };

            //                    _statisticManager.NewStatistic(newStatistic);
            //                }
            //                catch
            //                {
            //                }
            //            }
            //            else
            //            {
            //                //WRITE A ROW IN LOG FILE AND DB
            //                try
            //                {
            //                    var logRow = new LogRowIn()
            //                    {
            //                        ErrorMessage = $"RelatedObject: {socialAction.IdActionRelatedObject}",
            //                        ErrorMessageCode = "US-ER-0012",
            //                        ErrorSeverity = LogLevel.Errors,
            //                        FileOrigin = _networkManager.GetCurrentPageName(),
            //                        IdUser = HttpContext.Current.Session["IDUser"].ToString(),
            //                    };

            //                    _logManager.WriteLog(logRow);
            //                }
            //                catch
            //                {
            //                }
            //            }
            //            break;
            //        case RequestResult.Unauthorized:
            //            var url = "/auth/auth.aspx?twitterauth=true";
            //            HttpContext.Current.Response.Redirect(url, true);
            //            break;
            //        case RequestResult.FileNotFound:
            //            break;
            //        case RequestResult.BadRequest:
            //            break;
            //        case RequestResult.NotAcceptable:
            //            break;
            //        case RequestResult.RateLimited:
            //            break;
            //        case RequestResult.TwitterIsDown:
            //            break;
            //        case RequestResult.TwitterIsOverloaded:
            //            break;
            //        case RequestResult.ConnectionFailure:
            //            break;
            //        case RequestResult.Unknown:
            //            break;
            //        case RequestResult.ProxyAuthenticationRequired:
            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //   }
            // }
        }

        #endregion
    }
}