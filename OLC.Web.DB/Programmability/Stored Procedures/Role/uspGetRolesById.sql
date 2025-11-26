CREATE PROCEDURE [dbo].[uspGetRolesById]
(
@roleId bigint
)

	WITH RECOMPILE

AS

BEGIN

  SELECT
	   [Id]
	  ,[Name]
	  ,[Code]
	  ,[CreatedBy]
	  ,[CreatedOn]
	  ,[ModifiedBy]
	  ,[ModifiedOn]
	  ,[IsActive]
  FROM [dbo].[Role]

 WHERE Id=@roleId

END