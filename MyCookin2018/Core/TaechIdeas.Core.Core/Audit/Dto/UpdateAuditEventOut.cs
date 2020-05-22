namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class UpdateAuditEventOut
    {
        public string ResultExecutionCode { get; set; }

        public string USPReturnValue { get; set; }

        public bool isError { get; set; }
    }
}