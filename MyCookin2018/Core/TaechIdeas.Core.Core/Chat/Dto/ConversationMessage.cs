using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class ConversationMessage
    {
        public Guid IdMessage { get; set; }
        public string Message { get; set; }
        public Guid IdMessageRecipient { get; set; }
        public Guid IdUserConversation { get; set; }
        public Guid IdUserSender { get; set; }
        public DateTime SentOn { get; set; }
        public Guid IdConversation { get; set; }
        public DateTime CreatedOn { get; set; }

        public string RecipientsIDs { get; set; }

        public Guid IdUserConversationOwner { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public bool Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}