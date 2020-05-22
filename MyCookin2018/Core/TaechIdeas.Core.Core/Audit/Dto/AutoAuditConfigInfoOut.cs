namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class AutoAuditConfigInfoOut
    {
        public int IdAutoAuditConfig { get; set; }
        public string ObjectType { get; set; }
        public int AuditEventLevel { get; set; }
        public bool EnableAutoAudit { get; set; }
    }
}