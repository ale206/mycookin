using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipesInRecipeBookIn
    {
        public Guid UserId { get; set; }
        public int recipeType { get; set; }
        public int showFilter { get; set; }
        public string recipeNameFilter { get; set; }
        public bool vegan { get; set; }
        public bool vegetarian { get; set; }
        public bool glutenFree { get; set; }
        public double lightThreshold { get; set; }
        public int quickThreshold { get; set; }
        public bool showDraft { get; set; }
        public int LanguageId { get; set; }
        public int rowOffSet { get; set; }
        public int fetchRows { get; set; }
    }
}