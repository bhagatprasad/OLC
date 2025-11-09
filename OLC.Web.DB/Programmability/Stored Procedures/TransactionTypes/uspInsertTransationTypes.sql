CREATE PROCEDURE [dbo].[uspInsertTransactionTypes]
(
	 @name				NVARCHAR(255)
	,@code				NVARCHAR(255)
	,@createdBy			bigint
)
AS
BEGIN
	INSERT INTO [dbo].[TransactionType]
( 
		 Name
		,code
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
		,@createdBy
		,GETDATE()
		,@createdBy
		,GETDATE()
		,1
)
END