using System.Collections.Generic;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.Core.LogAndMessage
{
    public interface IErrorAndMessageRepository
    {
        GetErrorOrMessageOut GetErrorOrMessage(GetErrorOrMessageIn getErrorOrMessageIn);
        InsertErrorLogOut InsertErrorLog(InsertErrorLogIn insertErrorLogIn);
        GetLastErrorLogDateOut GetLastErrorLogDate(GetLastErrorLogDateIn getLastErrorLogDateIn);
        DeleteErrorByErrorMessageOut DeleteErrorByErrorMessage(DeleteErrorByErrorMessageIn deleteErrorByErrorMessageIn);
        IEnumerable<GetErrorsListOut> GetErrorsList(GetErrorsListIn getErrorsList);
    }
}