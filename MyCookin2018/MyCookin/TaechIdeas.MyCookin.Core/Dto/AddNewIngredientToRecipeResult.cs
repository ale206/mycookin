using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddNewIngredientToRecipeResult
    {
        public bool NewIngredientToRecipeAdded { get; set; }
        public Guid NewRecipeIngredientId { get; set; }
    }
}