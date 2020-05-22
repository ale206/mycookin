namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class NewAuditEventOutput
    {
        public string ResultExecutionCode { get; set; }

        public string UspReturnValue { get; set; }

        public bool IsError { get; set; }
    }
}