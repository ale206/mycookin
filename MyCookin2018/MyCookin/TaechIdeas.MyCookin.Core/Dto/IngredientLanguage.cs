using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientLanguage : Ingredient
    {
        public Guid IdIngredientLanguage { get; set; }

        public int IdLanguage { get; set; }
        public string IngredientSingular { get; set; }
        public string IngredientPlural { get; set; }
        public string IngredientDescription { get; set; }
        public bool IsAutoTranslate { get; set; }
        public int? GeoIdRegion { get; set; }
    }
}