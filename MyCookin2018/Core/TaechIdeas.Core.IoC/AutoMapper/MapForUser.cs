using AutoMapper;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForUser : Profile
    {
        public MapForUser()
        {
            //USER
            /**************************************************************************/
            CreateMap<LoginUserResult, CheckTokenRequest>();

            CreateMap<UserIdFromSocialLoginsInput, UserIdFromSocialLoginsIn>();
            CreateMap<UpdateSocialTokensInput, UpdateSocialTokensIn>();
            CreateMap<CheckTokenRequest, CheckTokenInput>();
            CreateMap<CheckTokenOutput, CheckTokenResult>();
            CreateMap<CheckTokenInput, CheckTokenIn>();
            CreateMap<CheckTokenOut, CheckTokenOutput>();

            CreateMap<LoginUserRequest, LoginUserInput>();
            CreateMap<LoginUserInput, VerifyNewUserLoginRequestInput>();
            CreateMap<LoginUserInput, LoginUserIn>();
            CreateMap<LoginUserOutput, LoginUserResult>();

            CreateMap<LogoutUserRequest, LogoutUserInput>();
            CreateMap<LogoutUserOutput, LogoutUserResult>();
            CreateMap<LogoutUserInput, LogoutUserIn>();
            CreateMap<LogoutUserOut, LogoutUserOutput>();

            CreateMap<UserIdFromSocialLoginsOut, UserIdFromSocialLoginsOutput>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IDUser));
            CreateMap<UserIdFromSocialLoginsOutput, UserIdFromSocialLoginsResult>();

            CreateMap<NewSocialLoginInput, NewSocialLoginIn>();
            CreateMap<UpdateLastLogonAndSetUserAsOnLineInput, UpdateLastLogonAndSetUserAsOnLineIn>();

            CreateMap<UserIdByUserTokenInput, UserIdByUserTokenIn>();
            CreateMap<UserIdByUserTokenOut, UserIdByUserTokenOutput>();
            CreateMap<UserIdByUserTokenOutput, UserIdByUserTokenResult>();

            CreateMap<ActivateUserRequest, ActivateUserInput>();
            CreateMap<ActivateUserInput, ActivateUserIn>();
            CreateMap<ActivateUserOut, ActivateUserOutput>();
            CreateMap<ActivateUserOutput, ActivateUserResult>();
            CreateMap<ActivateUserInput, UserByIdInput>()
                .ForMember(dest => dest.CheckTokenInput, opt => opt.MapFrom(src => new CheckTokenInput {UserId = src.UserId}));

            CreateMap<UserProfilePathInput, UserByIdInput>();
            CreateMap<NewUserRequest, NewUserInput>();
            CreateMap<NewUserInput, SendEmailToConfirmRegistrationInput>();
            CreateMap<NewUserInput, VerifyNewUserRequestInput>();
            CreateMap<NewUserInput, UsernameAlreadyExistsInput>();
            CreateMap<NewUserInput, EmailAlreadyExistsInput>();
            CreateMap<NewUserOutput, NewUserResult>();

            CreateMap<NewTokenInput, NewTokenIn>();
            CreateMap<NewTokenOut, NewTokenOutput>();

            CreateMap<UserByIdRequest, UserByIdInput>()
                .ForMember(dest => dest.CheckTokenInput, opt => opt.MapFrom(src => src.CheckTokenRequest));
            CreateMap<UserByIdOut, UserByIdOutput>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdUser))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.IdGender))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMail))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IdLanguage))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.IdCity))
                .ForMember(dest => dest.ProfilePhotoId, opt => opt.MapFrom(src => src.IdProfilePhoto))
                .ForMember(dest => dest.SecurityQuestionId, opt => opt.MapFrom(src => src.IdSecurityQuestion))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));

            CreateMap<UserByIdOutput, UserByIdResult>();

            CreateMap<UpdateSocialTokensRequest, UpdateSocialTokensInput>();
            CreateMap<UpdateSocialTokensOutput, UpdateSocialTokensResult>();
            CreateMap<UpdateSocialTokensInput, UpdateSocialTokensIn>();
            CreateMap<UpdateSocialTokensOut, UpdateSocialTokensOutput>();

            CreateMap<UserByEmailOut, UserByEmailOutput>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdUser))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.IdGender))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMail))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IdLanguage))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.IdCity))
                .ForMember(dest => dest.ProfilePhotoId, opt => opt.MapFrom(src => src.IdProfilePhoto))
                .ForMember(dest => dest.SecurityQuestionId, opt => opt.MapFrom(src => src.IdSecurityQuestion))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));

            CreateMap<UserByEmailOutput, UserByEmailResult>();

            CreateMap<UserByEmailInput, UserByEmailIn>();

            CreateMap<ResetPasswordProcessRequest, ResetPasswordProcessInput>();
            CreateMap<ResetPasswordProcessOutput, ResetPasswordProcessResult>();
            CreateMap<ResetPasswordProcessInput, UserByEmailInput>();
            CreateMap<UserByEmailOutput, GenerateNewTemporaryCodeInput>();
            CreateMap<GenerateNewTemporaryCodeOutput, UpdateTemporarySecurityAnswerInput>();

            CreateMap<UpdateTemporarySecurityAnswerInput, UpdateTemporarySecurityAnswerIn>();
            CreateMap<UpdateTemporarySecurityAnswerOut, UpdateTemporarySecurityAnswerOutput>();

            CreateMap<CheckForValidResetPasswordProcessRequest, CheckForValidResetPasswordProcessInput>();
            CreateMap<UpdatePasswordInput, CheckForValidResetPasswordProcessInput>();
            CreateMap<CheckForValidResetPasswordProcessOutput, CheckForValidResetPasswordProcessResult>();
            CreateMap<CheckForValidResetPasswordProcessInput, UserByIdInput>();
            CreateMap<CheckForValidResetPasswordProcessInput, UpdateTemporarySecurityAnswerInput>();

            CreateMap<UpdatePasswordRequest, UpdatePasswordInput>();
            CreateMap<UpdatePasswordInput, UpdatePasswordIn>();
            CreateMap<UpdatePasswordOut, UpdatePasswordOutput>();
            CreateMap<UpdatePasswordOutput, UpdatePasswordResult>();

            CreateMap<UpdatePasswordInput, UpdateTemporarySecurityAnswerInput>();

            CreateMap<UserByIdInput, UserByIdIn>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CheckTokenInput.UserId));

            CreateMap<UserByIdOut, UserByIdOutput>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdUser))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.IdGender))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMail))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IdLanguage))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.IdCity))
                .ForMember(dest => dest.ProfilePhotoId, opt => opt.MapFrom(src => src.IdProfilePhoto))
                .ForMember(dest => dest.SecurityQuestionId, opt => opt.MapFrom(src => src.IdSecurityQuestion))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));

            CreateMap<NewSocialLoginRequest, NewSocialLoginInput>();
            CreateMap<NewSocialLoginOutput, NewSocialLoginResult>();
            CreateMap<NewSocialLoginInput, NewSocialLoginIn>();
            CreateMap<NewSocialLoginOut, NewSocialLoginOutput>();

            CreateMap<SecurityQuestionsByLanguageRequest, SecurityQuestionsByLanguageInput>();
            CreateMap<SecurityQuestionsByLanguageInput, SecurityQuestionsByLanguageIn>();
            CreateMap<SecurityQuestionsByLanguageOut, SecurityQuestionsByLanguageOutput>()
                .ForMember(dest => dest.SecurityQuestionId, opt => opt.MapFrom(src => src.IDSecurityQuestion));
            CreateMap<SecurityQuestionsByLanguageOutput, SecurityQuestionsByLanguageResult>();

            CreateMap<GenderByLanguageRequest, GenderByLanguageInput>();
            CreateMap<GenderByLanguageInput, GenderByLanguageIn>();
            CreateMap<GenderByLanguageOut, GenderByLanguageOutput>()
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.IDGender));
            CreateMap<GenderByLanguageOutput, GenderByLanguageResult>();

            CreateMap<DeleteAccountRequest, DeleteAccountInput>()
                .ForMember(dest => dest.CheckTokenInput, opt => opt.MapFrom(src => src.CheckTokenRequest));
            CreateMap<DeleteAccountInput, DeleteAccountIn>()
                .ForMember(dest => dest.UserToken, opt => opt.MapFrom(src => src.CheckTokenInput.UserToken));
            CreateMap<DeleteAccountOut, DeleteAccountOutput>();
            CreateMap<DeleteAccountOutput, DeleteAccountResult>();

            CreateMap<UpdateUserInfoRequest, UpdateUserInfoInput>()
                .ForMember(dest => dest.CheckTokenInput, opt => opt.MapFrom(src => src.CheckTokenRequest));
            CreateMap<UpdateUserInfoInput, UpdateUserInfoIn>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CheckTokenInput.UserId));
            CreateMap<UpdateUserInfoOut, UpdateUserInfoOutput>();
            CreateMap<UpdateUserInfoOutput, UpdateUserInfoResult>();

            CreateMap<FacebookLoginRequest, FacebookLoginInput>();
            //g.CreateMap<FacebookLoginInput, FacebookLoginIn>();
            //g.CreateMap<FacebookLoginOut, FacebookLoginOutput>();
            CreateMap<FacebookLoginOutput, FacebookLoginResult>();

            CreateMap<FacebookLoginInput, UserByEmailInput>();

            CreateMap<LoginUserByIdInput, LoginUserByIdIn>();
            CreateMap<LoginUserByIdOut, LoginUserByIdOutput>();

            CreateMap<LoginUserByIdOutput, FacebookLoginOutput>();
            CreateMap<LoginUserByIdInput, UserByEmailInput>();

            CreateMap<UserByIdResult, UpdateUserInfoRequest>();

            /**************************************************************************/
        }
    }
}