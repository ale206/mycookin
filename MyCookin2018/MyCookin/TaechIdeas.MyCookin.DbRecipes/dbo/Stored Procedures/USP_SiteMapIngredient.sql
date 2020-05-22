


-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 03/03/2014>
-- Description:	<Description, [USP_SiteMapIngredient]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SiteMapIngredient] (@IDLanguage INT)
AS
BEGIN
	DECLARE @LanguageCode NVARCHAR(50);
	SELECT @LanguageCode = CASE @IDLanguage
		WHEN 1 THEN 'http://www.mycookin.com/en/ingredient/'
		WHEN 2 THEN 'http://www.mycookin.com/it/ingrediente/'
		WHEN 3 THEN 'http://www.mycookin.com/es/elemento/'
		ELSE 'http://www.mycookin.com/en/ingredient/'
	END

	SELECT  @LanguageCode + REPLACE(LTRIM(RTRIM(LOWER(IngredientPlural))),' ','-') + '/' + CONVERT(NVARCHAR(50),Ingredients.IDIngredient) AS SitemapURL
	FROM Ingredients INNER JOIN IngredientsLanguages
		ON Ingredients.IDIngredient = IngredientsLanguages.IDIngredient
	WHERE Checked=1
			AND (IDIngredientImage IS NOT NULL OR IDIngredientImage!='00000000-0000-0000-0000-000000000000')
			AND IngredientEnabled = 1
			AND IDLanguage = @IDLanguage
	ORDER BY IngredientPlural

	
END



