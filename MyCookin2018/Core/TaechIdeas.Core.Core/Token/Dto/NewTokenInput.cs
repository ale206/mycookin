using System;

namespace TaechIdeas.Core.Core.Token.Dto
{
    public class NewTokenInput
    {
        public Guid UserId { get; set; }
        public int TokenExpireMinutes { get; set; }
        public string Source { get; set; }
    }
}