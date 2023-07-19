using System;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchRecipesWithEmptyFridgeOut : PaginationFieldsOut
    {
        public Guid IdRecipe { get; set; }
        public Guid IDRecipeLanguage { get; set; }
        public double RecipeAvgRating { get; set; }
        public string RecipeName { get; set; }
        public Guid IDRecipeImage { get; set; }
        public string FriendlyId { get; set; }

        /// <summary>
        ///     Number of ingredient found in recipe
        /// </summary>
        public int NumSearchedIngr { get; set; }

        /// <summary>
        ///     Total ingredients number
        /// </summary>
        public int NumIngr { get; set; }
    }
}