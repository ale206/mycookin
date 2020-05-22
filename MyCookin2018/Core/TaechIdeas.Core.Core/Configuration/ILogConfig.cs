namespace TaechIdeas.Core.Core.Configuration
{
    public interface ILogConfig
    {
        int IdLanguageForLog { get; }
        int DebugLevel { get; }
        int FileLevel { get; }
        bool SendEmailForLog { get; }
        string LogPath { get; }
        string LogBaseFileName { get; }
        string LogFileNameDateFormat { get; }
        int SendEmailIntervalTime { get; }
        string EmailFromLog { get; }
        string EmailToLog { get; }
        string LogDelimiter { get; }
        string EmailSubjectForLog { get; }
    }
}