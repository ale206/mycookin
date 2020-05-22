-- =============================================
-- Author:		<Author, Alessio Di Salvo>
-- Create date: <Create Date, 25/01/2016>
-- Description:	<Get MediaAlternativeSizeConfigParameters By Media Type>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMediaAlternativeSizeConfigParametersByMediaType] 
@MediaType nvarchar(50) 

AS
BEGIN
	
		SELECT MediaType, MediaSizeType, SavePath, MediaHeight, MediaWidth
		FROM MediaAlternativesSizesConfig
        WHERE(MediaType = @MediaType)
                   
END


