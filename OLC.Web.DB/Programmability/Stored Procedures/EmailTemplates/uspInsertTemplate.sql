CREATE PROCEDURE [dbo].[uspInsertTemplate]
	(
	  @name      varchar(max),
	  @code      varchar(max),
	  @template  varchar(max),
	  @createdBy bigint
	)
	AS
	BEGIN
	  SET NOCOUNT ON;

	  INSERT INTO [dbo].[EmailTemplate]
	    (
		  Name
		 ,Code
		 ,Template
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
		   ,@template
		   ,@createdBy
		   ,GETDATE()
		   ,@createdBy
		   ,GETDATE()
		   ,1
		 )
END
