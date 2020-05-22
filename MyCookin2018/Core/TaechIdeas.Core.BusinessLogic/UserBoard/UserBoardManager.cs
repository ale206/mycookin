using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.UserBoard;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.BusinessLogic.UserBoard
{
    public class UserBoardManager : IUserBoardManager
    {
        private readonly IUserBoardRepository _userBoardRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;

        public UserBoardManager(IUserBoardRepository userBoardRepository, ITokenManager tokenManager, IMapper mapper)
        {
            _userBoardRepository = userBoardRepository;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }

        #region OLD - InsertAction & InsertNotification

        ///// <summary>
        ///// Insert new Action - New Comment, new like, etc.
        ///// </summary>
        ///// <returns></returns>
        //public UspReturnValue InsertAction(Dto.UserBoard userBoard)
        //{
        //    UspReturnValue insertActionResult;

        //    try
        //    {
        //        var taInsertAction = new InsertActionSPTableAdapter();

        //        var actionInsertedOn = DateTime.UtcNow;

        //        insertActionResult =
        //            _uspReturnValueManager.GetUspReturnValue(taInsertAction.InsertAction(userBoard.IdUser, userBoard.IdUserActionFather, (int) userBoard.IdUserActionType,
        //                userBoard.IdActionRelatedObject,
        //                userBoard.UserActionMessage, userBoard.IdVisibility, userBoard.UserActionDate));

        //        try
        //        {
        //            //Get Correct Template
        //            GetTemplate(userBoard.IdUserActionType, userBoard.IdLanguage);

        //            var userByIdInput = new UserByIdInput()
        //            {
        //                UserId = userBoard.IdUser
        //            };

        //            var userInAction = _userManager.UserById(userByIdInput);

        //            Guid idUser;
        //            var correctActionType = ActionTypes.NotSpecified;
        //            string urlNotification = null;
        //            Guid? idRelatedObject = null;
        //            Guid? notificationImage = null;
        //            Guid? idUserOwnerRelatedObject = null;
        //            string userNotification;

        //            //For some Types, notification is not necessary.
        //            //For example when a user post a new profile status, it should be notificated to ALL is following him. Then disactivate for cases like this.
        //            //Set Notification Insert method active by default.
        //            var notificationActive = true;

        //            //TODO: Create method
        //            string routingRecipe;

        //            switch (userBoard.IdLanguage)
        //            {
        //                case 1:
        //                    routingRecipe = _networkConfig.RoutingRecipeEn;
        //                    break;
        //                case 2:
        //                    routingRecipe = _networkConfig.RoutingRecipeIt;
        //                    break;
        //                case 3:
        //                    routingRecipe = _networkConfig.RoutingRecipeEs;
        //                    break;
        //                default:
        //                    routingRecipe = _networkConfig.RoutingRecipeEn;
        //                    break;
        //            }

        //            switch (userBoard.IdUserActionType)
        //            {
        //                //Someone is following someone else - Actions WITHOUT _IDUserActionFather valorized
        //                case ActionTypes.NewFollower:
        //                    idUser = (Guid) userBoard.IdActionRelatedObject;
        //                    correctActionType = (ActionTypes) userBoard.IdUserActionType;
        //                    urlNotification = "/" + userInAction.Username + "/";
        //                    idRelatedObject = userBoard.IdUser;
        //                    notificationImage = null;
        //                    idUserOwnerRelatedObject = (Guid) userBoard.IdActionRelatedObject;

        //                    //Create Notification String
        //                    userNotification = string.Format("<a href=\"" + urlNotification + "\">" + userBoard.NotificationTemplate + "</a>", userInAction.Name + " " + userInAction.Surname);

        //                    break;

        //                case ActionTypes.NewRecipeShare:

        //                    //var UserBoardAction = new UserBoard((Guid)userBoard.IdActionRelatedObject, true);
        //                    var userBoardAction = GetUserActionInfo(userBoard.IdUser);

        //                    //var RecipeObj = new RecipeByIdAndLanguageOutput((Guid) UserBoardAction.IdActionRelatedObject, userBoard.RecipeLanguageId);

        //                    var recipeByLanguageInput3 = new RecipeByIdAndLanguageInput()
        //                    {
        //                        LanguageId = userBoard.IdLanguage,
        //                        RecipeId = (Guid) userBoard.IdActionRelatedObject
        //                    };

        //                    var recipeObj = _recipeManager.RecipeByIdAndLanguage(recipeByLanguageInput3);

        //                    idUser = userBoard.IdUser;
        //                    correctActionType = userBoard.IdUserActionType;

        //                    //TODO: Remove this Guid at the end
        //                    urlNotification = Path.Combine(routingRecipe, recipeObj.RecipeName, recipeObj.RecipeId.ToString());
        //                    //urlNotification = _appConfigManager.GetValue("RoutingRecipe" + userBoard.RecipeLanguageId, AppDomain.CurrentDomain) + recipeObj.RecipeName + "/" + recipeObj.RecipeId.ToString();

        //                    idRelatedObject = userBoard.IdActionRelatedObject;
        //                    notificationImage = null;
        //                    idUserOwnerRelatedObject = userBoard.IdUser;

        //                    //Create Notification String
        //                    userNotification = string.Format("<a href=\"" + urlNotification + "\">" + userBoard.NotificationTemplate + "</a>", userInAction.Name + " " + userInAction.Surname);

        //                    break;

        //                case ActionTypes.RecipeAddedToRecipeBook:
        //                    //IDActionRelatedObject is IDRecipe 

        //                    var recipeByLanguageInput = new RecipeByIdAndLanguageInput()
        //                    {
        //                        LanguageId = userBoard.IdLanguage,
        //                        RecipeId = (Guid) userBoard.IdActionRelatedObject
        //                    };

        //                    var recipeObj2 = _recipeManager.RecipeByIdAndLanguage(recipeByLanguageInput);
        //                    //RecipeObj2.RecipeByIdAndLanguage();
        //                    //RecipeObj2.QueryBaseRecipeInfo();

        //                    var idUserOwner = recipeObj2.OwnerId;

        //                    idUser = userBoard.IdUser;
        //                    correctActionType = userBoard.IdUserActionType;

        //                    //TODO: Remove this Guid at the end
        //                    urlNotification = Path.Combine(routingRecipe, recipeObj2.RecipeName, recipeObj2.RecipeId.ToString());
        //                    //urlNotification = _appConfigManager.GetValue("RoutingRecipe" + userBoard.RecipeLanguageId, AppDomain.CurrentDomain) + recipeObj2.RecipeName + "/" + recipeObj2.RecipeId.ToString();

        //                    idRelatedObject = userBoard.IdActionRelatedObject;
        //                    notificationImage = null;
        //                    idUserOwnerRelatedObject = idUserOwner;

        //                    //Create Notification String
        //                    userNotification = string.Format("<a href=\"" + urlNotification + "\">" + userBoard.NotificationTemplate + "</a>", userInAction.Name + " " + userInAction.Surname);

        //                    break;

        //                case ActionTypes.RecipeCooked:
        //                    //IDActionRelatedObject is IDRecipe 

        //                    //var RecipeObj3 = new RecipeByIdAndLanguageOutput(_IDActionRelatedObject, IDLanguage);
        //                    //RecipeObj3.RecipeByIdAndLanguage();
        //                    //RecipeObj3.QueryBaseRecipeInfo();
        //                    var recipeByLanguageInput2 = new RecipeByIdAndLanguageInput()
        //                    {
        //                        LanguageId = userBoard.IdLanguage,
        //                        RecipeId = (Guid) userBoard.IdActionRelatedObject
        //                    };

        //                    var recipeObj3 = _recipeManager.RecipeByIdAndLanguage(recipeByLanguageInput2);

        //                    var idUserRecipeOwner = recipeObj3.OwnerId;

        //                    idUser = userBoard.IdUser;
        //                    correctActionType = userBoard.IdUserActionType;

        //                    //TODO: Remove this Guid at the end
        //                    urlNotification = Path.Combine(routingRecipe, recipeObj3.RecipeName, recipeObj3.RecipeId.ToString());
        //                    //urlNotification = _appConfigManager.GetValue("RoutingRecipe" + userBoard.RecipeLanguageId, AppDomain.CurrentDomain) + recipeObj3.RecipeName + "/" + recipeObj3.RecipeId.ToString();

        //                    idRelatedObject = userBoard.IdActionRelatedObject;
        //                    notificationImage = null;
        //                    idUserOwnerRelatedObject = idUserRecipeOwner;

        //                    //Create Notification String: {0} sta cucinando la tua ricetta {1}
        //                    userNotification = string.Format("<a href=\"" + urlNotification + "\">" + userBoard.NotificationTemplate + "</a>", userInAction.Name + " " + userInAction.Surname,
        //                        recipeObj3.RecipeName);

        //                    break;
        //                case ActionTypes.PostOnFriendUserBoard:
        //                    idUser = userBoard.IdUser;
        //                    correctActionType = (ActionTypes) userBoard.IdUserActionType;
        //                    urlNotification = "/" + userInAction.Username + "/";
        //                    idRelatedObject = (Guid) userBoard.IdActionRelatedObject;
        //                    notificationImage = null;
        //                    idUserOwnerRelatedObject = (Guid) userBoard.IdActionRelatedObject;

        //                    //Create Notification String
        //                    userNotification = string.Format("<a href=\"" + urlNotification + "\">" + userBoard.NotificationTemplate + "</a>", userInAction.Name + " " + userInAction.Surname);

        //                    break;
        //                case ActionTypes.PersonalMessage:
        //                case ActionTypes.UserProfileUpdated:
        //                case ActionTypes.NewRecipe:
        //                    idUser = userBoard.IdUser;
        //                    correctActionType = userBoard.IdUserActionType;
        //                    urlNotification = "/" + userInAction.Username + "/";
        //                    notificationImage = null;

        //                    //Create Notification String
        //                    userNotification = string.Format("<a href=\"" + urlNotification + "\">" + userBoard.NotificationTemplate + "</a>", userInAction.Name + " " + userInAction.Surname);

        //                    //Disable Notification if NOT present IDUserOwnerRelatedObject like in this case.
        //                    notificationActive = false;

        //                    break;
        //                default:
        //                    //All Actions WITH _IDUserActionFather valorized
        //                    var ownerActionFatherList = GetActionOwnerByFatherAction((Guid) userBoard.IdUserActionFather);

        //                    idUser = new Guid(HttpContext.Current.Session["IDUser"].ToString());
        //                    correctActionType = userBoard.IdUserActionType;
        //                    urlNotification = "/User/UserBoardPost.aspx?UserActionId=" + userBoard.IdUserActionFather;
        //                    idRelatedObject = ownerActionFatherList[0].IdActionRelatedObject;
        //                    notificationImage = null;
        //                    //IDUserOwnerRelatedObject = OwnerActionFatherList[0]._IDActionRelatedObject;
        //                    idUserOwnerRelatedObject = ownerActionFatherList[0].IdUser;

        //                    //Create Notification String
        //                    userNotification = string.Format("<a href=\"" + urlNotification + "\">" + userBoard.NotificationTemplate + "</a>", userInAction.Name + " " + userInAction.Surname);

        //                    //Disable Notification for some Actions
        //                    switch (userBoard.IdUserActionType)
        //                    {
        //                        case ActionTypes.LikeForRecipeAddedToRecipeBook:
        //                            notificationActive = false;
        //                            break;
        //                    }

        //                    break;
        //            }

        //            //Insert Notification
        //            //This "if" avoid notifications for action on own Types (Ex.: notification for a comment on our post)
        //            if (notificationActive)
        //            {
        //                if (idUserOwnerRelatedObject.ToString() != HttpContext.Current.Session["IDUser"].ToString())
        //                {
        //                    var newNotification = new UserBoardNotification
        //                    {
        //                        IdUser = idUser,
        //                        IdUserActionType = correctActionType,
        //                        UrlNotification = urlNotification,
        //                        IdRelatedObject = idRelatedObject,
        //                        NotificationImage = notificationImage,
        //                        UserNotification = userNotification,
        //                        IdUserOwnerRelatedObject = idUserOwnerRelatedObject
        //                    };

        //                    _userBoardNotificationManager.InsertNotification(newNotification);
        //                }
        //                try
        //                {
        //                    //JUST A LOG
        //                    var logRow = new LogRowIn()
        //                    {
        //                        ErrorMessage = "Notification Inserted (UserBoard.cs)",
        //                        ErrorMessageCode = "US-IN-0055",
        //                        ErrorSeverity = LogLevel.JustALog,
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdUser = HttpContext.Current.Session["IDUser"].ToString(),
        //                    };

        //                    _logManager.WriteLog(logRow);
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            try
        //            {
        //                //ERROR LOG
        //                var logRow = new LogRowIn()
        //                {
        //                    ErrorMessage = $"Error in InsertAction -> InsertNotification: {ex.Message}",
        //                    ErrorMessageCode = "US-ER-9999",
        //                    ErrorSeverity = LogLevel.CriticalErrors,
        //                    FileOrigin = _networkManager.GetCurrentPageName(),
        //                    IdUser = HttpContext.Current.Session["IDUser"].ToString(),
        //                };

        //                _logManager.WriteLog(logRow);
        //            }
        //            catch
        //            {
        //            }
        //        }

        //        //UserLogUSP(LogLevel.Debug, LogLevel.Debug, false, InsertActionResult);
        //        //WRITE A ROW IN STATISTICS DB
        //        try
        //        {
        //            //This is for EVERY Action
        //            //NewStatisticInput NewStatistic = new NewStatisticInput(_IDUser, _IDUserActionFather, StatisticsActionType.SC_NewActionOnUserBoard, "New Action on UserBoard", _networkManager.GetCurrentPageName(), "", "");
        //            //NewStatistic.NewStatistic();

        //            //This is for SPECIFIC Action

        //            #region StatisticsForCorrectType

        //            switch (userBoard.IdUserActionType)
        //            {
        //                case ActionTypes.Comment:
        //                    var newStatisticForNewComment = new NewStatisticInput
        //                    {
        //                        Comments = "New Comment",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_NewComment
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticForNewComment);

        //                    break;
        //                case ActionTypes.PersonalMessage:

        //                    var newStatisticForNewPersonalMessage = new NewStatisticInput
        //                    {
        //                        Comments = "New Personal Message on UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_NewPersonalMessage
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticForNewPersonalMessage);

        //                    break;
        //                case ActionTypes.PostOnFriendUserBoard:
        //                    var newStatisticForNewPost = new NewStatisticInput
        //                    {
        //                        Comments = "New Post on Friend UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_NewPostOnFriendUserBoard
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticForNewPost);

        //                    break;
        //                case ActionTypes.UserProfileUpdated:
        //                    var newStatisticUpu = new NewStatisticInput
        //                    {
        //                        Comments = "Profile Updated",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.US_UpdateProfile
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticUpu);
        //                    break;

        //                case ActionTypes.LikeForComment:
        //                    var NewStatisticLFC = new NewStatisticInput
        //                    {
        //                        Comments = "New Like on a Comment of the UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_LikeForComment
        //                    };

        //                    _statisticManager.NewStatistic(NewStatisticLFC);

        //                    break;
        //                case ActionTypes.LikeForNewFollower:

        //                    var newStatisticLfnf = new NewStatisticInput
        //                    {
        //                        Comments = "New Like on a New Follower News of the UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_LikeForNewFollower
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticLfnf);
        //                    break;
        //                case ActionTypes.LikeForNewIngredient:

        //                    var newStatisticLfni = new NewStatisticInput
        //                    {
        //                        Comments = "New Like on a New Ingredient on the UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_LikeForNewIngredient
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticLfni);

        //                    break;
        //                case ActionTypes.LikeForNewRecipe:
        //                    var newStatisticLfnr = new NewStatisticInput
        //                    {
        //                        Comments = "New Like on a New Recipe on the UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_LikeForNewRecipe
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticLfnr);
        //                    break;
        //                case ActionTypes.LikeForPersonalMessage:
        //                    var NewStatisticLFPM = new NewStatisticInput
        //                    {
        //                        Comments = "New Like on a Personal Message on the UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_LikeForPersonalMessage
        //                    };

        //                    _statisticManager.NewStatistic(NewStatisticLFPM);

        //                    break;
        //                case ActionTypes.LikeForPostOnFriendUserBoard:
        //                    var newStatisticLfpf = new NewStatisticInput
        //                    {
        //                        Comments = "New Like on a Post on Friend UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_LikeForPostOnFriendUserBoard
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticLfpf);
        //                    break;
        //                case ActionTypes.LikeForProfileUpdate:
        //                    var newStatisticLfpu = new NewStatisticInput
        //                    {
        //                        Comments = "New Like on a Profile Update on the UserBoard",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.SC_LikeForProfileUpdate
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticLfpu);
        //                    break;
        //                case ActionTypes.RecipeAddedToRecipeBook:
        //                    var newStatisticRatrb = new NewStatisticInput
        //                    {
        //                        Comments = "Recipe added to Recipe Book",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.RC_AddedToRecipeBook
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticRatrb);

        //                    break;
        //                case ActionTypes.RecipeCooked:
        //                    var newStatisticCr = new NewStatisticInput
        //                    {
        //                        Comments = "Recipe cooked",
        //                        FileOrigin = _networkManager.GetCurrentPageName(),
        //                        IdRelatedObject = userBoard.IdUserActionFather,
        //                        IdUser = userBoard.IdUser,
        //                        OtherInfo = "",
        //                        SearchString = "",
        //                        StatisticsActionType = StatisticsActionType.RC_AddedToRecipeBook
        //                    };

        //                    _statisticManager.NewStatistic(newStatisticCr);

        //                    break;
        //            }

        //            #endregion
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        insertActionResult = _uspReturnValueManager.GetUspReturnValue("US-ER-9999", sqlEx.Message, true);
        //        _utilsManager.LogStoredProcedure(LogLevel.Errors, insertActionResult);
        //    }
        //    catch (Exception ex)
        //    {
        //        insertActionResult = _uspReturnValueManager.GetUspReturnValue("US-ER-9999", ex.Message, true);
        //        _utilsManager.LogStoredProcedure(LogLevel.CriticalErrors, insertActionResult);
        //    }

        //    return insertActionResult;
        //}

        #endregion

        #region BlockElement

        public IEnumerable<BlockElementOutput> BlockElement(BlockElementInput blockElementInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(blockElementInput.CheckTokenInput);

            return _mapper.Map<IEnumerable<BlockElementOutput>>(_userBoardRepository.BlockElement(_mapper.Map<BlockElementIn>(blockElementInput)));
        }

        #endregion

        #region FatherOrSon

        public IEnumerable<FatherOrSonOutput> FatherOrSon(FatherOrSonInput fatherOrSonInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(fatherOrSonInput.CheckTokenInput);

            return _mapper.Map<IEnumerable<FatherOrSonOutput>>(_userBoardRepository.FatherOrSon(_mapper.Map<FatherOrSonIn>(fatherOrSonInput)));
        }

        #endregion

        #region WithPagination

        public IEnumerable<WithPaginationOutput> WithPagination(WithPaginationInput withPaginationInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(withPaginationInput.CheckTokenInput);

            return _mapper.Map<IEnumerable<WithPaginationOutput>>(_userBoardRepository.WithPagination(_mapper.Map<WithPaginationIn>(withPaginationInput)));
        }

        #endregion

        #region BlockLoad

        public IEnumerable<BlockLoadOutput> BlockLoad(BlockLoadInput blockLoadInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(blockLoadInput.CheckTokenInput);

            return _mapper.Map<IEnumerable<BlockLoadOutput>>(_userBoardRepository.BlockLoad(_mapper.Map<BlockLoadIn>(blockLoadInput)));
        }

        #endregion

        #region MyNewsBlockLoad

        public IEnumerable<MyNewsBlockLoadOutput> MyNewsBlockLoad(MyNewsBlockLoadInput myNewsBlockLoadInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(myNewsBlockLoadInput.CheckTokenInput);

            return _mapper.Map<IEnumerable<MyNewsBlockLoadOutput>>(_userBoardRepository.MyNewsBlockLoad(_mapper.Map<MyNewsBlockLoadIn>(myNewsBlockLoadInput)));
        }

        #endregion

        #region CheckIfShared

        public CheckIfSharedOutput CheckIfShared(CheckIfSharedInput checkIfSharedInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(checkIfSharedInput.CheckTokenInput);

            return _mapper.Map<CheckIfSharedOutput>(_userBoardRepository.CheckIfShared(_mapper.Map<CheckIfSharedIn>(checkIfSharedInput)));
        }

        #endregion

        #region InsertActionShared

        public InsertActionSharedOutput InsertActionShared(InsertActionSharedInput insertActionSharedInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(insertActionSharedInput.CheckTokenInput);

            return _mapper.Map<InsertActionSharedOutput>(_userBoardRepository.InsertActionShared(_mapper.Map<InsertActionSharedIn>(insertActionSharedInput)));
        }

        #endregion

        #region InsertAction

        public InsertActionOutput InsertAction(InsertActionInput insertActionInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(insertActionInput.CheckTokenInput);

            return _mapper.Map<InsertActionOutput>(_userBoardRepository.InsertAction(_mapper.Map<InsertActionIn>(insertActionInput)));
        }

        #endregion

        #region CheckIfSharedOnPersonalBoard

        public CheckIfSharedOnPersonalBoardOutput CheckIfSharedOnPersonalBoard(CheckIfSharedOnPersonalBoardInput checkIfSharedOnPersonalBoardInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(checkIfSharedOnPersonalBoardInput.CheckTokenInput);

            return _mapper.Map<CheckIfSharedOnPersonalBoardOutput>(_userBoardRepository.CheckIfSharedOnPersonalBoard(_mapper.Map<CheckIfSharedOnPersonalBoardIn>(checkIfSharedOnPersonalBoardInput)));
        }

        #endregion

        #region CountLikesOrComment

        public CountLikesOrCommentOutput CountLikesOrComment(CountLikesOrCommentInput countLikesOrCommentInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(countLikesOrCommentInput.CheckTokenInput);

            return _mapper.Map<CountLikesOrCommentOutput>(_userBoardRepository.CountLikesOrComment(_mapper.Map<CountLikesOrCommentIn>(countLikesOrCommentInput)));
        }

        #endregion

        #region CountNumberOfActionsByUserAndType

        public CountNumberOfActionsByUserAndTypeOutput CountNumberOfActionsByUserAndType(CountNumberOfActionsByUserAndTypeInput countNumberOfActionsByUserAndTypeInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(countNumberOfActionsByUserAndTypeInput.CheckTokenInput);

            return
                _mapper.Map<CountNumberOfActionsByUserAndTypeOutput>(
                    _userBoardRepository.CountNumberOfActionsByUserAndType(_mapper.Map<CountNumberOfActionsByUserAndTypeIn>(countNumberOfActionsByUserAndTypeInput)));
        }

        #endregion

        #region DeleteLike

        public DeleteLikeOutput DeleteLike(DeleteLikeInput deleteLikeInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(deleteLikeInput.CheckTokenInput);

            return _mapper.Map<DeleteLikeOutput>(_userBoardRepository.DeleteLike(_mapper.Map<DeleteLikeIn>(deleteLikeInput)));
        }

        #endregion

        #region ActionOwnerByFather

        public ActionOwnerByFatherOutput ActionOwnerByFather(ActionOwnerByFatherInput actionOwnerByFatherInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(actionOwnerByFatherInput.CheckTokenInput);

            return _mapper.Map<ActionOwnerByFatherOutput>(_userBoardRepository.ActionOwnerByFather(_mapper.Map<ActionOwnerByFatherIn>(actionOwnerByFatherInput)));
        }

        #endregion

        #region IdUserFromIdUserAction

        public IdUserFromIdUserActionOutput IdUserFromIdUserAction(IdUserFromIdUserActionInput idUserFromIdUserActionInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(idUserFromIdUserActionInput.CheckTokenInput);

            return _mapper.Map<IdUserFromIdUserActionOutput>(_userBoardRepository.IdUserFromIdUserAction(_mapper.Map<IdUserFromIdUserActionIn>(idUserFromIdUserActionInput)));
        }

        #endregion

        #region IdUserByActionTypeAndActionFather

        public IEnumerable<IdUserByActionTypeAndActionFatherOutput> IdUserByActionTypeAndActionFather(IdUserByActionTypeAndActionFatherInput idUserByActionTypeAndActionFatherInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(idUserByActionTypeAndActionFatherInput.CheckTokenInput);

            return
                _mapper.Map<IEnumerable<IdUserByActionTypeAndActionFatherOutput>>(
                    _userBoardRepository.IdUserByActionTypeAndActionFather(_mapper.Map<IdUserByActionTypeAndActionFatherIn>(idUserByActionTypeAndActionFatherInput)));
        }

        #endregion

        #region ObjectYouLike

        public ObjectYouLikeOutput ObjectYouLike(ObjectYouLikeInput objectYouLikeInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(objectYouLikeInput.CheckTokenInput);

            return _mapper.Map<ObjectYouLikeOutput>(_userBoardRepository.ObjectYouLike(_mapper.Map<ObjectYouLikeIn>(objectYouLikeInput)));
        }

        #endregion

        #region UserActionInfoById

        public UserActionInfoByIdOutput UserActionInfoById(UserActionInfoByIdInput userActionInfoByIdInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(userActionInfoByIdInput.CheckTokenInput);

            return _mapper.Map<UserActionInfoByIdOutput>(_userBoardRepository.UserActionInfoById(_mapper.Map<UserActionInfoByIdIn>(userActionInfoByIdInput)));
        }

        #endregion

        #region DeleteUserAction

        public DeleteUserActionOutput DeleteUserAction(DeleteUserActionInput deleteUserActionInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(deleteUserActionInput.CheckTokenInput);

            return _mapper.Map<DeleteUserActionOutput>(_userBoardRepository.DeleteUserAction(_mapper.Map<DeleteUserActionIn>(deleteUserActionInput)));
        }

        #endregion

        #region TemplateByTypeAndLanguage

        public TemplateByTypeAndLanguageOutput TemplateByTypeAndLanguage(TemplateByTypeAndLanguageInput templateByTypeAndLanguageInput)
        {
            //Check for Valid Token
            _tokenManager.CheckToken(templateByTypeAndLanguageInput.CheckTokenInput);

            return
                _mapper.Map<TemplateByTypeAndLanguageOutput>(
                    _userBoardRepository.TemplateAccordingToTypeAndLanguage(_mapper.Map<TemplateByTypeAndLanguageIn>(templateByTypeAndLanguageInput)));
        }

        #endregion
    }
}