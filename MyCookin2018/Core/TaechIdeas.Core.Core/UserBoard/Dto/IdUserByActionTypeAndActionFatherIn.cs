using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class IdUserByActionTypeAndActionFatherIn
    {
        public int UserActionTypeId { get; set; }
        public Guid UserActionFatherId { get; set; }
        public int NumberOfResults { get; set; }
    }
}