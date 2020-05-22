using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class NewAuditEventInput
    {
        public string AuditEventMessage { get; set; }
        public Guid ObjectId { get; set; }
        public ObjectType ObjectType { get; set; }
        public string ObjectTxtInfo { get; set; }
        public AuditEventLevel AuditEventLevel { get; set; }
    }
}