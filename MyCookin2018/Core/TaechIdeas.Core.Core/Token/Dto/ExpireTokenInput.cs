using System;

namespace TaechIdeas.Core.Core.Token.Dto
{
    public class ExpireTokenInput
    {
        public Guid UserToken { get; set; }
        public Guid UserId { get; set; }
    }
}