using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class SetAllConversationMessagesAsViewedIn
    {
        public Guid? IDUserConversationOwner { get; set; }
        public Guid? IDConversation { get; set; }
    }
}