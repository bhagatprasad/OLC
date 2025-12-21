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

MERGE INTO [dbo].[TransactionFee] AS target
USING (
    VALUES 
        ('Instant Payment Process', 'User will receive in 5mins', 4.00000000000),
        ('4Hrs Payment Process',   'User will receive in 4Hrs',   3.50000000000),
        ('Next Banking Day',        'Mostly Tomorrow by 9AM',     2.50000000000),
        ('Rewards withdraw',        'Mostly Tomorrow by 9AM',     0.00000000000)
) AS source ([Name], [Code], [Price])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT (
        [Name],
        [Code],
        [Price],
        [CreatedBy],
        [CreatedOn],
        [ModifiedOn],
        [IsActive]
    )
    VALUES (
        source.[Name],
        source.[Code],
        source.[Price],
        -1,
        SYSDATETIMEOFFSET(),
        -1,
        1
    )
WHEN MATCHED THEN
    UPDATE SET
        target.[Code] = source.[Code],
        target.[Price] = source.[Price],
        target.[ModifiedBy] = -1,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;
