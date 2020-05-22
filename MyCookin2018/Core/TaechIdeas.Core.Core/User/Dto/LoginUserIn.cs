using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class LoginUserIn
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Offset { get; set; }
        public string Ip { get; set; }
        public MyWebsite WebsiteId { get; set; }
    }
}