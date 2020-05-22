using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipePropertyByIdRecipeAndLanguageOut
    {
        public bool Value { get; set; }
        public Guid IDRecipePropertyValue { get; set; }
        public Guid IDRecipe { get; set; }
        public int IDRecipeProperty { get; set; }
        public int IDRecipePropertyType { get; set; }
        public bool isDishType { get; set; }
        public bool isCookingType { get; set; }
        public bool isColorType { get; set; }
        public bool isEatType { get; set; }
        public bool isUseType { get; set; }
        public bool isPeriodType { get; set; }
        public int IDRecipePropertyLanguage { get; set; }
        public string RecipeProperty { get; set; }
        public string RecipePropertyType { get; set; }
        public int IDLanguage { get; set; }
        public int IDRecipePropertyTypeLanguage { get; set; }
    }
}