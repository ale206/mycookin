using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientsByIdRecipeAndLanguageOut
    {
        public double Quantity { get; set; }
        public int IDQuantityType { get; set; }
        public string IngredientDescription { get; set; }
        public Guid IDIngredientImage { get; set; }
        public double Kcal100gr { get; set; }
        public double grProteins { get; set; }
        public double grFats { get; set; }
        public double grCarbohydrates { get; set; }
        public double grAlcohol { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsHotSpicy { get; set; }
        public int RecipeIngredientGroupNumber { get; set; }
        public Guid IDIngredientPreparationRecipe { get; set; }

        public string QuantityNotStd { get; set; }
        public int IDQuantityNotStd { get; set; }
        public int IDLanguage { get; set; }
        public string IngredientSingular { get; set; }
        public string IngredientPlural { get; set; }

        public bool IsPrincipalIngredient { get; set; }
        public Guid IDRecipe { get; set; }
        public bool QuantityNotSpecified { get; set; }
        public Guid IDRecipeIngredientAlternative { get; set; }
        public int IngredientRelevance { get; set; }
        public Guid IDIngredientLanguage { get; set; }
        public bool isAutoTranslate { get; set; }
        public int GeoIDRegion { get; set; }
        public Guid IDIngredient { get; set; }
        public double AverageWeightOfOnePiece { get; set; }
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
        public Guid IDRecipeIngredient { get; set; }
        public string FriendlyId { get; set; }
    }
}