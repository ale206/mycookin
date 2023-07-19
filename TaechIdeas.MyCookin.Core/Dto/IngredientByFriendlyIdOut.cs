using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientByFriendlyIdOut
    {
        public Guid IDIngredient { get; set; }

        public Guid? IDIngredientPreparationRecipe { get; set; }

        public Guid? IDIngredientImage { get; set; }

        public double? AverageWeightOfOnePiece { get; set; }

        public double? Kcal100gr { get; set; }

        public double? grProteins { get; set; }

        public double? grFats { get; set; }

        public double? grCarbohydrates { get; set; }

        public double? grAlcohol { get; set; }

        public double? mgCalcium { get; set; }

        public double? mgSodium { get; set; }

        public double? mgPhosphorus { get; set; }

        public double? mgPotassium { get; set; }

        public double? mgIron { get; set; }

        public double? mgMagnesium { get; set; }

        public double? mcgVitaminA { get; set; }

        public double? mgVitaminB1 { get; set; }

        public double? mgVitaminB2 { get; set; }

        public double? mcgVitaminB9 { get; set; }

        public double? mcgVitaminB12 { get; set; }

        public double? mgVitaminC { get; set; }

        public double? grSaturatedFat { get; set; }

        public double? grMonounsaturredFat { get; set; }

        public double? grPolyunsaturredFat { get; set; }

        public double? mgCholesterol { get; set; }

        public double? mgPhytosterols { get; set; }

        public double? mgOmega3 { get; set; }

        public bool? IsForBaby { get; set; }

        public bool? IsMeat { get; set; }

        public bool? IsFish { get; set; }

        public bool IsVegetarian { get; set; }

        public bool IsVegan { get; set; }

        public bool IsGlutenFree { get; set; }

        public bool IsHotSpicy { get; set; }

        public bool Checked { get; set; }

        public Guid? IngredientCreatedBy { get; set; }

        public DateTime? IngredientCreationDate { get; set; }

        public Guid? IngredientModifiedByUser { get; set; }

        public DateTime? IngredientLastMod { get; set; }

        public bool? IngredientEnabled { get; set; }

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

        public double? grDietaryFiber { get; set; }

        public double? grStarch { get; set; }

        public double? grSugar { get; set; }

        //FROM INGREDIENTLANGUAGES TABLE
        public Guid IDIngredientLanguage { get; set; }

        public int IDLanguage { get; set; }

        public string IngredientSingular { get; set; }

        public string IngredientPlural { get; set; }

        public string IngredientDescription { get; set; }

        public bool isAutoTranslate { get; set; }

        public int? GeoIDRegion { get; set; }

        public string FriendlyId { get; set; }
    }
}