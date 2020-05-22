using TaechIdeas.Core.Core.Statistic.Dto;

namespace TaechIdeas.Core.Core.Statistic
{
    public interface IStatisticRepository
    {
        NewStatisticOut NewStatistic(NewStatisticIn newStatisticIn);
    }
}