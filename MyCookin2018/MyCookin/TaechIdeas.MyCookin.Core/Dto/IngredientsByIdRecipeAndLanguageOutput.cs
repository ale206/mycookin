using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientsByIdRecipeAndLanguageOutput
    {
        public double Quantity { get; set; }
        public string QuantityType { get; set; }
        public string IngredientDescription { get; set; }
        public string ImageUrl { get; set; }
        public double Kcal100Gr { get; set; }
        public double GrProteins { get; set; }
        public double GrFats { get; set; }
        public double GrCarbohydrates { get; set; }
        public double GrAlcohol { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsHotSpicy { get; set; }
        public int RecipeIngredientGroupNumber { get; set; }
        public Guid IngredientPreparationRecipeId { get; set; }

        public string QuantityNotStd { get; set; }
        public int QuantityNotStdId { get; set; }
        public int IngredientQuantityTypeId { get; set; }
        public int LanguageId { get; set; }
        public string IngredientSingular { get; set; }
        public string IngredientPlural { get; set; }
        public Guid? IngredientImageId { get; set; }

        /// <summary>
        ///     Returns something like 'Kg of', 'pieces of', etc.
        /// </summary>
        public string CalculatedQuantityTypeLanguage { get; set; }

        /// <summary>
        ///     Returns name of the ingredient according to singular or plural quantity
        /// </summary>
        public string IngredientName { get; set; }

        public string FriendlyId { get; set; }

        //THESE ARE IN IngredientsByIdRecipeAndLanguageOut BUT ARE NOT USED
        //public bool QuantityNotSpecified { get; set; }
        //public Guid RecipeIngredientAlternativeId { get; set; }
        //public int IngredientRelevance { get; set; }
        //public bool IsPrincipalIngredient { get; set; }
        //public Guid RecipeId { get; set; }
        //public Guid IdIngredientLanguage { get; set; }
        //public bool IsAutoTranslate { get; set; }
        //public int GeoIdRegion { get; set; }
        //public Guid IngredientId { get; set; }

        //public double AverageWeightOfOnePiece { get; set; }
        //public bool Checked { get; set; }
        //public Guid IngredientCreatedBy { get; set; }
        //public DateTime IngredientCreationDate { get; set; }
        //public bool January { get; set; }
        //public bool February { get; set; }
        //public bool March { get; set; }
        //public bool April { get; set; }
        //public bool May { get; set; }
        //public bool June { get; set; }
        //public bool July { get; set; }
        //public bool August { get; set; }
        //public bool September { get; set; }
        //public bool October { get; set; }
        //public bool November { get; set; }
        //public bool December { get; set; }
        //public Guid RecipeIngredientId { get; set; }
    }
}