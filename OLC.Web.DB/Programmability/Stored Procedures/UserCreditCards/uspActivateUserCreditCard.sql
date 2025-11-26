CREATE PROCEDURE [dbo].[uspActivateUserCreditCard]
(
	@creditcardId     bigint,
	@modifiedBy      bigint
)
AS
 BEGIN 
	UPDATE   [dbo].[UserCreditCard]  SET  
	                                    
										IsActive   = 1,
										ModifiedBy = @modifiedBy,
										ModifiedOn = GETDATE()

								   WHERE        Id = @creditcardId
 END