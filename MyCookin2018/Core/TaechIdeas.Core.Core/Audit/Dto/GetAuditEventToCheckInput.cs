using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class GetAuditEventToCheckInput
    {
        public int NumberOfResults { get; set; }
        public ObjectType ObjectType { get; set; }
    }
}