using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class NewConversationInput
    {
        public Guid? idUserSender { get; set; }
        public Guid? idConversation { get; set; }
        public Guid? idUserRecipient { get; set; }
    }
}