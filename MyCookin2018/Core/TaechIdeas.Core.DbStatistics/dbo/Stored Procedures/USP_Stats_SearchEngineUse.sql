-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <03/11/2013,,>
-- Last Edit:   <,,>
-- Description:	<Search Engine Use,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_Stats_SearchEngineUse]
	--@StartDate datetime,
	--@EndDate datetime,
	@Filter nvarchar(50)	--ONE OF: Vegan, Vegetarian, GlutenFree, FrigoMix, Light, Quick

AS
	IF (@Filter = 'Vegan')
		BEGIN

			SELECT * FROM [dbo].[UsersActionsStatistics]
			WHERE OtherInfo LIKE '%Vegan=True%'
		
		END
	ELSE IF (@Filter = 'Vegetarian')
		BEGIN

			SELECT * FROM [dbo].[UsersActionsStatistics]
			WHERE OtherInfo LIKE '%Vegetarian=True%'

		END
	ELSE IF (@Filter = 'GlutenFree')
		BEGIN

			SELECT * FROM [dbo].[UsersActionsStatistics]
			WHERE OtherInfo LIKE '%GlutenFree=True%'

		END
	ELSE IF (@Filter = 'FrigoMix')
		BEGIN

			SELECT * FROM [dbo].[UsersActionsStatistics]
			WHERE OtherInfo LIKE '%FrigoMix=True%'

		END
	ELSE IF (@Filter = 'Light')
		BEGIN

			SELECT * FROM [dbo].[UsersActionsStatistics]
			WHERE OtherInfo LIKE '%Light=True%'

		END
	ELSE IF (@Filter = 'Quick')
		BEGIN

			SELECT * FROM [dbo].[UsersActionsStatistics]
			WHERE OtherInfo LIKE '%Quick=True%'

		END
		
