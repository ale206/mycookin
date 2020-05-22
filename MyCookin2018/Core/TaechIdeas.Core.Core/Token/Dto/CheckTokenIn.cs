using System;

namespace TaechIdeas.Core.Core.Token.Dto
{
    public class CheckTokenIn
    {
        public Guid UserToken { get; set; }
        public Guid UserId { get; set; }
        public int TokenRenewMinutes { get; set; }
        public int WebsiteId { get; set; }
    }
}