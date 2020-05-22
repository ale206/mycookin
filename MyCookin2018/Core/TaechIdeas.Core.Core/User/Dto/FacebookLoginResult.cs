using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class FacebookLoginResult
    {
        public Guid UserId { get; set; }
        public Guid UserToken { get; set; }
    }
}