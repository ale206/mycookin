using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SaveRecipeInBookInput
    {
        public Guid? idRecipeBookRecipe { get; set; }
        public Guid? idUser { get; set; }
        public Guid? idRecipe { get; set; }
        public DateTime? recipeAddedOn { get; set; }
        public int? recipeOrder { get; set; }
    }
}