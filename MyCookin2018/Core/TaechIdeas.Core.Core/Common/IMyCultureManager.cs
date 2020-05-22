namespace TaechIdeas.Core.Core.Common
{
    public interface IMyCultureManager
    {
        int LanguageIdByLanguageCode(string languageCode);

        string LanguageCodeByLanguageId(int languageId);

        //string LanguageCodeByLanguageId(int id);
        string GetBrowserCurrentCulture();
        int GetIdLanguageFromLangShortCode(string languageCode);
        string GetLangShortCodeFromIdLanguage(int idLanguage);
        int GetIdLanguageFromBrowser();
    }
}