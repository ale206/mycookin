





-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, [USP_GetIngredientByIngredientName]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIngredientByIngredientName](@IngredientName nvarchar(100), @IDLanguage INT)
AS
BEGIN
DECLARE @LanguageCode nvarchar(5);
DECLARE @Sql nvarchar(MAX);
DECLARE @FulltextVar nvarchar(100);
SET @FulltextVar = REPLACE(REPLACE(@IngredientName,'"',' '),'''',' ')

	SELECT @LanguageCode = CASE @IDLanguage
		WHEN 1 THEN 'En'
		WHEN 2 THEN 'It'
		WHEN 3 THEN 'Es'
		ELSE 'En'
	END
DECLARE @ContainFilter nvarchar(150)
--SET @ContainFilter = '''"'+@IngredientName+'*"'''
	BEGIN TRY
		
		SET @Sql = 'SELECT TOP 1 IDIngredientLanguage, IDIngredient, IngredientSingular FROM
					(
						SELECT IDIngredientLanguage, IDIngredient, IngredientSingular, 0 AS rowOrder FROM dbo.vIndexedIngredientLang_'+@LanguageCode+'
						WHERE IngredientSingular='''+@IngredientName+''' OR IngredientPlural='''+@IngredientName+'''
						UNION
						SELECT IDIngredientLanguage, IDIngredient, IngredientSingular, 1 AS rowOrder FROM dbo.vIndexedIngredientLang_'+@LanguageCode+'
						WHERE (FREETEXT((IngredientSingular,IngredientPlural),'''+@FulltextVar+''') 
						OR CONTAINS((IngredientSingular,IngredientPlural),''"'+@FulltextVar+'*"'')) 
						AND (IDLanguage = '+CONVERT(nvarchar,@IDLanguage)+')
					) AS t
					ORDER BY rowOrder'
		EXECUTE sp_executesql @Sql
		--select @Sql
	END TRY
	
	BEGIN CATCH
		
		SELECT DISTINCT IDIngredientLanguage, Ingredients.IDIngredient, IngredientSingular
		FROM dbo.IngredientsLanguages INNER JOIN dbo.Ingredients ON Ingredients.IDIngredient = IngredientsLanguages.IDIngredient
		WHERE  (IngredientSingular LIKE @IngredientName+'%' OR IngredientPlural LIKE @IngredientName+'%')  
				AND IDLanguage = @IDLanguage
		ORDER BY IngredientSingular
		
		--select 'error'
		
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




