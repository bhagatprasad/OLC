CREATE PROCEDURE [dbo].[uspProcessPaymentStatus]
(
    @paymentOrderId BIGINT = NULL,
    @sessionId NVARCHAR(255) = NULL,
    @paymentIntentId NVARCHAR(255) = NULL,
    @paymentMethod NVARCHAR(255) = NULL,
    @orderStatusId BIGINT = NULL,
    @paymentStatusId BIGINT = NULL,
    @description NVARCHAR(MAX) = NULL,
    @userId VARCHAR(MAX) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @depositAmount DECIMAL(18,6);
    DECLARE @orderExists BIT = 0;

    IF @paymentOrderId IS NOT NULL
    BEGIN
        BEGIN TRY
            -- Check if payment order exists and get deposit amount
            SELECT @depositAmount = TotalAmountToDepositToCustomer, @orderExists = 1
            FROM PaymentOrder
            WHERE Id = @paymentOrderId;

            IF @orderExists = 0
            BEGIN
                -- Avoid substitution: build message in variable
                DECLARE @NotFoundMsg NVARCHAR(500) = 'Payment order with ID ' + CAST(@paymentOrderId AS NVARCHAR(20)) + ' not found.';
                RAISERROR(@NotFoundMsg, 16, 1);
                RETURN;
            END

            -- Update PaymentOrder
            UPDATE PaymentOrder
            SET OrderStatusId = @orderStatusId,
                PaymentStatusId = @paymentStatusId,
                StripePaymentIntentId = @paymentIntentId,
                StripePaymentChargeId = @sessionId,
                ModifiedBy = @userId,
                ModifiedOn = GETDATE()
            WHERE Id = @paymentOrderId;

            -- Insert payment order history
            EXEC [dbo].[uspInsertPaymentOrderHistory] @paymentOrderId, @orderStatusId, @description, @userId;

            -- Insert transaction reward
            EXEC [dbo].[uspInsertTransactionReward] @paymentOrderId, @userId, @depositAmount;

            -- Return updated payment order
            EXEC [dbo].[uspGetPaymentOrderById] @paymentOrderId;
        END TRY
        BEGIN CATCH
            -- Avoid substitution: use variable for full message
            DECLARE @FullErrorMessage NVARCHAR(4000) = 'Error processing payment status: ' + ISNULL(ERROR_MESSAGE(), 'Unknown error');
            RAISERROR(@FullErrorMessage, 16, 1);
        END CATCH
    END
    ELSE
    BEGIN
        RAISERROR('PaymentOrderId cannot be NULL.', 16, 1);
    END
END
GO

