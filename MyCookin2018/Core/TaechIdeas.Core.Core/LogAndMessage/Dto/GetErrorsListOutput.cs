namespace TaechIdeas.Core.Core.LogAndMessage.Dto
{
    public class GetErrorsListOutput
    {
        public string ErrorMessage { get; set; }
        public int NumberOfEveniences { get; set; }
        public string ErrorSeverity { get; set; }
        public string FileOrigin { get; set; }
        public string ErrorLine { get; set; }
        public string ErrorMessageCode { get; set; }
        public bool IsStoredProcedureError { get; set; }
        public bool IsTriggerError { get; set; }
        public bool IsApplicationError { get; set; }
    }
}