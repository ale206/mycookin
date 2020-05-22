using System;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class UpdateAuditEventInput
    {
        public Guid AuditEventId { get; set; }
        public Guid EventUpdatedById { get; set; }
        public bool IsEventAuditOpen { get; set; }
    }
}