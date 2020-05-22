using System;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class MediaByIdOutput
    {
        public Guid IdMedia { get; set; }
        public Guid? IDMediaOwner { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public bool IsLink { get; set; }
        public bool IsEsternalSource { get; set; }
        public string MediaServer { get; set; }
        public string MediaBakcupServer { get; set; }
        public string MediaPath { get; set; }
        public string MediaMd5Hash { get; set; }
        public bool Checked { get; set; }
        public Guid? CheckedByUser { get; set; }
        public bool MediaDisabled { get; set; }
        public DateTime MediaUpdatedOn { get; set; }
        public DateTime? MediaDeletedOn { get; set; }
        public bool UserIsMediaOwner { get; set; }
        public bool MediaOnCdn { get; set; }
        public string MediaType { get; set; }
    }
}