using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class FollowUserIn
    {
        public Guid UserIdFriend1 { get; set; }
        public Guid UserIdFriend2 { get; set; }
    }
}