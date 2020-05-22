using System;
using System.Collections.Generic;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class NewMessageRecipientOut
    {
        public Guid IdMessageRecipient { get; set; }
        public Guid IdMessage { get; set; }
        public NewConversationOut ConversationOut { get; set; }
        public Guid IdUserSender { get; set; }
        public Guid IdUserRecipient { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime? ViewedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public List<NewConversationOut> UserConversationComponents { get; set; }
        public Guid IdUserConversationOwner { get; set; } //Owner of the conversation
        public bool OnlyMessageToRead { get; set; }
        public Guid IdConversation { get; set; }
    }
}