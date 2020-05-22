using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class TypeOfMessageInfoByIdOutput
    {
        public MessageType IdMessageType { get; set; }
        public string MessageType { get; set; }
        public int MessageMaxLength { get; set; }
        public bool MessageTypeEnabled { get; set; }
    }
}