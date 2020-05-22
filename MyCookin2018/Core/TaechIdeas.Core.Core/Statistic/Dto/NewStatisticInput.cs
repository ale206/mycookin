using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Statistic.Dto
{
    public class NewStatisticInput
    {
        public Guid IdUserActionStatistic { get; set; }
        public Guid? IdUser { get; set; }
        public Guid? IdRelatedObject { get; set; }
        public StatisticsActionType StatisticsActionType { get; set; }
        public string Comments { get; set; }
        public string FileOrigin { get; set; }
        public string SearchString { get; set; }
        public string OtherInfo { get; set; }
    }
}