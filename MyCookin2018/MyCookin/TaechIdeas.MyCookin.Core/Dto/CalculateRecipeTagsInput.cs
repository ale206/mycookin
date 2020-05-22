using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class CalculateRecipeTagsInput
    {
        public Guid RecipeId { get; set; }
        public int LanguageId { get; set; }
        public bool IncludeIngredientList { get; set; }
        public bool IncludeDynamicProp { get; set; }
        public bool IncludeIngredientCategory { get; set; }
    }
}