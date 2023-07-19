using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SimilarRecipesInput
    {
        public SimilarRecipesInput(Guid? recipeId)
        {
            RecipeId = recipeId;
        }

        public string RecipeName { get; set; }
        public bool? Vegan { get; set; }
        public bool? Vegetarian { get; set; }
        public bool? GlutenFree { get; set; }
        public int LanguageId { get; set; }
        public Guid? RecipeId { get; set; }
    }
}