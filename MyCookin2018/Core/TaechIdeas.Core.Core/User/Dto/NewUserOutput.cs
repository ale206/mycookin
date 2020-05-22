using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class NewUserOutput
    {
        public Guid UserId { get; set; }
        public Guid UserToken { get; set; }
    }
}