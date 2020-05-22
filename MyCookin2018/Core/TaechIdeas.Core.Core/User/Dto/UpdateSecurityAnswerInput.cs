using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdateSecurityAnswerInput
    {
        public Guid UserId { get; set; }
        public string SecurityAnswer { get; set; }
        public int SecurityQuestionId { get; set; }
    }
}