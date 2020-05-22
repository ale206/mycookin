using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class LoginUserByIdOutput
    {
        public bool UserLoggedIn { get; set; }
        public Guid UserId { get; set; }
        public Guid UserToken { get; set; }
    }
}