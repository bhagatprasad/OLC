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

MERGE INTO [dbo].[AccountType] AS target
USING (
    VALUES 
        ('Savings Account', 'SAVINGS'),
        ('Current Account', 'CURRENT'),
        ('Checking Account', 'CHECKING'),
        ('Salary Account', 'SALARY'),
        ('Fixed Deposit', 'FIXED_DEPOSIT'),
        ('Recurring Deposit', 'RECURRING_DEPOSIT'),
        ('Basic Savings Bank', 'BSBDA'),
        ('NRI Account', 'NRI'),
        ('Joint Account', 'JOINT'),
        ('Corporate Account', 'CORPORATE'),
        ('Business Account', 'BUSINESS'),
        ('Student Account', 'STUDENT'),
        ('Senior Citizen Account', 'SENIOR_CITIZEN'),
        ('Minor Account', 'MINOR'),
        ('Demat Account', 'DEMAT'),
        ('Loan Account', 'LOAN'),
        ('Credit Card Account', 'CREDIT_CARD'),
        ('Overdraft Account', 'OVERDRAFT'),
        ('Foreign Currency Account', 'FOREIGN_CURRENCY'),
        ('Trust Account', 'TRUST')
) AS source ([Name], [Code])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Code] = source.[Code],
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;