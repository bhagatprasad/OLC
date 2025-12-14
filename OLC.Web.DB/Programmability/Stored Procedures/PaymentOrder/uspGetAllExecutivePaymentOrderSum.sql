CREATE PROCEDURE [dbo].[uspGetAllExecutivePaymentOrderSum]
(
    @FromDate          DATETIMEOFFSET = NULL,
    @ToDate            DATETIMEOFFSET = NULL,
    @OrderStatusId     BIGINT = NULL,
    @PaymentStatusId   BIGINT = NULL,
    @DepositStatusId   BIGINT = NULL,
    @PaymentOrderType  VARCHAR(MAX) = NULL,           -- Send, Receive, Withdraw
    @WalletId          VARCHAR(MAX) = NULL            -- Wallet ID
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SUM(po.Amount) AS TotalAmount,
        SUM(po.TotalAmountToDepositToCustomer) AS DepositedAmount,
        SUM(po.PlatformFeeAmount) AS PlatformFee,
        SUM(CASE WHEN po.OrderStatusId = 8 THEN po.Amount ELSE 0 END) AS CancelledAmount,
        SUM(CASE WHEN po.OrderStatusId = 14 THEN po.Amount ELSE 0 END) AS FailedAmount,
        SUM(CASE WHEN po.OrderStatusId = 15 THEN po.Amount ELSE 0 END) AS SuccessAmount
    FROM
        [dbo].[PaymentOrder] po
    WHERE
        po.IsActive = 1
        AND (@FromDate IS NULL OR po.ModifiedOn >= @FromDate)
        AND (@ToDate IS NULL OR po.ModifiedOn <= @ToDate)
        AND (@OrderStatusId IS NULL OR po.OrderStatusId = @OrderStatusId)
        AND (@PaymentStatusId IS NULL OR po.PaymentStatusId = @PaymentStatusId)
        AND (@DepositStatusId IS NULL OR po.DepositStatusId = @DepositStatusId)
        AND (@PaymentOrderType IS NULL OR po.PaymentOrderType = @PaymentOrderType)
        AND (@WalletId IS NULL OR po.WalletId = @WalletId);
END
GO
