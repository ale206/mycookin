using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientCheckedOut
    {
        public Guid IDIngredientLanguage { get; set; }
        public Guid IDIngredient { get; set; }
        public string IngredientSingular { get; set; }
        public Guid IngredientModifiedByUser { get; set; }
    }
}