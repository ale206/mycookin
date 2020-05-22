
-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 10/07/2012>
-- Description:	<Description, USP_GetAllCategoryByIDLanguage>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetAllCategoryByIDLanguage](@IDLanguage INT)
AS
BEGIN
----============Variable for table column========
	DECLARE @IDIngredientCategory INT
	DECLARE @IDIngredientCategoryFather INT
	DECLARE @IngredientCategoryLanguage VARCHAR(100)
	DECLARE @Depth INT

----============Variable for SP Data=============	
	DECLARE @IngredientCategoryStringSeparator VARCHAR(10)
	SET @IngredientCategoryStringSeparator = ' -> '
	DECLARE @TempIngredientCategoryLanguageStringCostructor VARCHAR(500)
	DECLARE @IngredientCategoryTempWork TABLE
			(
			  IDIngredientCategory int,
			  IDIngredientCategoryFather int,
			  IngredientCategoryLanguage VARCHAR(100)
			)
	DECLARE @ChekIDLanguageExist INT
----=============================================
	
	--Controllo se ho valori per la lingua selezionata
	SELECT TOP 1 @ChekIDLanguageExist = IDIngredientCategoryLanguage 
		FROM dbo.IngredientsCategoriesLanguages
		WHERE IDLanguage = @IDLanguage
	
	--Se la lingua non esiste prendo quella minore
	IF @ChekIDLanguageExist IS NULL
	BEGIN
		SELECT @IDLanguage = MIN(IDLanguage) 
			FROM dbo.IngredientsCategoriesLanguages
	END
	
	DECLARE cur_ReadCategory Cursor
	FOR 
	
	-- PER ORA il 24 di alimenti è statico, andebbe dinamizzato per poter
	-- sbrogliare più catene indipendenti di categorie!!!!!!!!
		SELECT Res.IDIngredientCategory,Res.IDIngredientCategoryFather, IngredientsCategoriesLanguages.IngredientCategoryLanguage, Res.Depth 
			FROM UDF_GetAllSbucategoryFromFather(24) Res
			INNER JOIN dbo.IngredientsCategoriesLanguages
			ON Res.IDIngredientCategory = IngredientsCategoriesLanguages.IDIngredientCategory
			WHERE IngredientsCategoriesLanguages.IDLanguage=2 AND Res.Enabled = 1
			ORDER BY Res.Depth

	Open cur_ReadCategory
				
	FETCH NEXT FROM cur_ReadCategory INTO @IDIngredientCategory, @IDIngredientCategoryFather, @IngredientCategoryLanguage, @Depth
		While (@@FETCH_STATUS <> -1)
		BEGIN
			--Corpo USP
			
			IF @IDIngredientCategoryFather IS NULL
				BEGIN
					INSERT INTO @IngredientCategoryTempWork(IDIngredientCategory,IDIngredientCategoryFather,IngredientCategoryLanguage)
					VALUES(@IDIngredientCategory,@IDIngredientCategoryFather,@IngredientCategoryLanguage)
				END
			ELSE
				BEGIN
					SELECT @TempIngredientCategoryLanguageStringCostructor = IngredientCategoryLanguage
						FROM @IngredientCategoryTempWork
						WHERE IDIngredientCategory = @IDIngredientCategoryFather
					SET @TempIngredientCategoryLanguageStringCostructor = @TempIngredientCategoryLanguageStringCostructor + @IngredientCategoryStringSeparator + @IngredientCategoryLanguage
					
					INSERT INTO @IngredientCategoryTempWork(IDIngredientCategory,IDIngredientCategoryFather,IngredientCategoryLanguage)
					VALUES(@IDIngredientCategory,@IDIngredientCategoryFather,@TempIngredientCategoryLanguageStringCostructor)
				END
			
			--FINE
		
			FETCH NEXT FROM cur_ReadCategory INTO @IDIngredientCategory, @IDIngredientCategoryFather, @IngredientCategoryLanguage, @Depth 
		END
		CLOSE cur_ReadCategory
		DEALLOCATE cur_ReadCategory
		
		SELECT * FROM @IngredientCategoryTempWork
		ORDER BY IngredientCategoryLanguage
	
END

