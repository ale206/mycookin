using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeBook
    {
        public Guid IdRecipeBookRecipe { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdRecipe { get; set; }
        public DateTime RecipeAddedOn { get; set; }
        public int RecipeOrder { get; set; }
    }
}