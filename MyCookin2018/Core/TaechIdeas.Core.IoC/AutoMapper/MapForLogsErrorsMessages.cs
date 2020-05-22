using AutoMapper;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForLogsErrorsMessages : Profile
    {
        public MapForLogsErrorsMessages()
        {
            //MEDIA
            /**************************************************************************/
            CreateMap<GetErrorOrMessageInput, GetErrorOrMessageIn>();
            CreateMap<GetErrorOrMessageOut, GetErrorOrMessageOutput>();

            CreateMap<InsertErrorLogInput, InsertErrorLogIn>();
            CreateMap<InsertErrorLogOut, InsertErrorLogOutput>();

            CreateMap<GetLastErrorLogDateInput, GetLastErrorLogDateIn>();
            CreateMap<GetLastErrorLogDateOut, GetLastErrorLogDateOutput>();

            CreateMap<DeleteErrorByErrorMessageInput, DeleteErrorByErrorMessageIn>();
            CreateMap<DeleteErrorByErrorMessageOut, DeleteErrorByErrorMessageOutput>();
            CreateMap<DeleteErrorByErrorMessageRequest, DeleteErrorByErrorMessageInput>();
            CreateMap<DeleteErrorByErrorMessageOutput, DeleteErrorByErrorMessageResult>();

            CreateMap<GetErrorsListInput, GetErrorsListIn>();
            CreateMap<GetErrorsListOut, GetErrorsListOutput>()
                .ForMember(dest => dest.IsApplicationError, opt => opt.MapFrom(src => src.isApplicationError))
                .ForMember(dest => dest.IsStoredProcedureError, opt => opt.MapFrom(src => src.isStoredProcedureError))
                .ForMember(dest => dest.IsTriggerError, opt => opt.MapFrom(src => src.isTriggerError));
            CreateMap<GetErrorsListOutput, GetErrorsListResult>();
            CreateMap<GetErrorsListRequest, GetErrorsListInput>();

            /**************************************************************************/
        }
    }
}