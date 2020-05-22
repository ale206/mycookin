namespace TaechIdeas.Core.Core.LogAndMessage
{
    public interface IRetrieveMessageManager
    {
        string RetrieveDbMessage(int idLanguage, string code);
        string DefaultErrorMessage(int idLanguage);
    }
}