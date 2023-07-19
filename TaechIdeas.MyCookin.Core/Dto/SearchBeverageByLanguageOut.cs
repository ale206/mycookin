using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchBeverageByLanguageOut
    {
        public Guid IDIngredientLanguage { get; set; }
        public Guid IDIngredient { get; set; }
        public string IngredientSingular { get; set; }
        public string FriendlyId { get; set; }
    }
}