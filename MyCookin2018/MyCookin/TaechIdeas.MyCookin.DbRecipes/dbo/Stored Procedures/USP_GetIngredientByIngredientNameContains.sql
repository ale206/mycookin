





-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, [USP_GetNoFatherRecipeByRecipeNameContains]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIngredientByIngredientNameContains](@IngredientName nvarchar(100), @IDLanguage INT)
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
	
	BEGIN TRY
		
		SET @Sql = 'SELECT TOP 20 IDIngredientLanguage, IDIngredient, IngredientSingular,Lenght, MIN(Relevance) AS Relevance  FROM		
					(
					SELECT TOP 15 IDIngredientLanguage, IDIngredient, IngredientSingular,LEN(IngredientSingular) AS Lenght, 0 AS Relevance FROM dbo.vIndexedIngredientLang_'+@LanguageCode+'
					WHERE IngredientSingular='''+REPLACE(@IngredientName,'''','''''')+''' OR IngredientPlural='''+REPLACE(@IngredientName,'''','''''')+'''
					UNION
					SELECT  TOP 15 IDIngredientLanguage, IDIngredient, IngredientSingular,LEN(IngredientSingular) AS Lenght, 1 AS Relevance FROM dbo.vIndexedIngredientLang_'+@LanguageCode+'
					WHERE (CONTAINS(IngredientSingular,''"'+@FulltextVar+'*"'')) 
					UNION
					SELECT  TOP 15 IDIngredientLanguage, IDIngredient, IngredientSingular,LEN(IngredientSingular) AS Lenght, 2 AS Relevance FROM dbo.vIndexedIngredientLang_'+@LanguageCode+'
					WHERE (FREETEXT((IngredientSingular,IngredientPlural),'''+@FulltextVar+''')) 
					) AS t
					GROUP BY IDIngredientLanguage, IDIngredient, IngredientSingular,Lenght
					ORDER BY Relevance, Lenght, IngredientSingular'
		EXECUTE sp_executesql @Sql
		--select @Sql
	END TRY
	
	BEGIN CATCH
		
		SELECT  TOP 20 IDIngredientLanguage, Ingredients.IDIngredient, IngredientSingular
		FROM dbo.IngredientsLanguages INNER JOIN dbo.Ingredients ON Ingredients.IDIngredient = IngredientsLanguages.IDIngredient
		WHERE  (IngredientSingular LIKE '%'+@IngredientName+'%' OR IngredientPlural LIKE '%'+@IngredientName+'%') AND IngredientEnabled=1
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




