using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class ActivateUserRequest
    {
        public Guid UserId { get; set; }
        public string IpAddress { get; set; }
        public string ConfirmationCode { get; set; }
    }
}