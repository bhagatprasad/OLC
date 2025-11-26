CREATE PROCEDURE [dbo].[uspGetPriority]
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
  FROM [dbo].Priority

END