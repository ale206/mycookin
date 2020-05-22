using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class MessageByIdOut
    {
        public Guid IdMessage { get; set; }
        public int IdMessageType { get; set; }
        public string Message { get; set; }
    }
}