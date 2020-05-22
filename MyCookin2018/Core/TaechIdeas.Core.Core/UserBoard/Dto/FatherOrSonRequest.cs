using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class FatherOrSonRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public int UserActionTypeId { get; set; }
        public Guid UserId { get; set; }
        public Guid UserActionFatherId { get; set; }
        public int NumberOfResults { get; set; }
        public string SortOrder { get; set; }
        public int LanguageId { get; set; }
    }
}