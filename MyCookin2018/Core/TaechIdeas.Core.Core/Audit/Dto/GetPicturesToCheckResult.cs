using System;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class GetPicturesToCheckResult
    {
        public Guid AuditEventId { get; set; }
        public string ImageUrl { get; set; }
        public string Username { get; set; }
        public DateTime? EventInsertedOn { get; set; }
        public Guid? ObjectId { get; set; }
    }
}