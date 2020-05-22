using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class SocialFriendsInput
    {
        public Guid UserId { get; set; }
        public int SocialNetworkId { get; set; }
    }
}