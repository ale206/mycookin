namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesListByTypeLanguageAndRecipeOut
    {
        public int IDRecipePropertyType { get; set; }
        public string RecipePropertyType { get; set; }
        public int IDRecipeProperty { get; set; }
        public string RecipeProperty { get; set; }
        public int IDLanguage { get; set; }
        public bool isPeriodType { get; set; }
        public bool isUseType { get; set; }
        public bool isEatType { get; set; }
        public bool isCookingType { get; set; }
        public bool isDishType { get; set; }
        public bool isColorType { get; set; }
        public string Value { get; set; }
    }
}