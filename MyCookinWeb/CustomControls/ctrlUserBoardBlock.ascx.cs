using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Common;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.SocialAction;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.Log;
using MyCookin.ObjectManager.RecipeManager;
using System.Data;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlUserBoardBlock : System.Web.UI.UserControl
    {

        #region PublicFields
        //id azione
        public Guid IDUserAction
        {
            get { return new Guid(hfIDUserAction.Value); }
            set { hfIDUserAction.Value = value.ToString(); }
        }

        //public int TypeOfLike
        //{
        //    get { return Convert.ToInt32(hfTypeOfLike.Value); }
        //    set { hfTypeOfLike.Value = value.ToString(); }
        //}

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            hfIDLanguage.Value = Session["IDLanguage"].ToString();

            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "TextAreaAutoGrow('" + txtNewComment.ClientID + "');", true);

            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "$(\"#" + imgSharingLoader.ClientID + "\").hide();", true);
        }

        protected override void DataBindChildren()
        {
            if (hfControlBoardLoaded.Value != "true")
            {
                LoadControl();
                ctrlUserBoardBlockLikes.StartLikeControl();
            }
        }

        private void LoadControl()
        {
            try
            {
                bool IsPublicProfile;

                #region DisableDeleteBotton
                //Show Delete Botton only for our actions
                //If not logged, in case of public profle, this will be in error. 
                try
                {
                    Guid me = new Guid(Session["IDUser"].ToString());

                    UserBoard ActionBoard = new UserBoard(IDUserAction, false);
                    if (ActionBoard.GetIDUserFromIDUserAction() != me)
                    {
                        ibtnDelete.Visible = false;
                    }

                    IsPublicProfile = false;
                }
                catch
                {
                    IsPublicProfile = true;

                    //NOT LOGGED - PUBLIC PROFILE
                    ibtnDelete.Visible = false;
                    pnlAddComment.Visible = false;
                    pnlCommentContainer.Visible = true;
                }
                #endregion

                //Set correct default button when press Enter
                txtNewComment.Attributes.Add("onkeypress", "return clickButton(event,'" + ibtnComment.ClientID + "')");
                ibtnDelete.OnClientClick = GetLocalResourceObject("ibtnDelete.OnClientClick").ToString();

                //COMMON FOR ALL
                UserBoard newUserBoard = new UserBoard(IDUserAction, false);
                Guid IDUserGuid = newUserBoard.GetIDUserFromIDUserAction();

                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                UserBoard UserBoardBlock = new UserBoard(IDUserAction, false);

                List<UserBoard> UserBoardBlockElementList = new List<UserBoard>();

                UserBoardBlockElementList = UserBoardBlock.GetUsersBoardBlockElement();

                #region InstantiateVariables
                int IDUserActionTypeLanguage;
                bool UserActionTypeMailNotice;
                bool UserActionTypeSiteNotice;
                bool UserActionTypeSmsNotice;
                int? IDVisibility;
                DateTime UserActionDate;
                Guid IDUser;
                Guid? IDUserActionFather;
                string UserActionType;
                string UserActionTypeToolTip;
                //int UserActionTypeMessageMaxLength;
                //string UserActionMessage;
                //string UserActionTypeTemplate;
                Guid? IDActionRelatedObject;
                #endregion
                
                //Load Comments
                rptComments.DataSource = GetComments(IDUserGuid, IDLanguage, IDUserAction);

                rptComments.DataBind();

                //CommentsTemplate - Ex: (34 Comments)
                lblCountComments.Text = CommentsTemplate(IDUserAction, IDLanguage, rptComments.Items.Count);

                ActivateScrollbarComment();
         
                //Foreach element of the list
                foreach (UserBoard UserBoardElement in UserBoardBlockElementList)
                {
                    //For ALL
                    #region FieldsForAll
                    IDUserActionTypeLanguage = UserBoardElement.IDUserActionTypeLanguage;
                    UserActionTypeMailNotice = UserBoardElement.UserActionTypeMailNotice;
                    UserActionTypeSiteNotice = UserBoardElement.UserActionTypeSiteNotice;
                    UserActionTypeSmsNotice = UserBoardElement.UserActionTypeSmsNotice;
                    IDVisibility = UserBoardElement.IDVisibility;
                    UserActionDate = UserBoardElement.UserActionDate;
                    IDLanguage = UserBoardElement.IDLanguage;
                    IDUser = UserBoardElement.IDUser;
                    IDUserActionFather = UserBoardElement.IDUserActionFather;
                    #endregion

                    //New User Object
                    MyUser UserInAction = new MyUser(IDUser);
                    UserInAction.GetUserBasicInfoByID();

                    //Others Fields
                    UserActionType = UserBoardElement.UserActionType;
                    UserActionTypeToolTip = UserBoardElement.UserActionTypeToolTip;

                    #region Common - ProfilePic, Name, Date
                    //Profile Picture
                    //***************
                    Media ProfilePic = new Media(UserInAction.IDProfilePhoto);
                    ProfilePic.QueryMediaInfo();

                    string _imgUserPath = "";

                    _imgUserPath = ProfilePic.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);

                    if (_imgUserPath == "")
                    {
                        _imgUserPath = ProfilePic.GetCompletePath(false, false, true);
                    }

                    if (_imgUserPath != "")
                    {
                        btnImgUser.ImageUrl = _imgUserPath;
                        btnImgUser.CssClass = "imgUser";
                    }
                    else
                    {
                        btnImgUser.CssClass = "imgUserNoPic";
                    }
                    btnImgUser.AlternateText = UserInAction.Name + " " + UserInAction.Surname + " (" + UserInAction.UserName + ")";
                    //***************

                    //Link of the User
                    //****************
                    hlUser.Text = UserInAction.Name + " " + UserInAction.Surname;
                    hlUser.NavigateUrl = ("/" + UserInAction.UserName + "/").ToLower();
                    btnImgUser.PostBackUrl = ("/" + UserInAction.UserName + "/").ToLower();
                    btnImgUser.AlternateText = UserInAction.Name + " " + UserInAction.Surname;
                    //****************

                    //Date
                    lblDatePublish.Text = UserActionDate.ToString(AppConfig.GetValue("DateTimeFormatCSharp_v1", AppDomain.CurrentDomain)).Replace(".", ":");
                    #endregion

                    //int IdUserActionTypeInt = (int)(UserBoardElement.IDUserActionType);
                    hfTypeOfLike.Value = UserBoard.GetTypeOfLike(UserBoardElement.IDUserActionType).ToString();

                    //Here will be ONLY Fathers..
                    switch (UserBoardElement.IDUserActionType)
                    {
                        #region Follower
                        //NEW FOLLOWER
                        case ActionTypes.NewFollower:

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            pnlImageAndDescription.Visible = false;

                            IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;   //L'utente che si sta seguendo..

                            //Get info of this user
                            MyUser UserFollowed = new MyUser((Guid)IDActionRelatedObject);
                            UserFollowed.GetUserBasicInfoByID();

                            string UserFollowedProfileUrl = ("/" + UserFollowed.UserName + "/").ToLower();
                            string UserFollowedWithLink = "<a href=\"" + UserFollowedProfileUrl + "\">" + UserFollowed.Name + " " + UserFollowed.Surname + "</a>";

                            //{0} ha iniziato a seguire {1}
                            lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, UserInAction.Name, UserFollowedWithLink);

                            break;
                        #endregion

                        #region PersonalMessage
                        //PERSONAL MESSAGE
                        case ActionTypes.PersonalMessage:
                            //Get the personal message

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            pnlImageAndDescription.Visible = false;

                            //{0} scrive {1}
                            lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, UserInAction.Name, UserBoardElement.UserActionMessage);

                            break;
                        #endregion

                        #region FotoUploaded
                        //FOTO UPLOADED
                        case ActionTypes.FotoUploaded:
                            IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            break;
                        #endregion

                        #region NewRecipe
                        //NEW RECIPE
                        case ActionTypes.NewRecipe:

                            //pnlShare.Visible = true;

                            IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;

                            RecipeLanguage Recipe = new RecipeLanguage(IDActionRelatedObject, IDLanguage);
                            Recipe.QueryRecipeLanguageInfo();
                            Recipe.QueryBaseRecipeInfo();
                            Recipe.GetRecipeSteps();

                            try
                            {
                                btnImgRelatedObject.ImageUrl = Recipe.RecipeImage.GetCompletePath(false, false, true);
                            }
                            catch
                            {
                                //No image! Set here one of default.
                            }                                                             // ******* Da impostare nel css!

                            hlRelatedObject.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + Recipe.RecipeName.Replace(" ", "-") + "/" + Recipe.IDRecipe.ToString()).ToLower();
                            hlRelatedObject.Text = Recipe.RecipeName;
                            btnImgRelatedObject.PostBackUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + Recipe.RecipeName.Replace(" ", "-") + "/" + Recipe.IDRecipe.ToString()).ToLower();
                            btnImgRelatedObject.AlternateText = Recipe.RecipeName;
                            try
                            {
                                if (Recipe.RecipeSteps[0].Step.Length < 550)
                                {
                                    lblDescriptionObject.Text = Recipe.RecipeSteps[0].Step;
                                }
                                else
                                {
                                    lblDescriptionObject.Text = Recipe.RecipeSteps[0].Step.Substring(0, 547) + "...";
                                }
                            }
                            catch { }

                            //{0} ha catalogato la ricetta {1}
                            lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, UserInAction.Name, Recipe.RecipeName);

                            //Active Possibility of Sharing on Facebook, if not already shared.
                            //if (!SocialAction.CheckIfShared(IDUserAction, IDUserGuid, SocialNetwork.Faceboook))
                            //{
                                ibtnShareFacebook.Visible = false;
                            //}
                            //else
                            //{
                            //    ibtnShareFacebook.Visible = false;
                            //}

                            //Active Possibility of Sharing on Twitter, if not already shared.
                            //if (!SocialAction.CheckIfShared(IDUserAction, IDUserGuid, SocialNetwork.Twitter))
                            //{
                                ibtnShareTwitter.Visible = false;
                            //}
                            //else
                            //{
                            //    ibtnShareTwitter.Visible = false;
                            //}

                            try
                            {
                                //Guid me = new Guid(Session["IDUser"].ToString());

                                //if (!SocialAction.CheckIfSharedOnPersonalBoard(IDUserAction, me, ActionTypes.NewRecipeShare))
                                //{

                                    ibtnShareUserBoard.Visible = true;
                                //}
                                //else
                                //{
                                //    ibtnShareUserBoard.Visible = false;
                                //}
                            }
                            catch
                            { }

                            break;
                        #endregion

                        #region RecipeAddedToRecipeBook
                        //RECIPE ADDED TO RECIPE BOOK
                        case ActionTypes.RecipeAddedToRecipeBook:

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;

                            RecipeLanguage RecipeAdded = new RecipeLanguage(IDActionRelatedObject, IDLanguage);
                            RecipeAdded.QueryRecipeLanguageInfo();
                            RecipeAdded.QueryBaseRecipeInfo();
                            RecipeAdded.GetRecipeSteps();

                            try
                            {
                                btnImgRelatedObject.ImageUrl = RecipeAdded.RecipeImage.GetCompletePath(false, false, true);
                            }
                            catch
                            {
                                //No image! Set here one of default.
                            }                                                             // ******* Da impostare nel css!

                            hlRelatedObject.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + RecipeAdded.RecipeName.Replace(" ", "-") + "/" + RecipeAdded.IDRecipe.ToString()).ToLower();
                            hlRelatedObject.Text = RecipeAdded.RecipeName;
                            btnImgRelatedObject.PostBackUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + RecipeAdded.RecipeName.Replace(" ", "-") + "/" + RecipeAdded.IDRecipe.ToString()).ToLower();
                            btnImgRelatedObject.AlternateText = RecipeAdded.RecipeName;
                            try
                            {
                                if (RecipeAdded.RecipeSteps[0].Step.Length < 550)
                                {
                                    lblDescriptionObject.Text = RecipeAdded.RecipeSteps[0].Step;
                                }
                                else
                                {
                                    lblDescriptionObject.Text = RecipeAdded.RecipeSteps[0].Step.Substring(0, 547) + "...";
                                }
                            }
                            catch { }

                            MyUser RecipeOwner = new MyUser(RecipeAdded.Owner.IDUser);
                            RecipeOwner.GetUserBasicInfoByID();

                            //ha aggiunto una ricetta di {0} al suo ricettario
                            lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, RecipeOwner.Name + " " + RecipeOwner.Surname);

                            break;
                        #endregion

                        #region RecipeCooked
                        //RECIPE COOKED
                        case ActionTypes.RecipeCooked:

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;

                            RecipeLanguage RecipeCooked = new RecipeLanguage(IDActionRelatedObject, IDLanguage);
                            RecipeCooked.QueryRecipeLanguageInfo();
                            RecipeCooked.QueryBaseRecipeInfo();
                            RecipeCooked.GetRecipeSteps();

                            try
                            {
                                btnImgRelatedObject.ImageUrl = RecipeCooked.RecipeImage.GetCompletePath(false, false, true);
                            }
                            catch
                            {
                                //No image! Set here one of default.
                            }                                                             // ******* Da impostare nel css!

                            hlRelatedObject.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + RecipeCooked.RecipeName.Replace(" ", "-") + "/" + RecipeCooked.IDRecipe.ToString()).ToLower();
                            hlRelatedObject.Text = RecipeCooked.RecipeName;
                            btnImgRelatedObject.PostBackUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + RecipeCooked.RecipeName.Replace(" ", "-") + "/" + RecipeCooked.IDRecipe.ToString()).ToLower();
                            btnImgRelatedObject.AlternateText = RecipeCooked.RecipeName;
                            try
                            {
                                if (RecipeCooked.RecipeSteps[0].Step.Length < 550)
                                {
                                    lblDescriptionObject.Text = RecipeCooked.RecipeSteps[0].Step;
                                }
                                else
                                {
                                    lblDescriptionObject.Text = RecipeCooked.RecipeSteps[0].Step.Substring(0, 547) + "...";
                                }
                            }
                            catch { }

                            MyUser OwnerOfRecipe = new MyUser(RecipeCooked.Owner.IDUser);
                            OwnerOfRecipe.GetUserBasicInfoByID();

                            //sta cucinando una ricetta di {0} - sta cucinando una sua ricetta

                            //If the recipe is mine
                            if (Session["IDUser"].ToString().Equals(OwnerOfRecipe.IDUser.ToString()))
                            {
                                UserBoard UBObj = new UserBoard(ActionTypes.OwnRecipeShared, MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1), null);
                                UBObj.GetTemplate();

                                lblMessage.Text = UBObj.UserActionTypeTemplate;
                            }
                            else
                            {
                                //The recipe is not mine
                                lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, OwnerOfRecipe.Name + " " + OwnerOfRecipe.Surname);
                            }
                            //lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, OwnerOfRecipe.Name + " " + OwnerOfRecipe.Surname);

                            break;
                        #endregion

                        #region NewIngredient
                        case ActionTypes.NewIngredient:
                            try
                            {
                                //pnlShare.Visible = true;

                                IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;

                                IngredientLanguage Ingredient = new IngredientLanguage(IDActionRelatedObject, IDLanguage);
                                Ingredient.QueryIngredientLanguageInfo();
                                Ingredient.QueryIngredientInfo();

                                try
                                {
                                    btnImgRelatedObject.ImageUrl = Ingredient.IngredientImage.GetCompletePath(false,false,true);
                                }
                                catch
                                {
                                    //No image! Set here one of default.
                                }

                                hlRelatedObject.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingIngredient" + IDLanguage.ToString(), AppDomain.CurrentDomain) + Ingredient.IngredientSingular.Replace(" ", "-") + "/" + Ingredient.IDIngredient.ToString()).ToLower();
                                hlRelatedObject.Text = Ingredient.IngredientSingular;
                                btnImgRelatedObject.PostBackUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingIngredient" + IDLanguage.ToString(), AppDomain.CurrentDomain) + Ingredient.IngredientSingular.Replace(" ", "-") + "/" + Ingredient.IDIngredient.ToString()).ToLower();
                                btnImgRelatedObject.AlternateText = Ingredient.IngredientSingular;
                                lblDescriptionObject.Text = Ingredient.IngredientDescription;   //Creare Template Appropriato

                                //{0} ha catalogato l'ingrediente {1}
                                lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, UserInAction.Name, Ingredient.IngredientSingular);
                            }
                            catch
                            {
                                //If here, the action New Ingredient has been inserted in UserActions without the IDActionRelatedObject

                                //WRITE A ROW IN LOG FILE AND DB
                                LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0018", "Action New Ingredient without IDActionRelatedObject. IDUserAction" + IDUserAction, Session["IDUser"].ToString(), true, false);
                                LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                                LogManager.WriteFileLog(LogLevel.Errors, false, NewRowForLog);
                            }
                            break;

                        #endregion

                        #region UserProfileUpdated
                        //USER PROFILE UPDATED
                        case ActionTypes.UserProfileUpdated:

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            pnlImageAndDescription.Visible = false;

                            IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;

                            //{0} ha aggiornato il suo profilo
                            lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, UserInAction.Name);

                            break;
                        #endregion

                        #region PostOnFriendUserBoard
                        //PERSONAL MESSAGE
                        case ActionTypes.PostOnFriendUserBoard:
                            //Get the message publiched on Friend UserBoard

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;

                            MyUser Friend = new MyUser((Guid)IDActionRelatedObject);
                            Friend.GetUserBasicInfoByID();

                            pnlImageAndDescription.Visible = false;

                            //scrive a {1}: {2}
                            lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, UserInAction.Name, Friend.Name + " " + Friend.Surname, UserBoardElement.UserActionMessage);

                            break;
                        #endregion

                        #region UserLikesIngredient
                        case ActionTypes.LikeForNewIngredient:

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            UserBoard UserBoardObject = new UserBoard((int)ActionTypes.LikeForNewIngredient, (Guid)UserBoardElement.IDUserActionFather);
                            List<UserBoard> ObjectYouLikeList = new List<UserBoard>();
                            ObjectYouLikeList = UserBoardObject.GetObjectYouLike();

                            switch (ObjectYouLikeList[0].IDUserActionType)
                            { 
                                //User likes an Ingredient
                                case ActionTypes.NewIngredient:
                                    IDActionRelatedObject = ObjectYouLikeList[0].IDActionRelatedObject;

                                    IngredientLanguage Ingredient = new IngredientLanguage(IDActionRelatedObject, IDLanguage);
                                    Ingredient.QueryIngredientLanguageInfo();
                                    Ingredient.QueryIngredientInfo();

                                    try
                                    {
                                        btnImgRelatedObject.ImageUrl = Ingredient.IngredientImage.GetCompletePath(false,false,true);
                                    }
                                    catch
                                    { 
                                        //No image! Set here one of default.
                                    }



                                    hlRelatedObject.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingIngredient", AppDomain.CurrentDomain) + Ingredient.IngredientSingular.Replace(" ", "-") + "/" + Ingredient.IDIngredient.ToString()).ToLower();
                                    hlRelatedObject.Text = Ingredient.IngredientSingular;
                                    btnImgRelatedObject.PostBackUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingIngredient", AppDomain.CurrentDomain) + Ingredient.IngredientSingular.Replace(" ", "-") + "/" + Ingredient.IDIngredient.ToString()).ToLower();
                                    btnImgRelatedObject.AlternateText = Ingredient.IngredientSingular;
                                    lblDescriptionObject.Text = Ingredient.IngredientDescription;   //Creare Template Appropriato

                                    //{0} ha catalogato l'ingrediente {1}
                                    lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, UserInAction.Name, Ingredient.IngredientSingular);
                                    break;

                                 default:
                                    //In other case, hide the panel
                                    pnlUserBoardBlock.Visible = false;
                                    break;
                            }
                                

                            break;

                        #endregion

                        #region NewRecipeShare
                        //NEW RECIPE SHARE ON OWN WALL
                        case ActionTypes.NewRecipeShare:

                            //Social Sharing disabled for this type
                            ibtnShareUserBoard.Visible = false;
                            ibtnShareTwitter.Visible = false;
                            ibtnShareFacebook.Visible = false;

                            IDActionRelatedObject = UserBoardElement.IDActionRelatedObject;

                            UserBoard UserBoardAction = new UserBoard((Guid)IDActionRelatedObject, true);
                            UserBoardAction.GetUserActionInfo();

                            MyUser UserOwner = new MyUser(UserBoardAction.IDUser);
                            UserOwner.GetUserBasicInfoByID();

                            RecipeLanguage RecipeShared = new RecipeLanguage(UserBoardAction.IDActionRelatedObject, IDLanguage);
                            RecipeShared.QueryRecipeLanguageInfo();
                            RecipeShared.QueryBaseRecipeInfo();
                            RecipeShared.GetRecipeSteps();

                            try
                            {
                                btnImgRelatedObject.ImageUrl = RecipeShared.RecipeImage.GetCompletePath(false, false, true);
                            }
                            catch
                            {
                                //No image! Is not possible.
                            }

                            hlRelatedObject.NavigateUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + RecipeShared.RecipeName.Replace(" ","-") + "/" + RecipeShared.IDRecipe.ToString()).ToLower();
                            hlRelatedObject.Text = RecipeShared.RecipeName;
                            btnImgRelatedObject.PostBackUrl = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(IDLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + RecipeShared.RecipeName.Replace(" ", "-") + "/" + RecipeShared.IDRecipe.ToString()).ToLower();
                            btnImgRelatedObject.AlternateText = RecipeShared.RecipeName;
                            try
                            {
                                if (RecipeShared.RecipeSteps[0].Step.Length < 550)
                                {
                                    lblDescriptionObject.Text = RecipeShared.RecipeSteps[0].Step;
                                }
                                else
                                {
                                    lblDescriptionObject.Text = RecipeShared.RecipeSteps[0].Step.Substring(0, 547) + "...";
                                }
                            }
                            catch { }

                            //ha condiviso una ricetta di {0}

                            lblMessage.Text = string.Format(UserBoardElement.UserActionTypeTemplate, UserOwner.Name + " " + UserOwner.Surname);

                            break;
                        #endregion

                    }
                }
            
                //Repeater Likes
                if (!IsPublicProfile)
                {
                    upnLikes.Visible = true;
                    ctrlUserBoardBlockLikes.IDUserActionFather = new Guid(hfIDUserAction.Value);
                    ctrlUserBoardBlockLikes.TypeOfLike = hfTypeOfLike.Value;
                }
                else
                {
                    upnLikes.Visible = false;
                }

                hfControlBoardLoaded.Value = "true";
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in Load Control UserBoardBlock: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
            
        }

        #region GetComments
        public List<UserBoard> GetComments(Guid IDUserGuid, int IDLanguage, Guid IDUserActionFather)
        {
            
            int NumberOfResults = MyConvert.ToInt32(AppConfig.GetValue("BoardCommentsResultsNumber", AppDomain.CurrentDomain).ToString(), 5);

            UserBoard UserBoardElement = new UserBoard(ActionTypes.Comment, IDLanguage, IDUserGuid, IDUserActionFather, "asc", NumberOfResults);

            List<UserBoard> CommentsList = new List<UserBoard>();

            try
            {
                CommentsList = UserBoardElement.GetUsersBoardFatherOrSons();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> GetComments: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }

            return CommentsList;
        }
        #endregion

        #region InsertNewComment
        protected void ibtnComment_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtNewComment.Text))
                {
                    int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);

                    Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                    UserBoard NewUserBoardAction = new UserBoard(IDUserGuid, IDUserAction, ActionTypes.Comment, null, txtNewComment.Text, null, DateTime.UtcNow, IDLanguage);
                    NewUserBoardAction.InsertAction();

                    rptComments.DataSource = GetComments(IDUserGuid, IDLanguage, IDUserAction);
                    rptComments.DataBind();

                    lblCountComments.Text = CommentsTemplate(IDUserAction, IDLanguage, rptComments.Items.Count);

                    //Empty the textbox
                    txtNewComment.Text = "";

                    ActivateScrollbarComment();
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> Insert New Comment: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ActivateScrollbarComment
        protected void ActivateScrollbarComment()
        {
            try
            {
                //Activate scrollbar if necessary
                if (rptComments.Items.Count >= MyConvert.ToInt32(AppConfig.GetValue("NumberOfCommentsBeforeScrollActivate", AppDomain.CurrentDomain), 5))
                {
                    pnlComments.CssClass = "content";
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ActivateScroller('" + pnlComments.ClientID + "');", true);
                }
                else
                {
                    pnlComments.CssClass = "contentNoScroll";
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> ActivateScrollbarComment: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnDelete_Click
        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                UserBoard NewUserBoardAction = new UserBoard(IDUserAction, false);
                NewUserBoardAction.UpdateActionDeletedOn();
                
                pnlUserBoardBlock.Visible = false;
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> ibtnDelete_Click: " + ex.Message, Request["IDUserRequested"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnShareFacebook_Click
        protected void ibtnShareFacebook_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton _button = (ImageButton)sender;

                //string IDUserActionClicked = _button.CommandArgument.ToString();
                //Guid IDUserActionClickedGuid = new Guid(IDUserActionClicked);

                Guid IDUserActionClickedGuid = IDUserAction;

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                //Check if the user is registered to this Social Network.
                //If not, ask for autorization
                if (!MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Facebook))
                {
                    #region AuthorizeFacebook
                    //Your Website Url which needs to Redirected
                    string callBackUrl = "";
                    if (!String.IsNullOrEmpty(Request.Url.Query))
                    {
                        callBackUrl = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "");
                    }
                    else
                    {
                        callBackUrl = Request.Url.AbsoluteUri;
                    }

                    string FacebookClientId = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                    string FacebookScopes = AppConfig.GetValue("facebook_scopes", AppDomain.CurrentDomain);

                    Response.Redirect(string.Format("https://graph.facebook.com/oauth/" +
                      "authorize?client_id={0}&redirect_uri={1}&scope={2}",
                      FacebookClientId, callBackUrl, FacebookScopes));
                    #endregion
                }
                else
                {
                    //Get IDUserSocial And AccessToken
                    //*****************************************
                    MyUserSocial UserSocialInfo = new MyUserSocial(IDUserGuid, (int)SocialNetwork.Facebook);
                    UserSocialInfo.GetUserSocialInformations();

                    //UserSocialInfo.IDUserSocial;
                    //UserSocialInfo.AccessToken;
                    //*****************************************

                    //Get UserBoard Action Info - To know what kind of object is
                    UserBoard UserBoardActionInfo = new UserBoard(IDUserActionClickedGuid, false);
                    List<UserBoard> UserBoardActionInfoList = new List<UserBoard>();

                    UserBoardActionInfoList = UserBoardActionInfo.GetUserActionInfo();

                    Guid? IDActionRelatedObject = new Guid();
                    try
                    {
                        IDActionRelatedObject = new Guid(UserBoardActionInfoList[0].IDActionRelatedObject.ToString());

                        //Insert Statistics for sharing
                        try
                        {
                            switch (UserBoardActionInfoList[0].IDUserActionType)
                            {
                                case ActionTypes.NewRecipe:
                                case ActionTypes.NewRecipeShare:
                                case ActionTypes.RecipeCooked:
                                    MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDActionRelatedObject, StatisticsActionType.RC_RecipeSharedOnFacebookFromWall, "Recipe Shared on Facebook From Wall", Network.GetCurrentPageName(), "", Session.SessionID);
                                    NewStatistic.InsertNewRow();
                                    break;
                            }
                        }
                        catch
                        { }
                    }
                    catch
                    {
                        IDActionRelatedObject = null;
                    }

                    //Get info according to IDUserAction
                    SocialAction NewFBAction = new SocialAction(UserSocialInfo.IDUserSocial, UserSocialInfo.AccessToken, UserSocialInfo.RefreshToken, IDActionRelatedObject, UserBoardActionInfoList[0].IDUserActionType, IDUserActionClickedGuid);

                    string IDPost = NewFBAction.FB_PostOnWall();

                    //Error in publish: user removed authorization
                    if (String.IsNullOrEmpty(IDPost))
                    {
                        #region AskForAuthorizations
                        //Ask again for authorizations..
                        //Your Website Url which needs to Redirected
                        string callBackUrl = "";
                        if (!String.IsNullOrEmpty(Request.Url.Query))
                        {
                            callBackUrl = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "");
                        }
                        else
                        {
                            callBackUrl = Request.Url.AbsoluteUri;
                        }

                        string FacebookClientId = AppConfig.GetValue("facebook_appid", AppDomain.CurrentDomain);
                        string FacebookScopes = AppConfig.GetValue("facebook_scopes", AppDomain.CurrentDomain);

                        Response.Redirect(string.Format("https://graph.facebook.com/oauth/" +
                          "authorize?client_id={0}&redirect_uri={1}&scope={2}",
                          FacebookClientId, callBackUrl, FacebookScopes));
                        #endregion
                    }
                    else
                    {
                        //OK! SHARED :)
                        ibtnShareFacebook.Visible = false;

                        //Noty Alert
                        string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0061");

                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                            "noty({text: '" + NotificationText + "'});", true);
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "$(\"#" + imgSharingLoader.ClientID + "\").hide();", true);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //Noty Alert
                    
                    
                    string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0012");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "noty({text: '" + NotificationText + "'});", true);

                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> ibtnShareFacebook_Click: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnShareTwitter_Click
        protected void ibtnShareTwitter_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton _button = (ImageButton)sender;

                //string IDUserActionClicked = _button.CommandArgument.ToString();
                //Guid IDUserActionClickedGuid = new Guid(IDUserActionClicked);

                Guid IDUserActionClickedGuid = IDUserAction;

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                //Check if the user is registered to this Social Network.
                //If not, ask for autorization
                if (!MyUserSocial.IsUserRegisteredToThisSocial(IDUserGuid, (int)SocialNetwork.Twitter))
                {
                    string url = "/auth/auth.aspx?twitterauth=true";
                    Response.Redirect(url, true);
                }
                else
                {
                    //Get IDUserSocial And AccessToken
                    //*****************************************
                    MyUserSocial UserSocialInfo = new MyUserSocial(IDUserGuid, (int)SocialNetwork.Twitter);
                    UserSocialInfo.GetUserSocialInformations();

                    //UserSocialInfo.IDUserSocial;
                    //UserSocialInfo.AccessToken;
                    //*****************************************

                    //Get UserBoard Action Info - To know what kind of object is
                    UserBoard UserBoardActionInfo = new UserBoard(IDUserActionClickedGuid, false);
                    List<UserBoard> UserBoardActionInfoList = new List<UserBoard>();

                    UserBoardActionInfoList = UserBoardActionInfo.GetUserActionInfo();

                    Guid? IDActionRelatedObject = new Guid();
                    try
                    {
                        IDActionRelatedObject = new Guid(UserBoardActionInfoList[0].IDActionRelatedObject.ToString());

                        //Insert Statistics for sharing
                        try
                        {
                            switch (UserBoardActionInfoList[0].IDUserActionType)
                            {
                                case ActionTypes.NewRecipe:
                                case ActionTypes.NewRecipeShare:
                                case ActionTypes.RecipeCooked:
                                    MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDActionRelatedObject, StatisticsActionType.RC_RecipeSharedOnTwitterFromWall, "Recipe Shared on Twitter From Wall", Network.GetCurrentPageName(), "", Session.SessionID);
                                    NewStatistic.InsertNewRow();
                                    break;
                            }
                        }
                        catch
                        { }
                         
                    }
                    catch
                    {
                        IDActionRelatedObject = null;
                    }

                    //Get info according to IDUserAction
                    SocialAction NewTWAction = new SocialAction(UserSocialInfo.IDUserSocial, UserSocialInfo.AccessToken, UserSocialInfo.RefreshToken, IDActionRelatedObject, UserBoardActionInfoList[0].IDUserActionType, IDUserActionClickedGuid);

                    NewTWAction.TW_tweet();

                    //OK. SHARED :)
                    ibtnShareTwitter.Visible = false;

                    //Noty Alert
                    
                    
                    string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0062");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "noty({text: '" + NotificationText  + "'});", true);

                }

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "$(\"#" + imgSharingLoader.ClientID + "\").hide();", true);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //Noty Alert
                    
                    
                    string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0013");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "noty({text: '" + NotificationText + "'});", true);

                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> ibtnShareTwitter_Click: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region ibtnShareUserBoard_Click
        protected void ibtnShareUserBoard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton _button = (ImageButton)sender;

                //string IDUserActionClicked = _button.CommandArgument.ToString();
                //Guid IDUserActionClickedGuid = new Guid(IDUserActionClicked);

                Guid IDUserActionClickedGuid = IDUserAction;

                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                //Get UserBoard Action Info - To know what kind of object is
                UserBoard UserBoardActionInfo = new UserBoard(IDUserActionClickedGuid, false);
                List<UserBoard> UserBoardActionInfoList = new List<UserBoard>();

                UserBoardActionInfoList = UserBoardActionInfo.GetUserActionInfo();

                Guid? IDActionRelatedObject = new Guid();
                ActionTypes CorrectActionType = ActionTypes.NotSpecified;

                try
                {
                    IDActionRelatedObject = new Guid(UserBoardActionInfoList[0].IDActionRelatedObject.ToString());

                    

                    //Insert Statistics for sharing
                    try
                    {
                        switch (UserBoardActionInfoList[0].IDUserActionType)
                        {
                            case ActionTypes.NewRecipe:
                            case ActionTypes.NewRecipeShare:
                            case ActionTypes.RecipeCooked:
                                MyStatistics NewStatistic = new MyStatistics(IDUserGuid, IDActionRelatedObject, StatisticsActionType.RC_RecipeSharedOnOwnWallFromWall, "Recipe Shared on Own Wall from Wall", Network.GetCurrentPageName(), "", Session.SessionID);
                                NewStatistic.InsertNewRow();

                                CorrectActionType = ActionTypes.NewRecipeShare;
                                break;
                        }
                    }
                    catch
                    { }

                }
                catch
                {
                    IDActionRelatedObject = null;
                }

                //INSERT ACTION IN USER BOARD
                UserBoard NewActionInUserBoard = new UserBoard(IDUserGuid, null, CorrectActionType, IDUserAction, null, null, DateTime.UtcNow, MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1));
                ManageUSPReturnValue InsertActionResult = NewActionInUserBoard.InsertAction();

                //Noty Alert
                string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0064");

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                    "noty({text: '" + NotificationText + "'});", true);

                //OK SHARED
                ibtnShareUserBoard.Visible = false;

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "$(\"#" + imgSharingLoader.ClientID + "\").hide();", true);
            }
            catch(Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    //Noty Alert
                    
                    
                    string NotificationText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-ER-0020");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "noty({text: '" + NotificationText + "'});", true);

                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> ibtnShareUserBoard_Click: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region CommentsTemplate
        /// <summary>
        /// Create Complete Likes Template - Ex.: (34 Likes)
        /// </summary>
        /// <param name="IDUserActionFather"></param>
        /// <param name="IDLanguage"></param>
        /// <returns></returns>
        public string CommentsTemplate(Guid IDUserActionFather, int IDLanguage, int NumberOfComments)
        {
            try
            {
                UserBoard UserBoardElement = new UserBoard(ActionTypes.Comment, IDLanguage, IDUserActionFather);

                //int NumberOfComments = UserBoardElement.CountLikesOrComments();

                if (NumberOfComments == 1)
                {
                    return NumberOfComments + " " + UserBoardElement.UserActionTypeTemplate;
                }
                else
                {
                    return NumberOfComments + " " + UserBoardElement.UserActionTypeTemplatePlural;
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> CommentsTemplate: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }

                return "";
            }
        }
        #endregion

        #region UpdateCommentCount
        protected void UpdateCommentCount(object sender, EventArgs e)
        {
            try
            {
                int IDLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                Guid IDUserGuid = new Guid(Session["IDUser"].ToString());

                rptComments.DataSource = GetComments(IDUserGuid, IDLanguage, IDUserAction);
                rptComments.DataBind();

                lblCountComments.Text = CommentsTemplate(IDUserAction, IDLanguage, rptComments.Items.Count);

                ActivateScrollbarComment();
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "General Error in ctrlUserBoardBlock -> UpdateCommentCount: " + ex.Message, "", true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);
                }
                catch { }
            }
        }
        #endregion

        protected void LikeChangedEvent(object sender, EventArgs e)
        {
            ctrlUserBoardBlockLikes.StartLikeList();
        }
    }
}