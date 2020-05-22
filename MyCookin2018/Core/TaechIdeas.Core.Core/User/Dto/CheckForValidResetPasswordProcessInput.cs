using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class CheckForValidResetPasswordProcessInput
    {
        public Guid UserId { get; set; }
        public string ConfirmationCode { get; set; }
    }
}