using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientsByIdRecipeOutput
    {
        public bool IsPrincipalIngredient { get; set; }
        public Guid RecipeId { get; set; }
        public string QuantityNotStd { get; set; }
        public int QuantityNotStdId { get; set; }
        public double Quantity { get; set; }
        public int QuantityTypeId { get; set; }
        public bool QuantityNotSpecified { get; set; }
        public int RecipeIngredientGroupNumber { get; set; }
        public Guid RecipeIngredientAlternativeId { get; set; }
        public int IngredientRelevance { get; set; }

        public Guid IngredientId { get; set; }

        public Guid IngredientPreparationRecipeId { get; set; }
        public Guid? IngredientImageId { get; set; }
        public double AverageWeightOfOnePiece { get; set; }
        public double Kcal100Gr { get; set; }
        public double GrProteins { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsHotSpicy { get; set; }
        public bool Checked { get; set; }
        public Guid IngredientCreatedBy { get; set; }
        public DateTime IngredientCreationDate { get; set; }
        public bool January { get; set; }
        public bool February { get; set; }
        public bool March { get; set; }
        public bool April { get; set; }
        public bool May { get; set; }
        public bool June { get; set; }
        public bool July { get; set; }
        public bool August { get; set; }
        public bool September { get; set; }
        public bool October { get; set; }
        public bool November { get; set; }
        public bool December { get; set; }
        public Guid RecipeIngredientId { get; set; }
        public string ImageUrl { get; set; }
    }
}