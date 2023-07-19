CREATE VIEW dbo.vViewAllRecipes
AS
SELECT     dbo.RecipesLanguages.RecipeName, dbo.RecipePropertiesLanguages.RecipeProperty, dbo.IngredientsLanguages.IngredientSingular, 
                      dbo.RecipesIngredients.IsPrincipalIngredient, dbo.RecipesIngredients.QuantityNotStd, dbo.IngredientsQuantityTypesLanguages.IngredientQuantityTypeSingular, 
                      dbo.Recipes.NumberOfPerson, dbo.RecipesSteps.RecipeStep, dbo.Recipes.IDRecipe
FROM         dbo.RecipePropertiesLanguages INNER JOIN
                      dbo.RecipeProperties ON dbo.RecipePropertiesLanguages.IDRecipeProperty = dbo.RecipeProperties.IDRecipeProperty INNER JOIN
                      dbo.RecipesPropertiesValues ON dbo.RecipeProperties.IDRecipeProperty = dbo.RecipesPropertiesValues.IDRecipeProperty INNER JOIN
                      dbo.Recipes INNER JOIN
                      dbo.RecipesIngredients ON dbo.Recipes.IDRecipe = dbo.RecipesIngredients.IDRecipe INNER JOIN
                      dbo.RecipesLanguages ON dbo.Recipes.IDRecipe = dbo.RecipesLanguages.IDRecipe INNER JOIN
                      dbo.RecipesSteps ON dbo.RecipesLanguages.IDRecipeLanguage = dbo.RecipesSteps.IDRecipeLanguage INNER JOIN
                      dbo.IngredientsLanguages ON dbo.RecipesIngredients.IDIngredient = dbo.IngredientsLanguages.IDIngredient ON 
                      dbo.RecipesPropertiesValues.IDRecipe = dbo.Recipes.IDRecipe INNER JOIN
                      dbo.IngredientsQuantityTypes ON dbo.RecipesIngredients.IDQuantityType = dbo.IngredientsQuantityTypes.IDIngredientQuantityType INNER JOIN
                      dbo.IngredientsQuantityTypesLanguages ON 
                      dbo.IngredientsQuantityTypes.IDIngredientQuantityType = dbo.IngredientsQuantityTypesLanguages.IDIngredientQuantityType

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[70] 4[4] 2[12] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RecipePropertiesLanguages"
            Begin Extent = 
               Top = 244
               Left = 506
               Bottom = 378
               Right = 718
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RecipeProperties"
            Begin Extent = 
               Top = 122
               Left = 579
               Bottom = 215
               Right = 768
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RecipesPropertiesValues"
            Begin Extent = 
               Top = 131
               Left = 323
               Bottom = 239
               Right = 514
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Recipes"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RecipesIngredients"
            Begin Extent = 
               Top = 136
               Left = 6
               Bottom = 310
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RecipesLanguages"
            Begin Extent = 
               Top = 11
               Left = 273
               Bottom = 119
               Right = 500
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "RecipesSteps"
            Begin Extent = 
               Top = 7
               Left = 610
               Bottom = 115
          ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vViewAllRecipes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'     Right = 785
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "IngredientsLanguages"
            Begin Extent = 
               Top = 242
               Left = 215
               Bottom = 350
               Right = 403
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "IngredientsQuantityTypes"
            Begin Extent = 
               Top = 354
               Left = 38
               Bottom = 462
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "IngredientsQuantityTypesLanguages"
            Begin Extent = 
               Top = 457
               Left = 260
               Bottom = 598
               Right = 514
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 2550
         Width = 1500
         Width = 1800
         Width = 1920
         Width = 1500
         Width = 2475
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2505
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vViewAllRecipes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vViewAllRecipes';

