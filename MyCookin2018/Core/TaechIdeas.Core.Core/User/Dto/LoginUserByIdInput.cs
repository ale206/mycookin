using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class LoginUserByIdInput
    {
        public Guid UserId { get; set; }
        public int Offset { get; set; }
        public string Ip { get; set; }
        public MyWebsite WebsiteId { get; set; }
    }
}