using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientsToTranslateOut
    {
        public Guid IDIngredientLanguage { get; set; }
        public Guid IDIngredient { get; set; }
        public int IDLanguage { get; set; }
        public string IngredientSingular { get; set; }
        public string IngredientPlural { get; set; }
        public string IngredientDescription { get; set; }
    }
}