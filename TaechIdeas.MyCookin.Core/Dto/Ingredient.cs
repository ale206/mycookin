using System;
using System.Collections.Generic;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class Ingredient
    {
        public Guid IdIngredient { get; set; }
        public Guid? IngredientPreparationRecipe { get; set; }
        public Guid? IngredientImage { get; set; }
        public double? AverageWeightOfOnePiece { get; set; }
        public double? Kcal100Gr { get; set; }
        public double? GrProteins { get; set; }
        public double? GrFats { get; set; }
        public double? GrCarbohydrates { get; set; }
        public double? GrAlcohol { get; set; }
        public double? MgCalcium { get; set; }
        public double? MgSodium { get; set; }
        public double? MgPhosphorus { get; set; }
        public double? MgPotassium { get; set; }
        public double? MgIron { get; set; }
        public double? MgMagnesium { get; set; }
        public double? McgVitaminA { get; set; }
        public double? MgVitaminB1 { get; set; }
        public double? MgVitaminB2 { get; set; }
        public double? McgVitaminB9 { get; set; }
        public double? McgVitaminB12 { get; set; }
        public double? MgVitaminC { get; set; }
        public double? GrSaturatedFat { get; set; }
        public double? GrMonounsaturredFat { get; set; }
        public double? GrPolyunsaturredFat { get; set; }
        public double? MgCholesterol { get; set; }
        public double? MgPhytosterols { get; set; }
        public double? MgOmega3 { get; set; }
        public bool IsForBaby { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsHotSpicy { get; set; }
        public bool Checked { get; set; }
        public Guid? IngredientCreatedBy { get; set; }
        public DateTime? IngredientCreationDate { get; set; }
        public Guid? IngredientModifiedByUser { get; set; }
        public DateTime? IngredientLastMod { get; set; }
        public bool IngredientEnabled { get; set; }
        public IEnumerable<IngredientCategory> IngredientCategory { get; set; }
        public IEnumerable<IngredientQuantityType> IngredientQuantityTypes { get; set; }

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
        public IEnumerable<IngredientAlternative> AlternativeIngredients { get; set; }
        public double? GrDietaryFiber { get; set; }
        public double? GrStarch { get; set; }
        public double? GrSugar { get; set; }
    }
}