using System.Collections.Generic;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.Core.User
{
    public interface IFriendshipManager
    {
        IEnumerable<UsersYouMayKnowByFollowerOutput> UsersYouMayKnowByFollower(UsersYouMayKnowByFollowerInput usersYouMayKnowByFollowerInput);
        IEnumerable<FriendshipRequestsOutput> FriendshipRequests(FriendshipRequestsInput friendshipRequestsInput);
        IEnumerable<FriendsOutput> Friends(FriendsInput friendsInput);
        IEnumerable<CommonFriendsOutput> CommonFriends(CommonFriendsInput commonFriendsInput);
        IEnumerable<BlockedFriendsOutput> BlockedFriends(BlockedFriendsInput blockedFriendsInput);
        RequestOrAcceptFriendshipOutput RequestOrAcceptFriendship(RequestOrAcceptFriendshipInput requestOrAcceptFriendshipInput);
        DeclineFriendshipOutput DeclineFriendship(DeclineFriendshipInput declineFriendshipInput);
        BlockUserOutput BlockUser(BlockUserInput blockUserInput);
        RemoveBlockUserOutput RemoveBlockUser(RemoveBlockUserInput removeBlockUserInput);
        RemoveFriendshipOutput RemoveFriendship(RemoveFriendshipInput removeFriendshipInput);
        RemoveFriendshipForUseWithFollowOutput RemoveFriendshipForUseWithFollow(RemoveFriendshipForUseWithFollowInput removeFriendshipForUseWithFollowInput);
        CheckFriendshipOutput CheckFriendship(CheckFriendshipInput checkFriendshipInput);
        CheckUserBlockedOutput CheckUserBlocked(CheckUserBlockedInput checkUserBlockedInput);
        FriendshipCompletedDateOutput FriendshipCompletedDate(FriendshipCompletedDateInput friendshipCompletedDateInput);
        DaysToYearsOutput DaysToYears(DaysToYearsInput daysToYearsInput);
        DaysWithoutYearsOutput DaysWithoutYears(DaysWithoutYearsInput daysWithoutYearsInput);
        FriendshipTimeOutput FriendshipTime(FriendshipTimeInput friendshipTimeInput);
        FriendShipTimeStringOutput FriendShipTimeString(FriendShipTimeStringInput friendShipTimeStringInput);
        NumberOfFriendsOutput NumberOfFriends(NumberOfFriendsInput numberOfFriendsInput);
        IEnumerable<FindFriendsByWordsOutput> FindFriendsByWords(FindFriendsByWordsInput findFriendsByWordsInput);
    }
}