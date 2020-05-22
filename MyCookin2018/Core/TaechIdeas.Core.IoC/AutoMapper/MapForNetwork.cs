using AutoMapper;
using TaechIdeas.Core.Core.Network.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForNetwork : Profile
    {
        public MapForNetwork()
        {
            //Network Service
            /**************************************************************************/
            CreateMap<EmailsToSendInput, EmailsToSendIn>();
            CreateMap<EmailsToSendOut, EmailsToSendOutput>()
                .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.Id));

            CreateMap<SaveEmailToSendInput, SaveEmailToSendIn>();
            CreateMap<SaveEmailToSendOut, SaveEmailToSendOutput>();

            CreateMap<EmailsToSendOutput, SendEmailInput>();
            CreateMap<SendEmailInput, UpdateEmailStatusIn>();
            CreateMap<UpdateEmailStatusOut, SendEmailOutput>();

            /**************************************************************************/
        }
    }
}