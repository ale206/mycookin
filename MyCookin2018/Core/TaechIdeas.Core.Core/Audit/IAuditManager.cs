using System.Collections.Generic;
using TaechIdeas.Core.Core.Audit.Dto;

namespace TaechIdeas.Core.Core.Audit
{
    public interface IAuditManager
    {
        AutoAuditConfigInfoOutput AutoAuditConfigInfo(AutoAuditConfigInfoInput autoAuditConfigInfoInput);
        NewAuditEventOutput NewAuditEvent(NewAuditEventInput newAuditEventInput);
        UpdateAuditEventOutput UpdateAuditEvent(UpdateAuditEventInput updateAuditEventInput);
        CheckUserSpamReportedOutput CheckUserSpamReported(CheckUserSpamReportedInput checkUserSpamReportedInput);
        DeleteByObjectIdOutput DeleteByObjectId(DeleteByObjectIdInput deleteByObjectIdInput);
        GetAuditEventByIdOutput GetAuditEventById(GetAuditEventByIdInput getAuditEventByIdInput);
        IEnumerable<GetAuditEventToCheckOutput> GetAuditEventToCheck(GetAuditEventToCheckInput getAuditEventToCheckInput);
        GetNumberOfEventToCheckOutput GetNumberOfEventToCheck(GetNumberOfEventToCheckInput getNumberOfEventToCheckInput);
        IEnumerable<GetObjectIdNumberOfEveniencesOutput> GetObjectIdNumberOfEveniences(GetObjectIdNumberOfEveniencesInput getObjectIdNumberOfEveniencesInput);
        IEnumerable<GetPicturesToCheckOutput> GetPicturesToCheck(GetPicturesToCheckInput getPicturesToCheckInput);
    }
}