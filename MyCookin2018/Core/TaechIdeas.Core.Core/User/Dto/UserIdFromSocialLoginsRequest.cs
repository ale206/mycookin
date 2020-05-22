using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UserIdFromSocialLoginsRequest
    {
        [ApiMember(Name = "UserIdOnSocial", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "UserIdOnSocial Required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "UserIdOnSocial Length must be between 1 and 50 characters")]
        public string UserIdOnSocial { get; set; }

        [ApiMember(Name = "SocialNetworkId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "SocialNetworkId Required")]
        [Range(1, 3, ErrorMessage = "SocialNetworkId must be between 1 and 3")]
        public int SocialNetworkId { get; set; }
    }
}