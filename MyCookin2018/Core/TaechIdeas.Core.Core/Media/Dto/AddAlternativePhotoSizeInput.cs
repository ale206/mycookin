using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class AddAlternativePhotoSizeInput
    {
        public MediaType MediaType { get; set; }
        public string PictureCroppedFilePath { get; set; }
        public Guid MediaId { get; set; }
    }
}