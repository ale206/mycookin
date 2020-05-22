using System;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class GetMediaNotInCdnOut
    {
        public Guid IDMedia { get; set; }

        public Guid IDMediaOwner { get; set; }

        public bool isImage { get; set; }

        public bool isVideo { get; set; }

        public bool isLink { get; set; }

        public bool isEsternalSource { get; set; }

        public string MediaServer { get; set; }

        public string MediaBakcupServer { get; set; }

        public string MediaPath { get; set; }

        public string MediaMD5Hash { get; set; }

        public bool Checked { get; set; }

        public Guid? CheckedByUser { get; set; }

        public bool MediaDisabled { get; set; }

        public DateTime MediaUpdatedOn { get; set; }

        public DateTime? MediaDeletedOn { get; set; }

        public bool UserIsMediaOwner { get; set; }

        public bool MediaOnCDN { get; set; }

        public string MediaType { get; set; }
    }
}