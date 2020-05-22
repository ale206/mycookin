namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class TypeOfMessageInfoByIdOut
    {
        public int IdMessageType { get; set; }
        public string MessageType { get; set; }
        public int MessageMaxLength { get; set; }
        public bool MessageTypeEnabled { get; set; }
    }
}