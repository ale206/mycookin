using TaechIdeas.Core.Core.Media.Enums;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class MediaAlternativeSizeConfigParametersOut
    {
        public MediaType MediaType { get; set; }
        public string MediaSizeType { get; set; }
        public string SavePath { get; set; }
        public int MediaHeight { get; set; }
        public int MediaWidth { get; set; }
    }
}