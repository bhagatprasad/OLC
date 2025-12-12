CREATE PROCEDURE [dbo].[uspUpdateQueueConfiguration]
(
    @id          BIGINT,
    @key         NVARCHAR(100),
    @value       NVARCHAR(MAX),
    @dataType    NVARCHAR(20),
    @description NVARCHAR(MAX),
    @modifiedBy  BIGINT,
    @isActive    BIT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[QueueConfiguration]
    SET
        [Key] = @key,
        [Value] = @value,
        [DataType] = @dataType,
        [Description]=@description,
        [ModifiedBy] = @modifiedBy,
        [ModifiedOn] = GETUTCDATE(),
        [IsActive] = @isActive
    WHERE [Id] = @id;

END;


