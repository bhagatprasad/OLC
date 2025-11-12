CREATE PROCEDURE [dbo].[uspGetDepositOrdersByOrderId]
	(
	@Id     BIGINT
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

		  WHERE Id = @Id;

		  END