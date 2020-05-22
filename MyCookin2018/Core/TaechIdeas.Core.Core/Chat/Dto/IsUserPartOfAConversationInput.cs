using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class IsUserPartOfAConversationInput
    {
        public Guid? idUserSender { get; set; }
        public Guid? idConversation { get; set; }
    }
}