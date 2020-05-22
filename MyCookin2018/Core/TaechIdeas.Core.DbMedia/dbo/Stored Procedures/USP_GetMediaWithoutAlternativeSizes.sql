

-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 16/10/2013>
-- Description:	<Description, [USP_GetMediaWithoutAlternativeSizes]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMediaWithoutAlternativeSizes] (@NumRow INT, 
													@MediaType NVARCHAR(50),
													@MediaSizeType NVARCHAR(50))
AS
BEGIN
	
	SELECT TOP (@NumRow) IDMedia, IDMediaOwner, isImage, isVideo, isLink, 
							isEsternalSource, MediaServer, MediaBakcupServer, 
							MediaPath, Checked, CheckedByUser, MediaDisabled, MediaUpdatedOn, 
							MediaDeletedOn FROM dbo.Media
	WHERE IDMedia NOT IN (SELECT IDMedia FROM MediaAlternativesSizes WHERE MediaSizeType = @MediaSizeType)
			AND MediaDeletedOn IS NULL
			AND MediaOnCDN = 0
            AND MediaType = @MediaType
			AND Checked = 0
END


