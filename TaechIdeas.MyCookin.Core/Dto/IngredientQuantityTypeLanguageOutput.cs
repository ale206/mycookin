namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientQuantityTypeLanguageOutput : IngredientQuantityType
    {
        public int IdIngredientQuantityTypeLanguage { get; set; }
        public string IngredientQuantityTypeSingular { get; set; }
        public string IngredientQuantityTypePlural { get; set; }
        public double ConvertionRatio { get; set; }
        public string IngredientQuantityTypeX1000Singular { get; set; }
        public string IngredientQuantityTypeX1000Plural { get; set; }
        public string IngredientQuantityTypeWordsShowBefore { get; set; }
        public string IngredientQuantityTypeWordsShowAfter { get; set; }
        public int IDIngredientQuantityType { get; set; }
        public int IdLanguage { get; set; }
    }
}