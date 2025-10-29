CREATE PROC [dbo].[uspProcessPaymentOrder]
(
@paymentOrderId     bigint,
@orderStatusId      bigint,
@paymentStatusId    bigint,
@depositeStatusId   bigint,
@createdBy          bigint,
@description        varchar(max)
)
 
 
AS
 
BEGIN
 
 
    UPDATE PaymentOrder
 
 
    SET
 
    OrderStatusId  = @orderStatusId
   ,PaymentStatusId = @paymentStatusId
   ,DepositStatusId  = @depositeStatusId
   ,ModifiedBy = @createdBy   
   ,ModifiedOn  = GETDATE()
 
   where Id = @paymentOrderId
 
   EXEC [dbo].[uspInsertPaymentOrderHistory] @paymentOrderId,@orderStatusId,@description,@createdBy

   EXEC [dbo].[uspGetPaymentOrderById] @paymentOrderId
 
END