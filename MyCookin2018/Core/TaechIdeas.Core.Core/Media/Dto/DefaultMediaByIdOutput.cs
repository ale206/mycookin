using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class DefaultMediaByIdOutput : MediaByIdOutput
    {
        public int IdDefaultMediaForObject { get; set; }
        public ObjectType ObjectCode { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
    }
}