using System;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class GetPicturesToCheckOutput
    {
        public Guid AuditEventId { get; set; }
        public Guid? ObjectId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? EventInsertedOn { get; set; }
        public string ObjectTxtInfo { get; set; }
        public Guid MediaOwnerId { get; set; }
        public string UserName { get; set; }
    }
}