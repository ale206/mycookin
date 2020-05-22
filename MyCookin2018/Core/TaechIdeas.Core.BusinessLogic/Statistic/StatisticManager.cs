using AutoMapper;
using TaechIdeas.Core.Core.Statistic;
using TaechIdeas.Core.Core.Statistic.Dto;

namespace TaechIdeas.Core.BusinessLogic.Statistic
{
    public class StatisticManager : IStatisticManager
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly IMapper _mapper;

        public StatisticManager(IStatisticRepository statisticRepository, IMapper mapper)
        {
            _statisticRepository = statisticRepository;
            _mapper = mapper;
        }

        #region NewStatistic

        public NewStatisticOutput NewStatistic(NewStatisticInput newStatisticInput)
        {
            return _mapper.Map<NewStatisticOutput>(_statisticRepository.NewStatistic(_mapper.Map<NewStatisticIn>(newStatisticInput)));
        }

        #endregion
    }
}