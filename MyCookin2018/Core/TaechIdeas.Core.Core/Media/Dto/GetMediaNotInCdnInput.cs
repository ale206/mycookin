using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class GetMediaNotInCdnInput : TokenRequiredInput
    {
        public int NumRow { get; set; }
        public string MediaSizeType { get; set; }
    }
}