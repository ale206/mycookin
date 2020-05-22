using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MemorizeFacebookContactInput
    {
        public string accessToken { get; set; }
        public Guid UserId { get; set; }
    }
}