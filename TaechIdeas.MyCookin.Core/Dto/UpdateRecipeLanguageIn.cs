using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class UpdateRecipeLanguageIn
    {
        public Guid RecipeLanguageId { get; set; }
        public int LanguageId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeHistory { get; set; }
        public DateTime RecipeHistoryDate { get; set; }
        public string RecipeNote { get; set; }
        public string RecipeSuggestion { get; set; }
        public int GeoRegionId { get; set; }
        public string RecipeLanguageTags { get; set; }
    }
}