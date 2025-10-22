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
MERGE INTO [dbo].[City] AS target
USING (
    SELECT c.Id AS CountryId, s.Id AS StateId, ct.Name, ct.Code
    FROM (
        VALUES 
            -- India
            ('India', 'Maharashtra', 'Mumbai', 'MUM'),
            ('India', 'Maharashtra', 'Pune', 'PUN'),
            ('India', 'Maharashtra', 'Nagpur', 'NGP'),
            ('India', 'Karnataka', 'Bangalore', 'BLR'),
            ('India', 'Karnataka', 'Mysore', 'MYS'),
            ('India', 'Karnataka', 'Mangalore', 'MNG'),
            ('India', 'Tamil Nadu', 'Chennai', 'CHE'),
            ('India', 'Tamil Nadu', 'Coimbatore', 'CBE'),
            ('India', 'Tamil Nadu', 'Madurai', 'MDU'),
            -- United States
            ('United States', 'California', 'Los Angeles', 'LAX'),
            ('United States', 'California', 'San Francisco', 'SFO'),
            ('United States', 'California', 'San Diego', 'SAN'),
            ('United States', 'New York', 'New York City', 'NYC'),
            ('United States', 'New York', 'Buffalo', 'BUF'),
            ('United States', 'New York', 'Albany', 'ALB'),
            ('United States', 'Texas', 'Houston', 'HOU'),
            ('United States', 'Texas', 'Dallas', 'DAL'),
            ('United States', 'Texas', 'Austin', 'AUS'),
            -- United Kingdom
            ('United Kingdom', 'England', 'London', 'LON'),
            ('United Kingdom', 'England', 'Manchester', 'MAN'),
            ('United Kingdom', 'England', 'Birmingham', 'BIR'),
            ('United Kingdom', 'Scotland', 'Edinburgh', 'EDI'),
            ('United Kingdom', 'Scotland', 'Glasgow', 'GLA'),
            ('United Kingdom', 'Scotland', 'Aberdeen', 'ABZ'),
            ('United Kingdom', 'Wales', 'Cardiff', 'CWL'),
            ('United Kingdom', 'Wales', 'Swansea', 'SWA'),
            ('United Kingdom', 'Wales', 'Newport', 'NWP'),
            -- Canada
            ('Canada', 'Ontario', 'Toronto', 'TOR'),  -- Note: Ontario wasn't in the previous list, but assuming it's added or selecting from existing
            ('Canada', 'Ontario', 'Ottawa', 'OTT'),
            ('Canada', 'Ontario', 'Hamilton', 'HAM'),
            ('Canada', 'British Columbia', 'Vancouver', 'VAN'),
            ('Canada', 'British Columbia', 'Victoria', 'VIC'),
            ('Canada', 'British Columbia', 'Surrey', 'SUR'),
            ('Canada', 'Alberta', 'Calgary', 'CAL'),
            ('Canada', 'Alberta', 'Edmonton', 'EDM'),
            ('Canada', 'Alberta', 'Red Deer', 'RED'),
            -- Australia
            ('Australia', 'New South Wales', 'Sydney', 'SYD'),
            ('Australia', 'New South Wales', 'Newcastle', 'NTL'),
            ('Australia', 'New South Wales', 'Wollongong', 'WOL'),
            ('Australia', 'Victoria', 'Melbourne', 'MEL'),
            ('Australia', 'Victoria', 'Geelong', 'GEL'),
            ('Australia', 'Victoria', 'Ballarat', 'BAL'),
            ('Australia', 'Queensland', 'Brisbane', 'BNE'),
            ('Australia', 'Queensland', 'Gold Coast', 'OOL'),
            ('Australia', 'Queensland', 'Cairns', 'CNS')
    ) AS ct(CountryName, StateName, Name, Code)
    INNER JOIN [dbo].[Country] c ON c.Name = ct.CountryName
    INNER JOIN [dbo].[State] s ON s.Name = ct.StateName AND s.CountryId = c.Id
) AS source
ON target.Name = source.Name AND target.StateId = source.StateId AND target.CountryId = source.CountryId
WHEN NOT MATCHED THEN
    INSERT ([CountryId], [StateId], [Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.CountryId, source.StateId, source.Name, source.Code, NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Code] = source.Code,
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;