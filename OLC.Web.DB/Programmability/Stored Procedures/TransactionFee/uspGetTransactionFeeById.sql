CREATE PROCEDURE [dbo].[uspGetTransactionFeeById]
(
      @feeId    bigint	
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
          Id = @feeId
    END
