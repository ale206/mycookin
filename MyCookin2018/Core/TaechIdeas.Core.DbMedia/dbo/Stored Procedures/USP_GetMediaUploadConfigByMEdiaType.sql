-- =============================================
-- Author:		<Author, Alessio Di Salvo>
-- Create date: <Create Date, 14/01/2016>
-- Description:	<Get MediaUploadConfig By Media Type>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMediaUploadConfigByMediaType] 
@MediaType nvarchar(50) 

AS
BEGIN
	
        SELECT IDMediaUploadConfig, MediaType, UploadPath, UploadOriginalFilePath, MaxSizeByte, AcceptedContentTypes, AcceptedFileExtension, AcceptedFileExtensionRegex,
				EnableUploadForMediaType, ComputeMD5Hash, MediaFinalHeight, MediaFinalWidth, MediaPercPlusSizeForCrop, MediaSmallerSideMinSize
        FROM MediaUploadConfig
        WHERE(MediaType = @MediaType)
                   
END


