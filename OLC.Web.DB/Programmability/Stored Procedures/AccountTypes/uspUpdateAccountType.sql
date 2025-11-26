CREATE PROCEDURE [dbo].[uspUpdateAccountType]
(
 @id                     bigint
,@name					 NVARCHAR(250)
,@code					 NVARCHAR(250)
,@modifiedBy			 bigint
,@isActive				 bit
)
AS
BEGIN

	 UPDATE [dbo].[AccountType] 
     SET  
			  Name            =@name
			 ,Code            =@code
			 ,ModifiedOn      =GETDATE()
			 ,ModifiedBy	  =@modifiedBy	
			 ,IsActive		  =@isActive
		WHERE 
		      ID			  =@id
 
END
