using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class UpdateRecipeIngredientInput : TokenRequiredInput
    {
        public Guid RecipeIngredientId { get; set; }
        public Guid IngredientId { get; set; }
        public bool IsPrincipalIngredient { get; set; }
        public string QuantityNotStd { get; set; }
        public int QuantityNotStdId { get; set; }
        public float Quantity { get; set; }
        public int QuantityTypeId { get; set; }
        public bool IsQuantityNotSpecified { get; set; }
        public bool GroupNumber { get; set; }
        public Guid AlternativeIngredientId { get; set; }
        public int Relevance { get; set; }
        public Guid RecipeId { get; set; }
    }
}