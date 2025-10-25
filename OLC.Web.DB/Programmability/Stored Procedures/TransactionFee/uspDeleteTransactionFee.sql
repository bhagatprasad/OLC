CREATE PROCEDURE [dbo].[uspDeleteTransactionFee]
(
    @transactionFeeId       bigint        
)

AS 

 BEGIN
    
    DELETE  FROM [dbo].[TransactionFee] WHERE Id = @transactionFeeId
 
END
       

