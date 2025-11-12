CREATE PROCEDURE [dbo].[uspGetDepositOrdersByOrderId]
(
	   @PaymentOrderId     bigint
)
	AS

	  BEGIN 

	   SELECT
	       [Id]
		  ,[PaymentOrderId]
		  ,[OrderReference]
		  ,[DepositAmount]
		  ,[ActualDepositAmount]
		  ,[PendingDepositAmount]
		  ,[StripeDepositIntentId]
		  ,[StripeDepositChargeId]
		  ,[IsPartialPayment]
		  ,[CreatedBy]
		  ,[CreatedOn]
		  ,[ModifiedBy]
		  ,[ModifiedOn]
		  ,[IsActive]

		  FROM [dbo].[DepositOrder]

		  WHERE PaymentOrderId = @PaymentOrderId;

		  END