using System;

namespace TaechIdeas.Core.Core.LogAndMessage.Dto
{
    public class NewReplyIn
    {
        public Guid ContactRequestId { get; set; }
        public Guid UserIdWhoReplied { get; set; }
        public string Reply { get; set; }
        public string IpAddress { get; set; }
    }
}