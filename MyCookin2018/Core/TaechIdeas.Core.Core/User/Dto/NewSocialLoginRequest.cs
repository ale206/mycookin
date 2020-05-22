using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class NewSocialLoginRequest
    {
        [ApiMember(Name = "UserId", DataType = "guid", IsRequired = true)]
        [Required(ErrorMessage = "UserId Required")]
        public Guid UserId { get; set; }

        [ApiMember(Name = "SocialNetworkId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "SocialNetworkId Required")]
        [Range(1, 3, ErrorMessage = "SocialNetworkId must be between 1 and 3")]
        public int SocialNetworkId { get; set; }

        [ApiMember(Name = "Link", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Link Required")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Link Length must be between 1 and 300 characters")]
        public string Link { get; set; }

        [ApiMember(Name = "VerifiedEmail", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for VerifiedEmail")]
        public bool VerifiedEmail { get; set; }

        [ApiMember(Name = "PictureUrl", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "PictureUrl Required")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "PictureUrl Length must be between 1 and 300 characters")]
        public string PictureUrl { get; set; }

        [ApiMember(Name = "Locale", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Locale Required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Locale Length must be between 1 and 20 characters")]
        public string Locale { get; set; }

        [ApiMember(Name = "AccessToken", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "AccessToken Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "AccessToken Length must be between 1 and 100 characters")]
        public string AccessToken { get; set; }

        [ApiMember(Name = "RefreshToken", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "RefreshToken Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "RefreshToken Length must be between 1 and 100 characters")]
        public string RefreshToken { get; set; }

        [ApiMember(Name = "FriendsRetrievedOn", DataType = "DateTime")]
        [DataType(DataType.Date)]
        public DateTime? FriendsRetrievedOn { get; set; }

        [ApiMember(Name = "UserIdOnSocial", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "UserIdOnSocial Required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "UserIdOnSocial Length must be between 1 and 50 characters")]
        public string UserIdOnSocial { get; set; }
    }
}