using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UserSocialInformationOutput
    {
        public Guid IdUser { get; set; }
        public int IdSocialNetwork { get; set; }
        public string Link { get; set; }
        public bool VerifiedEmail { get; set; }
        public string PictureUrl { get; set; }
        public string Locale { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? FriendsRetrievedOn { get; set; }
        public string IdUserSocial { get; set; }
    }
}