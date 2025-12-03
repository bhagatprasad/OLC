CREATE PROCEDURE [dbo].[uspGetCryptocurrencyById]
(
    @Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Symbol,
        Name,
        Blockchain,
        ContractAddress,
        Decimals,
        IsStablecoin,
        MinDepositAmount,
        MinWithdrawalAmount,
        WithdrawalFee,
        IconUrl,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    FROM [dbo].[Cryptocurrency]
    WHERE Id = @Id;
END