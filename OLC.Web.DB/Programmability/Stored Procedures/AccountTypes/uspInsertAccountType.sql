CREATE PROCEDURE [dbo].[uspInsertAccountType]
(		
	@name					 NVARCHAR(250)
   ,@code					 NVARCHAR(250)
   ,@createdBy               bigint
)
AS
BEGIN 
     INSERT INTO [dbo].[AccountType]
	   (	
	        
			 Name
			,Code
			,CreatedBy
			,CreatedOn
			,ModifiedOn
			,ModifiedBy
			,IsActive
	   ) VALUES
       (	   
		    @name
		   ,@code
		   ,@createdBy
		   ,GETDATE()
		   ,GETDATE()
		   ,@createdBy
		   ,1
       )
END


