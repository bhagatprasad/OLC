CREATE TABLE [dbo].[RewardConfiguration]
(
    [Id]                                BIGINT          NOT NULL PRIMARY KEY IDENTITY(1,1),
    [RewardName]                        NVARCHAR(100)   NOT NULL,
    [RewardType]                        NVARCHAR(20)    NOT NULL, -- 'percentage', 'fixed', 'tiered'
    [RewardValue]                       DECIMAL(18,6)   NOT NULL, -- Percentage (0.1) or Fixed amount (10)
    [MinimumTransactionAmount]          DECIMAL(18,6)   NOT NULL DEFAULT 0,
    [MaximumReward]                     DECIMAL(18,6)   NULL, -- Maximum reward per transaction
    [IsActive]                          BIT             NOT NULL DEFAULT 1,
    [ValidFrom]                         DATETIMEOFFSET  NOT NULL DEFAULT GETUTCDATE(),
    [ValidTo]                           DATETIMEOFFSET  NULL,
    [CreatedBy]                         BIGINT          NULL,
    [CreatedOn]                         DATETIMEOFFSET  NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedOn]                        DATETIMEOFFSET  NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]                        BIGINT          NULL
);