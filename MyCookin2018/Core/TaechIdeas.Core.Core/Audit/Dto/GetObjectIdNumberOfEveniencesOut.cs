using System;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class GetObjectIdNumberOfEveniencesOut
    {
        public Guid IDAuditEvent { get; set; }

        public string AuditEventMessage { get; set; }

        public Guid? ObjectID { get; set; }

        public string ObjectType { get; set; }

        public string ObjectTxtInfo { get; set; }

        public int? AuditEventLevel { get; set; }

        public DateTime? EventInsertedOn { get; set; }

        public DateTime? EventUpdatedOn { get; set; }

        public Guid? IDEventUpdatedBy { get; set; }

        public bool? AuditEventIsOpen { get; set; }
    }
}