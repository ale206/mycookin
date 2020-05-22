using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Token.Dto
{
    public class NewTokenIn
    {
        public Guid UserId { get; set; }
        public int TokenExpireMinutes { get; set; }
        public MyWebsite WebsiteId { get; set; }
    }
}