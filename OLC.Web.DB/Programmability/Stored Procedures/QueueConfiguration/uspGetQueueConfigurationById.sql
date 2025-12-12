CREATE PROCEDURE [dbo].[uspGetQueueConfigurationById]
(
  @QueueConfigurationId       BIGINT
)
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

   WHERE Id=@QueueConfigurationId;

END
