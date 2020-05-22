using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeLanguageListInput : PaginationFieldsInput
    {
        public int LanguageId { get; set; }
    }
}