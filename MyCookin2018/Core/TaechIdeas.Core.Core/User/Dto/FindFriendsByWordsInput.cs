using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class FindFriendsByWordsInput
    {
        public Guid UserId { get; set; }
        public string Words { get; set; }
    }
}