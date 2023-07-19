-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 24/03/2013>
-- Description:	<Description, [USP_GetBeverageByNameContains]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetBeverageByNameContains](@BeverageName nvarchar(100), @IDLanguage INT)
AS
BEGIN
DECLARE @LanguageCode nvarchar(5);
DECLARE @Sql nvarchar(MAX);
	
	SELECT @LanguageCode = CASE @IDLanguage
		WHEN 1 THEN 'En'
		WHEN 2 THEN 'It'
		WHEN 3 THEN 'Es'
		ELSE 'En'
	END
	
	BEGIN TRY
		
		SET @Sql = 'SELECT TOP 15 IDIngredientLanguage, IDIngredient, IngredientSingular, FriendlyId 
					FROM dbo.vIndexedIngredientLang_'+@LanguageCode+' INNER JOIN  
					dbo.Beverages 
                    ON IDBeverage = IDIngredient
					WHERE IngredientSingular='''+@BeverageName+'''
					UNION
					SELECT TOP 15 IDIngredientLanguage, IDIngredient, IngredientSingular, FriendlyId 
					FROM dbo.vIndexedIngredientLang_'+@LanguageCode+' INNER JOIN  
					dbo.Beverages 
                    ON IDBeverage = IDIngredient
					WHERE (FREETEXT(IngredientSingular,'''+@BeverageName+''') OR CONTAINS(IngredientSingular,''"'+@BeverageName+'*"'')) 
					AND (IDLanguage = '+CONVERT(nvarchar,@IDLanguage)+')
					ORDER BY IngredientSingular'
		EXECUTE sp_executesql @Sql
		--select @Sql
	END TRY
	
	BEGIN CATCH
		
		SELECT IDIngredientLanguage, Ingredients.IDIngredient, IngredientSingular, FriendlyId
		FROM dbo.IngredientsLanguages 
		INNER JOIN dbo.Ingredients ON Ingredients.IDIngredient = IngredientsLanguages.IDIngredient
		INNER JOIN dbo.Beverages ON dbo.Beverages.IDBeverage = dbo.Ingredients.IDIngredient
		WHERE  IngredientSingular LIKE @BeverageName+'%' AND IDLanguage = @IDLanguage
		ORDER BY IngredientSingular
		
		
		--ERROR CATCH
		 DECLARE @ErrorNumber varchar(MAX)		= ERROR_NUMBER();
		 DECLARE @ErrorSeverity varchar(MAX)	= ERROR_SEVERITY();
		 DECLARE @ErrorState varchar(MAX)		= ERROR_STATE();
		 DECLARE @ErrorProcedure varchar(MAX)	= ERROR_PROCEDURE();
		 DECLARE @ErrorLine varchar(MAX)		= ERROR_LINE();
		 DECLARE @ErrorMessage varchar(MAX)		= ERROR_MESSAGE();
		 DECLARE @DateError smalldatetime		= GETUTCDATE();
		 DECLARE @ErrorMessageCode varchar(MAX)	= 'Possible Error on FULL TEXT INDEX';

		EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														'USP_GetIngredientByIngredientNameContains', @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
	END CATCH
	
END




