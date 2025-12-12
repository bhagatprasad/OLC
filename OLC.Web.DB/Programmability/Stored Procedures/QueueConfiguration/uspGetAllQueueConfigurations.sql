CREATE PROCEDURE [dbo].[uspGetAllQueueConfigurations]
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
     [Id]
    ,[Key]
    ,[Value]
    ,[DataType]
    ,[Description]
    ,[CreatedBy]
    ,[CreatedOn]
    ,[ModifiedBy]
    ,[ModifiedOn]
    ,[IsActive]

    FROM [dbo].[QueueConfiguration]

    ORDER BY
     CreatedOn DESC
END