using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Media.Dto;

namespace TaechIdeas.Core.Core.Media
{
    public interface IMediaManager
    {
        byte[] ImageToByteArray(Image imageIn);
        Image ByteArrayToImage(byte[] byteArrayIn);
        MediaUploadConfigParametersOutput MediaUploadConfigParameters(MediaUploadConfigParametersInput mediaUploadConfigParametersInput);
        NewMediaOutput NewMedia(NewMediaInput newMediaInput);
        SaveOrUpdateMediaOutput SaveOrUpdateMedia(SaveOrUpdateMediaInput saveOrUpdateMediaInput);
        string ControlsOnFilename(string objectName);
        MediaByIdOutput MediaById(MediaByIdInput mediaByIdInput);
        AddAlternativePhotoSizeOutput AddAlternativePhotoSize(AddAlternativePhotoSizeInput addAlternativePhotoSizeInput);
        CropPictureOutput CropPicture(CropPictureInput cropPictureInput);
        ResizeAndCompressOutput ResizeAndCompress(ResizeAndCompressInput resizeAndCompressInput);
        IEnumerable<MediaAlternativeSizeConfigParametersOutput> MediaAlternativeSizeConfigParameters(MediaAlternativeSizeConfigParametersInput mediaUploadConfigParametersInput);
        PhotoSize CalculatePhotoSize(int imgWidth, int imgHeight, int imgFinalWidth, int imgFinalHeight, int percPlusSizeForCrop);
        string ResizeAndSave(string stgOriginalPath, string stgNewPath, int newWidth, int newHeight, int quality);
        UploadPhotoOnCdnOutput UploadPhotoOnCdn(UploadPhotoOnCdnInput uploadPhotoOnCdnInput);
        DisableMediaOutput DisableMedia(DisableMediaInput disableMediaInput);
        IEnumerable<GetMediaNotInCdnOutput> GetMediaNotInCdn(GetMediaNotInCdnInput getMediaNotInCdnInput);
        DeleteMediaOutput DeleteMedia(DeleteMediaInput deleteMediaInput);
        MediaPathByMediaIdOutput MediaPathByMediaId(MediaPathByMediaIdInput mediaPathByMediaIdInput);
        ImageCodecInfo ImageCodecInfo(string mimeType);
        string GetDefaultMediaPath(MediaType mediaType);
    }
}