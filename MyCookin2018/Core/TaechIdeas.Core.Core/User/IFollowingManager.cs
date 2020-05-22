using System.Collections.Generic;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.Core.User
{
    public interface IFollowingManager
    {
        FollowUserOutput FollowUser(FollowUserInput followUserInput);
        DefollowUserOutput DefollowUser(DefollowUserInput defollowUserInput);
        IEnumerable<FollowingOutput> Following(FollowingInput followingInput);
        IEnumerable<FollowersOutput> Followers(FollowersInput followersInput);
        CheckFollowingOutput CheckFollowing(CheckFollowingInput checkFollowingInput);
        NumberOfFollowersOutput NumberOfFollowers(NumberOfFollowersInput numberOfFollowersInput);
        NumberOfFollowingOutput NumberOfFollowing(NumberOfFollowingInput numberOfFollowingInput);
    }
}