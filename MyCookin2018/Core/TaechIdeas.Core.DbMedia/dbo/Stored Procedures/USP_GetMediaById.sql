-- =============================================
-- Author:		<Author, Alessio Di Salvo>
-- Create date: <Create Date, 10/11/2015>
-- Description:	<Get Media by its Id>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMediaById] 
@IdMedia uniqueidentifier 

AS
BEGIN
	
        SELECT     IDMedia, IDMediaOwner, isImage, isVideo, isLink, isEsternalSource, MediaServer, MediaBakcupServer, MediaPath,MediaMD5Hash, Checked, CheckedByUser, MediaDisabled,
        MediaUpdatedOn, MediaDeletedOn,UserIsMediaOwner, MediaOnCDN, MediaType
        FROM         Media
        WHERE     IDMedia = @IdMedia AND MediaDeletedOn IS NULL
                   
END


