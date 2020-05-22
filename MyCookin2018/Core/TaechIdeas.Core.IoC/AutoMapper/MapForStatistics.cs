using AutoMapper;
using TaechIdeas.Core.Core.Statistic.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForStatistics : Profile
    {
        public MapForStatistics()
        {
            //STATISTICS
            /**************************************************************************/
            CreateMap<NewStatisticInput, NewStatisticIn>();
            CreateMap<NewStatisticOut, NewStatisticOutput>()
                .ForMember(dest => dest.NewStatisticInserted, opt => opt.MapFrom(src => src.isError));

            /**************************************************************************/
        }
    }
}