using System.Collections.Generic;
using TaechIdeas.Core.Core.Media.Dto;

namespace TaechIdeas.Core.Core.Media
{
    public interface IMediaRepository
    {
        MediaByIdOut MediaById(MediaByIdIn mediaByIdIn);
        MediaUploadConfigParametersOut MediaUploadConfigParameters(MediaUploadConfigParametersIn mediaUploadConfigParametersIn);
        SaveOrUpdateMediaOut SaveOrUpdateMedia(SaveOrUpdateMediaIn saveOrUpdateMediaIn);
        IEnumerable<MediaAlternativeSizeConfigParametersOut> MediaAlternativeSizeConfigParameters(MediaAlternativeSizeConfigParametersIn mediaAlternativeSizeConfigParametersIn);
        AddAlternativePhotoSizeOut AddAlternativePhotoSize(AddAlternativePhotoSizeIn addAlternativePhotoSizeIn);
        DisableMediaOut DisableMedia(DisableMediaIn disableMediaIn);
        IEnumerable<GetMediaNotInCdnOut> GetMediaNotInCdn(GetMediaNotInCdnIn getMediaNotInCdnIn);
    }
}