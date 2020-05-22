using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class AutoAuditConfigInfoOutput
    {
        public int IdAutoAuditConfig { get; set; }
        public ObjectType ObjectType { get; set; }
        public AuditEventLevel AuditEventLevel { get; set; }
        public bool EnableAutoAudit { get; set; }
    }
}