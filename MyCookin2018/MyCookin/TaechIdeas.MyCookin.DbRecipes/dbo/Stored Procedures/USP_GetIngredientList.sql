


-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 24/02/2013>
-- Description:	<Description, [USP_GetIngredientList]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIngredientList](@StartWith nvarchar(10),@Vegan BIT, @Vegetarian BIT, 
													@GlutenFree BIT, @HotSpicy BIT, @IDLanguage INT, @OffsetRows INT, @FetchRows INT)
AS
BEGIN
	IF @StartWith = '0'
		BEGIN
			SET @StartWith = '%'
		END
	ELSE
		BEGIN
			SET @StartWith = @StartWith + '%'
		END

	SELECT IDIngredientLanguage, Ingredients.IDIngredient, IngredientSingular, IngredientPlural, IDIngredientImage
		FROM Ingredients INNER JOIN IngredientsLanguages 
			ON Ingredients.IDIngredient = IngredientsLanguages.IDIngredient
		WHERE IngredientPlural LIKE @StartWith 
			AND (IsVegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
				OR IsVegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
				OR isnull(IsVegan,0)=@vegan)
			AND (IsVegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
				OR IsVegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
				OR isnull(IsVegetarian,0)=@Vegetarian)
			AND (IsGlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
				OR IsGlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
				OR isnull(IsGlutenFree,0)=@GlutenFree)
			AND (IsHotSpicy = CASE  WHEN @HotSpicy = 1 THEN 1 ELSE 1 END
				OR IsHotSpicy = CASE  WHEN @HotSpicy = 1 THEN 1 ELSE 0 END
				OR isnull(IsHotSpicy,0)=@HotSpicy)
			AND IDLanguage = @IDLanguage
			AND IngredientEnabled = 1
			AND IDIngredientImage IS NOT NULL
		ORDER BY IngredientPlural
		OFFSET @OffsetRows ROWS
		FETCH NEXT @FetchRows ROWS ONLY

END


