using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class GenerateNewTemporaryCodeOutput
    {
        public string TemporaryCode { get; set; }
        public Guid UserId { get; set; }
    }
}