CREATE PROCEDURE [dbo].[uspGetCitiesById]
	(
@cityId bigint
)

	WITH RECOMPILE

AS

BEGIN
      SELECT
	   [Id]
	  ,[CountryId]
	  ,[StateId]
	  ,[Name]
	  ,[Code]
	  ,[CreatedBy]
	  ,[CreatedOn]
	  ,[ModifiedBy]
	  ,[ModifiedOn]
	  ,[IsActive]
  FROM [dbo].[City]

 WHERE Id=@cityId

END
