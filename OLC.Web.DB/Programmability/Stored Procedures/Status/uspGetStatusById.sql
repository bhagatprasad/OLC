CREATE PROCEDURE [dbo].[uspGetStatusById]
(
  @statusId  bigint
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
  FROM [dbo].[Status]

  WHERE 

  Id= @statusId

END