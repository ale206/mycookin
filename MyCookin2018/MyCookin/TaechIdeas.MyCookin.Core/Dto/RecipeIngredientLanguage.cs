using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeIngredientLanguage : RecipeIngredient
    {
        public Guid IdRecipeIngredientLanguage { get; set; }
        public string RecipeIngredientNote { get; set; }
        public string RecipeIngredientGroupName { get; set; }
    }
}