using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class SuggestedUsersInput
    {
        public Guid RequesterId { get; set; }
        public int RowOffset { get; set; }
        public int FetchRows { get; set; }
    }
}