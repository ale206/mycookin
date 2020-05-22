using System;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class AddAlternativePhotoSizeIn
    {
        public Guid MediaAlternativeSizeId { get; set; }
        public Guid MediaId { get; set; }
        public string MediaSizeType { get; set; }
        public string MediaServer { get; set; }
        public string MediaBackupServer { get; set; }
        public string MediaPath { get; set; }
        public string MediaMd5Hash { get; set; }
        public bool MediaOnCdn { get; set; }
    }
}