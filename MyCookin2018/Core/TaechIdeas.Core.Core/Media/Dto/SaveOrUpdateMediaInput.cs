using System;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class SaveOrUpdateMediaInput
    {
        public Guid? MediaId { get; set; }
        public Guid OwnerId { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public bool IsLink { get; set; }
        public bool IsAnExternalSource { get; set; }
        public string MediaServer { get; set; }
        public string MediaBakcupServer { get; set; }
        public string MediaPath { get; set; }
        public string MediaMd5Hash { get; set; }
        public bool IsChecked { get; set; }
        public Guid? CheckedByUser { get; set; }
        public bool IsMediaDisabled { get; set; }
        public bool IsUserMediaOwner { get; set; }
        public bool IsMediaOnCdn { get; set; }
        public string MediaType { get; set; }
        public DateTime? MediaDeletedOn { get; set; }
    }
}