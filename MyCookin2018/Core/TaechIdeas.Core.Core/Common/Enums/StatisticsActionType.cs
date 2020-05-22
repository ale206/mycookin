namespace TaechIdeas.Core.Core.Common.Enums
{
    public enum StatisticsActionType
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
}