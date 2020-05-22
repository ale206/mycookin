using System.Collections.Generic;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.Core.User
{
    public interface IUserRepository
    {
        NewUserOut NewUser(NewUserIn newUserIn);

        LoginUserOut LoginUser(LoginUserIn loginUserIn);

        UsernameAlreadyExistsOut UsernameAlreadyExists(UsernameAlreadyExistsIn usernameAlreadyExistsIn);

        EmailAlreadyExistsOut EmailAlreadyExists(EmailAlreadyExistsIn emailAlreadyExistsIn);

        UpdateLastLogonAndSetUserAsOnLineOut UpdateLastLogonAndSetUserAsOnLine(UpdateLastLogonAndSetUserAsOnLineIn updateLastLogonAndSetUserAsOnLineIn);

        LogoutUserOut LogoutUser(LogoutUserIn logoutUserIn);

        UserIdFromSocialLoginsOut UserIdFromSocialLogins(UserIdFromSocialLoginsIn userIdFromSocialLoginsIn);

        UserByIdOut UserById(UserByIdIn userByIdIn);

        UpdateSocialTokensOut UpdateSocialTokens(UpdateSocialTokensIn updateSocialTokensIn);

        UserByEmailOut UserByEmail(UserByEmailIn userByEmailIn);

        NewSocialLoginOut NewSocialLogin(NewSocialLoginIn newSocialLoginIn);

        UserIdByUserTokenOut UserIdByTokenId(UserIdByUserTokenIn userIdByUserTokenIn);

        ActivateUserOut ActivateUser(ActivateUserIn activateUserIn);

        LanguageIdByLanguageCodeOut LanguageIdByLanguageCode(LanguageIdByLanguageCodeIn languageIdByLanguageCodeIn);

        LanguageCodeByLanguageIdOut LanguageCodeByLanguageId(LanguageCodeByLanguageIdIn languageCodeByLanguageIdIn);

        IEnumerable<SecurityQuestionsByLanguageOut> SecurityQuestionsByLanguage(SecurityQuestionsByLanguageIn securityQuestionsByLanguageIn);
        IEnumerable<GenderByLanguageOut> GendersByLanguage(GenderByLanguageIn genderByLanguageIn);
        DeleteAccountOut DeleteAccount(DeleteAccountIn deleteAccountIn);
        UpdateUserInfoOut UpdateUserInfo(UpdateUserInfoIn updateUserInfoIn);

        IEnumerable<GetSecurityUserGroupsOut> GetSecurityUserGroups(GetSecurityUserGroupsIn getSecurityUserGroupsIn);

        UserByUsernameOut UserByUsername(UserByUsernameIn userByUsernameIn);

        GetLanguageListOut GetLanguageList(GetLanguageListIn getLanguageListIn);

        NumberOfUsersOut NumberOfUsers(NumberOfUsersIn numberOfUsersIn);

        UpdatePasswordOut UpdatePassword(UpdatePasswordIn updatePasswordIn);

        GetSecurityQuestionOut GetSecurityQuestion(GetSecurityQuestionIn getSecurityQuestionIn);

        UpdateLastProfileUpdateDateOut UpdateLastProfileUpdateDate(UpdateLastProfileUpdateDateIn updateLastProfileUpdateDateIn);

        UpdateTemporarySecurityAnswerOut UpdateTemporarySecurityAnswer(UpdateTemporarySecurityAnswerIn updateTemporarySecurityAnswerIn);

        UpdateSecurityAnswerOut UpdateSecurityAnswer(UpdateSecurityAnswerIn updateSecurityAnswerIn);

        GetIdGenderByGenderNameAndIdLanguageOut GetIdGenderByGenderNameAndIdLanguage(GetIdGenderByGenderNameAndIdLanguageIn getIdGenderByGenderNameAndIdLanguageIn);

        DeleteSecurityQuestionAndAnswerOut DeleteSecurityQuestionAndAnswer(DeleteSecurityQuestionAndAnswerIn deleteSecurityQuestionAndAnswerIn);

        IEnumerable<FindUserByWordsOut> FindUserByWords(FindUserByWordsIn findUserByWordsIn);

        IEnumerable<SuggestedUsersOut> SuggestedUsers(SuggestedUsersIn suggestedUsersIn);

        IEnumerable<NewUsersForMailchimpOut> NewUsersForMailchimp(NewUsersForMailchimpIn newUsersForMailchimpIn);

        //Following
        FollowUserOut FollowUser(FollowUserIn followUserIn);
        DefollowUserOut DefollowUser(DefollowUserIn defollowUserIn);
        IEnumerable<FollowingOut> Following(FollowingIn followingIn);
        IEnumerable<FollowersOut> Followers(FollowersIn followersIn);
        CheckFollowingOut CheckFollowing(CheckFollowingIn checkFollowingIn);
        NumberOfFollowersOut NumberOfFollowers(NumberOfFollowersIn numberOfFollowersIn);
        NumberOfFollowingOut NumberOfFollowing(NumberOfFollowingIn numberOfFollowingIn);

        //Friendship
        IEnumerable<UsersYouMayKnowByFollowerOut> UsersYouMayKnowByFollower(UsersYouMayKnowByFollowerIn usersYouMayKnowByFollowerIn);
        IEnumerable<FriendshipRequestsOut> FriendshipRequests(FriendshipRequestsIn friendshipRequestsIn);
        IEnumerable<FriendsOut> Friends(FriendsIn friendsIn);
        IEnumerable<CommonFriendsOut> CommonFriends(CommonFriendsIn commonFriendsIn);
        IEnumerable<BlockedFriendsOut> BlockedFriends(BlockedFriendsIn blockedFriendsIn);
        RequestOrAcceptFriendshipOut RequestOrAcceptFriendship(RequestOrAcceptFriendshipIn requestOrAcceptFriendshipIn);
        DeclineFriendshipOut DeclineFriendship(DeclineFriendshipIn declineFriendshipIn);
        BlockUserOut BlockUser(BlockUserIn blockUserIn);
        RemoveBlockUserOut RemoveBlockUser(RemoveBlockUserIn removeBlockUserIn);
        RemoveFriendshipOut RemoveFriendship(RemoveFriendshipIn removeFriendshipIn);
        RemoveFriendshipForUseWithFollowOut RemoveFriendshipForUseWithFollow(RemoveFriendshipForUseWithFollowIn removeFriendshipForUseWithFollowIn);
        CheckFriendshipOut CheckFriendship(CheckFriendshipIn checkFriendshipIn);
        CheckUserBlockedOut CheckUserBlocked(CheckUserBlockedIn checkUserBlockedIn);
        FriendshipCompletedDateOut FriendshipCompletedDate(FriendshipCompletedDateIn friendshipCompletedDateIn);
        NumberOfFriendsOut NumberOfFriends(NumberOfFriendsIn numberOfFriendsIn);
        IEnumerable<FindFriendsByWordsOut> FindFriendsByWords(FindFriendsByWordsIn findFriendsByWordsIn);

        //UserNotification
        IEnumerable<NotificationListOut> NotificationList(NotificationListIn notificationListIn);
        UpdateUserNotificationSettingOut UpdateUserNotificationSetting(UpdateUserNotificationSettingIn notificationSettingIn);
        IsNotificationEnabledOut IsNotificationEnabled(IsNotificationEnabledIn isNotificationEnabledIn);

        //Social
        UserSocialInformationOut UserSocialInformation(UserSocialInformationIn userSocialInformationIn);
        IEnumerable<SocialFriendsByIdUserOut> SocialFriendsByIdUser(SocialFriendsByIdUserIn socialFriendsByIdUserIn);
        IEnumerable<SocialFriendsOut> SocialFriends(SocialFriendsIn socialFriendsIn);
        IEnumerable<UsersIdWithOldFriendsRetrievedOnOut> UsersIdWithOldFriendsRetrievedOn(UsersIdWithOldFriendsRetrievedOnIn usersIdWithOldFriendsRetrievedOnIn);
        UserIdSocialFromIdUserAndIdSocialNetworkOut UserIdSocialFromIdUserAndIdSocialNetwork(UserIdSocialFromIdUserAndIdSocialNetworkIn userIdSocialFromIdUserAndIdSocialNetworkIn);
        UpdateFriendsRetrievedOnOut UpdateFriendsRetrievedOn(UpdateFriendsRetrievedOnIn updateFriendsRetrievedOnIn);
        MemorizeSocialContactFriendOut MemorizeSocialContactFriend(MemorizeSocialContactFriendIn memorizeSocialContactFriendIn);
        IsUserRegisteredToThisSocialOut IsUserRegisteredToThisSocial(IsUserRegisteredToThisSocialIn isUserRegisteredToThisSocialIn);
        LoginUserByIdOut LoginUserById(LoginUserByIdIn loginUserByIdIn);
    }
}