using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipesToTranslateOut
    {
        public Guid IDRecipeLanguage { get; set; }
        public Guid IDRecipe { get; set; }
        public int IDLanguage { get; set; }
        public string RecipeName { get; set; }
        public string RecipeHistory { get; set; }
        public string RecipeNote { get; set; }
        public string RecipeSuggestion { get; set; }
        public string RecipeLanguageTags { get; set; }
    }
}