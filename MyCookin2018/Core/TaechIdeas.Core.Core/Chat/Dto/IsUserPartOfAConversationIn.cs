using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class IsUserPartOfAConversationIn
    {
        public Guid? IDUserSender { get; set; }
        public Guid? IDConversation { get; set; }
    }
}