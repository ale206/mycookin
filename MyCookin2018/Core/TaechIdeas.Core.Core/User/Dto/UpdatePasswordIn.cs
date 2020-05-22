using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdatePasswordIn
    {
        public Guid UserId { get; set; }
        public string NewPasswordHash { get; set; }
    }
}