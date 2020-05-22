using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class Audit
    {
        public Guid IdAuditEvent { get; set; }
        public string AuditEventMessage { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectTxtInfo { get; set; }
        public AuditEventLevel AuditEventLevel { get; set; }
        public DateTime EventInsertedOn { get; set; }
        public DateTime? EventUpdatedOn { get; set; }
        public Guid? IdEventUpdatedBy { get; set; }
        public bool AuditEventIsOpen { get; set; }

        public int NumberOfResults { get; set; }
        public string ExecutionError { get; set; }
        public int NumberOfEveniences { get; set; }
    }
}