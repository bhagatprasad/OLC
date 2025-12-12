CREATE PROCEDURE [dbo].[uspSaveQueueConfiguration]
(
    @Key            NVARCHAR(100),
    @Value          NVARCHAR(MAX),
    @DataType       NVARCHAR(20),
    @Description    NVARCHAR(500)  = NULL,
    @CreatedBy      BIGINT         = NULL,
    @ModifiedBy     BIGINT         = NULL
 )

 AS

 BEGIN

   SET NOCOUNT ON;

   INSERT INTO [dbo].[QueueConfiguration]
    ( 
            [Key],
            [Value],
            [DataType],
            [Description],
            [CreatedBy],
            [CreatedOn],
            [ModifiedBy],
            [ModifiedOn],
            [IsActive]
    )
      VALUES
        (
            @Key,
            @Value,
            @DataType,
            @Description,
            @CreatedBy,
            GETUTCDATE(),
            @CreatedBy,
            GETUTCDATE(),
            1
        )
    END