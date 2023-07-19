using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class NewRecipePropertyRequest
    {
        public RecipePropertyTypeRequest RecipePropertyType { get; set; }

        public int OrderPosition { get; set; }

        [ApiMember(Name = "IsEnabled", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for IsEnabled")]
        public bool IsEnabled { get; set; }

        [ApiMember(Name = "LanguageId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "LanguageId Required")]
        [Range(1, 3, ErrorMessage = "LanguageId must be between 1 and 3")]
        public int LanguageId { get; set; }

        [ApiMember(Name = "Property", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "Property Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Property Length must be between 1 and 100 characters")]
        public string Property { get; set; }

        [ApiMember(Name = "RecipePropertyToolTip", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "RecipePropertyToolTip Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "RecipePropertyToolTip Length must be between 1 and 100 characters")]
        public string RecipePropertyToolTip { get; set; }
    }
}