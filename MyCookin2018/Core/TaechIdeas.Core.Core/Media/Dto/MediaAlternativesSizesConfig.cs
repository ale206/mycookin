using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class MediaAlternativesSizesConfig
    {
        public int IdMediaAlternativeSizeConfig { get; set; }
        public MediaType MediaType { get; set; }
        public MediaSizeTypes MediaSizeType { get; set; }
        public string SavePath { get; set; }
        public int? MediaHeight { get; set; }
        public int? MediaWidth { get; set; }
    }
}