CREATE TABLE [dbo].[DepositOrder]
(
    [Id]                            BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
    [PaymentOrderId]                BIGINT              NOT NULL,
    [OrderReference]                NVARCHAR(50)        NOT NULL UNIQUE,
    [DepositeAmount]                DECIMAL(18,6)       NOT NULL,
    [ActualDepositeAmount]          DECIMAL(18,6)       NOT NULL,
    [PendingDepositeAmount]         DECIMAL(18,6)       NOT NULL,
    [StripeDepositeIntentId]        NVARCHAR(255)       NULL,
    [StripeDepositeChargeId]        NVARCHAR(255)       NULL,
    [IsPartialPayment]              BIGINT              NULL,
    [CreatedBy]                     BIGINT              NULL,
    [CreatedOn]                     DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedOn]                    DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]                    BIGINT              NULL,
    [IsActive]                      BIT                 NOT NULL DEFAULT 1,
)