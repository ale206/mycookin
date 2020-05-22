using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Contact.Dto
{
    public class RequestMessagesOutput
    {
        public Guid IdContactRequest { get; set; }
        public int IdLanguage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RequestText { get; set; }
        public bool PrivacyAccept { get; set; }
        public DateTime RequestDate { get; set; }
        public string IpAddress { get; set; }
        public TypeOfMessage IdContactRequestType { get; set; }
        public bool IsRequestClosed { get; set; }
        public bool JustNotClosedRequests { get; set; }

        public bool IsError { get; set; }
        public string ResultExecutionCode { get; set; }
        public string UspReturnValue { get; set; }
    }
}