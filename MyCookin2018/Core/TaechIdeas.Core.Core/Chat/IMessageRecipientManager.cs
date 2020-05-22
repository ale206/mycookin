using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat.Dto;

namespace TaechIdeas.Core.Core.Chat
{
    public interface IMessageRecipientManager
    {
        IEnumerable<NewMessageRecipientOutput> NewMessageRecipient(NewMessageRecipientInput newMessageRecipientInput);
        SetMessageAsViewedOutput SetMessageAsViewed(SetMessageAsViewedInput setMessageAsViewedInput);
        SetAllConversationMessagesAsViewedOutput SetAllConversationMessagesAsViewed(SetAllConversationMessagesAsViewedInput setAllConversationMessagesAsViewedInpu);
        SetMessageAsDeletedOutput SetMessageAsDeleted(SetMessageAsDeletedInput setMessageAsDeletedInput);
    }
}