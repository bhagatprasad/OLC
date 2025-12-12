CREATE TABLE [dbo].[QueueConfiguration]
(
	[Id]                    BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Key]                   NVARCHAR(100)       NOT NULL UNIQUE,
    [Value]                 NVARCHAR(MAX)       NOT NULL,
    [DataType]              NVARCHAR(20)        NOT NULL DEFAULT 'String', -- 'String', 'Int', 'Bool', 'Decimal'
    [Description]           NVARCHAR(500)       NULL,
    [CreatedBy]             BIGINT              NULL,
    [CreatedOn]             DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedOn]            DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]            BIGINT              NULL,
    [IsActive]              BIT                 NOT NULL DEFAULT 1,
)
