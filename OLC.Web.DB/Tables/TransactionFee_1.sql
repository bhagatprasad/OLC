CREATE TABLE [dbo].[TransactionFee]
(
	[Id]                            bigint                 NOT NULL    PRIMARY KEY   identity(1,1),
	[Name]                          NVARCHAR(255)          NULL,
    [Code]                          NVARCHAR(255)          NULL,
    [Price]                         DECIMAL(22,11)        NULL,
    [CreatedBy]                     BIGINT                 NULL,
    [CreatedOn]                     DATETIMEOFFSET         NULL DEFAULT GETDATE(),
    [ModifiedBy]                    BIGINT                 NULL,
    [ModifiedOn]                    DATETIMEOFFSET         NULL DEFAULT GETDATE(),
    [IsActive]                      BIT                    NULL DEFAULT 1
)
