CREATE TABLE [dbo].[PaymentOrderHistory]
(
    [Id]                    BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
    [PaymentOrderId]        BIGINT              NULL,
    [StatusId]              BIGINT              NULL,
    [Description]           NVARCHAR(MAX)       NULL,
    [CreatedBy]             BIGINT              NULL,
    [CreatedOn]             DATETIMEOFFSET      NULL DEFAULT GETDATE(),
    [ModifiedOn]            DATETIMEOFFSET      NULL DEFAULT GETDATE(),
    [ModifiedBy]            BIGINT              NULL,
    [IsActive]              BIT                 NULL DEFAULT 1
);