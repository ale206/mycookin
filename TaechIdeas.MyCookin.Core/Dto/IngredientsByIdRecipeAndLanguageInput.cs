using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientsByIdRecipeAndLanguageInput
    {
        public Guid RecipeId { get; set; }
        public int LanguageId { get; set; }
    }
}