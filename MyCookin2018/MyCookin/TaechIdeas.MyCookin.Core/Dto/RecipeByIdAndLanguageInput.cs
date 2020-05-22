using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeByIdAndLanguageInput
    {
        public Guid RecipeId { get; set; }
        public int LanguageId { get; set; }
        public bool IncludeSteps { get; set; } = false;
        public bool IncludeIngredients { get; set; } = false;
        public bool IncludeProperties { get; set; } = false;
    }
}