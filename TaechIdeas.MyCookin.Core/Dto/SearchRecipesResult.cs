using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Common.Dto;
using TaechIdeas.MyCookin.Core.Enums;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchRecipesResult : PaginationFieldsResult
    {
        //public Guid RecipeId { get; set; }
        //public decimal Rank { get; set; }
        //public double RecipeAvgRating { get; set; }
        //public string ImageUrl { get; set; }
        //public string RecipeName { get; set; }
        //public Guid RecipeLanguageId { get; set; }
        //public string FriendlyId { get; set; }
        //public string Difficulty { get; set; }
        //public int Kcal { get; set; }
        //public int CookingTime { get; set; }
        //public string Username { get; set; }
        //public string Name { get; set; }
        //public string Surname { get; set; }

        public Guid RecipeId { get; set; }
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
        public int RecipeRated { get; set; }
        public RecipeDifficulty RecipeDifficulty { get; set; }
        public Guid RecipeLanguageId { get; set; }
        public int LanguageId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeNote { get; set; }
        public string RecipeSuggestion { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageResult> IngredientsForRecipes { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageResult> IngredientsForDough { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageResult> IngredientsForFilling { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageResult> IngredientsForDressing { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageResult> IngredientsForSauce { get; set; }
        public IEnumerable<IngredientsByIdRecipeAndLanguageResult> IngredientsForDecoration { get; set; }
        public IEnumerable<StepsByIdRecipeAndLanguageResult> Steps { get; set; }
        public string PropertiesJoined { get; set; }
        public string ImageUrl { get; set; }
        public RecipeOwnerResult RecipeOwner { get; set; }
        public string FriendlyId { get; set; }
        public PropertiesByRecipeAndLanguageResult Properties { get; set; }
    }
}