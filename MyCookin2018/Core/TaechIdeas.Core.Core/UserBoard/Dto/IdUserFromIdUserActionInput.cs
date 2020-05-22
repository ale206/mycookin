using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class IdUserFromIdUserActionInput : TokenRequiredInput
    {
        public Guid UserActionId { get; set; }
    }
}