using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class NewUserInput
    {
        [ApiMember(Name = "Name", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Name Required")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Name Length must be between 1 and 30 characters")]
        //[RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage =
        //    "Numbers and special characters are not allowed in the name.")]
        public string Name { get; set; }

        [ApiMember(Name = "Surname", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Surname Required")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Surname Length must be between 1 and 30 characters")]
        public string Surname { get; set; }

        [ApiMember(Name = "Username", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Username Required")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Username Length must be between 1 and 30 characters")]
        public string Username { get; set; }

        [ApiMember(Name = "Password", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Password Required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password Length must be between 1 and 30 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ApiMember(Name = "ContractSigned", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "ContractSigned Required")]
        public bool ContractSigned { get; set; }

        [ApiMember(Name = "DateOfBirth", DataType = "DateTime")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [ApiMember(Name = "Email", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Email Required")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Email Length must be between 1 and 30 characters")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ApiMember(Name = "RecipeLanguageId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "RecipeLanguageId Required")]
        [Range(1, 3, ErrorMessage = "RecipeLanguageId must be between 1 and 3")]
        public int LanguageId { get; set; }

        [ApiMember(Name = "CityId", DataType = "int")]
        [Range(1, int.MaxValue, ErrorMessage = "CityId must be greater than 0")]
        public int? CityId { get; set; }

        [ApiMember(Name = "Offset", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "Offset Required")]
        public int Offset { get; set; }

        [ApiMember(Name = "Mobile", DataType = "string")]
        [StringLength(30, ErrorMessage = "Mobile Length must be 30 characters max")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [ApiMember(Name = "Ip", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Ip Required")]
        [StringLength(15, ErrorMessage = "Ip Length must be 15 characters")]
        public string Ip { get; set; }

        [ApiMember(Name = "GenderId", DataType = "int")]
        [Range(1, 2, ErrorMessage = "GenderId must be 1 or 2")]
        public int? GenderId { get; set; }

        [ApiMember(Name = "WebsiteId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "WebsiteId Required")]
        public MyWebsite WebsiteId { get; set; }
    }
}