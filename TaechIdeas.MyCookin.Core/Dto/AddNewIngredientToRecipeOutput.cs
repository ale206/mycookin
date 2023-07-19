using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AddNewIngredientToRecipeOutput
    {
        public bool NewIngredientToRecipeAdded { get; set; }
        public Guid NewRecipeIngredientId { get; set; }
    }
}