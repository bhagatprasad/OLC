CREATE PROCEDURE [dbo].[uspInsertStatus]
(
	  @name			varchar(max),
	  @code			varchar(max),
	  @createdBy    bigint
)
AS 
  BEGIN
     INSERT INTO [dbo].[Status]
	             ( Name
				  ,Code
				  ,CreatedBy
				  ,CreatedOn
				  ,ModifiedBy
				  ,ModifiedOn
				  ,IsActive
				  ) Values
				  (@name
				  ,@code
				  ,@createdBy
				  ,GETDATE()
				  ,@createdBy
				  ,GETDATE()
				  ,1)
  END