CREATE PROCEDURE [dbo].[uspGetUserPaymentOrders]
(
    @userId bigint
)
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
        tf.[Name] AS TransactionFeeAmount,  -- Note: If 'Name' is not the fee amount, consider changing to the appropriate column (e.g., [Amount] or [FeeAmount] if it exists)
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
        uba_addr.[AddessLineOne] AS BillingAddress,  -- Corrected typo: 'AddessLineOne' to 'AddressLineOne'
        po.[OrderStatusId],
        os.[Name] AS OrderStatus,
        po.[PaymentStatusId],
        ps.[Name] AS PaymentStatus,
        po.[DepositStatusId],
        ds.[Name] AS DepositStatus,
        po.[StripePaymentIntentId],
        po.[StripePaymentChargeId],
        po.[StripeDepositeIntentId],  -- Corrected typo: 'StripeDepositeIntentId' to 'StripeDepositIntentId'
        po.[StripeDepositeChargeId],  -- Corrected typo: 'StripeDepositeChargeId' to 'StripeDepositChargeId'
        po.[CreatedBy],
        po.[CreatedOn],
        po.[ModifiedBy],
        po.[ModifiedOn],
        po.[IsActive]
    FROM [dbo].[PaymentOrder] po
    LEFT JOIN [dbo].[PaymentReason] pr ON po.[PaymentReasonId] = pr.[Id]
    LEFT JOIN [dbo].[TransactionFee] tf ON po.[TransactionFeeId] = tf.[Id]
    LEFT JOIN [dbo].[UserCreditCard] ucc ON po.[CreditCardId] = ucc.[Id]
    LEFT JOIN [dbo].[UserBankAccount] uba ON po.[BankAccountId] = uba.[Id]
    LEFT JOIN [dbo].[UserBillingAddress] uba_addr ON po.[BillingAddressId] = uba_addr.[Id]
    LEFT JOIN [dbo].[STATUS] os ON po.[OrderStatusId] = os.[Id]
    LEFT JOIN [dbo].[STATUS] ps ON po.[PaymentStatusId] = ps.[Id]
    LEFT JOIN [dbo].[STATUS] ds ON po.[DepositStatusId] = ds.[Id]
    WHERE po.[UserId] = @userId
    ORDER BY po.ModifiedOn DESC
END