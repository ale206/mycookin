﻿using System;
using System.Collections.Generic;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SimilarRecipesOut
    {
        public Guid RecipeId { get; set; }

        public IEnumerable<IngredientsByIdRecipeOut> IngredientsByIdRecipeOut { get; set; }

        //public IEnumerable<StepForRecipeOut> StepsForRecipeOut { get; set; }
        //public IEnumerable<RecipeFeedback> RecipeFeedbacks { get; set; }
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
        public int RecipeDifficulty { get; set; }

        //Calculated to use in RecipeByIdAndLanguageResult
        public string RecipeImageUrl { get; set; }
        public Guid RecipeOwner { get; set; }

        //Not used on RecipeByIdAndLanguageResult
        public Guid? OwnerId { get; set; }
        public Guid? IdRecipeImage { get; set; }
        public Guid? IdRecipeVideo { get; set; }
        public int? IdCity { get; set; }
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
    }
}