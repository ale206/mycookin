using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientsByIdRecipeAndLanguageIn
    {
        public Guid RecipeId { get; set; }
        public int LanguageId { get; set; }
    }
}