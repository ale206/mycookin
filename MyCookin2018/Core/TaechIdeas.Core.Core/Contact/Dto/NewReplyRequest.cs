using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Contact.Dto
{
    public class NewReplyRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid ContactRequestId { get; set; }
        public Guid UserIdWhoReplied { get; set; }
        public string Reply { get; set; }
        public string IpAddress { get; set; }
    }
}