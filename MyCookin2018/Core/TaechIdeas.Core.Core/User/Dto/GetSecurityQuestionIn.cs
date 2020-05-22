using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class GetSecurityQuestionIn
    {
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }
}