CREATE PROCEDURE [dbo].[uspGetCountries]
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

END
