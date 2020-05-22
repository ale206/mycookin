namespace TaechIdeas.Core.Core.LogAndMessage.Dto
{
    public class GetErrorsListOut
    {
        public string ErrorMessage { get; set; }
        public int NumberOfEveniences { get; set; }
        public string ErrorSeverity { get; set; }
        public string FileOrigin { get; set; }
        public string ErrorLine { get; set; }
        public string ErrorMessageCode { get; set; }
        public bool isStoredProcedureError { get; set; }
        public bool isTriggerError { get; set; }
        public bool isApplicationError { get; set; }
    }
}