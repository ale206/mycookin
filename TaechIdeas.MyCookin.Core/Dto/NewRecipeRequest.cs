using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class NewRecipeRequest
    {
        [ApiMember(Name = "RecipeName", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "RecipeName Required")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Recipe Name Length must be between 1 and 150 characters")]
        public string RecipeName { get; set; }

        [ApiMember(Name = "LanguageId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "LanguageId Required")]
        [Range(1, 3, ErrorMessage = "LanguageId must be between 1 and 3")]
        public int LanguageId { get; set; }

        [ApiMember(Name = "ImageId", DataType = "guid", IsRequired = true)]
        [Required(ErrorMessage = "ImageId Required")]
        public Guid ImageId { get; set; }

        public CheckTokenRequest CheckTokenRequest { get; set; }
    }
}