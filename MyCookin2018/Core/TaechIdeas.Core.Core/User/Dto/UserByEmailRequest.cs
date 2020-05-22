using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UserByEmailRequest
    {
        [ApiMember(Name = "Email", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Email Required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Email Length must be between 1 and 50 characters")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}