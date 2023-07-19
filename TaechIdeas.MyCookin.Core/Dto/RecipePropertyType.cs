namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipePropertyType
    {
        public int IdRecipePropertyType { get; set; }
        public bool IsDishType { get; set; }
        public bool IsCookingType { get; set; }
        public bool IsColorType { get; set; }
        public bool IsEatType { get; set; }
        public bool IsUseType { get; set; }
        public bool IsPeriodType { get; set; }
        public int OrderPosition { get; set; }
        public bool Enabled { get; set; }
        public int IdLanguage { get; set; }
        public string PropertyType { get; set; }
        public string RecipePropertyTypeToolTip { get; set; }
    }
}