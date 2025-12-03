CREATE PROCEDURE [dbo].[uspGetAllCryptocurrencies]

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
  
    ORDER BY Name ASC;
END