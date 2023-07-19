using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SimilarRecipesIn
    {
        public string RecipeName { get; set; }
        public bool? Vegan { get; set; }
        public bool? Vegetarian { get; set; }
        public bool? GlutenFree { get; set; }
        public int LanguageId { get; set; }
        public Guid? RecipeId { get; set; }
    }
}