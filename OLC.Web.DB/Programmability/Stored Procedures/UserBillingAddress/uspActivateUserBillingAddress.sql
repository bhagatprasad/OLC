CREATE PROCEDURE [dbo].[uspActivateUserBillingAddress]
(
@billingAddressId    bigint,
@modifiedBy          bigint
)
AS 
  BEGIN
       UPDATE [dbo].[UserBillingAddress] SET

                                            IsActive = 1,
                                            ModifiedBy =@modifiedBy,
                                            ModifiedOn = GETDATE()
                                       
                                       WHERE Id = @billingAddressId

END


