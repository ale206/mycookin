using System;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientLanguageListOut : PaginationFieldsOut
    {
        //Ingredient
        public Guid IDIngredient { get; set; }

        public Guid? IDIngredientPreparationRecipe { get; set; }

        public Guid? IDIngredientImage { get; set; }

        public float AverageWeightOfOnePiece { get; set; }

        public float Kcal100gr { get; set; }

        public float grProteins { get; set; }

        public float grFats { get; set; }

        public float grCarbohydrates { get; set; }

        public float grAlcohol { get; set; }

        public float mgCalcium { get; set; }

        public float mgSodium { get; set; }

        public float mgPhosphorus { get; set; }

        public float mgPotassium { get; set; }

        public float mgIron { get; set; }

        public float mgMagnesium { get; set; }

        public float mcgVitaminA { get; set; }

        public float mgVitaminB1 { get; set; }

        public float mgVitaminB2 { get; set; }

        public float mcgVitaminB9 { get; set; }

        public float mcgVitaminB12 { get; set; }

        public float mgVitaminC { get; set; }

        public float grSaturatedFat { get; set; }

        public float grMonounsaturredFat { get; set; }

        public float grPolyunsaturredFat { get; set; }

        public float mgCholesterol { get; set; }

        public float mgPhytosterols { get; set; }

        public float mgOmega3 { get; set; }

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

        public float grDietaryFiber { get; set; }

        public float grStarch { get; set; }

        public float grSugar { get; set; }

        //IngredientsLanguages
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