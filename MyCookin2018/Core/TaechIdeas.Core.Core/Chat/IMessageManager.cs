using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat.Dto;

namespace TaechIdeas.Core.Core.Chat
{
    public interface IMessageManager
    {
        InsertNewMessageOutput InsertNewMessage(InsertNewMessageInput insertNewMessageInput);
        IEnumerable<MessageByIdOutput> MessageById(MessageByIdInput messageInfoByIdInput);
    }
}