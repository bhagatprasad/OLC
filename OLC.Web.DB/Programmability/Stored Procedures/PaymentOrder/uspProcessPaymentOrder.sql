CREATE PROCEDURE [dbo].[uspProcessPaymentOrder]
(
    @paymentOrderId     BIGINT,
    @orderStatusId      BIGINT,
    @paymentStatusId    BIGINT,
    @depositeStatusId   BIGINT,
    @createdBy          BIGINT,
    @description        VARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[PaymentOrder]
    SET
        OrderStatusId    = @orderStatusId,
        PaymentStatusId  = @paymentStatusId,
        DepositStatusId  = @depositeStatusId,
        ModifiedBy       = @createdBy,
        ModifiedOn       = GETUTCDATE()   -- UTC time
    WHERE
        Id = @paymentOrderId;

    -- Insert payment order history
    EXEC [dbo].[uspInsertPaymentOrderHistory] 
        @paymentOrderId,
        @orderStatusId,
        @description,
        @createdBy;


    --push paymentorder to order que 
    EXEC [dbo].[uspInsertOrderQueue] @paymentOrderId;


    -- Return the updated payment order
    EXEC [dbo].[uspGetPaymentOrderById] @paymentOrderId;
END
GO
