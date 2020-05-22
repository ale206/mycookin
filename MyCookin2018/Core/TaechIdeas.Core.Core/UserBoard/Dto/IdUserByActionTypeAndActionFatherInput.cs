using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class IdUserByActionTypeAndActionFatherInput : TokenRequiredInput
    {
        public int UserActionTypeId { get; set; }
        public Guid UserActionFatherId { get; set; }
        public int NumberOfResults { get; set; }
    }
}