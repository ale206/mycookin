using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class SetAllConversationMessagesAsViewedInput
    {
        public Guid? idUserConversationOwner { get; set; }
        public Guid? idConversation { get; set; }
    }
}