-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <01/03/2016,,>
-- Last Edit:   <,,>
-- Description:	<Create Sitemap for recipes,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SitemapForRecipes]
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
  
	DECLARE @FriendlyId nvarchar(500)
	DECLARE @IDLanguage int
	DECLARE @CurrentDate nvarchar(100) = STUFF(CONVERT(VARCHAR(50),GETUTCDATE(), 127) ,20,4,'')  +'+00:00' 
	DECLARE @FriendlyIdWithUrl nvarchar(500)

	DECLARE MY_CURSOR CURSOR 
	  LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR 

	SELECT [FriendlyId], IDLanguage
	  FROM [DBRecipes].[dbo].[RecipesLanguages] WHERE FriendlyId IS NOT NULL AND FriendlyId <> ''
	  ORDER BY IDLanguage, FriendlyId

	PRINT 
	'<?xml version="1.0" encoding="utf-8"?>' + CHAR(13) +
	'  <urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">'

	OPEN MY_CURSOR
	FETCH NEXT FROM MY_CURSOR INTO @FriendlyId, @IDLanguage
	WHILE @@FETCH_STATUS = 0
	BEGIN 
		--Do something with Id here
		IF @IDLanguage = 1
			BEGIN
				SET @FriendlyIdWithUrl = 'http://www.mycookin.com/en/recipe/' + @FriendlyId
			END
		ELSE IF @IDLanguage = 2
			BEGIN
				SET @FriendlyIdWithUrl = 'http://www.mycookin.com/it/ricetta/' + @FriendlyId
			END
		ELSE IF @IDLanguage = 3
			BEGIN
				SET @FriendlyIdWithUrl = 'http://www.mycookin.com/es/receta/' + @FriendlyId
			END


		PRINT 
			'   <url>' + CHAR(13) +
			'     <loc>' + @FriendlyIdWithUrl + '</loc>' + CHAR(13) +
			'     <lastmod>'+ @CurrentDate +'</lastmod>' + CHAR(13) +
			'   </url>'
	
		FETCH NEXT FROM MY_CURSOR INTO @FriendlyId, @IDLanguage
	END

	PRINT
	'  </urlset>'

	CLOSE MY_CURSOR
	DEALLOCATE MY_CURSOR


END TRY

BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

  -- There was an error
  IF @@TRANCOUNT > 0
     ROLLBACK

	 SET @ResultExecutionCode = 'RC-ER-9999' --Error in the StoredProcedure
	 SET @isError = 1
	  
	 DECLARE @ErrorNumber nvarchar(MAX)			= ERROR_NUMBER();
	 DECLARE @ErrorSeverity nvarchar(MAX)		= ERROR_SEVERITY();
	 DECLARE @ErrorState nvarchar(MAX)			= ERROR_STATE();
	 DECLARE @ErrorProcedure nvarchar(MAX)		= ERROR_PROCEDURE();
	 DECLARE @ErrorLine nvarchar(MAX)			= ERROR_LINE();
	 DECLARE @ErrorMessage nvarchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime			= GETUTCDATE();
	 DECLARE @ErrorMessageCode nvarchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorMessage
	
	 --SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin nvarchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
					
													 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH