using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class FriendsFromSocialNetworkIn
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public int idSocialNetwork { get; set; }
        public Guid UserId { get; set; }
    }
}