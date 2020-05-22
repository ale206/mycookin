using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdateSocialTokensRequest
    {
        [ApiMember(Name = "SocialNetworkId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "SocialNetworkId Required")]
        [Range(1, 3, ErrorMessage = "SocialNetworkId must be between 1 and 3")]
        public int SocialNetworkId { get; set; }

        [ApiMember(Name = "UserId", DataType = "guid", IsRequired = true)]
        [Required(ErrorMessage = "UserId Required")]
        public Guid UserId { get; set; }

        [ApiMember(Name = "AccessToken", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "AccessToken Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "AccessToken Length must be between 1 and 100 characters")]
        public string AccessToken { get; set; }

        [ApiMember(Name = "RefreshToken", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "RefreshToken Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "RefreshToken Length must be between 1 and 100 characters")]
        public string RefreshToken { get; set; }
    }
}