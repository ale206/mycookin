using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeByIdAndLanguageOutput
    {
        public Guid RecipeLanguageId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeNote { get; set; }
        public string RecipeSuggestion { get; set; }
        public int LanguageId { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
    }
}