using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class StepForRecipeOutput
    {
        public Guid RecipeStepId { get; set; }
        public Guid RecipeLanguageId { get; set; }
        public string StepGroup { get; set; }
        public int StepNumber { get; set; }
        public string RecipeStep { get; set; }
        public int? StepTimeMinutes { get; set; }
        public Guid? RecipeStepImageId { get; set; }

        public string ImageUrl { get; set; }
    }
}