using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MyLogin
    {
        [ApiMember(Name = "Email", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Email Required")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Email Length must be between 1 and 30 characters")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ApiMember(Name = "Password", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Password Required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password Length must be between 1 and 30 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ApiMember(Name = "Offset", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "Offset Required")]
        public int Offset { get; set; }

        [ApiMember(Name = "Ip", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Ip Required")]
        [StringLength(15, ErrorMessage = "Ip Length must be 15 characters")]
        public string IpAddress { get; set; }

        [ApiMember(Name = "IsPasswordHashed", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "IsPasswordHashed Required")]
        public bool IsPasswordHashed { get; set; }

        [ApiMember(Name = "LanguageId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "LanguageId Required")]
        [Range(1, 3, ErrorMessage = "LanguageId must be between 1 and 3")]
        public int LanguageId { get; set; }
    }
}