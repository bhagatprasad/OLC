
CREATE PROCEDURE [dbo].[uspGetApprovedOrdersForDeposit]
AS
BEGIN
       SELECT
		     [Id]
			,[OrderReference]
			,[UserId]							
			,[PaymentReasonId]
			,[PaymentOrderType]
			,[WalletId]
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
     OrderStatusId=42 and   PaymentStatusId = 24  AND DepositStatusId=1 and TransactionFeeId = 1 and [IsActive] = 1 
    ORDER BY [ModifiedOn] DESC;
END
GO