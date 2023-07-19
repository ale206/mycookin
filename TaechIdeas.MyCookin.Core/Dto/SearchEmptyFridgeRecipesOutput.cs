using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SearchEmptyFridgeRecipesOutput
    {
        public Guid RecipeId { get; set; }
        public int NumSearchedIngr { get; set; }
        public int NumIngr { get; set; }
    }
}