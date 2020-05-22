using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class DeleteLikeInput : TokenRequiredInput
    {
        public int UserActionTypeId { get; set; }
        public Guid UserActionFatherId { get; set; }
        public Guid UserId { get; set; }
    }
}