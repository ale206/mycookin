using AutoMapper;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.Core.Verification.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForVerification : Profile
    {
        public MapForVerification()
        {
            //VERIFICATION
            /**************************************************************************/
            CreateMap<UsernameAlreadyExistsRequest, UsernameAlreadyExistsInput>();
            CreateMap<UsernameAlreadyExistsInput, UsernameAlreadyExistsIn>();
            CreateMap<UsernameAlreadyExistsOut, UsernameAlreadyExistsOutput>();
            CreateMap<UsernameAlreadyExistsOutput, UsernameAlreadyExistsResult>();

            CreateMap<EmailAlreadyExistsRequest, EmailAlreadyExistsInput>();
            CreateMap<EmailAlreadyExistsInput, EmailAlreadyExistsIn>();
            CreateMap<EmailAlreadyExistsOut, EmailAlreadyExistsOutput>();
            CreateMap<EmailAlreadyExistsOutput, EmailAlreadyExistsResult>();

            CreateMap<CheckTokenRequest, CheckTokenInput>();
            CreateMap<CheckTokenInput, CheckTokenIn>();
            /**************************************************************************/
        }
    }
}