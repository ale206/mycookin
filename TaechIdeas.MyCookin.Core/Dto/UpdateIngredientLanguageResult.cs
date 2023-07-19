using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class UpdateIngredientLanguageResult : TokenRequiredInput
    {
        public bool IngredientLanguageUpdated { get; set; }
    }
}