CREATE PROCEDURE [dbo].[uspGetDepositOrdersByUserId]
	@userId bigint
	AS
BEGIN
    SELECT 
       do. Id,
       po.UserId AS UserId,
       do. PaymentOrderId,
       do. OrderReference,
       do.DepositeAmount,
       do.ActualDepositeAmount,
       do.PendingDepositeAmount,
       do.StripeDepositeIntentId,
       do.StripeDepositeChargeId,
       do.IsPartialPayment,
       do.CreatedBy,
       do.CreatedOn,
       do. ModifiedBy,
       do. ModifiedOn,
       do. IsActive
    FROM [DepositOrder] do
 
   INNER JOIN [dbo].[PaymentOrder] po
        ON do.PaymentOrderId = po.Id
    WHERE po.UserId = @UserId
    ORDER BY do.Id DESC;
END