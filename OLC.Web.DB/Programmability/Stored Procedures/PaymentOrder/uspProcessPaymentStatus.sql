-- Create or Alter the procedure for processing payment status
CREATE  PROCEDURE [dbo].[uspProcessPaymentStatus]
(
    @paymentOrderId     bigint = NULL,
    @sessionId          nvarchar(255) = NULL,
    @paymentIntentId    nvarchar(255) = NULL,
    @paymentMethod      nvarchar(255) = NULL,
    @orderStatusId      bigint = NULL,
    @paymentStatusId    bigint = NULL,
    @description        nvarchar(max) = NULL,
    @userId             varchar(max) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Assuming this procedure updates the PaymentOrder table with the provided statuses
    -- and inserts a history record. Adjust logic as needed based on your requirements.

    IF @paymentOrderId IS NOT NULL
    BEGIN
        -- Update PaymentOrder with the provided statuses and additional fields
        UPDATE PaymentOrder
        SET
            OrderStatusId = @orderStatusId,
            PaymentStatusId = @paymentStatusId,
            StripePaymentIntentId = @paymentIntentId,  -- Assuming this maps to PaymentIntentId
            StripePaymentChargeId = @sessionId,
            ModifiedBy = @userId,  -- Or pass a createdBy if available
            ModifiedOn = GETDATE()
        WHERE Id = @paymentOrderId;

        -- Insert payment order history
        EXEC [dbo].[uspInsertPaymentOrderHistory] @paymentOrderId, @orderStatusId, @description, @userId;  -- Adjust createdBy if available

        -- Optionally, retrieve the updated payment order
        EXEC [dbo].[uspGetPaymentOrderById] @paymentOrderId;
    END
    ELSE
    BEGIN
        -- Handle case where PaymentOrderId is NULL, perhaps log or return an error
        RAISERROR('PaymentOrderId cannot be NULL.', 16, 1);
    END
END