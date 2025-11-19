CREATE PROCEDURE [dbo].[uspInsertPriority]
(
	 @name				NVARCHAR(255)
	,@code				NVARCHAR(255)
	,@createdBy			bigint
)
AS
BEGIN
	INSERT INTO [dbo].Priority
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