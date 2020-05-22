using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class MediaAlternativeSizeConfigParametersOutput
    {
        public MediaType MediaType { get; set; }
        public string MediaSizeType { get; set; }
        public string SavePath { get; set; }
        public int MediaHeight { get; set; }
        public int MediaWidth { get; set; }
    }
}