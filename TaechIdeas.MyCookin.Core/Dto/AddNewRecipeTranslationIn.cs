using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddNewRecipeTranslationIn
    {
        public string RecipeName { get; set; }
        public string RecipeHistory { get; set; }
        public string RecipeNote { get; set; }
        public string RecipeSuggestion { get; set; }
        public string RecipeLanguageTags { get; set; }
        public Guid RecipeId { get; set; }
        public int LanguageId { get; set; }
        public Guid? TranslatedBy { get; set; }
        public bool IsAutoTranslate { get; set; }
    }
}