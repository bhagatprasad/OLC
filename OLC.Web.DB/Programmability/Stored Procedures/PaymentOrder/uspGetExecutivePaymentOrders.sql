﻿CREATE PROCEDURE [dbo].[uspGetExecutivePaymentOrders]
AS
BEGIN
    SELECT
        po.[Id] AS OrderId,
        po.[OrderReference],
        po.[Amount],
        u.[Email] AS UserEmail,
        u.[Phone] AS UserPhone,
        po.[UserId],
        po.[PaymentReasonId],
        pr.[Name] AS PaymentReasonName,
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
    LEFT JOIN [dbo].[User] u ON po.[UserId] = u.[Id]  -- Assuming User table has Id, Email, Phone columns
    LEFT JOIN [dbo].[PaymentReason] pr ON po.[PaymentReasonId] = pr.[Id]
    LEFT JOIN [dbo].[TransactionFee] tf ON po.[TransactionFeeId] = tf.[Id]
    LEFT JOIN [dbo].[UserCreditCard] ucc ON po.[CreditCardId] = ucc.[Id]
    LEFT JOIN [dbo].[UserBankAccount] uba ON po.[BankAccountId] = uba.[Id]
    LEFT JOIN [dbo].[UserBillingAddress] uba_addr ON po.[BillingAddressId] = uba_addr.[Id]
    LEFT JOIN [dbo].[STATUS] os ON po.[OrderStatusId] = os.[Id]
    LEFT JOIN [dbo].[STATUS] ps ON po.[PaymentStatusId] = ps.[Id]
    LEFT JOIN [dbo].[STATUS] ds ON po.[DepositStatusId] = ds.[Id]
    ORDER BY po.ModifiedOn DESC
END