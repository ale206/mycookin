using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchIngredientByLanguageAndNameInput : TokenRequiredInput
    {
        public string IngredientName { get; set; }
        public int LanguageId { get; set; }
    }
}