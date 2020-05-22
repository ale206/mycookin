using System;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeLanguageListOut : PaginationFieldsOut
    {
        //Recipe
        public Guid IDRecipe { get; set; }

        public Guid? IDRecipeFather { get; set; }

        public Guid? IDOwner { get; set; }

        public int? NumberOfPerson { get; set; }

        public int? PreparationTimeMinute { get; set; }

        public int? CookingTimeMinute { get; set; }

        public int? RecipeDifficulties { get; set; }

        public Guid? IDRecipeImage { get; set; }

        public Guid? IDRecipeVideo { get; set; }

        public int? IDCity { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdate { get; set; }

        public Guid? UpdatedByUser { get; set; }

        public int RecipeConsulted { get; set; }

        public float RecipeAvgRating { get; set; }

        public bool isStarterRecipe { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool BaseRecipe { get; set; }

        public bool RecipeEnabled { get; set; }

        public bool? Checked { get; set; }

        public int? RecipeCompletePerc { get; set; }

        public double? RecipePortionKcal { get; set; }

        public double? RecipePortionProteins { get; set; }

        public double? RecipePortionFats { get; set; }

        public double? RecipePortionCarbohydrates { get; set; }

        public double? RecipePortionQta { get; set; }

        public bool? Vegetarian { get; set; }

        public bool? Vegan { get; set; }

        public bool? GlutenFree { get; set; }

        public bool? HotSpicy { get; set; }

        public double? RecipePortionAlcohol { get; set; }

        public bool? Draft { get; set; }

        public int? RecipeRated { get; set; }

        //--RecipeLanguage

        public Guid IDRecipeLanguage { get; set; }

        public int IDLanguage { get; set; }

        public string RecipeName { get; set; }

        public bool RecipeLanguageAutoTranslate { get; set; }

        public string RecipeHistory { get; set; }

        public DateTime? RecipeHistoryDate { get; set; }

        public string RecipeNote { get; set; }

        public string RecipeSuggestion { get; set; }

        public bool RecipeDisabled { get; set; }

        public int? GeoIDRegion { get; set; }

        public string RecipeLanguageTags { get; set; }

        public bool? OriginalVersion { get; set; }

        public Guid? TranslatedBy { get; set; }

        public string FriendlyId { get; set; }
    }
}