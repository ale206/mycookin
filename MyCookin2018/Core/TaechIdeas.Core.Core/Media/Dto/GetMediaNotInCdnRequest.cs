using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class GetMediaNotInCdnRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public int NumRow { get; set; }
        public string MediaSizeType { get; set; }
    }
}