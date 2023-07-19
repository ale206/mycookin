namespace TaechIdeas.MyCookin.Core.Dto
{
    public class TranslateBunchOfIngredientsInput
    {
        public int LanguageIdFrom { get; set; }
        public int LanguageIdTo { get; set; }
        public int NumberOfIngredientsToTranslate { get; set; }
    }
}