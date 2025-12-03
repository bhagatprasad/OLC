CREATE PROCEDURE [dbo].[uspDeleteWalletTypes]
	(
	  @id  bigint
	)
 AS

 BEGIN

    SET NOCOUNT ON;  

 update [dbo].[WalletType]

    SET  
	    IsActive=0,
	    ModifiedOn = GETDATE() 
	    where Id=@id;

END