CREATE PROCEDURE [dbo].[uspGetCountriesById]
	(
	@countryId bigint
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
  FROM [dbo].[Country]

   WHERE Id=@countryId

END