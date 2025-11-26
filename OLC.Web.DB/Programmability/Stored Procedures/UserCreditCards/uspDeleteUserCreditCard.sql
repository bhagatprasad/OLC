CREATE PROCEDURE [dbo].[uspDeleteUserCreditCard]
(
	@creditcardId     bigint
)
AS
BEGIN 
	UPDATE  [dbo].[UserCreditCard] SET IsActive=0  WHERE Id = @creditcardId
END

	    