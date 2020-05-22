using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddNewIngredientToRecipeOut
    {
        public bool NewIngredientToRecipeAdded { get; set; }
        public Guid NewRecipeIngredientId { get; set; }
    }
}