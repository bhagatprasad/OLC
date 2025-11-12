CREATE PROCEDURE [dbo].[uspGetDepositOrdersByOrderId]
(
    @PaymentOrderId BIGINT
)
AS
BEGIN
    SELECT
        [Id],
        [PaymentOrderId],
        [OrderReference],
        [DepositeAmount],
        [ActualDepositeAmount],
        [PendingDepositeAmount],
        [StripeDepositeIntentId],
        [StripeDepositeChargeId],
        [IsPartialPayment],
        [CreatedBy],
        [CreatedOn],
        [ModifiedBy],
        [ModifiedOn],
        [IsActive]
    FROM [dbo].[DepositOrder]
    WHERE PaymentOrderId = @PaymentOrderId
    ORDER BY [CreatedOn] DESC;
END