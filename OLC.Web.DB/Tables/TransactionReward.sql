CREATE TABLE [dbo].[TransactionReward]
(
	[Id]                                BIGINT             NOT NULL PRIMARY KEY IDENTITY(1,1),
    [PaymentOrderId]                    BIGINT             NOT NULL,
    [UserId]                            BIGINT             NOT NULL,
    [RewardConfigurationId]             BIGINT             NOT NULL,
    [TransactionAmount]                 DECIMAL(18,6)      NOT NULL, -- Original transaction amount
    [RewardAmount]                      DECIMAL(18,6)      NOT NULL, -- Calculated reward (10 for 10000)
    [RewardRate]                        DECIMAL(18,6)      NOT NULL, -- Rate used (0.001 for 0.1%)
    [RewardStatus]                      NVARCHAR(20)       NOT NULL DEFAULT 'Pending', -- 'pending', 'credited', 'cancelled'
    [CreditedToWalletId]                BIGINT             NULL,
    [CreditedOn]                        DATETIMEOFFSET     NULL,
    [ExpiryDate]                        DATETIMEOFFSET     NULL,
    [IsActive]                          BIT                NOT NULL DEFAULT 1,
    [CreatedBy]                         BIGINT             NULL,
    [CreatedOn]                         DATETIMEOFFSET     NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedOn]                        DATETIMEOFFSET     NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]                        BIGINT             NULL
)
