using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Contact.Dto
{
    public class RequestMessagesRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public int ContactRequestTypeId { get; set; }
        public bool JustNotClosedRequests { get; set; }
    }
}