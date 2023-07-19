using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeIngredientDto
    {
        public Guid IdRecipeIngredient { get; set; }
        public Guid IDRecipe { get; set; }
        public Guid IDIngredient { get; set; }
        public bool IsPrincipalIngredient { get; set; }
        public string QuantityNotStd { get; set; }
        public int? IDQuantityNotStd { get; set; }
        public double? Quantity { get; set; }
        public int? IDQuantityType { get; set; }
        public bool QuantityNotSpecified { get; set; }
        public int RecipeIngredientGroupNumber { get; set; }
        public Guid? IdRecipeIngredientAlternative { get; set; }
        public int IngredientRelevance { get; set; }
    }
}