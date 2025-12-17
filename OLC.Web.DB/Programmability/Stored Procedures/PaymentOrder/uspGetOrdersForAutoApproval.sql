CREATE PROCEDURE [dbo].[uspGetOrdersForAutoApproval]
(
  @transactionFeeId bigint
)
AS
BEGIN
    SET NOCOUNT ON;

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
        uba_addr.[AddessLineOne] AS BillingAddress,  -- Note: likely typo, should be AddressLineOne?
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
        (
            SELECT SUM([DepositeAmount])
            FROM [dbo].[DepositOrder]
            WHERE [PaymentOrderId] = po.[Id]
        ) AS TotalDepositAmount,
        (
            SELECT MIN([PendingDepositeAmount])
            FROM [dbo].[DepositOrder]
            WHERE [PaymentOrderId] = po.[Id]
        ) AS PendingDepositAmount,
        po.[CreatedBy],
        po.[CreatedOn],
        po.[ModifiedBy],
        po.[ModifiedOn],
        po.[IsActive],

        -- New columns added
        po.[PaymentOrderType],   -- Send, Receive, Withdraw
        po.[WalletId]

    FROM [dbo].[PaymentOrder] po
    LEFT JOIN [dbo].[User] u ON po.[UserId] = u.[Id]
    LEFT JOIN [dbo].[PaymentReason] pr ON po.[PaymentReasonId] = pr.[Id]
    LEFT JOIN [dbo].[TransactionFee] tf ON po.[TransactionFeeId] = tf.[Id]
    LEFT JOIN [dbo].[UserCreditCard] ucc ON po.[CreditCardId] = ucc.[Id]
    LEFT JOIN [dbo].[UserBankAccount] uba ON po.[BankAccountId] = uba.[Id]
    LEFT JOIN [dbo].[UserBillingAddress] uba_addr ON po.[BillingAddressId] = uba_addr.[Id]
    LEFT JOIN [dbo].[Status] os ON po.[OrderStatusId] = os.[Id]          -- Consistent naming
    LEFT JOIN [dbo].[Status] ps ON po.[PaymentStatusId] = ps.[Id]
    LEFT JOIN [dbo].[Status] ds ON po.[DepositStatusId] = ds.[Id]
    WHERE
       po.TransactionFeeId = @transactionFeeId  and po.PaymentStatusId=1 and   po.[IsActive] = 1 -- Only active orders, consistent with your other procs
    ORDER BY po.[ModifiedOn] DESC;
END
GO