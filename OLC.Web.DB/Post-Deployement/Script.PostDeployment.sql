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

-- Include seed data for Specialization
:r .\SeedRole.sql

-- Include seed data for SeedAccountType
:r .\SeedAccountType.sql

-- Include seed data for SeedAddressType
:r .\SeedAddressType.sql

-- Include seed data for SeedPaymentReason
:r .\SeedPaymentReason.sql

-- Include seed data for SeedCardType
:r .\SeedCardType.sql

-- Include seed data for SeedStatus
:r .\SeedStatus.sql

-- Include seed data for SeedTransactionType
:r .\SeedTransactionType.sql

-- Include seed data for SeedCountry
:r .\SeedCountry.sql

-- Include seed data for SeedStates
:r .\SeedStates.sql

-- Include seed data for SeedCities
:r .\SeedCities.sql

-- Include seed data for SeedBanks
:r .\SeedBanks.sql

-- Include seed data for SeedUsers
:r .\SeedUsers.sql

-- Include seed data for SeedUserBankAccount
:r .\SeedUserBankAccount.sql

-- Include seed data for SeedUserCreditCards
:r .\SeedUserCreditCards.sql

-- Include seed data for SeedPaymentOrders
:r .\SeedPaymentOrders.sql

-- Include seed data for SeedUsers
:r .\SeedUsers.sql

-- Include seed data for SeedUsers
:r .\SeedRewardConfiguration.sql

-- Include seed data for SeedCryptocurrency
:r .\SeedCryptocurrency.sql