using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdateSocialTokensIn
    {
        public int SocialNetworkId { get; set; }
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}