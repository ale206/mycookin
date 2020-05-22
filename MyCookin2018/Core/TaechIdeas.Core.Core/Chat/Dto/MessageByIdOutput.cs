using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class MessageByIdOutput
    {
        public Guid IdMessage { get; set; }
        public MessageType IdMessageType { get; set; }
        public string Message { get; set; }
    }
}