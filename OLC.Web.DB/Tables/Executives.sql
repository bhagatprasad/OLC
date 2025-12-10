CREATE TABLE [dbo].[Executives]
(
	[Id]                    BIGINT                   NOT NULL PRIMARY KEY IDENTITY(1,1),
    [UserId]                BIGINT                   NOT NULL,
    [FirstName]             NVARCHAR(50)             NULL,
    [LastName]              NVARCHAR(50)             NULL,
    [Email]                 NVARCHAR(100)            NULL,
    [MaxConcurrentOrders]   INT                      NULL DEFAULT 100,
    [IsAvailable]           BIT                      NULL DEFAULT 1,
    [CurrentOrderCount]     INT                      NULL DEFAULT 0,
    [CreatedBy]             BIGINT                   NULL,
    [CreatedOn]             DATETIMEOFFSET           NULL DEFAULT GETDATE(),
    [ModifiedBy]            BIGINT                   NULL,
    [ModifiedOn]            DATETIMEOFFSET           NULL DEFAULT GETDATE(),
    [IsActive]              BIT                      NULL DEFAULT 1
)
