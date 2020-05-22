using System.Collections.Generic;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.Core.Contact
{
    public interface IContactRepository
    {
        NewMessageOut NewMessage(NewMessageIn newMessageIn);
        IEnumerable<RequestMessagesOut> RequestMessages(RequestMessagesIn requestMessagesIn);
        NewReplyOut NewReply(NewReplyIn newReplyIn);
    }
}