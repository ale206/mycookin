using System.Collections.Generic;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class UpdateRecipeIn
    {
        public UpdateRecipeLanguageIn UpdateRecipeLanguageIn { get; set; }
        public UpdateRecipeLanguageStepIn UpdateRecipeLanguageStepIn { get; set; }

        public IEnumerable<UpdateRecipeIngredientIn> UpdateRecipeWithIngredientIn { get; set; }
    }
}