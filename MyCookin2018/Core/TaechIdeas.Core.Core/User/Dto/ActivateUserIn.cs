using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class ActivateUserIn
    {
        public Guid UserId { get; set; }
        public string Ip { get; set; }
        public DateTime AccountExpireOn { get; set; }
    }
}