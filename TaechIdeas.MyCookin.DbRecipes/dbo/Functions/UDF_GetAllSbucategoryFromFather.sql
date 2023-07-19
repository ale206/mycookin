CREATE FUNCTION [dbo].[UDF_GetAllSbucategoryFromFather]
    ( @IDCategory INT) 
RETURNS TABLE 
RETURN 
WITH IngredientCategory_cte AS (
 SELECT p.*, 1 AS Depth 
 FROM dbo.IngredientsCategories c
 JOIN dbo.IngredientsCategories p 
 ON p.IDIngredientCategoryFather = c.IDIngredientCategory
 WHERE c.IDIngredientCategory = @IDCategory
 UNION ALL
 
 SELECT c.*, Depth + 1 AS Depth
 FROM IngredientCategory_cte p
 JOIN dbo.IngredientsCategories c 
 ON c.IDIngredientCategoryFather  = p.IDIngredientCategory
) 
SELECT distinct IDIngredientCategory, IDIngredientCategoryFather,Enabled, Depth FROM IngredientCategory_cte
UNION ALL
SELECT IDIngredientCategory, IDIngredientCategoryFather,1 AS Enabled, 0 AS Depth
 FROM IngredientsCategories
 WHERE IDIngredientCategory = @IDCategory