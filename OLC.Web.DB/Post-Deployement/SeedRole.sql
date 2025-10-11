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

MERGE INTO [dbo].[Role] AS target
USING (
    VALUES 
        ('Administrator', 'ADMIN'),
        ('User', 'USER'),
        ('Executive', 'EXEC'),
        ('Manager', 'MANAGER'),
        ('Supervisor', 'SUPERVISOR'),
        ('Analyst', 'ANALYST'),
        ('Support', 'SUPPORT'),
        ('Viewer', 'VIEWER'),
        ('Editor', 'EDITOR'),
        ('Auditor', 'AUDITOR'),
        ('Developer', 'DEV'),
        ('Finance', 'FINANCE'),
        ('HR', 'HR'),
        ('Sales', 'SALES'),
        ('Marketing', 'MARKETING')
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