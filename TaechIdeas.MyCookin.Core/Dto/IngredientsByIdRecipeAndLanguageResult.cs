namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientsByIdRecipeAndLanguageResult
    {
        public double Quantity { get; set; }
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

        /// <summary>
        ///     Returns something like 'Kg of', 'pieces of', etc.
        /// </summary>
        public string QuantityType { get; set; }

        /// <summary>
        ///     Returns name of the ingredient according to singular or plural quantity
        /// </summary>
        public string IngredientName { get; set; }

        public string FriendlyId { get; set; }
    }
}