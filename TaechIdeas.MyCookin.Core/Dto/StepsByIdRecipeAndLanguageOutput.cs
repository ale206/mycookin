using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class StepsByIdRecipeAndLanguageOutput
    {
        public Guid RecipeStepId { get; set; }
        public string StepGroup { get; set; }
        public int StepNumber { get; set; }
        public string RecipeStep { get; set; }
        public int? StepTimeMinute { get; set; }
        public Guid LanguageId { get; set; }

        public Guid? RecipeStepImageId { get; set; }
        public string ImageUrl { get; set; }
    }
}