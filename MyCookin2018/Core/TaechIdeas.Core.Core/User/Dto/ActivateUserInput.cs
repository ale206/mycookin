using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class ActivateUserInput
    {
        public Guid UserId { get; set; }
        public string IpAddress { get; set; }
        public string ConfirmationCode { get; set; }
    }
}