


-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, [USP_GetNoFatherRecipeByRecipeNameContains]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetNoFatherRecipeByRecipeNameContains](@RecipeName nvarchar(100), @IDLanguage INT)
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
		
		SET @Sql = 'SELECT NEWID() AS IDRecipeLanguage, NULL AS IDRecipe, ''Select Value'' AS RecipeName 
					UNION
					SELECT IDRecipeLanguage, IDRecipe, RecipeName FROM dbo.vIndexedRecipeLang_'+@LanguageCode+'
					WHERE CONTAINS(RecipeName,''"'+@RecipeName+'"'') AND IDRecipeFather IS NULL
					UNION
					SELECT TOP 20 IDRecipeLanguage, IDRecipe, RecipeName FROM dbo.vIndexedRecipeLang_'+@LanguageCode+'
					WHERE FREETEXT(RecipeName,'''+@RecipeName+''') AND IDRecipeFather IS NULL
					ORDER BY RecipeName'
		--EXECUTE sp_executesql @Sql
		SELECT NEWID() AS IDRecipeLanguage, NULL AS IDRecipe, 'Select Value' AS RecipeName
		UNION
		SELECT IDRecipeLanguage, Recipes.IDRecipe,RecipeName
		FROM dbo.RecipesLanguages INNER JOIN dbo.Recipes ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
		WHERE  RecipeName LIKE '%'+@RecipeName+'%' AND RecipeDisabled = 0 AND IDLanguage = @IDLanguage
				AND Recipes.IDRecipeFather IS NULL
		ORDER BY RecipeName
	END TRY
	
	BEGIN CATCH
		SELECT NEWID() AS IDRecipeLanguage, NULL AS IDRecipe, 'Select Value' AS RecipeName
		UNION
		SELECT IDRecipeLanguage, Recipes.IDRecipe,RecipeName
		FROM dbo.RecipesLanguages INNER JOIN dbo.Recipes ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
		WHERE  RecipeName LIKE '%'+@RecipeName+'%' AND RecipeDisabled = 0 AND IDLanguage = @IDLanguage
				AND Recipes.IDRecipeFather IS NULL
		ORDER BY RecipeName
		
		
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
														'USP_GetNoFatherRecipeByRecipeNameContains', @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
	END CATCH
	
END

