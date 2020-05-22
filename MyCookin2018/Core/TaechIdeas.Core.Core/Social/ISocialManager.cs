using System.Collections.Generic;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.Core.Social
{
    public interface ISocialManager
    {
        UserIdFromSocialLoginsOutput UserIdFromSocialLogins(UserIdFromSocialLoginsInput userIdFromSocialLoginsInput);
        UpdateSocialTokensOutput UpdateSocialTokens(UpdateSocialTokensInput updateSocialTokensInput);
        NewSocialLoginOutput NewSocialLogin(NewSocialLoginInput newSocialLoginInput);
        UserSocialInformationOutput UserSocialInformation(UserSocialInformationInput userSocialInformationInput);
        IEnumerable<SocialFriendsByIdUserOutput> SocialFriendsByIdUser(SocialFriendsByIdUserInput socialFriendsByIdUserInput);
        IEnumerable<SocialFriendsOutput> SocialFriends(SocialFriendsInput socialFriendsInput);
        IEnumerable<UsersIdWithOldFriendsRetrievedOnOutput> UsersIdWithOldFriendsRetrievedOn(UsersIdWithOldFriendsRetrievedOnInput usersIdWithOldFriendsRetrievedOnInput);
        UserIdSocialFromIdUserAndIdSocialNetworkOutput UserIdSocialFromIdUserAndIdSocialNetwork(UserIdSocialFromIdUserAndIdSocialNetworkInput userIdSocialFromIdUserAndIdSocialNetworkInput);
        UpdateFriendsRetrievedOnOutput UpdateFriendsRetrievedOn(UpdateFriendsRetrievedOnInput updateFriendsRetrievedOnInput);
        FriendsFromSocialNetworkOutput FriendsFromSocialNetwork(FriendsFromSocialNetworkInput friendsFromSocialNetworkInput);
        MemorizeGoogleContactsOutput MemorizeGoogleContacts(MemorizeGoogleContactsInput memorizeGoogleContactsInput);
        MemorizeFacebookContactOutput MemorizeFacebookContact(MemorizeFacebookContactInput memorizeFacebookContactInput);
        MemorizeSocialContactFriendOutput MemorizeSocialContactFriend(MemorizeSocialContactFriendInput memorizeSocialContactFriendInput);
        IsUserRegisteredToThisSocialOutput IsUserRegisteredToThisSocial(IsUserRegisteredToThisSocialInput isUserRegisteredToThisSocialInput);
    }
}