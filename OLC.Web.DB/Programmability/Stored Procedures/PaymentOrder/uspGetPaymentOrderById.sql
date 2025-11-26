CREATE PROCEDURE [dbo].[uspGetPaymentOrderById]
(
    @paymentOrderId         bigint
)

AS

BEGIN

       SELECT
		     [Id]
			,[OrderReference]
			,[UserId]							
			,[PaymentReasonId]					
			,[Amount]							
			,[TransactionFeeId]					
			,[PlatformFeeAmount]					
			,[FeeCollectionMethod]				
			,[TotalAmountToChargeCustomer]		
			,[TotalAmountToDepositToCustomer]
			,[TotalPlatformFee]					
			,[Currency]							
			,[CreditCardId]						
			,[BankAccountId]						
			,[BillingAddressId]				
			,[OrderStatusId]						
			,[PaymentStatusId]					
			,[DepositStatusId]					
			,[StripePaymentIntentId]				
			,[StripePaymentChargeId]				
			,[StripeDepositeIntentId]				
			,[StripeDepositeChargeId]					
			,[CreatedBy]
			,[CreatedOn]
			,[ModifiedBy]
			,[ModifiedOn]
			,[IsActive]
        FROM [dbo].[PaymentOrder]

			WHERE 
			Id		=   @paymentOrderId	
END