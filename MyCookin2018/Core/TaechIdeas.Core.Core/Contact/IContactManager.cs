using System.Collections.Generic;
using TaechIdeas.Core.Core.Contact.Dto;

namespace TaechIdeas.Core.Core.Contact
{
    public interface IContactManager
    {
        NewMessageOutput NewMessage(NewMessageInput newMessageInput);
        IEnumerable<RequestMessagesOutput> RequestMessages(RequestMessagesInput requestMessagesInput);
        NewReplyOutput NewReply(NewReplyInput newReplyInput);
    }
}