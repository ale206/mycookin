using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class CheckIfSharedOnPersonalBoardIn
    {
        public Guid UserId { get; set; }
        public int UserActionTypeId { get; set; }
        public Guid ActionRelatedObjectId { get; set; }
    }
}