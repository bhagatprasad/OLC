CREATE PROCEDURE [dbo].[uspGetTemplateById]
(
  @templateId   bigint
)

WITH RECOMPILE

AS

BEGIN

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

    WHERE Id=@templateId

    END