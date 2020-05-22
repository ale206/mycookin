using AutoMapper;
using TaechIdeas.Core.Core.Audit.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForAudit : Profile
    {
        public MapForAudit()
        {
            //AUDIT
            /**************************************************************************/
            CreateMap<AutoAuditConfigInfoOut, AutoAuditConfigInfoOutput>();
            CreateMap<AutoAuditConfigInfoInput, AutoAuditConfigInfoIn>();

            CreateMap<NewAuditEventOut, NewAuditEventOutput>()
                .ForMember(dest => dest.UspReturnValue, opt => opt.MapFrom(src => src.USPReturnValue))
                .ForMember(dest => dest.IsError, opt => opt.MapFrom(src => src.isError));
            CreateMap<NewAuditEventInput, NewAuditEventIn>();

            CreateMap<UpdateAuditEventOut, UpdateAuditEventOutput>()
                .ForMember(dest => dest.UspReturnValue, opt => opt.MapFrom(src => src.USPReturnValue))
                .ForMember(dest => dest.IsError, opt => opt.MapFrom(src => src.isError));
            CreateMap<UpdateAuditEventInput, UpdateAuditEventIn>();

            CreateMap<CheckUserSpamReportedOut, CheckUserSpamReportedOutput>()
                .ForMember(dest => dest.AuditEventId, opt => opt.MapFrom(src => src.IDAuditEvent))
                .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectID))
                .ForMember(dest => dest.EventUpdatedById, opt => opt.MapFrom(src => src.IDEventUpdatedBy));
            CreateMap<CheckUserSpamReportedInput, CheckUserSpamReportedIn>();

            CreateMap<DeleteByObjectIdOut, DeleteByObjectIdOutput>();
            CreateMap<DeleteByObjectIdInput, DeleteByObjectIdIn>();

            CreateMap<GetAuditEventByIdOut, GetAuditEventByIdOutput>()
                .ForMember(dest => dest.AuditEventId, opt => opt.MapFrom(src => src.IDAuditEvent))
                .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectID))
                .ForMember(dest => dest.EventUpdatedById, opt => opt.MapFrom(src => src.IDEventUpdatedBy));
            CreateMap<GetAuditEventByIdInput, GetAuditEventByIdIn>();

            CreateMap<GetAuditEventToCheckOut, GetAuditEventToCheckOutput>()
                .ForMember(dest => dest.AuditEventId, opt => opt.MapFrom(src => src.IDAuditEvent))
                .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectID))
                .ForMember(dest => dest.EventUpdatedById, opt => opt.MapFrom(src => src.IDEventUpdatedBy))
                .ForMember(dest => dest.MediaOwnerId, opt => opt.MapFrom(src => src.IDMediaOwner));
            CreateMap<GetAuditEventToCheckInput, GetAuditEventToCheckIn>();

            CreateMap<GetNumberOfEventToCheckOut, GetNumberOfEventToCheckOutput>();
            CreateMap<GetNumberOfEventToCheckInput, GetNumberOfEventToCheckIn>();

            CreateMap<GetObjectIdNumberOfEveniencesOut, GetObjectIdNumberOfEveniencesOutput>()
                .ForMember(dest => dest.AuditEventId, opt => opt.MapFrom(src => src.IDAuditEvent))
                .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectID))
                .ForMember(dest => dest.EventUpdatedById, opt => opt.MapFrom(src => src.IDEventUpdatedBy));
            CreateMap<GetObjectIdNumberOfEveniencesInput, GetObjectIdNumberOfEveniencesIn>();

            CreateMap<GetAuditEventToCheckOutput, GetPicturesToCheckOutput>();

            CreateMap<GetPicturesToCheckRequest, GetPicturesToCheckInput>()
                .ForMember(dest => dest.CheckTokenInput, opt => opt.MapFrom(src => src.CheckTokenRequest));
            //g.CreateMap<GetPicturesToCheckInput, GetPicturesToCheckIn>();
            //g.CreateMap<GetPicturesToCheckOut, GetPicturesToCheckOutput>();
            CreateMap<GetPicturesToCheckOutput, GetPicturesToCheckResult>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ObjectTxtInfo));
        }
    }
}