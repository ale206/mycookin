namespace TaechIdeas.MyCookin.Core.Dto
{
    public class StepsToTranslateInput
    {
        public int LanguageIdFrom { get; set; }
        public int LanguageIdTo { get; set; }
        public int NumberOfStepsToTranslate { get; set; }
    }
}