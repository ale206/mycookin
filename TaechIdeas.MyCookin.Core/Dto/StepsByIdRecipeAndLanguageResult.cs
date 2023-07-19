using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class StepsByIdRecipeAndLanguageResult
    {
        public Guid RecipeStepId { get; set; }
        public string StepGroup { get; set; }
        public int StepNumber { get; set; }
        public string RecipeStep { get; set; }
        public int? StepTimeMinutes { get; set; }
        public string ImageUrl { get; set; }
    }
}