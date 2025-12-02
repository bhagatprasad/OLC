CREATE PROCEDURE [dbo].[uspGetAllDepositOrders]
	AS
BEGIN
    SELECT 
        Id,
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
    FROM DepositOrder
    ORDER BY Id DESC;
END
