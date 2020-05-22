-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <16/10/2013,,>
-- Last Edit:   <,,>
-- Description:	<Get Number of new users,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_Stats_NumberOfNewUsers]
	@StartDate datetime,
	@EndDate datetime,
	@IDGender int
		
AS
   
	IF @IDGender = -1		--ALL USERS
		BEGIN
			SELECT convert(varchar,DateMembership,103) AS DateMembership, Count(1) AS NewUsers	
			FROM [DBUsersProfile].[dbo].[Users]
			WHERE (DateMembership BETWEEN @Startdate AND @EndDate)
			GROUP BY convert(varchar,DateMembership,103)
			ORDER BY DateMembership desc	
		END
	ELSE IF @IDGender = 0	--ALL USERS WITH GENDER UNKNOW
		BEGIN
			SELECT convert(varchar,DateMembership,103) AS DateMembership, Count(1) AS NewUsers	
			FROM [DBUsersProfile].[dbo].[Users]
			WHERE (DateMembership BETWEEN @Startdate AND @EndDate) AND IDGender IS NULL
			GROUP BY convert(varchar,DateMembership,103)
			ORDER BY DateMembership desc	
		END
	ELSE IF @IDGender > 0	--ALL USERS WITH GENDER M OR F
		BEGIN
			SELECT convert(varchar,DateMembership,103) AS DateMembership, Count(1) AS NewUsers	
			FROM [DBUsersProfile].[dbo].[Users]
			WHERE (DateMembership BETWEEN @Startdate AND @EndDate) AND IDGender = @IDGender
			GROUP BY convert(varchar,DateMembership,103)
			ORDER BY DateMembership desc
		END
	
		
