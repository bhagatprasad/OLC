CREATE PROCEDURE [dbo].[uspInsertWalletTypes]
	(
	  @name            varchar(max),
	  @code            varchar(max),
	  @createdBy       bigint
	)
	AS

	BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[WalletType]
	     (  Name
		   ,Code
		   ,CreatedBy
		   ,CreatedOn
		   ,ModifiedBy
		   ,ModifiedOn
		   ,IsActive
		   )
	Values
	    ( @name
		 ,@code
		 ,@createdBy
		 ,GETDATE()
		 ,@createdBy
		 ,GETDATE()
		 ,1 )

		 END