namespace TaechIdeas.MyCookin.Core.Dto
{
    public class TranslateBunchOfIngredientsRequest
    {
        public int LanguageIdFrom { get; set; }
        public int LanguageIdTo { get; set; }
        public int NumberOfIngredientsToTranslate { get; set; }
    }
}