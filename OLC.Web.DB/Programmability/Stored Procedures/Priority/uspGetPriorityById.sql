CREATE PROCEDURE [dbo].[uspGetPriorityById]
	(
		@priorityId		bigint
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
  FROM [dbo].[Priority]
  WHERE 
		   Id  = @priorityId
END