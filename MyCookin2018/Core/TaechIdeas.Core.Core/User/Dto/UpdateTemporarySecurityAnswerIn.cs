using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdateTemporarySecurityAnswerIn
    {
        public Guid UserId { get; set; }
        public string TemporaryCode { get; set; }
    }
}