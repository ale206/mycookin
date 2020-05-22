using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class BlockElementRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid UserActionId { get; set; }
        public int LanguageId { get; set; }
    }
}