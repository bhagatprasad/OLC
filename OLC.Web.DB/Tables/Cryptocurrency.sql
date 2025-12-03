CREATE TABLE [dbo].[Cryptocurrency]
(
	[Id]                            BIGINT                 NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Symbol]                        NVARCHAR(10)           NOT NULL UNIQUE, -- BTC, ETH, USDT
    [Name]                          NVARCHAR(100)          NOT NULL, -- Bitcoin, Ethereum
    [Blockchain]                    NVARCHAR(50)           NOT NULL, -- 'Bitcoin', 'Ethereum', 'BinanceSmartChain'
    [ContractAddress]               NVARCHAR(255)          NULL, -- For ERC20 tokens
    [Decimals]                      INT                    NOT NULL DEFAULT 18,
    [IsStablecoin]                  BIT                    NOT NULL DEFAULT 0,
    [MinDepositAmount]              DECIMAL(28,18)         NOT NULL DEFAULT 0.0001,
    [MinWithdrawalAmount]           DECIMAL(28,18)         NOT NULL DEFAULT 0.001,
    [WithdrawalFee]                 DECIMAL(28,18)         NOT NULL DEFAULT 0.0005,
    [IconUrl]                       NVARCHAR(500)          NULL,
    [CreatedBy]                     BIGINT                 NULL,
    [CreatedOn]                     DATETIMEOFFSET         NULL DEFAULT GETDATE(),
    [ModifiedBy]                    BIGINT                 NULL,
    [ModifiedOn]                    DATETIMEOFFSET         NULL DEFAULT GETDATE(),
    [IsActive]                      BIT                    NULL DEFAULT 1
)
