using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class FatherOrSonIn
    {
        public int UserActionTypeId { get; set; }
        public Guid UserId { get; set; }
        public Guid UserActionFatherId { get; set; }
        public int NumberOfResults { get; set; }
        public string SortOrder { get; set; }
        public int LanguageId { get; set; }
    }
}