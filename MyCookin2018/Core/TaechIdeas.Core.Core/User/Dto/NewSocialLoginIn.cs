using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class NewSocialLoginIn
    {
        public int SocialNetworkId { get; set; }
        public Guid UserId { get; set; }
        public string UserIdOnSocial { get; set; }
        public string Link { get; set; }
        public bool VerifiedEmail { get; set; }
        public string PictureUrl { get; set; }
        public string Locale { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}