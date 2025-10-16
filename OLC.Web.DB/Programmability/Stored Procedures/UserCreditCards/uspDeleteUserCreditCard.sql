CREATE PROCEDURE [dbo].[uspDeleteUserCreditCard]

@id BIGINT
AS
BEGIN 

	DELETE FROM [dbo].[UserCreditCard]
	WHERE Id =@id

END

	    