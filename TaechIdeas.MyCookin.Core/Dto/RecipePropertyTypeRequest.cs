using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipePropertyTypeRequest
    {
        [ApiMember(Name = "IsDishType", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for IsDishType")]
        public bool IsDishType { get; set; }

        [ApiMember(Name = "IsCookingType", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for IsCookingType")]
        public bool IsCookingType { get; set; }

        [ApiMember(Name = "IsColorType", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for IsColorType")]
        public bool IsColorType { get; set; }

        [ApiMember(Name = "IsEatType", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for IsEatType")]
        public bool IsEatType { get; set; }

        [ApiMember(Name = "IsUseType", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for IsUseType")]
        public bool IsUseType { get; set; }

        [ApiMember(Name = "IsPeriodType", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for IsPeriodType")]
        public bool IsPeriodType { get; set; }

        [ApiMember(Name = "OrderPosition", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for OrderPosition")]
        public int OrderPosition { get; set; }

        [ApiMember(Name = "LanguageId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "LanguageId Required")]
        [Range(1, 3, ErrorMessage = "LanguageId must be between 1 and 3")]
        public int IdLanguage { get; set; }

        [ApiMember(Name = "PropertyType", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "PropertyType Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "PropertyType Length must be between 1 and 100 characters")]
        public string PropertyType { get; set; }

        [ApiMember(Name = "RecipePropertyTypeToolTip", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "RecipePropertyTypeToolTip Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "RecipePropertyTypeToolTip Length must be between 1 and 100 characters")]
        public string RecipePropertyTypeToolTip { get; set; }
    }
}