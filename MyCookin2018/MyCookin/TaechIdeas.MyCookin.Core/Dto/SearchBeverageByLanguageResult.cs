using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchBeverageByLanguageResult
    {
        public Guid IngredientLanguageId { get; set; }
        public Guid IngredientId { get; set; }
        public string IngredientSingular { get; set; }
        public string FriendlyId { get; set; }
    }
}