using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class NumberOfProfileVisitsInput
    {
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }
}