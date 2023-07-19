using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class StepsForRecipeOut
    {
        public Guid IdRecipeStep { get; set; }
        public Guid IdRecipeLanguage { get; set; }
        public string StepGroup { get; set; }
        public int StepNumber { get; set; }
        public string RecipeStep { get; set; }
        public int? StepTimeMinute { get; set; }
        public Guid? IDRecipeStepImage { get; set; }
    }
}