CREATE PROCEDURE [dbo].[uspInsertDepositOrder]
(
   @PaymentOrderId BIGINT,
   @OrderReference NVARCHAR(100),
   @DepositAmount DECIMAL(18,6),
   @ActualDepositAmount DECIMAL(18,6),
   @PendingDepositAmount DECIMAL(18,6),
   @StripeDepositIntentId NVARCHAR(100),
   @StripeDepositChargeId NVARCHAR(100),
   @IsPartialPayment BIT,
   @CreatedBy BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[DepositOrder]
    (
        PaymentOrderId,
        OrderReference,
        DepositAmount,
        ActualDepositAmount,
        PendingDepositAmount,
        StripeDepositIntentId,
        StripeDepositChargeId,
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
        @DepositAmount,
        @ActualDepositAmount,
        @PendingDepositAmount,
        @StripeDepositIntentId,
        @StripeDepositChargeId,
        @IsPartialPayment,
        @CreatedBy,
        GETDATE(),
        @CreatedBy,
        GETDATE(),
        1
    );

    SELECT SCOPE_IDENTITY() AS NewId;
END
