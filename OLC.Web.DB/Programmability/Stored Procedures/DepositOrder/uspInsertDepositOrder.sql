CREATE PROCEDURE [dbo].[uspInsertDepositOrder]
(
    @PaymentOrderId BIGINT,
    @OrderReference NVARCHAR(50),
    @DepositeAmount DECIMAL(18,6),
    @ActualDepositeAmount DECIMAL(18,6),
    @PendingDepositeAmount DECIMAL(18,6),
    @StripeDepositeIntentId NVARCHAR(255),
    @StripeDepositeChargeId NVARCHAR(255),
    @IsPartialPayment BIGINT,
    @CreatedBy BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[DepositOrder]
    (
        PaymentOrderId,
        OrderReference,
        DepositeAmount,
        ActualDepositeAmount,
        PendingDepositeAmount,
        StripeDepositeIntentId,
        StripeDepositeChargeId,
        IsPartialPayment,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES
    (
        @PaymentOrderId,
        @OrderReference,
        @DepositeAmount,
        @ActualDepositeAmount,
        @PendingDepositeAmount,
        @StripeDepositeIntentId,
        @StripeDepositeChargeId,
        @IsPartialPayment,
        @CreatedBy,
        GETUTCDATE(),  -- Matches table default
        @CreatedBy,
        GETUTCDATE(),  -- Matches table default
        1
    );
END