CREATE PROCEDURE [dbo].[uspUpdateAccountType]
(
			 @id                     bigint
			,@name					 NVARCHAR(250)
			,@code					 NVARCHAR(250)
			,@createdBy               bigint
)
AS
BEGIN

	 UPDATE [dbo].[AccountType] 
     SET  

			  Name            =@name
			 ,Code            =@code
			 ,CreatedBy       =@createdBy
			 ,ModifiedOn      =GETDATE()
		WHERE 
		      ID			  =@id
 
END
