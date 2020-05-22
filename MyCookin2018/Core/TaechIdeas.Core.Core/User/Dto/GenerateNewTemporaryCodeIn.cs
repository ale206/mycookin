using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class GenerateNewTemporaryCodeIn
    {
        public Guid UserId { get; set; }
        public string PasswordHash { get; set; }
    }
}