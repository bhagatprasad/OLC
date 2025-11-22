CREATE TABLE [dbo].[UserWallet]
(
	[Id]                                BIGINT          NOT NULL PRIMARY KEY IDENTITY(1,1),
    [UserId]                            BIGINT          NOT NULL,
    [WalletId]                          NVARCHAR(20)    NOT NULL,
    [WalletType]                        NVARCHAR(20)    NOT NULL, -- 'cash', 'rewards', 'bonus'
    [CurrentBalance]                    DECIMAL(18,6)   NOT NULL DEFAULT 0,
    [TotalEarned]                       DECIMAL(18,6)   NOT NULL DEFAULT 0,
    [TotalSpent]                        DECIMAL(18,6)   NOT NULL DEFAULT 0,
    [Currency]                          NVARCHAR(3)     NOT NULL DEFAULT 'INR',
    [IsActive]                          BIT             NOT NULL DEFAULT 1,
    [CreatedBy]                         BIGINT          NULL,
    [CreatedOn]                         DATETIMEOFFSET  NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedOn]                        DATETIMEOFFSET  NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]                        BIGINT          NULL
)
