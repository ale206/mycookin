using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class DeleteLikeIn
    {
        public int UserActionTypeId { get; set; }
        public Guid UserActionFatherId { get; set; }
        public Guid UserId { get; set; }
    }
}