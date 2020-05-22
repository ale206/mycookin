using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class CheckIfSharedOnPersonalBoardInput : TokenRequiredInput
    {
        public Guid UserId { get; set; }
        public int UserActionTypeId { get; set; }
        public Guid ActionRelatedObjectId { get; set; }
    }
}