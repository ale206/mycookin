using System;

namespace TaechIdeas.Core.Core.Audit.Dto
{
    public class CheckUserSpamReportedInput
    {
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
    }
}