







-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,29/04/2013,>
-- Description:	<Description,Calculate Recipe nutritional facts,>
-- =============================================
CREATE PROCEDURE [dbo].[zzz_USP_CalculateRecipeNutritionalFacts]
	@IDRecipe uniqueidentifier
AS



BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
	SELECT PropKey,PropValue FROM
		(SELECT SUM([Kcal100gr]) AS totKcal100gr, 
				SUM([grProteins]) AS totgrProteins,
				SUM([grFats]) AS totgrFats,
				SUM([grCarbohydrates]) AS totgrCarbohydrates,
				SUM([grAlcohol]) AS totgrAlcohol,
				MIN(CONVERT(float,[NumberOfPerson])) AS NumOfPerson
		FROM [dbo].[RecipesIngredients] INNER JOIN [dbo].[Ingredients]
		ON [dbo].[Ingredients].IDIngredient = [dbo].[RecipesIngredients].IDIngredient
		INNER JOIN [dbo].[Recipes] ON [dbo].[RecipesIngredients].IDRecipe =  [dbo].[Recipes].IDRecipe
		WHERE [dbo].[Recipes].IDRecipe=@IDRecipe) as t1
		UNPIVOT(PropValue FOR PropKey IN (totKcal100gr,totgrProteins,totgrFats,totgrCarbohydrates,totgrAlcohol,NumOfPerson)) AS t2
					                            
   COMMIT
		
		
		
END TRY

BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

 

-- =======================================    
END CATCH









