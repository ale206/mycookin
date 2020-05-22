using TaechIdeas.Core.Core.Statistic.Dto;

namespace TaechIdeas.Core.Core.Statistic
{
    public interface IStatisticManager
    {
        NewStatisticOutput NewStatistic(NewStatisticInput newStatisticInput);
    }
}