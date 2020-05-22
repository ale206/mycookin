using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class LoginUserByIdOut
    {
        public bool UserLoggedIn { get; set; }
        public Guid UserId { get; set; }
    }
}