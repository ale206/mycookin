using AutoMapper;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForUserBoard : Profile
    {
        public MapForUserBoard()
        {
            //USERBOARD
            /**************************************************************************/
            CreateMap<BlockElementRequest, BlockElementInput>();
            CreateMap<BlockElementInput, BlockElementIn>();
            CreateMap<BlockElementOut, BlockElementOutput>();
            CreateMap<BlockElementOutput, BlockElementResult>();

            CreateMap<FatherOrSonRequest, FatherOrSonInput>();
            CreateMap<FatherOrSonInput, FatherOrSonIn>();
            CreateMap<FatherOrSonOut, FatherOrSonOutput>();
            CreateMap<FatherOrSonOutput, FatherOrSonResult>();

            CreateMap<WithPaginationRequest, WithPaginationInput>()
                .ForMember(dest => dest.CheckTokenInput, opt => opt.MapFrom(src => src.CheckTokenRequest));
            CreateMap<WithPaginationInput, WithPaginationIn>();
            CreateMap<WithPaginationOut, WithPaginationOutput>()
                .ForMember(dest => dest.UserActionTypeId, opt => opt.MapFrom(src => src.IDUserActionType))
                .ForMember(dest => dest.UserActionTypeLanguageId, opt => opt.MapFrom(src => src.IDUserActionTypeLanguage))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IDLanguage))
                .ForMember(dest => dest.UserActionId, opt => opt.MapFrom(src => src.IDUserAction))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IDUser))
                .ForMember(dest => dest.UserActionFatherId, opt => opt.MapFrom(src => src.IDUserActionFather))
                .ForMember(dest => dest.ActionRelatedObjectId, opt => opt.MapFrom(src => src.IDActionRelatedObject))
                .ForMember(dest => dest.VisibilityId, opt => opt.MapFrom(src => src.IDVisibility));

            CreateMap<WithPaginationOutput, WithPaginationResult>();

            CreateMap<BlockLoadRequest, BlockLoadInput>();
            CreateMap<BlockLoadInput, BlockLoadIn>();
            CreateMap<BlockLoadOut, BlockLoadOutput>();
            CreateMap<BlockLoadOutput, BlockLoadResult>();

            CreateMap<MyNewsBlockLoadRequest, MyNewsBlockLoadInput>();
            CreateMap<MyNewsBlockLoadInput, MyNewsBlockLoadIn>();
            CreateMap<MyNewsBlockLoadOut, MyNewsBlockLoadOutput>();
            CreateMap<MyNewsBlockLoadOutput, MyNewsBlockLoadResult>();

            CreateMap<CheckIfSharedRequest, CheckIfSharedInput>();
            CreateMap<CheckIfSharedInput, CheckIfSharedIn>();
            CreateMap<CheckIfSharedOut, CheckIfSharedOutput>();
            CreateMap<CheckIfSharedOutput, CheckIfSharedResult>();

            CreateMap<InsertActionSharedRequest, InsertActionSharedInput>();
            CreateMap<InsertActionSharedInput, InsertActionSharedIn>();
            CreateMap<InsertActionSharedOut, InsertActionSharedOutput>();
            CreateMap<InsertActionSharedOutput, InsertActionSharedResult>();

            CreateMap<InsertActionRequest, InsertActionInput>()
                .ForMember(dest => dest.CheckTokenInput, opt => opt.MapFrom(src => src.CheckTokenRequest));
            CreateMap<InsertActionInput, InsertActionIn>();
            CreateMap<InsertActionOut, InsertActionOutput>();
            CreateMap<InsertActionOutput, InsertActionResult>();

            CreateMap<CheckIfSharedOnPersonalBoardRequest, CheckIfSharedOnPersonalBoardInput>();
            CreateMap<CheckIfSharedOnPersonalBoardInput, CheckIfSharedOnPersonalBoardIn>();
            CreateMap<CheckIfSharedOnPersonalBoardOut, CheckIfSharedOnPersonalBoardOutput>();
            CreateMap<CheckIfSharedOnPersonalBoardOutput, CheckIfSharedOnPersonalBoardResult>();

            CreateMap<CountLikesOrCommentRequest, CountLikesOrCommentInput>();
            CreateMap<CountLikesOrCommentInput, CountLikesOrCommentIn>();
            CreateMap<CountLikesOrCommentOut, CountLikesOrCommentOutput>();
            CreateMap<CountLikesOrCommentOutput, CountLikesOrCommentResult>();

            CreateMap<CountNumberOfActionsByUserAndTypeRequest, CountNumberOfActionsByUserAndTypeInput>();
            CreateMap<CountNumberOfActionsByUserAndTypeInput, CountNumberOfActionsByUserAndTypeIn>();
            CreateMap<CountNumberOfActionsByUserAndTypeOut, CountNumberOfActionsByUserAndTypeOutput>();
            CreateMap<CountNumberOfActionsByUserAndTypeOutput, CountNumberOfActionsByUserAndTypeResult>();

            CreateMap<DeleteLikeRequest, DeleteLikeInput>();
            CreateMap<DeleteLikeInput, DeleteLikeIn>();
            CreateMap<DeleteLikeOut, DeleteLikeOutput>();
            CreateMap<DeleteLikeOutput, DeleteLikeResult>();

            CreateMap<ActionOwnerByFatherRequest, ActionOwnerByFatherInput>();
            CreateMap<ActionOwnerByFatherInput, ActionOwnerByFatherIn>();
            CreateMap<ActionOwnerByFatherOut, ActionOwnerByFatherOutput>();
            CreateMap<ActionOwnerByFatherOutput, ActionOwnerByFatherResult>();

            CreateMap<IdUserFromIdUserActionRequest, IdUserFromIdUserActionInput>();
            CreateMap<IdUserFromIdUserActionInput, IdUserFromIdUserActionIn>();
            CreateMap<IdUserFromIdUserActionOut, IdUserFromIdUserActionOutput>();
            CreateMap<IdUserFromIdUserActionOutput, IdUserFromIdUserActionResult>();

            CreateMap<IdUserByActionTypeAndActionFatherRequest, IdUserByActionTypeAndActionFatherInput>();
            CreateMap<IdUserByActionTypeAndActionFatherInput, IdUserByActionTypeAndActionFatherIn>();
            CreateMap<IdUserByActionTypeAndActionFatherOut, IdUserByActionTypeAndActionFatherOutput>();
            CreateMap<IdUserByActionTypeAndActionFatherOutput, IdUserByActionTypeAndActionFatherResult>();

            CreateMap<ObjectYouLikeRequest, ObjectYouLikeInput>();
            CreateMap<ObjectYouLikeInput, ObjectYouLikeIn>();
            CreateMap<ObjectYouLikeOut, ObjectYouLikeOutput>();
            CreateMap<ObjectYouLikeOutput, ObjectYouLikeResult>();

            CreateMap<UserActionInfoByIdRequest, UserActionInfoByIdInput>();
            CreateMap<UserActionInfoByIdInput, UserActionInfoByIdIn>();
            CreateMap<UserActionInfoByIdOut, UserActionInfoByIdOutput>();
            CreateMap<UserActionInfoByIdOutput, UserActionInfoByIdResult>();

            CreateMap<DeleteUserActionRequest, DeleteUserActionInput>();
            CreateMap<DeleteUserActionInput, DeleteUserActionIn>();
            CreateMap<DeleteUserActionOut, DeleteUserActionOutput>();
            CreateMap<DeleteUserActionOutput, DeleteUserActionResult>();

            CreateMap<TemplateByTypeAndLanguageRequest, TemplateByTypeAndLanguageInput>();
            CreateMap<TemplateByTypeAndLanguageInput, TemplateByTypeAndLanguageIn>();
            CreateMap<TemplateByTypeAndLanguageOut, TemplateByTypeAndLanguageOutput>();
            CreateMap<TemplateByTypeAndLanguageOutput, TemplateByTypeAndLanguageResult>();
        }
    }
}