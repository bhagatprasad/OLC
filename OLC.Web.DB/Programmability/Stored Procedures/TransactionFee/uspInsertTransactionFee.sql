CREATE PROCEDURE [dbo].[uspInsertTransactionFee]
(	
		 @name                nvarchar(255)
		,@code                nvarchar(255)
		,@price               decimal(22,11)
		,@createdBy           bigint
	
)
AS 
  BEGIN
      INSERT INTO [dbo].[TransactionFee]
(
		 Name
		,Code
		,Price
		,CreatedBy
		,CreatedOn
		,ModifiedBy
		,ModifiedOn
		,IsActive
)

VALUES

(
		 @name
		,@code
		,@price
		,@createdBy
		,getdate()
		,@createdBy
		,getdate()
		,1
)
END