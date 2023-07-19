using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipePropertyValue
    {
        public Guid IdRecipePropertyValue { get; set; }
        public RecipeProperty RecipeProp { get; set; }
        public bool Value { get; set; }
    }
}