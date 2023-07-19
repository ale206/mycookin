using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesByRecipeAndLanguageRequest
    {
        public int LanguageId { get; set; }
        public Guid RecipeId { get; set; }
    }
}