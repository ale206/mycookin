using System;
using System.Collections.Generic;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class NewMessageRecipientInput
    {
        public IEnumerable<UserConversationComponentsInput> userConversationComponents { get; set; }
        public Guid idUserSender { get; set; }
        public Guid idMessage { get; set; }
    }
}