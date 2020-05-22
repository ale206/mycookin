using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat.Dto;

namespace TaechIdeas.Core.Core.Chat
{
    public interface IMessageTypeManager
    {
        IEnumerable<TypeOfMessageInfoByIdOutput> TypeOfMessageInfoById(TypeOfMessageInfoByIdInput typeOfMessageInfoByIdInput);
    }
}