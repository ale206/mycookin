using TaechIdeas.Core.Core.MicrosoftTranslator.Dto;

namespace TaechIdeas.Core.Core.MicrosoftTranslator
{
    public interface IMicrosoftTranslationManager
    {
        TranslateSentenceOutput TranslateSentence(string languageFrom, string languageTo, string originalSentence);
    }
}