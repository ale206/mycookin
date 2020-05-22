using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Contact.Dto
{
    public class RequestMessagesInput : TokenRequiredInput
    {
        public int ContactRequestTypeId { get; set; }
        public bool JustNotClosedRequests { get; set; }
    }
}