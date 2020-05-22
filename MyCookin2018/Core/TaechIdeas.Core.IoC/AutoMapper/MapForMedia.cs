using AutoMapper;
using TaechIdeas.Core.Core.Media.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForMedia : Profile
    {
        public MapForMedia()
        {
            //MEDIA
            /**************************************************************************/
            CreateMap<NewMediaRequest, NewMediaInput>();
            CreateMap<NewMediaOutput, NewMediaResult>();
            CreateMap<CropPictureRequest, CropPictureInput>();

            CreateMap<MediaByIdOut, MediaByIdOutput>();
            CreateMap<MediaByIdInput, MediaByIdIn>();

            CreateMap<NewMediaInput, MediaUploadConfigParametersInput>();
            CreateMap<NewMediaInput, CropPictureInput>();
            CreateMap<SaveOrUpdateMediaOutput, NewMediaOutput>();

            CreateMap<AddAlternativePhotoSizeInput, MediaUploadConfigParametersInput>();
            CreateMap<AddAlternativePhotoSizeInput, MediaAlternativeSizeConfigParametersInput>();
            CreateMap<MediaAlternativeSizeConfigParametersInput, MediaAlternativeSizeConfigParametersIn>();
            CreateMap<MediaAlternativeSizeConfigParametersOut, MediaAlternativeSizeConfigParametersOutput>();

            CreateMap<SaveOrUpdateMediaInput, SaveOrUpdateMediaIn>();
            CreateMap<SaveOrUpdateMediaOut, SaveOrUpdateMediaOutput>()
                .ForMember(dest => dest.MediaId, opt => opt.MapFrom(src => src.USPReturnValue));

            CreateMap<MediaUploadConfigParametersInput, MediaUploadConfigParametersIn>();
            CreateMap<MediaUploadConfigParametersOut, MediaUploadConfigParametersOutput>()
                .ForMember(dest => dest.MediaUploadConfigId, opt => opt.MapFrom(src => src.IdMediaUploadConfig));

            CreateMap<DisableMediaRequest, DisableMediaInput>();
            CreateMap<DisableMediaInput, DisableMediaIn>();
            CreateMap<DisableMediaOut, DisableMediaOutput>();
            CreateMap<DisableMediaOutput, DisableMediaResult>();

            CreateMap<GetMediaNotInCdnRequest, GetMediaNotInCdnInput>();
            CreateMap<GetMediaNotInCdnInput, GetMediaNotInCdnIn>();
            CreateMap<GetMediaNotInCdnOut, GetMediaNotInCdnOutput>();
            CreateMap<GetMediaNotInCdnOutput, GetMediaNotInCdnResult>();

            /**************************************************************************/
        }
    }
}