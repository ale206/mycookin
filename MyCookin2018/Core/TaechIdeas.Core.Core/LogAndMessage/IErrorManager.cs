using System.Collections.Generic;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.Core.LogAndMessage
{
    public interface IErrorManager
    {
        GetErrorOrMessageOutput GetErrorOrMessage(GetErrorOrMessageInput getErrorOrMessageInput);
        InsertErrorLogOutput InsertErrorLog(InsertErrorLogInput insertErrorLogInput);
        GetLastErrorLogDateOutput GetLastErrorLogDate(GetLastErrorLogDateInput getLastErrorLogDateInput);
        DeleteErrorByErrorMessageOutput DeleteErrorByErrorMessage(DeleteErrorByErrorMessageInput deleteErrorByErrorMessageInput);
        IEnumerable<GetErrorsListOutput> GetErrorsList(GetErrorsListInput getErrorsListInput);
    }
}