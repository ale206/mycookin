CREATE VIEW dbo.[zzzvIngredient_It]
AS
SELECT     TOP (100) PERCENT dbo.Ingredients.IDIngredient, dbo.IngredientsLanguages.IngredientSingular, dbo.IngredientsLanguages.IngredientPlural, 
                      dbo.IngredientsLanguages.IngredientDescription, dbo.Ingredients.Kcal100gr, dbo.Ingredients.IsVegetarian, dbo.Ingredients.IsVegan, dbo.Ingredients.IsGlutenFree, 
                      dbo.Languages.IDLanguage, dbo.Languages.Language, dbo.Ingredients.IsHotSpicy, dbo.Ingredients.AverageWeightOfOnePiece
FROM         dbo.Ingredients INNER JOIN
                      dbo.IngredientsLanguages ON dbo.Ingredients.IDIngredient = dbo.IngredientsLanguages.IDIngredient INNER JOIN
                      dbo.Languages ON dbo.IngredientsLanguages.IDLanguage = dbo.Languages.IDLanguage
ORDER BY dbo.IngredientsLanguages.IngredientSingular

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[38] 2[4] 3) )"
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
         Begin Table = "Ingredients"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 202
               Right = 298
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "IngredientsLanguages"
            Begin Extent = 
               Top = 6
               Left = 386
               Bottom = 177
               Right = 574
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Languages"
            Begin Extent = 
               Top = 6
               Left = 612
               Bottom = 114
               Right = 764
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
         Width = 3120
         Width = 2610
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3075
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'zzzvIngredient_It';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'zzzvIngredient_It';

