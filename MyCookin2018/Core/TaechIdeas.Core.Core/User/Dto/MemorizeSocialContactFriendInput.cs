using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MemorizeSocialContactFriendInput
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
        public Guid IdSocialLogin { get; set; }
        public DateTime? LastTimeContacted { get; set; }
        public bool? ContactAgain { get; set; }
        public string FullName { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Emails { get; set; }
        public string Phones { get; set; }
        public string PhotoUrl { get; set; }
        public string IdUserOnSocial { get; set; }
    }
}