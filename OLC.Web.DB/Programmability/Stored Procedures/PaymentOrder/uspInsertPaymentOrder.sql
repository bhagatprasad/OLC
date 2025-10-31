CREATE PROCEDURE [dbo].[uspInsertPaymentOrder]
(
     @orderReference					nvarchar(50) = NULL
    ,@userId							bigint = NULL
    ,@paymentReasonId					bigint = NULL
    ,@amount							decimal(18,6) = NULL
    ,@transactionFeeId					bigint = NULL
    ,@platformFeeAmount					decimal(18,6) = NULL
    ,@feeCollectionMethod				nvarchar(20) = NULL
    ,@totalAmountToChargeCustomer		decimal(18,6) = NULL
    ,@totalAmountToDepositToCustomer	decimal(18,6) = NULL
    ,@totalPlatformFee					decimal(18,6) = NULL
    ,@currency							nvarchar(3) = NULL
    ,@creditCardId						bigint = NULL
    ,@bankAccountId						bigint = NULL
    ,@billingAddressId					bigint = NULL
    ,@orderStatusId						bigint = NULL
    ,@paymentStatusId					bigint = NULL
    ,@depositStatusId					bigint = NULL
    ,@stripePaymentIntentId				nvarchar(255) = NULL
    ,@stripePaymentChargeId				nvarchar(255) = NULL
    ,@stripeDepositIntentId				nvarchar(255) = NULL
    ,@stripeDepositeChargeId			nvarchar(255) = NULL
    ,@createdBy							bigint = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PaymentOrderId BIGINT;

    INSERT INTO PaymentOrder (
        OrderReference,
        UserId,
        PaymentReasonId,
        Amount,
        TransactionFeeId,
        PlatformFeeAmount,
        FeeCollectionMethod,
        TotalAmountToChargeCustomer,
        TotalAmountToDepositToCustomer,
        TotalPlatformFee,
        Currency,
        CreditCardId,
        BankAccountId,
        BillingAddressId,
        OrderStatusId,
        PaymentStatusId,
        DepositStatusId,
        StripePaymentIntentId,
        StripePaymentChargeId,
        StripeDepositeIntentId,
        StripeDepositeChargeId,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    ) VALUES (
        @orderReference,
        @userId,
        @paymentReasonId,
        @amount,
        @transactionFeeId,
        @platformFeeAmount,
        @feeCollectionMethod,
        @totalAmountToChargeCustomer,
        @totalAmountToDepositToCustomer,
        @totalPlatformFee,
        @currency,
        @creditCardId,
        @bankAccountId,
        @billingAddressId,
        @orderStatusId,
        @paymentStatusId,
        @depositStatusId,
        @stripePaymentIntentId,
        @stripePaymentChargeId,
        @stripeDepositIntentId,
        @stripeDepositeChargeId,
        @createdBy,
        GETDATE(),  -- Assuming CreatedOn is set to current date/time
        @createdBy,       -- ModifiedBy
         GETDATE(),       -- ModifiedOn
        1           -- IsActive, assuming default to active
    );

    SET @PaymentOrderId = SCOPE_IDENTITY();

    EXEC [dbo].[uspInsertPaymentOrderHistory] @PaymentOrderId, @orderStatusId, 'Order placed', @createdBy;

    EXEC [dbo].[uspGetPaymentOrderById] @PaymentOrderId;

    -- Optionally, return the inserted Id if needed
    SELECT @PaymentOrderId AS Id;
END