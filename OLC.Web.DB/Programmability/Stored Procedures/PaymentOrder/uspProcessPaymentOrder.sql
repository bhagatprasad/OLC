CREATE PROCEDURE [dbo].[uspProcessPaymentOrder]
(
    @paymentOrderId BIGINT,
    @orderStatusId BIGINT,
    @paymentStatusId BIGINT,
    @depositeStatusId BIGINT,
    @createdBy BIGINT,
    @description VARCHAR(MAX),
    @paymentOrderType VARCHAR(MAX) = NULL,
    @walletId VARCHAR(MAX) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[PaymentOrder]
    SET
        OrderStatusId = @orderStatusId,
        PaymentStatusId = @paymentStatusId,
        DepositStatusId = @depositeStatusId,
        ModifiedBy = @createdBy,
        ModifiedOn = GETUTCDATE(),  -- Recommended: use UTC for consistency
        
        -- Update new columns only if values are provided
        PaymentOrderType = ISNULL(@paymentOrderType, PaymentOrderType),
        WalletId = ISNULL(@walletId, WalletId)
    WHERE
        Id = @paymentOrderId;

    -- Insert history (assuming this proc records status changes)
    EXEC [dbo].[uspInsertPaymentOrderHistory] 
        @paymentOrderId, 
        @orderStatusId, 
        @description, 
        @createdBy;

    -- Return the updated payment order
    EXEC [dbo].[uspGetPaymentOrderById] @paymentOrderId;
END
GO