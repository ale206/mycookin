using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdateTemporarySecurityAnswerInput
    {
        public Guid UserId { get; set; }
        public string TemporaryCode { get; set; }
    }
}