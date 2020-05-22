using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SuggestedBeverageByRecipeOut
    {
        public Guid IDBeverageRecipe { get; set; }

        public Guid IDRecipe { get; set; }

        public Guid IDBeverage { get; set; }

        public Guid IDUserSuggestedBy { get; set; }

        public DateTime DateSuggestion { get; set; }

        public double? BeverageRecipeAvgRating { get; set; }
    }
}