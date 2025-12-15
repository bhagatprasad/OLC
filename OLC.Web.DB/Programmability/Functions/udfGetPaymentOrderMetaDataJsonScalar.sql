CREATE FUNCTION dbo.udfGetPaymentOrderMetaDataJsonScalar
(
    @PaymentOrderId BIGINT
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @Result NVARCHAR(MAX);
    
    SELECT @Result = (
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
            FROM dbo.PaymentOrder
            WHERE Id = @PaymentOrderId
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );
    RETURN @Result;
END;
GO