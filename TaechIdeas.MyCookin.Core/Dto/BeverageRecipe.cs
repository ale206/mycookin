using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class BeverageRecipe
    {
        public Guid IdBeverageRecipe { get; set; }
        public Guid IdRecipe { get; set; }
        public Beverage IdBeverage { get; set; }
        public Guid IdUserSuggestedBy { get; set; }
        public DateTime DateSuggestion { get; set; }
        public double BeverageRecipeAvgRating { get; set; }
    }
}