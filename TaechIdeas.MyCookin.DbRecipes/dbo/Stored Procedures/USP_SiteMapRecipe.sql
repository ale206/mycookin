


-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 03/03/2014>
-- Description:	<Description, [USP_SiteMapRecipe]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SiteMapRecipe] (@IDLanguage INT)
AS
BEGIN
	DECLARE @LanguageCode NVARCHAR(50);
	SELECT @LanguageCode = CASE @IDLanguage
		WHEN 1 THEN 'http://www.mycookin.com/en/recipe/'
		WHEN 2 THEN 'http://www.mycookin.com/it/ricetta/'
		WHEN 3 THEN 'http://www.mycookin.com/es/receta/'
		ELSE 'http://www.mycookin.com/en/recipe/'
	END

	SELECT  @LanguageCode + REPLACE(LTRIM(RTRIM(LOWER(RecipeName))),' ','-') AS SitemapURL
	FROM Recipes INNER JOIN RecipesLanguages
		ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
	WHERE Checked=1
			AND (IDRecipeImage IS NOT NULL OR IDRecipeImage!='00000000-0000-0000-0000-000000000000')
			AND RecipePortionKcal > 1
			AND Draft = 0
			AND DeletedOn IS NULL
			AND IDLanguage = @IDLanguage
	ORDER BY isStarterRecipe, RecipeName

	
END



