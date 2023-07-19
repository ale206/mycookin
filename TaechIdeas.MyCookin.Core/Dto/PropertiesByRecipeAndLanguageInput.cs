using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesByRecipeAndLanguageInput
    {
        public int LanguageId { get; set; }
        public Guid RecipeId { get; set; }
    }
}