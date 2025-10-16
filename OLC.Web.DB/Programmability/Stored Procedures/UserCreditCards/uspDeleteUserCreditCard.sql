CREATE PROCEDURE [dbo].[uspDeleteUserCreditCard]
(
	@creditcardId     bigint
)
AS
BEGIN 
	DELETE FROM [dbo].[UserCreditCard] WHERE Id = @creditcardId
END

	    