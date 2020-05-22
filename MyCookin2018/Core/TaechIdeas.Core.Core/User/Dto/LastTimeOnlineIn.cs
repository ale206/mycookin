using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class LastTimeOnlineIn
    {
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
        public int Offset { get; set; }
    }
}