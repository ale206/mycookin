using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class MediaUploadConfigParametersOutput
    {
        public int MediaUploadConfigId { get; set; }
        public MediaType MediaType { get; set; }

        /// <summary>
        ///     Path for Cropped images Ex.: /Photo/Recipe/Cropped/
        /// </summary>
        public string UploadPath { get; set; }

        /// <summary>
        ///     Path for original images
        /// </summary>
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