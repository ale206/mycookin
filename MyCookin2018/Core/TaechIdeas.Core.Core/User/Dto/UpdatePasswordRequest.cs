using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdatePasswordRequest
    {
        public Guid UserId { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmationCode { get; set; }
    }
}