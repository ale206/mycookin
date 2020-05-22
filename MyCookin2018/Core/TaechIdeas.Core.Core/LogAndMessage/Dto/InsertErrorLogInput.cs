using System;

namespace TaechIdeas.Core.Core.LogAndMessage.Dto
{
    public class InsertErrorLogInput
    {
        public string ErrorNumber { get; set; }

        public string ErrorSeverity { get; set; }

        public string ErrorState { get; set; }

        public string ErrorProcedure { get; set; }

        public string ErrorLine { get; set; }

        public string ErrorMessage { get; set; }

        public string FileOrigin { get; set; }

        public DateTime? DateError { get; set; }

        public string ErrorMessageCode { get; set; }

        public bool? IsStoredProcedureError { get; set; }

        public bool? IsTriggerError { get; set; }

        public bool? IsApplicationError { get; set; }

        public string UserId { get; set; }

        public bool? IsApplicationLog { get; set; }
    }
}