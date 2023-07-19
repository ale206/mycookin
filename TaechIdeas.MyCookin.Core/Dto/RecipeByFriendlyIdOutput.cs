using System;
using System.Collections.Generic;
using TaechIdeas.MyCookin.Core.Enums;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeByFriendlyIdOutput
    {
        public int LanguageId { get; set; }
        public Guid RecipeId { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> IngredientsForRecipes { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> IngredientsForDough { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> IngredientsForFilling { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> IngredientsForDressing { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> IngredientsForSauce { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> IngredientsForDecoration { get; set; }
        public IEnumerable<StepForRecipeOutput> Steps { get; set; }
        public IEnumerable<FeedbackInfoOutput> RecipeFeedbacks { get; set; }
        public Guid? RecipeFatherId { get; set; }
        public int NumberOfPeople { get; set; }
        public int PreparationTimeMinutes { get; set; }
        public int? CookingTimeMinutes { get; set; }
        public DateTime? CreationDate { get; set; }
        public int RecipeConsulted { get; set; }
        public double RecipeAvgRating { get; set; }
        public double? RecipePortionKcal { get; set; }
        public double? RecipePortionProteins { get; set; }
        public double? RecipePortionFats { get; set; }
        public double? RecipePortionCarbohydrates { get; set; }
        public double? RecipePortionAlcohol { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsHotSpicy { get; set; }
        public int? RecipeRated { get; set; }
        public RecipeDifficulty RecipeDifficulty { get; set; }

        //Calculated to use in RecipeByIdAndLanguageResult
        public string ImageUrl { get; set; }
        public RecipeOwnerOutput RecipeOwner { get; set; }

        public Guid? OwnerId { get; set; }
        public Guid? RecipeImageId { get; set; }
        public Guid? RecipeVideoId { get; set; }
        public int? CityId { get; set; }
        public DateTime? LastUpdate { get; set; }
        public Guid? UpdatedByUser { get; set; }
        public bool IsAStarterRecipe { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsABaseRecipe { get; set; }
        public bool IsRecipeEnabled { get; set; }
        public bool IsChecked { get; set; }
        public int? RecipeCompletePercentage { get; set; }
        public double? RecipePortionQta { get; set; }
        public bool? IsADraft { get; set; }

        public Guid RecipeLanguageId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeNote { get; set; }
        public string RecipeSuggestion { get; set; }
        public string PropertiesJoined { get; set; }
        public string FriendlyId { get; set; }

        public PropertiesByRecipeAndLanguageOutput Properties { get; set; }
    }
}