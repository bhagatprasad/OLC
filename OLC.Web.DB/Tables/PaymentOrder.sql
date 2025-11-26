CREATE TABLE [dbo].[PaymentOrder]
(
    [Id]                            BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
    -- Reference & Identification
    [OrderReference]                NVARCHAR(50)        NOT NULL UNIQUE,
    [UserId]                        BIGINT              NOT NULL,
    [PaymentReasonId]               BIGINT              NULL,
    -- User Input Amount
    [Amount]                        DECIMAL(18,6)       NOT NULL, -- Original amount user entered (10000)
    -- Fee Configuration
    [TransactionFeeId]              BIGINT              NOT NULL,
    [PlatformFeeAmount]             DECIMAL(18,6)       NOT NULL, -- Calculated fee (100)
    [FeeCollectionMethod]           NVARCHAR(20)        NOT NULL DEFAULT 'added', -- 'added' or 'deducted'
    -- Amount Calculations Based on Fee Method
    [TotalAmountToChargeCustomer]   DECIMAL(18,6)       NOT NULL, -- Amount to charge credit card
    [TotalAmountToDepositToCustomer] DECIMAL(18,6)      NOT NULL, -- Amount user receives
    [TotalPlatformFee]              DECIMAL(18,6)       NOT NULL, -- Total fee company keeps
    -- Breakdown for transparency
    [Currency]                      NVARCHAR(3)         NOT NULL DEFAULT 'USD',
    -- Payment Sources
    [CreditCardId]                  BIGINT              NULL,
    [BankAccountId]                 BIGINT              NULL,
    [BillingAddressId]              BIGINT              NULL,
    -- Status Tracking
    [OrderStatusId]                 BIGINT              NULL,
    [PaymentStatusId]               BIGINT              NULL,
    [DepositStatusId]               BIGINT              NULL,
    -- External References
    [StripePaymentIntentId]         NVARCHAR(255)       NULL,
    [StripePaymentChargeId]         NVARCHAR(255)       NULL,
 
     
    -- External References
    [StripeDepositeIntentId]         NVARCHAR(255)       NULL,
    [StripeDepositeChargeId]         NVARCHAR(255)       NULL,
    -- Audit Fields
    [CreatedBy]                     BIGINT              NULL,
    [CreatedOn]                     DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedOn]                    DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]                    BIGINT              NULL,
    [IsActive]                      BIT                 NOT NULL DEFAULT 1,
)

