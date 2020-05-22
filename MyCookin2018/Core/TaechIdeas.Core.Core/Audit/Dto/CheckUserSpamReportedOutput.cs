using System;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class CheckUserSpamReportedOutput
    {
        public Guid AuditEventId { get; set; }

        public string AuditEventMessage { get; set; }

        public Guid? ObjectId { get; set; }

        public string ObjectType { get; set; }

        public string ObjectTxtInfo { get; set; }

        public int? AuditEventLevel { get; set; }

        public DateTime? EventInsertedOn { get; set; }

        public DateTime? EventUpdatedOn { get; set; }

        public Guid? EventUpdatedById { get; set; }

        public bool? AuditEventIsOpen { get; set; }
    }
}