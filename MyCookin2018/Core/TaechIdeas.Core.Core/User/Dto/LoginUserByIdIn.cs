using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class LoginUserByIdIn
    {
        public Guid UserId { get; set; }
        public int Offset { get; set; }
        public string Ip { get; set; }
    }
}