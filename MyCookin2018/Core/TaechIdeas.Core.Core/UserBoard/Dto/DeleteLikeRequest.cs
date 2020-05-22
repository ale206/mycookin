using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class DeleteLikeRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public int UserActionTypeId { get; set; }
        public Guid UserActionFatherId { get; set; }
        public Guid UserId { get; set; }
    }
}