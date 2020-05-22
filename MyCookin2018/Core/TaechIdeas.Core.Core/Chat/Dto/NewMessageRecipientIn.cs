using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class NewMessageRecipientIn
    {
        public Guid IDUserConversation; //TODO ??

        public Guid IDUserRecipient { get; set; }
        public Guid IDUserSender { get; set; }
        public Guid IDMessage { get; set; }
    }
}