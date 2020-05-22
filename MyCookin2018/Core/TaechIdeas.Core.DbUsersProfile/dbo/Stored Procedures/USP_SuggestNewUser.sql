

create PROCEDURE [dbo].[USP_SuggestNewUser]
	@IDUserRequester uniqueidentifier,
	@OffsetRows INT, 
	@FetchRows INT
AS

	SELECT IDUser, UserName, IDProfilePhoto, Name, Surname from Users
		WHERE AccountDeletedOn IS NULL
				AND UserEnabled = 1
				AND UserLocked = 0
				AND IDUser NOT IN (SELECT IDUser1 FROM UsersFriends WHERE IDUser2 = @IDUserRequester)
				AND IDUser NOT IN (SELECT IDUser2 FROM UsersFriends WHERE IDUser1 = @IDUserRequester)
				AND IDUser <> @IDUserRequester
		ORDER BY LastLogon DESC
		OFFSET @OffsetRows ROWS
		FETCH NEXT @FetchRows ROWS ONLY	
