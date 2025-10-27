CREATE PROCEDURE [dbo].[uspGetPaymentOrderHistory]
	(
  @paymentOrderId           bigint
)
AS
   BEGIN
 
      select
        poh.Id	,
        poh.PaymentOrderId,
        poh.StatusId,
        poh.Description	,
        poh.CreatedBy	,
        poh.CreatedOn,
        poh.ModifiedOn,	
        poh.ModifiedBy,	
        poh.IsActive 
        from [dbo].[PaymentOrderHistory] poh 
        inner join [dbo].[PaymentOrder] po 
        on poh.PaymentOrderId = po.Id
        Where poh.PaymentOrderId= @paymentOrderId
END