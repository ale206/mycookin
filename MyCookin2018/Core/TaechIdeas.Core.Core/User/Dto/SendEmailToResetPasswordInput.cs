using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class SendEmailToResetPasswordInput
    {
        public Guid UserId { get; set; }
        public string ConfirmationCode { get; set; }
        public string Email { get; set; }
        public int LanguageId { get; set; }
    }
}