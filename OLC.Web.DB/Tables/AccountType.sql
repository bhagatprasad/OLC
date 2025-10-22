CREATE TABLE [dbo].[AccountType]
(
    [Id]                    BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name]                  NVARCHAR(255)       NULL,
    [Code]                  NVARCHAR(255)       NULL,
    [CreatedBy]             BIGINT              NULL,
    [CreatedOn]             DATETIMEOFFSET      NULL DEFAULT GETDATE(),
    [ModifiedOn]            DATETIMEOFFSET      NULL DEFAULT GETDATE(),
    [ModifiedBy]            BIGINT              NULL,
    [IsActive]              BIT                 NULL DEFAULT 1
);
