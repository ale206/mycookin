namespace TaechIdeas.MyCookin.Core.Dto
{
    public class PropertiesListByTypeLanguageAndRecipeResult
    {
        public int RecipePropertyTypeId { get; set; }
        public string RecipePropertyType { get; set; }
        public int RecipePropertyId { get; set; }
        public string RecipeProperty { get; set; }
        public int LanguageId { get; set; }
        public bool IsPeriodType { get; set; }
        public bool IsUseType { get; set; }
        public bool IsEatType { get; set; }
        public bool IsCookingType { get; set; }
        public bool IsDishType { get; set; }
        public bool IsColorType { get; set; }
        public string Value { get; set; }
    }
}