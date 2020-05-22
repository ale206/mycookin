using System;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchRecipesOut : PaginationFieldsOut
    {
        public Guid IdRecipe { get; set; }
        public Guid IDRecipeLanguage { get; set; }
        public decimal Rank { get; set; }
        public double RecipeAvgRating { get; set; }
        public string RecipeName { get; set; }
        public Guid IDRecipeImage { get; set; }
        public string FriendlyId { get; set; }
    }
}