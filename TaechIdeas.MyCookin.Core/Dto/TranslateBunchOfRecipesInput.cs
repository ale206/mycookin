namespace TaechIdeas.MyCookin.Core.Dto
{
    public class TranslateBunchOfRecipesInput
    {
        public int LanguageIdFrom { get; set; }
        public int LanguageIdTo { get; set; }
        public int NumberOfRecipesToTranslate { get; set; }
    }
}