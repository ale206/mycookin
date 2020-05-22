using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class FacebookLoginOutput
    {
        public Guid UserId { get; set; }
        public Guid UserToken { get; set; }
    }
}