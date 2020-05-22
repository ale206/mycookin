using System.Collections.Generic;
using TaechIdeas.Core.Core.Audit.Dto;

namespace TaechIdeas.Core.Core.Audit
{
    public interface IAuditRepository
    {
        AutoAuditConfigInfoOut AutoAuditConfigInfo(AutoAuditConfigInfoIn autoAuditConfigInfoIn);
        NewAuditEventOut NewAuditEvent(NewAuditEventIn newAuditEventIn);
        UpdateAuditEventOut UpdateAuditEvent(UpdateAuditEventIn updateAuditEventIn);
        CheckUserSpamReportedOut CheckUserSpamReported(CheckUserSpamReportedIn checkUserSpamReportedIn);
        DeleteByObjectIdOut DeleteByObjectId(DeleteByObjectIdIn deleteByObjectIdIn);
        GetAuditEventByIdOut GetAuditEventById(GetAuditEventByIdIn getAuditEventByIdIn);
        IEnumerable<GetAuditEventToCheckOut> GetAuditEventToCheck(GetAuditEventToCheckIn getAuditEventToCheckIn);
        GetNumberOfEventToCheckOut GetNumberOfEventToCheck(GetNumberOfEventToCheckIn getNumberOfEventToCheckIn);
        IEnumerable<GetObjectIdNumberOfEveniencesOut> GetObjectIdNumberOfEveniences(GetObjectIdNumberOfEveniencesIn getObjectIdNumberOfEveniencesIn);
    }
}