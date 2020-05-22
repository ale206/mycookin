namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class UpdateAuditEventOutput
    {
        public string ResultExecutionCode { get; set; }

        public string UspReturnValue { get; set; }

        public bool IsError { get; set; }
    }
}