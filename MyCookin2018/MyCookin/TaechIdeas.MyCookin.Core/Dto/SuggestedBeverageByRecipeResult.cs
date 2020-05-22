using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SuggestedBeverageByRecipeResult
    {
        public Guid BeverageRecipeId { get; set; }

        public Guid RecipeId { get; set; }

        public Guid BeverageId { get; set; }

        public Guid UserId { get; set; }

        public DateTime SuggestionDate { get; set; }

        public double? AverageRating { get; set; }
    }
}