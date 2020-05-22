using AutoMapper;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForCommon : Profile
    {
        public MapForCommon()
        {
            //COMMON
            /**************************************************************************/
            CreateMap<PaginationFieldsRequest, PaginationFieldsInput>();
            CreateMap<PaginationFieldsInput, PaginationFieldsIn>();
            CreateMap<PaginationFieldsOut, PaginationFieldsOutput>();
            CreateMap<PaginationFieldsOutput, PaginationFieldsResult>();

            //cfg.CreateMap<MyWebsite, BusinessLogic.Core.Charlie.Common.Enums.MyWebsite>();
            //cfg.CreateMap<BusinessLogic.Core.Charlie.Common.Enums.MyWebsite, MyWebsite>();

            /**************************************************************************/
        }
    }
}