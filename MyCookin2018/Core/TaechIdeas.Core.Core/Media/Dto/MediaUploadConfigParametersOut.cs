using TaechIdeas.Core.Core.Media.Enums;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class MediaUploadConfigParametersOut
    {
        public int IdMediaUploadConfig { get; set; }
        public MediaType MediaType { get; set; }
        public string UploadPath { get; set; }
        public string UploadOriginalFilePath { get; set; }
        public int MaxSizeByte { get; set; }
        public string AcceptedContentTypes { get; set; }
        public string AcceptedFileExtension { get; set; }
        public string AcceptedFileExtensionRegex { get; set; }
        public bool EnableUploadForMediaType { get; set; }
        public bool ComputeMd5Hash { get; set; }
        public int MediaFinalHeight { get; set; }
        public int MediaFinalWidth { get; set; }
        public int MediaPercPlusSizeForCrop { get; set; }
        public int MediaSmallerSideMinSize { get; set; }
    }
}