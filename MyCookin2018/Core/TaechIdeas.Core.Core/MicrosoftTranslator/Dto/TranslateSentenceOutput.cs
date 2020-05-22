namespace TaechIdeas.Core.Core.MicrosoftTranslator.Dto
{
    public class TranslateSentenceOutput
    {
        public string LanguageSource { get; set; }

        public string LanguageTarget { get; set; }

        public string OriginalSentence { get; set; }

        public string TranslatedSentence { get; set; }

        public bool Result { get; set; }

        public string ErrorMessage { get; set; }
    }
}