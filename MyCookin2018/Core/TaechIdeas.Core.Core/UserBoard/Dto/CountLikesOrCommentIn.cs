using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class CountLikesOrCommentIn
    {
        public int UserActionTypeId { get; set; }
        public Guid UserActionFatherId { get; set; }
    }
}