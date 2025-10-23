CREATE PROCEDURE [dbo].[uspInsertBank]
(
	 @name                nvarchar(255)
	,@code                nvarchar(255)
	,@createdBy           bigint 
)
	
AS
 BEGIN
 	INSERT INTO [dbo].[Bank]
(
		 Name
        ,Code
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
		