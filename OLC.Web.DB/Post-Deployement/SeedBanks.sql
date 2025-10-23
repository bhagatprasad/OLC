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

MERGE INTO [dbo].[Bank] AS target
USING (
    VALUES 
        ('State Bank of India', 'SBI'),
        ('HDFC Bank', 'HDFC'),
        ('ICICI Bank', 'ICICI'),
        ('Axis Bank', 'AXIS'),
        ('Kotak Mahindra Bank', 'KOTAK'),
        ('IndusInd Bank', 'INDUSIND'),
        ('Yes Bank', 'YES'),
        ('Bandhan Bank', 'BANDHAN'),
        ('IDFC First Bank', 'IDFC'),
        ('Federal Bank', 'FEDERAL'),
        ('South Indian Bank', 'SIB'),
        ('RBL Bank', 'RBL'),
        ('City Union Bank', 'CUB'),
        ('Karur Vysya Bank', 'KVB'),
        ('Tamilnad Mercantile Bank', 'TMB'),
        ('Lakshmi Vilas Bank', 'LVB'),
        ('Dhanlaxmi Bank', 'DHANLAXMI'),
        ('Jammu & Kashmir Bank', 'JKB'),
        ('Karnataka Bank', 'KARNATAKA'),
        ('Bank of Baroda', 'BOB'),
        ('Punjab National Bank', 'PNB'),
        ('Canara Bank', 'CANARA'),
        ('Union Bank of India', 'UBI'),
        ('Indian Bank', 'INDIAN'),
        ('Central Bank of India', 'CBI'),
        ('Bank of India', 'BOI'),
        ('Indian Overseas Bank', 'IOB'),
        ('UCO Bank', 'UCO'),
        ('Punjab & Sind Bank', 'PSB'),
        ('Bank of Maharashtra', 'BOM')
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