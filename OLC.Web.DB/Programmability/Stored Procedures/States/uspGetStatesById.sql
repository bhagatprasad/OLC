CREATE PROCEDURE [dbo].[uspGetStatesById]
(
@stateId bigint
)

	WITH RECOMPILE

AS

BEGIN
      SELECT
	   [Id]
	  ,[CountryId]
	  ,[Name]
	  ,[Code]
	  ,[CreatedBy]
	  ,[CreatedOn]
	  ,[ModifiedBy]
	  ,[ModifiedOn]
	  ,[IsActive]
  FROM [dbo].[State]

 WHERE Id=@stateId

END
