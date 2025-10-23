CREATE PROCEDURE [dbo].[uspInsertBank]
(
	   @name                NVARCHAR(255)
	  ,@code                NVARCHAR(255)
	  ,@createdBy           BIGINT 
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
		