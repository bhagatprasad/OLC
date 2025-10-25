CREATE PROCEDURE [dbo].[uspDeleteUserBillingAddress]
	(@billingAddressId  bigint)

AS

BEGIN

update [dbo].[UserBillingAddress]
       set  IsActive = 0
       WHERE Id = @billingAddressId
END
