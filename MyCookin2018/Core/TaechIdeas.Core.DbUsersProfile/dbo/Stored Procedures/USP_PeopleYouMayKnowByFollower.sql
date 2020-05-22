
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <26/01/2013,,>
-- Last Edit: <,,>
-- Description:	<List of the people you may know according to follower
--               List of user followed by the user that YOU are following, ordered by number of repeats,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PeopleYouMayKnowByFollower]
	@Me uniqueidentifier,
	@NumberOfResults int
			
AS

BEGIN TRY

	SELECT TOP (@NumberOfResults) IDUserFollowed, COUNT(IDUserFollowed) As FollowerRepeatedNum FROM dbo.UsersFollowers 
		WHERE IDUser IN
		(
			--Tutte le persone che seguo
			SELECT IDUserFollowed FROM dbo.UsersFollowers WHERE IDUser = @Me
		)
		AND IDUserFollowed <> @Me
		AND IDUserFollowed NOT IN
		(
			SELECT IDUserFollowed FROM dbo.UsersFollowers WHERE IDUser = @Me
		)
		Group by IDUserFollowed
		ORDER BY FollowerRepeatedNum DESC
	   
END TRY

BEGIN CATCH
	
END CATCH