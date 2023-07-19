




-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,07/03/2013,>
-- Description:	<Description,Manage Alternative Ingredient,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageIngredientAlternative]
	@IDIngredientAlternative uniqueidentifier,
	@IDIngredientMain uniqueidentifier,
	@IDIngredientSlave uniqueidentifier,
	@AddedByUser uniqueidentifier,
	@AddedOn smalldatetime,
	@CheckedBy uniqueidentifier,
	@CheckedOn smalldatetime,
	@Checked bit,
	@isDeleteOperation bit
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================
DECLARE @AltID2 uniqueidentifier
SET @AltID2 = NEWID()

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
	IF @isDeleteOperation = 1
		BEGIN
			DELETE FROM  dbo.IngredientsAlternatives
			WHERE IDIngredientAlternative = @IDIngredientAlternative
		END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT IDIngredientAlternative FROM IngredientsAlternatives WHERE IDIngredientAlternative = @IDIngredientAlternative)
			BEGIN
				INSERT INTO [dbo].IngredientsAlternatives 
					([IDIngredientAlternative]
					   ,[IDIngredientMain]
					   ,[IDIngredientSlave]
					   ,[AddedByUser]
					   ,[AddedOn]
					   ,[CheckedBy]
					   ,[CheckedOn]
					   ,[Checked]) 
				
				VALUES (@IDIngredientAlternative,
							@IDIngredientMain,
							@IDIngredientSlave,
							@AddedByUser,
							@AddedOn,
							@CheckedBy,
							@CheckedOn,
							@Checked)
				IF NOT EXISTS (SELECT IDIngredientAlternative FROM IngredientsAlternatives WHERE IDIngredientAlternative = @AltID2)
					BEGIN
						INSERT INTO [dbo].IngredientsAlternatives 
							([IDIngredientAlternative]
							   ,[IDIngredientMain]
							   ,[IDIngredientSlave]
							   ,[AddedByUser]
							   ,[AddedOn]
							   ,[CheckedBy]
							   ,[CheckedOn]
							   ,[Checked]) 
						
						VALUES (@AltID2,
									@IDIngredientSlave,
									@IDIngredientMain,
									@AddedByUser,
									@AddedOn,
									@CheckedBy,
									@CheckedOn,
									@Checked)
					END
			END
		ELSE
			BEGIN
				UPDATE [DBRecipes].dbo.IngredientsAlternatives
				   SET IDIngredientMain=@IDIngredientMain,
						IDIngredientSlave=@IDIngredientSlave,
						AddedByUser=@AddedByUser,
						AddedOn=@AddedOn,
						CheckedBy=@CheckedBy,
						CheckedOn=@CheckedOn,
						Checked=@Checked
				 
				 WHERE IDIngredientAlternative=@IDIngredientAlternative
			END
	END
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='';
		SET @USPReturnValue =@IDIngredientAlternative;
					                            
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






