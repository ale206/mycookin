using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class ActionOwnerByFatherRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid UserActionFatherId { get; set; }
    }
}