using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class MyNewsBlockLoadInput : TokenRequiredInput
    {
        public Guid UserId { get; set; }
        public string SortOrder { get; set; }
        public int NumberOfResults { get; set; }
        public string OtherActionsIdToShow { get; set; }
        public int LanguageId { get; set; }
    }
}