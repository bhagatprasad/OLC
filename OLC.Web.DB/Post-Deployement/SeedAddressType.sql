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

MERGE INTO [dbo].[AddressType] AS target
USING (
    VALUES 
        ('Home', 'HOME'),
        ('Work', 'WORK'),
        ('Billing', 'BILLING'),
        ('Shipping', 'SHIPPING'),
        ('Office', 'OFFICE'),
        ('Mailing', 'MAILING'),
        ('Physical', 'PHYSICAL'),
        ('Temporary', 'TEMPORARY'),
        ('Permanent', 'PERMANENT'),
        ('Branch Office', 'BRANCH'),
        ('Headquarters', 'HQ'),
        ('Warehouse', 'WAREHOUSE'),
        ('Site', 'SITE'),
        ('Project', 'PROJECT'),
        ('Vacation', 'VACATION')
) AS source ([Name], [Code])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Code] = source.[Code],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;