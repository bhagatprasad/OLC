CREATE PROCEDURE [dbo].[uspActivateUserCreditCard]
(
	@creditcardId     bigint
)
AS
 BEGIN 
	UPDATE   [dbo].[UserCreditCard]  SET IsActive=1  WHERE   Id = @creditcardId
 END