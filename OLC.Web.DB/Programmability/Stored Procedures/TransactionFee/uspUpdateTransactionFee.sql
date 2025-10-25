CREATE PROCEDURE [dbo].[uspUpdateTransactionFee]
(
    @transactionFeeId       bigint        
   ,@name                   nvarchar(255)
   ,@code                   nvarchar(255)
   ,@price                  decimal(22,11)
   ,@modifiedBy             bigint
   ,@isActive               bit
)
AS 
 BEGIN
  UPDATE    [dbo].[TransactionFee]
        SET
             Name       =       @name,
             Code       =       @code,
             price      =       @price,             
             ModifiedOn =       GETDATE(),
             ModifiedBy =       @modifiedBy,
             IsActive   =       @isActive
       WHERE 
             ID         =       @transactionFeeId
END
