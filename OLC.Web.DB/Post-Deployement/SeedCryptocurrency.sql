/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [dbo].[Cryptocurrency] AS Target
USING
(
    VALUES
    -- 1
    (1, 'BTC', 'Bitcoin', 'Bitcoin', NULL, 8, 0,
     0.0001, 0.001, 0.0005, 'https://assets.coingecko.com/coins/images/1/large/bitcoin.png', 1, 1, 1),

    -- 2
    (2, 'ETH', 'Ethereum', 'Ethereum', NULL, 18, 0,
     0.001, 0.01, 0.0005, 'https://assets.coingecko.com/coins/images/279/large/ethereum.png', 1, 1, 1),

    -- 3
    (3, 'USDT', 'Tether USD (ERC20)', 'Ethereum',
     '0xdAC17F958D2ee523a2206206994597C13D831ec7', 6, 1,
     10, 20, 1, 'https://assets.coingecko.com/coins/images/325/large/Tether-logo.png', 1, 1, 1),

    -- 4
    (4, 'USDT-TRC20', 'Tether USD (TRC20)', 'Tron',
     'TXYZopYRdj2D9XRtbG411XZZ3kM5VkAeBf', 6, 1,
     10, 20, 1, 'https://assets.coingecko.com/coins/images/325/large/Tether-logo.png', 1, 1, 1),

    -- 5
    (5, 'BNB', 'BNB', 'BinanceSmartChain', NULL, 18, 0,
     0.01, 0.05, 0.002, 'https://assets.coingecko.com/coins/images/825/large/binance-coin-logo.png', 1, 1, 1),

    -- 6
    (6, 'TRX', 'Tron', 'Tron', NULL, 6, 0,
     10, 20, 1, 'https://assets.coingecko.com/coins/images/1094/large/tron-logo.png', 1, 1, 1),

    -- 7
    (7, 'XRP', 'Ripple', 'XRP', NULL, 6, 0,
     5, 10, 0.2, 'https://assets.coingecko.com/coins/images/44/large/xrp-symbol-white-128.png', 1, 1, 1),

    -- 8
    (8, 'ADA', 'Cardano', 'Cardano', NULL, 6, 0,
     5, 10, 0.3, 'https://assets.coingecko.com/coins/images/975/large/cardano.png', 1, 1, 1),

    -- 9
    (9, 'DOGE', 'Dogecoin', 'Dogecoin', NULL, 8, 0,
     10, 20, 1, 'https://assets.coingecko.com/coins/images/5/large/dogecoin.png', 1, 1, 1),

    -- 10
    (10, 'SOL', 'Solana', 'Solana', NULL, 9, 0,
     0.01, 0.05, 0.002, 'https://assets.coingecko.com/coins/images/4128/large/solana.png', 1, 1, 1),

    -- 11
    (11, 'MATIC', 'Polygon', 'Polygon', NULL, 18, 0,
     1, 5, 0.5, 'https://assets.coingecko.com/coins/images/4713/large/matic-token-icon.png', 1, 1, 1),

    -- 12
    (12, 'DOT', 'Polkadot', 'Polkadot', NULL, 10, 0,
     0.1, 1, 0.01, 'https://assets.coingecko.com/coins/images/12171/large/polkadot.png', 1, 1, 1),

    -- 13
    (13, 'LTC', 'Litecoin', 'Litecoin', NULL, 8, 0,
     0.01, 0.1, 0.001, 'https://assets.coingecko.com/coins/images/2/large/litecoin.png', 1, 1, 1),

    -- 14
    (14, 'SHIB', 'Shiba Inu', 'Ethereum',
     NULL, 18, 0,
     50000, 100000, 20000, 'https://assets.coingecko.com/coins/images/11939/large/shiba.png', 1, 1, 1),

    -- 15
    (15, 'DAI', 'DAI Stablecoin', 'Ethereum',
     '0x6B175474E89094C44Da98b954EedeAC495271d0F', 18, 1,
     10, 20, 1, 'https://assets.coingecko.com/coins/images/9956/large/4943.png', 1, 1, 1)

) AS Source
(
    Id, Symbol, Name, Blockchain, ContractAddress, Decimals, IsStablecoin,
    MinDepositAmount, MinWithdrawalAmount, WithdrawalFee, IconUrl,
    CreatedBy, ModifiedBy, IsActive
)
ON Target.Id = Source.Id

WHEN MATCHED THEN
    UPDATE SET
        Target.Symbol = Source.Symbol,
        Target.Name = Source.Name,
        Target.Blockchain = Source.Blockchain,
        Target.ContractAddress = Source.ContractAddress,
        Target.Decimals = Source.Decimals,
        Target.IsStablecoin = Source.IsStablecoin,
        Target.MinDepositAmount = Source.MinDepositAmount,
        Target.MinWithdrawalAmount = Source.MinWithdrawalAmount,
        Target.WithdrawalFee = Source.WithdrawalFee,
        Target.IconUrl = Source.IconUrl,
        Target.ModifiedBy = Source.ModifiedBy,
        Target.ModifiedOn = GETDATE(),
        Target.IsActive = Source.IsActive

WHEN NOT MATCHED THEN
    INSERT
    (
        Id, Symbol, Name, Blockchain, ContractAddress, Decimals, IsStablecoin,
        MinDepositAmount, MinWithdrawalAmount, WithdrawalFee, IconUrl,
        CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive
    )
    VALUES
    (
        Source.Id, Source.Symbol, Source.Name, Source.Blockchain,
        Source.ContractAddress, Source.Decimals, Source.IsStablecoin,
        Source.MinDepositAmount, Source.MinWithdrawalAmount,
        Source.WithdrawalFee, Source.IconUrl,
        Source.CreatedBy, GETDATE(), Source.ModifiedBy, GETDATE(), Source.IsActive
    );


