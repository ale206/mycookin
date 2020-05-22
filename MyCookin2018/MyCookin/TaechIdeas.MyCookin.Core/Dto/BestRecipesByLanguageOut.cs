using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class BestRecipesByLanguageOut
    {
        public Guid IDRecipe { get; set; }
        public Guid IDRecipeLanguage { get; set; }
        public string RecipeName { get; set; }
        public Guid IDRecipeImage { get; set; }

        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool GlutenFree { get; set; }
        public bool HotSpicy { get; set; }
        public int RecipeDifficulties { get; set; }
        public double RecipePortionKcal { get; set; }
        public int PreparationTimeMinute { get; set; }
        public int CookingTimeMinute { get; set; }
        public DateTime CreationDate { get; set; }
        public double RecipeAvgRating { get; set; }
        public Guid IDOwner { get; set; }

        public string FriendlyId { get; set; }
    }
}