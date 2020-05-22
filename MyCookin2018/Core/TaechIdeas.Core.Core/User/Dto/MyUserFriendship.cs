using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MyUserFriendship
    {
        public MyUser IdUserFriend1 { get; set; }
        public MyUser IdUserFriend2 { get; set; }

        public Guid IdUserFriend { get; set; }
        public DateTime FriendshipCompletedDate { get; set; }
        public DateTime FollowingDate { get; set; }
        public Guid IdUserFollower { get; set; }
    }
}