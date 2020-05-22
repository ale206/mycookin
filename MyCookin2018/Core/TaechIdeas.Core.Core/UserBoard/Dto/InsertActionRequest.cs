using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class InsertActionRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid UserId { get; set; }
        public Guid? UserActionFatherId { get; set; }
        public int UserActionTypeId { get; set; }
        public Guid ActionRelatedObjectId { get; set; }
        public string UserActionMessage { get; set; }
        public int VisibilityId { get; set; }
    }
}