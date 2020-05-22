using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class SuggestedUsersIn
    {
        public Guid RequesterId { get; set; }
        public int RowOffset { get; set; }
        public int FetchRows { get; set; }
    }
}