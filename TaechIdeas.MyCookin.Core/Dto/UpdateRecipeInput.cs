using System.Collections.Generic;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class UpdateRecipeInput
    {
        public UpdateRecipeLanguageInput UpdateRecipeLanguageInput { get; set; }
        public UpdateRecipeLanguageStepInput UpdateRecipeLanguageStepInput { get; set; }

        public IEnumerable<UpdateRecipeIngredientInput> UpdateRecipeWithIngredientInput { get; set; }
    }
}