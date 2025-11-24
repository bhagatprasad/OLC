CREATE TABLE [dbo].[UserWalletLog]
(
    [Id]                                BIGINT             NOT NULL PRIMARY KEY IDENTITY(1,1),
    [WalletId]                          BIGINT             NULL,  -- References UserWallet.Id
    [UserId]                            BIGINT             NULL,  -- For quick reference
    [Amount]                            DECIMAL(18,6)      NULL,  -- Positive for credit, negative for debit
    [TransactionType]                   NVARCHAR(20)       NULL,  -- 'Credit' or 'Debit'
    [Description]                       NVARCHAR(255)      NULL,      -- Optional description (e.g., 'Reward credited', 'Purchase debited')
    [ReferenceId]                       NVARCHAR(20)       NULL,      -- Optional: Link to TransactionReward.Id or other transaction ID
    [Currency]                          NVARCHAR(3)        NULL DEFAULT 'INR',
    [IsActive]                          BIT                NULL DEFAULT 1,
    [CreatedBy]                         BIGINT             NULL,
    [CreatedOn]                         DATETIMEOFFSET    NULL DEFAULT GETUTCDATE(),
    [ModifiedOn]                        DATETIMEOFFSET   NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]                        BIGINT          NULL
)