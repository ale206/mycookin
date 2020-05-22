using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class DeleteUserActionInput : TokenRequiredInput
    {
        public Guid UserActionId { get; set; }
    }
}