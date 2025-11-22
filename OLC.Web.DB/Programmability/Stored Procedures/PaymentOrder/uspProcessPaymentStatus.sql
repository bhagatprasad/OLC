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

    DECLARE @depositAmount DECIMAL(18,6);
    DECLARE @orderExists BIT = 0;

    IF @paymentOrderId IS NOT NULL
    BEGIN
        BEGIN TRY
            -- Check if payment order exists and get deposit amount
            SELECT 
                @depositAmount = TotalAmountToDepositToCustomer,
                @orderExists = 1
            FROM PaymentOrder 
            WHERE Id = @paymentOrderId;

            IF @orderExists = 0
            BEGIN
                RAISERROR('Payment order with ID %d not found.', 16, 1, @paymentOrderId);
                RETURN;
            END

            -- Update PaymentOrder
            UPDATE PaymentOrder
            SET
                OrderStatusId = @orderStatusId,
                PaymentStatusId = @paymentStatusId,
                StripePaymentIntentId = @paymentIntentId,
                StripePaymentChargeId = @sessionId,
                ModifiedBy = @userId,
                ModifiedOn = GETDATE()
            WHERE Id = @paymentOrderId;

            -- Insert payment order history
            EXEC [dbo].[uspInsertPaymentOrderHistory] @paymentOrderId, @orderStatusId, @description, @userId;

            -- Insert transaction reward with deposit amount
            EXEC [dbo].[uspInsertTransactionReward] @paymentOrderId, @userId, @depositAmount;

            -- Retrieve the updated payment order
            EXEC [dbo].[uspGetPaymentOrderById] @paymentOrderId;

        END TRY
        BEGIN CATCH
            DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
            RAISERROR('Error processing payment status: %s', 16, 1, @ErrorMessage);
        END CATCH
    END
    ELSE
    BEGIN
        RAISERROR('PaymentOrderId cannot be NULL.', 16, 1);
    END
END