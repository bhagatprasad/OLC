CREATE PROCEDURE [dbo].[uspGetTransactionFeeById]
(
      @transactionFeeId    bigint	
)
AS 
 BEGIN
   SELECT 
          [Id],
          [Name],
          [Code],
          [Price],
          [CreatedBy],
          [CreatedOn],
          [ModifiedBy],
          [ModifiedOn],
          [IsActive]
     FROM 
          [dbo].[TransactionFee] 
     WHERE 
          Id = @transactionFeeId
    END
