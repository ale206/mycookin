-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 16/10/2013>
-- Description:	<Description, [USP_GetMediaWithoutAlternativeSizes]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMediaNotInCDN] (
	@NumRow INT, 
	@MediaSizeType NVARCHAR(50))
AS
BEGIN
	IF @MediaSizeType = 'MediaCroppedSize'
		BEGIN
			SELECT TOP (@NumRow)   IDMedia, IDMediaOwner, isImage, isVideo, isLink, isEsternalSource, 
							MediaServer, MediaBakcupServer, MediaPath,MediaMD5Hash, Checked, CheckedByUser, 
							MediaDisabled, MediaUpdatedOn, MediaDeletedOn,UserIsMediaOwner, MediaOnCDN, MediaType
				FROM         Media
				WHERE MediaOnCDN = 0 
				AND MediaDeletedOn IS NULL
		END
	ELSE
		BEGIN
			SELECT TOP (@NumRow)   IDMedia, IDMediaOwner, isImage, isVideo, isLink, isEsternalSource, 
							MediaServer, MediaBakcupServer, MediaPath,MediaMD5Hash, Checked, CheckedByUser, 
							MediaDisabled, MediaUpdatedOn, MediaDeletedOn,UserIsMediaOwner, MediaOnCDN, MediaType
				FROM         Media
				WHERE MediaDeletedOn IS NULL
				AND IDMedia IN (SELECT IDMedia FROM MediaAlternativesSizes WHERE [MediaSizeType] = @MediaSizeType AND MediaOnCDN=0)
		END
END


