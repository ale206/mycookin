using System;
using System.Collections.Generic;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PercentageCompleteInput //: RecipeByIdOutput
    {
        public Guid RecipeLanguageId { get; set; }
        public int LanguageId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeNote { get; set; }
        public string RecipeSuggestion { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> Ingredients { get; set; }
        public IEnumerable<StepsByIdRecipeAndLanguageOutput> Steps { get; set; } //TODO: GET THESE
        public string PropertiesJoined { get; set; }

        public bool RecipeLanguageAutoTranslate { get; set; }
        public string RecipeHistory { get; set; }
        public DateTime? RecipeHistoryDate { get; set; }
        public bool RecipeDisabled { get; set; }
        public int? GeoIdRegion { get; set; }
        public string RecipeLanguageTags { get; set; }
        public bool? OriginalVersion { get; set; }
        public Guid? TranslatedBy { get; set; }

        public IEnumerable<RecipePropertyByIdRecipeAndLanguageOutput> RecipePropertyValues { get; set; }
        public string FriendlyId { get; set; }
    }
}