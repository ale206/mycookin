using System.ComponentModel.DataAnnotations;
using ServiceStack;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchRecipesRequest : PaginationFieldsRequest
    {
        [ApiMember(Name = "LanguageId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "LanguageId Required")]
        [Range(1, 3, ErrorMessage = "LanguageId must be between 1 and 3")]
        public int LanguageId { get; set; }

        [ApiMember(Name = "Vegan", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for Vegan")]
        public bool Vegan { get; set; }

        [ApiMember(Name = "Vegetarian", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for Vegetarian")]
        public bool Vegetarian { get; set; }

        [ApiMember(Name = "GlutenFree", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for GlutenFree")]
        public bool GlutenFree { get; set; }

        [ApiMember(Name = "Light", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for Light")]
        public bool Light { get; set; }

        [ApiMember(Name = "Quick", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "Insert true or false for Quick")]
        public bool Quick { get; set; }

        [ApiMember(Name = "ContractSigned", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "ContractSigned Required")]
        public bool IsEmptyFridge { get; set; }

        [ApiMember(Name = "IncludeIngredients", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "IncludeIngredients Required")]
        public bool IncludeIngredients { get; set; }

        [ApiMember(Name = "IncludeSteps", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "IncludeSteps Required")]
        public bool IncludeSteps { get; set; }

        [ApiMember(Name = "IncludeProperties", DataType = "bool", IsRequired = true)]
        [Required(ErrorMessage = "IncludeProperties Required")]
        public bool IncludeProperties { get; set; }
    }
}