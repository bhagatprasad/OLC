/* 
Post-Deployment Seed Script for WalletType
------------------------------------------
This script ensures the WalletType table always contains a consistent set of
system-defined wallet types. If a record already exists (matched by Code),
it will be updated; otherwise inserted.
*/

MERGE INTO [dbo].[WalletType] AS target
USING (
    VALUES
        ('Reward', 'REWARD', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('Crypto', 'CRYPTO', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('Fiat', 'FIAT', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('Loyalty Points', 'LOYALTY', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('GiftCard', 'GIFTCARD', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('Voucher', 'VOUCHER', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('Miles', 'MILES', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('Stablecoin', 'STABLE', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('Tokenized Asset', 'TOKEN_ASSET', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1),
        ('Prepaid', 'PREPAID', -1, SYSDATETIMEOFFSET(), -1, SYSDATETIMEOFFSET(), 1)
) AS source ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
ON target.[Code] = source.[Code]

WHEN MATCHED THEN
    UPDATE SET
        target.[Name] = source.[Name],
        target.[ModifiedBy] = source.[ModifiedBy],
        target.[ModifiedOn] = source.[ModifiedOn],
        target.[IsActive] = source.[IsActive]

WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], source.[CreatedBy], source.[CreatedOn], source.[ModifiedBy], source.[ModifiedOn], source.[IsActive]);
