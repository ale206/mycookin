using System;

namespace TaechIdeas.Core.Core.Statistic.Dto
{
    public class NewStatisticIn
    {
        public Guid? UserId { get; set; }
        public Guid? RelatedObjectId { get; set; }
        public int? ActionType { get; set; }
        public string Comment { get; set; }
        public string OriginFile { get; set; }
        public string SearchString { get; set; }
        public string OtherInfo { get; set; }
    }
}