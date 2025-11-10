
create PROCEDURE [dbo].[uspActivateTransactionTypes]
(
	@transactionTypeId     bigint,
	@modifiedBy      bigint
)
AS
 BEGIN 
	UPDATE   [dbo].[TransactionType]  SET  
	                                    
										IsActive   = 1,
										ModifiedBy = @modifiedBy,
										ModifiedOn = GETDATE()

								   WHERE        Id = @transactionTypeId
 END