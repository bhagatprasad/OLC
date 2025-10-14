CREATE PROCEDURE [dbo].[uspGetPaymentReasons]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id],
        [Name],
        [Description],
        [CreatedBy],
        [CreatedOn],
        [ModifiedBy],
        [ModifiedOn],
        [IsActive]
    FROM [dbo].[PaymentReason]
    ORDER BY [Name] ASC;
END