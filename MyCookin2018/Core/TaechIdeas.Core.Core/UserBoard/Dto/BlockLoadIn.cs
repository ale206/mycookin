using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class BlockLoadIn
    {
        public Guid UserId { get; set; }
        public string SortOrder { get; set; }
        public int NumberOfResults { get; set; }
        public string OtherActionsIdToShow { get; set; }
        public int LanguageId { get; set; }
    }
}