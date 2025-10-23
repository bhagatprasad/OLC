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

MERGE INTO [dbo].[Country] AS target
USING (
    VALUES 
        ('India', 'IN'),
        ('United States', 'US'),
        ('United Kingdom', 'GB'),
        ('Canada', 'CA'),
        ('Australia', 'AU'),
        ('Germany', 'DE'),
        ('France', 'FR'),
        ('Japan', 'JP'),
        ('Singapore', 'SG'),
        ('United Arab Emirates', 'AE'),
        ('China', 'CN'),
        ('Brazil', 'BR'),
        ('South Africa', 'ZA'),
        ('Malaysia', 'MY'),
        ('Sri Lanka', 'LK'),
        ('Bangladesh', 'BD'),
        ('Nepal', 'NP'),
        ('Pakistan', 'PK'),
        ('Russia', 'RU'),
        ('Italy', 'IT')
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