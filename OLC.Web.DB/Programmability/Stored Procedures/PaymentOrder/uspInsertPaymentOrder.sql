CREATE PROCEDURE [dbo].[uspInsertPaymentOrder]
(
		 @orderReference					nvarchar(50)
		,@userId							bigint
		,@paymentReasonId					bigint
		,@amount							decimal(18,6)
		,@transactionFeeId					bigint
		,@platformFeeAmount					decimal(18,6)
		,@feeCollectionMethod				nvarchar(20)
		,@totalAmountToChargeCustomer		decimal(18,6)
		,@totalAmountToDepositToCustomer	decimal(18,6)
		,@totalPlatformFee					decimal(18,6)
		,@currency							nvarchar(3)
		,@creditCardId						bigint
		,@bankAccountId						bigint
		,@billingAddressId					bigint
		,@orderStatusId						bigint
		,@paymentStatusId					bigint
		,@depositStatusId					bigint
		,@stripePaymentIntentId				nvarchar(255)
		,@stripePaymentChargeId				nvarchar(255)
		,@stripeDepositIntentId				nvarchar(255)
		,@stripeDepositeChargeId			nvarchar(255)
		,@createdBy							bigint
)
As
BEGIN
	INSERT INTO [dbo].[PaymentOrder]
(
		 OrderReference
	    ,UserId							
		,PaymentReasonId					
		,Amount							
		,TransactionFeeId					
		,PlatformFeeAmount					
		,FeeCollectionMethod				
		,TotalAmountToChargeCustomer		
		,TotalAmountToDepositToCustomer
		,TotalPlatformFee					
		,Currency							
		,CreditCardId						
		,BankAccountId						
		,BillingAddressId					
		,OrderStatusId						
		,PaymentStatusId					
		,DepositStatusId					
		,StripePaymentIntentId				
		,StripePaymentChargeId				
		,StripeDepositeIntentId				
		,StripeDepositeChargeId					
		,CreatedBy
		,CreatedOn
		,ModifiedBy
		,ModifiedOn
		,IsActive
)
	VALUES
(
		 @orderReference					
		,@userId							
		,@paymentReasonId					
		,@amount							
		,@transactionFeeId					
		,@platformFeeAmount				
		,@feeCollectionMethod				
		,@totalAmountToChargeCustomer		
		,@totalAmountToDepositToCustomer	
		,@totalPlatformFee					
		,@currency							
		,@creditCardId						
		,@bankAccountId						
		,@billingAddressId					
		,@orderStatusId						
		,@paymentStatusId					
		,@depositStatusId					
		,@stripePaymentIntentId				
		,@stripePaymentChargeId				
		,@stripeDepositIntentId				
		,@stripeDepositeChargeId					
		,@createdBy	
		,GETDATE()
		,@createdBy
		,GETDATE()
		,1
)
END