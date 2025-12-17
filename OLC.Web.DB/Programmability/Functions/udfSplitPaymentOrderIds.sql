CREATE FUNCTION [dbo].[udfSplitPaymentOrderIds]
(
    @paymentOrderIds NVARCHAR(MAX)
)
RETURNS TABLE
AS
RETURN
(
    SELECT DISTINCT
           CAST(LTRIM(RTRIM(value)) AS BIGINT) AS PaymentOrderId
    FROM STRING_SPLIT(@paymentOrderIds, ',')
    WHERE
        value IS NOT NULL
        AND LTRIM(RTRIM(value)) <> ''
);
