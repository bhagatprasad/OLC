CREATE PROCEDURE [dbo].[uspGetDepositOrdersByOrderId]
	(
	@paymentOrderId     BIGINT
	)
	AS

	  BEGIN 

	   SELECT
	       [Id]
		  ,[PaymentOrderId]
		  ,[OrderReference]
		  ,[DepositeAmount]
		  ,[ActualDepositeAmount]
		  ,[PendingDepositeAmount]
		  ,[StripeDepositeIntentId]
		  ,[StripeDepositeChargeId]
		  ,[IsPartialPayment]
		  ,[CreatedBy]
		  ,[CreatedOn]
		  ,[ModifiedBy]
		  ,[ModifiedOn]
		  ,[IsActive]

		  FROM [dbo].[DepositeOrder]

		  WHERE PaymentOrderId = @paymentOrderId;

		  END