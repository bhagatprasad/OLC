CREATE PROCEDURE [dbo].[uspGetAllUserPaymentOrders]
	
AS
BEGIN
    SELECT        
		po.[Id],
        po.[OrderReference],
        po.[UserId],
        po.[PaymentReasonId],
		pr.[Name] AS PaymentReasonName,
        po.[Amount],
        po.[TransactionFeeId],
		tf.[Name] AS TransactionFeeAmount,
		po.[PlatformFeeAmount],
        po.[FeeCollectionMethod],
        po.[TotalAmountToChargeCustomer],
        po.[TotalAmountToDepositToCustomer],
        po.[TotalPlatformFee],
        po.[Currency],
        po.[CreditCardId],
		ucc.[EncryptedCardNumber] AS CreditCardNumber,
		po.[BankAccountId],
		uba.[AccountNumber] AS BankAccountNumber,
		po.[BillingAddressId],
		uba_addr.[AddessLineOne] AS BillingAddress,
	    po.[OrderStatusId],
		os.[Name] AS OrderStatus,
		po.[PaymentStatusId],
		ps.[Name] AS PaymentStatus,
		po.[DepositStatusId],
		ds.[Name] AS DepositStatus,
		po.[StripePaymentIntentId],
        po.[StripePaymentChargeId],
        po.[StripeDepositeIntentId],  
        po.[StripeDepositeChargeId],  
        po.[CreatedBy],
        po.[CreatedOn],
        po.[ModifiedBy],
        po.[ModifiedOn],
		po.[IsActive]

	FROM [dbo].[PaymentOrder] po	

	LEFT JOIN [dbo].[PaymentReason] pr ON po.[PaymentReasonId]=pr.[Id]
	LEFT JOIN [dbo].[TransactionFee] tf ON po.[TransactionFeeId] =tf.[Id]
	LEFT JOIN [dbo].[UserCreditCard] ucc ON po.[CreditCardId] = ucc.[Id]
	LEFT JOIN [dbo].[UserBankAccount] uba ON po.[BankAccountId]= uba.[Id]
	LEFT JOIN [dbo].[UserBillingAddress] uba_addr ON po.[BillingAddressId] = uba_addr.[Id]
	LEFT JOIN [dbo].[Status] os ON po.[OrderStatusId] = os.[Id]
	LEFT JOIN [dbo].[Status] ps ON po.[PaymentStatusId]= ps.[Id]
	LEFT JOIN [dbo].[Status] ds ON po.[DepositStatusId]= ds.[Id]
	 
END
