using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat.Dto;

namespace TaechIdeas.Core.Core.Chat
{
    public interface IConversationMessageManager
    {
        IEnumerable<ConversationMessage> ViewConversation(Guid? idUserConversationOwner, Guid? idConversation);

        List<ConversationMessage> ViewConversationPaged(Guid idUserConversationOwner, Guid idConversation,
            int offset, int pageSize);

        IEnumerable<ConversationMessage> GetMessagesToRead(Guid? idUserConversationOwner);
        IEnumerable<ConversationMessage> GetMessagesToReadByUser(Guid? idUserConversationOwner, Guid? idUserSender);
        int GetMessagesNumber(Guid? idUserConversationOwner, Guid? idConversation);
        IEnumerable<ConversationMessage> SendNewMessage(Guid idUserSender, string recipientsIDs, string message);
    }
}