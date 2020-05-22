using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    /// <summary>
    ///     Use same column names
    /// </summary>
    public class RecipeByLanguageOut
    {
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool GlutenFree { get; set; }
        public Guid IDRecipeLanguage { get; set; }
        public Guid IDRecipe { get; set; }
        public int IDLanguage { get; set; }
        public string RecipeName { get; set; }
        public bool RecipeLanguageAutoTranslate { get; set; }
        public string RecipeHistory { get; set; }
        public DateTime? RecipeHistoryDate { get; set; }
        public string RecipeNote { get; set; }
        public string RecipeSuggestion { get; set; }
        public bool RecipeDisabled { get; set; }
        public int GeoIDRegion { get; set; }
        public string RecipeLanguageTags { get; set; }
        public bool? OriginalVersion { get; set; }
        public Guid? TranslatedBy { get; set; }
    }
}