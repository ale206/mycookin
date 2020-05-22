


-- ================================================
-- =============================================
-- Author:		<Saverio Cammarata>
-- Create date: <22/08/2012,,>
-- Description:	<Login User,,AGGIUNGERE UPDATE SE FILE ESISTE>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SaveAlternativeSizeMedia]
	@IDMediaAlternativeSize uniqueidentifier,
	@IDMedia uniqueidentifier,
	@MediaSizeType nvarchar(50),
	@MediaServer nvarchar(150),
	@MediaBakcupServer nvarchar(150),
	@MediaPath nvarchar(550),
	@MediaMD5Hash nvarchar(100),
	@MediaOnCDN bit
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

DECLARE @IDMediaFound uniqueidentifier

BEGIN TRY

	SELECT @IDMediaFound = IDMediaAlternativeSize FROM dbo.MediaAlternativesSizes WHERE IDMedia = @IDMedia AND MediaSizeType = @MediaSizeType

	IF @MediaMD5Hash <>'' AND @IDMediaFound IS NULL
		BEGIN
			SELECT @IDMediaFound = IDMedia FROM dbo.MediaAlternativesSizes WHERE MediaMD5Hash = @MediaMD5Hash
		END
	
	IF @IDMediaAlternativeSize IS NOT NULL AND @IDMediaFound IS NULL
		BEGIN
			SELECT @IDMediaFound = IDMediaAlternativeSize FROM dbo.MediaAlternativesSizes WHERE IDMediaAlternativeSize = @IDMediaAlternativeSize
		END

    BEGIN TRANSACTION    -- Start the transaction
		
	DECLARE @NewMediaID uniqueidentifier
	
	IF @IDMediaFound IS NULL
		BEGIN
			IF @IDMediaAlternativeSize IS NULL
				SET @NewMediaID = NEWID()
			ELSE
				SET @NewMediaID = @IDMediaAlternativeSize
				
			INSERT INTO [DBMedia].[dbo].[MediaAlternativesSizes]
				   ([IDMediaAlternativeSize],
				   [IDMedia],
				   [MediaSizeType],
				   [MediaServer],
				   [MediaBackupServer],
				   [MediaPath],
				   [MediaMD5Hash],
				   [MediaOnCDN])
			 VALUES
				   (@NewMediaID ,
				   @IDMedia ,
				   @MediaSizeType ,
				   @MediaServer ,
				   @MediaBakcupServer ,
				   @MediaPath ,
				   @MediaMD5Hash, 
				   @MediaOnCDN )
			
			SET @ResultExecutionCode = 'MD-IN-0001' --File uploaded
			SET @USPReturnValue = @NewMediaID -- The ID of the new media inserted
			SET @isError = 0
		END
	ELSE
		BEGIN
			UPDATE [DBMedia].[dbo].[MediaAlternativesSizes]
			   SET [IDMedia] = @IDMedia ,
				  [MediaSizeType] = @MediaSizeType ,
				  [MediaServer] = @MediaServer ,
				  [MediaBackupServer] = @MediaBakcupServer ,
				  [MediaPath] = @MediaPath ,
				  [MediaMD5Hash] = @MediaMD5Hash ,
				  [MediaOnCDN] = @MediaOnCDN
			WHERE IDMediaAlternativeSize = @IDMediaFound
			
			SET @ResultExecutionCode = 'MD-IN-0001' --File uploaded
			SET @USPReturnValue = @IDMediaFound -- The ID of the new media inserted
			SET @isError = 0
		END
			

			
			
	-- If we reach here, success!
   COMMIT
   
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
		-- =======================================
		
END TRY

BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

  -- There was an error
  IF @@TRANCOUNT > 0
     ROLLBACK

	 SET @ResultExecutionCode = 'US-ER-9999' --Error in the StoredProcedure
	 SET @isError = 1
	 
	 DECLARE @ErrorNumber varchar(MAX)		= ERROR_NUMBER();
	 DECLARE @ErrorSeverity varchar(MAX)	= ERROR_SEVERITY();
	 DECLARE @ErrorState varchar(MAX)		= ERROR_STATE();
	 DECLARE @ErrorProcedure varchar(MAX)	= ERROR_PROCEDURE();
	 DECLARE @ErrorLine varchar(MAX)		= ERROR_LINE();
	 DECLARE @ErrorMessage varchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime		= GETUTCDATE();
	 DECLARE @ErrorMessageCode varchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorMessage
	
	 SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
	 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin varchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)
	 
-- =======================================  
END CATCH

