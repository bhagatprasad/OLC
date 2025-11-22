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
MERGE INTO [dbo].[RewardConfiguration] AS target
USING (
    VALUES 
        ('Cashback 5%', 'percentage', 0.05, 50.00, 25.00, 1, '2023-01-01T00:00:00Z', '2024-12-31T23:59:59Z', 1, 1),
        ('Fixed $10 Reward', 'fixed', 10.00, 100.00, NULL, 1, '2023-06-01T00:00:00Z', NULL, 1, 1),
        ('Tiered Bonus', 'tiered', 0.10, 200.00, 50.00, 1, '2023-01-01T00:00:00Z', '2023-12-31T23:59:59Z', 1, 1),
        ('Inactive Reward', 'percentage', 0.02, 0.00, 10.00, 0, '2022-01-01T00:00:00Z', '2022-12-31T23:59:59Z', 1, 1),
        ('High-Value Fixed', 'fixed', 50.00, 500.00, NULL, 1, '2023-07-01T00:00:00Z', NULL, 2, 2),
        ('Loyalty 2%', 'percentage', 0.02, 20.00, 5.00, 1, '2023-03-01T00:00:00Z', NULL, 1, 1),
        ('Tiered Discount', 'tiered', 0.15, 300.00, 75.00, 1, '2023-04-01T00:00:00Z', '2024-03-31T23:59:59Z', 2, 2),
        ('Fixed $5 Promo', 'fixed', 5.00, 25.00, NULL, 1, '2023-08-01T00:00:00Z', '2023-11-30T23:59:59Z', 1, 1),
        ('Percentage Boost', 'percentage', 0.08, 150.00, 40.00, 1, '2023-05-01T00:00:00Z', NULL, 3, 3),
        ('Expired Fixed', 'fixed', 20.00, 200.00, NULL, 0, '2022-01-01T00:00:00Z', '2022-12-31T23:59:59Z', 1, 1),
        ('Tiered Elite', 'tiered', 0.20, 1000.00, 200.00, 1, '2023-09-01T00:00:00Z', NULL, 2, 2),
        ('Cashback 10%', 'percentage', 0.10, 100.00, 50.00, 1, '2023-02-01T00:00:00Z', '2024-01-31T23:59:59Z', 1, 1),
        ('Fixed $15', 'fixed', 15.00, 75.00, NULL, 1, '2023-10-01T00:00:00Z', NULL, 3, 3),
        ('Inactive Tiered', 'tiered', 0.05, 50.00, 10.00, 0, '2023-01-01T00:00:00Z', '2023-06-30T23:59:59Z', 1, 1),
        ('Percentage Deal', 'percentage', 0.03, 10.00, 3.00, 1, '2023-11-01T00:00:00Z', NULL, 2, 2),
        ('High Fixed', 'fixed', 100.00, 1000.00, NULL, 1, '2023-12-01T00:00:00Z', '2024-11-30T23:59:59Z', 1, 1),
        ('Tiered Reward', 'tiered', 0.12, 250.00, 60.00, 1, '2023-07-01T00:00:00Z', NULL, 3, 3),
        ('Fixed $1', 'fixed', 1.00, 5.00, NULL, 1, '2023-01-01T00:00:00Z', '2023-12-31T23:59:59Z', 1, 1),
        ('Percentage Low', 'percentage', 0.01, 1.00, 0.50, 1, '2023-08-01T00:00:00Z', NULL, 2, 2),
        ('Expired Percentage', 'percentage', 0.07, 80.00, 20.00, 0, '2022-05-01T00:00:00Z', '2022-10-31T23:59:59Z', 1, 1),
        ('Tiered Max', 'tiered', 0.25, 500.00, 125.00, 1, '2023-09-01T00:00:00Z', '2024-08-31T23:59:59Z', 3, 3),
        ('Fixed $25', 'fixed', 25.00, 125.00, NULL, 1, '2023-10-01T00:00:00Z', NULL, 1, 1),
        ('Percentage High', 'percentage', 0.20, 400.00, 100.00, 1, '2023-11-01T00:00:00Z', '2024-10-31T23:59:59Z', 2, 2),
        ('Inactive Fixed', 'fixed', 30.00, 150.00, NULL, 0, '2023-01-01T00:00:00Z', '2023-05-31T23:59:59Z', 1, 1),
        ('Tiered Basic', 'tiered', 0.08, 100.00, 20.00, 1, '2023-12-01T00:00:00Z', NULL, 3, 3)
) AS source ([RewardName], [RewardType], [RewardValue], [MinimumTransactionAmount], [MaximumReward], [IsActive], [ValidFrom], [ValidTo], [CreatedBy], [ModifiedBy])
ON target.[RewardName] = source.[RewardName]
WHEN NOT MATCHED THEN
    INSERT ([RewardName], [RewardType], [RewardValue], [MinimumTransactionAmount], [MaximumReward], [IsActive], [ValidFrom], [ValidTo], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn])
    VALUES (source.[RewardName], source.[RewardType], source.[RewardValue], source.[MinimumTransactionAmount], source.[MaximumReward], source.[IsActive], source.[ValidFrom], source.[ValidTo], source.[CreatedBy], SYSDATETIMEOFFSET(), source.[ModifiedBy], SYSDATETIMEOFFSET())
WHEN MATCHED THEN
    UPDATE SET 
        target.[RewardType] = source.[RewardType],
        target.[RewardValue] = source.[RewardValue],
        target.[MinimumTransactionAmount] = source.[MinimumTransactionAmount],
        target.[MaximumReward] = source.[MaximumReward],
        target.[IsActive] = source.[IsActive],
        target.[ValidFrom] = source.[ValidFrom],
        target.[ValidTo] = source.[ValidTo],
        target.[ModifiedBy] = source.[ModifiedBy],
        target.[ModifiedOn] = SYSDATETIMEOFFSET();