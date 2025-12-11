CREATE PROCEDURE [dbo].[uspGetAllTemplate]

WITH RECOMPILE

AS

BEGIN

   SET NOCOUNT ON;

   SELECT 
       [Id]
      ,[Name]
      ,[Code]
      ,[Template]
      ,[CreatedBy]
	  ,[CreatedOn]
	  ,[ModifiedBy]
	  ,[ModifiedOn]
	  ,[IsActive]

      FROM [dbo].[EmailTemplate]

      END
