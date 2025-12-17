CREATE  PROCEDURE [dbo].[uspGetPaymentOrderQueue]
AS
BEGIN
    SET NOCOUNT ON;
 
    ;WITH PaymentOrderCTE AS
    (
        SELECT
            po.[Id] AS PaymentOrderId,
            po.[OrderReference],
            po.[Amount],
            u.[Email] AS UserEmail,
            u.[Phone] AS UserPhone,
            po.[UserId],
            po.[PaymentReasonId],
            pr.[Name] AS PaymentReasonName,
            po.[TransactionFeeId],
            tf.[Name] AS TransactionFeeName,
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
 
            -- Aggregates
            (
                SELECT SUM(d.[DepositeAmount])
                FROM [dbo].[DepositOrder] d
                WHERE d.[PaymentOrderId] = po.[Id]
            ) AS TotalDepositAmount,
 
            (
                SELECT MIN(d.[PendingDepositeAmount])
                FROM [dbo].[DepositOrder] d
                WHERE d.[PaymentOrderId] = po.[Id]
            ) AS PendingDepositAmount,
 
            po.[CreatedBy],
            po.[CreatedOn],
            po.[ModifiedBy],
            po.[ModifiedOn],
            po.[IsActive],
 
            -- New columns
            po.[PaymentOrderType],   -- Send / Receive / Withdraw
            po.[WalletId]
 
        FROM [dbo].[PaymentOrder] po
        LEFT JOIN [dbo].[User] u ON po.[UserId] = u.[Id]
        LEFT JOIN [dbo].[PaymentReason] pr ON po.[PaymentReasonId] = pr.[Id]
        LEFT JOIN [dbo].[TransactionFee] tf ON po.[TransactionFeeId] = tf.[Id]
        LEFT JOIN [dbo].[UserCreditCard] ucc ON po.[CreditCardId] = ucc.[Id]
        LEFT JOIN [dbo].[UserBankAccount] uba ON po.[BankAccountId] = uba.[Id]
        LEFT JOIN [dbo].[UserBillingAddress] uba_addr ON po.[BillingAddressId] = uba_addr.[Id]
        LEFT JOIN [dbo].[Status] os ON po.[OrderStatusId] = os.[Id]
        LEFT JOIN [dbo].[Status] ps ON po.[PaymentStatusId] = ps.[Id]
        LEFT JOIN [dbo].[Status] ds ON po.[DepositStatusId] = ds.[Id]
        WHERE po.[IsActive] = 1
    )
 
    SELECT
        oq.[Id],
        po.[UserId],
        oq.[PaymentOrderId],
        oq.[AssignedExecutiveId],
        oq.[OrderReference] as 'QueueOrderReferance',
        po.[OrderReference],
        po.[Amount],
        po.[UserEmail],
        po.[UserPhone],
        oq.[QueueStatus],
        po.[TransactionFeeId],
        po.[TransactionFeeName],
        po.[PlatformFeeAmount],
        po.[FeeCollectionMethod],
        po.[TotalAmountToChargeCustomer],
        po.[TotalAmountToDepositToCustomer],
        po.[TotalPlatformFee],
        po.[Currency],
        po.[PaymentReasonId],
        po.[PaymentReasonName],
        po.[CreditCardId],
        po.[CreditCardNumber],
        po.[BankAccountId],
        po.[BankAccountNumber],
        po.[BillingAddressId],
        po.[BillingAddress],
        po.[OrderStatusId],
        po.[OrderStatus],
        po.[PaymentStatusId],
        po.[PaymentStatus],
        po.[DepositStatusId],
        po.[DepositStatus],
        po.[StripePaymentIntentId],
        po.[StripePaymentChargeId],
        po.[StripeDepositeIntentId],
        po.[StripeDepositeChargeId],
        oq.[Priority],
        oq.[AssignedOn],
        oq.[ProcessingStartedOn],
        oq.[ProcessingCompletedOn],
        oq.[RetryCount],
        oq.[MaxRetries],
        oq.[FailureReason],
        oq.[Metadata],
        oq.[CreatedBy],
        oq.[CreatedOn],
        oq.[ModifiedBy],
        oq.[ModifiedOn],
        oq.[IsActive]
    FROM [dbo].[OrderQueue] oq
    LEFT JOIN PaymentOrderCTE po
        ON oq.[PaymentOrderId] = po.[PaymentOrderId]
    WHERE oq.[QueueStatus] = 'Pending';
END;
GO
