using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class NewRecipeLanguageStepsRequest
    {
        public Guid RecipeLanguageId { get; set; }
        public string StepGroup { get; set; }
        public string StepNumber { get; set; }
        public string Description { get; set; }
        public int Minutes { get; set; }
        public Guid RecipeStepImageId { get; set; }
        public CheckTokenRequest CheckTokenRequest { get; set; }
    }
}