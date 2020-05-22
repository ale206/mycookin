using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.Core.LogAndMessage
{
    public interface ILogManager
    {
        void WriteLog(LogRowIn logRowIn);

        /************************************************************/

        void WriteDbLog(LogRowIn logRowIn);
        void WriteFileLog(LogRowIn logRowIn);
        void SendEmail(LogRowIn logRowIn);
        string GetLogRow(LogRowIn logRowIn);
        string GetLogDelimiter();
        string DeleteErrorByErrorMessage(string errorMessage);
    }
}