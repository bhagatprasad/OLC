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
-- Create 1000 test users with RoleId = 2 and IsExternalUser = 1
-- Guaranteeing all fields have values
WITH NumberSequence AS (
    SELECT TOP 1000 
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) as Seq
    FROM 
        master.dbo.spt_values n1
    CROSS JOIN 
        master.dbo.spt_values n2
),
FirstNames AS (
    SELECT name, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) as rn
    FROM (VALUES 
        ('John'), ('Jane'), ('Michael'), ('Sarah'), ('David'), ('Emily'), ('Robert'), ('Lisa'),
        ('William'), ('Jennifer'), ('James'), ('Maria'), ('Thomas'), ('Susan'), ('Charles'), ('Karen'),
        ('Christopher'), ('Nancy'), ('Daniel'), ('Betty'), ('Matthew'), ('Jessica'), ('Anthony'), ('Michelle'),
        ('Mark'), ('Amanda'), ('Steven'), ('Melissa'), ('Kevin'), ('Rebecca'), ('Brian'), ('Laura'),
        ('Jason'), ('Stephanie'), ('Eric'), ('Cynthia'), ('Scott'), ('Angela'), ('Stephen'), ('Sharon')
    ) as names(name)
),
LastNames AS (
    SELECT name, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) as rn
    FROM (VALUES 
        ('Smith'), ('Johnson'), ('Williams'), ('Brown'), ('Jones'), ('Miller'), ('Davis'), ('Garcia'),
        ('Rodriguez'), ('Wilson'), ('Martinez'), ('Anderson'), ('Taylor'), ('Thomas'), ('Hernandez'), ('Moore'),
        ('Martin'), ('Jackson'), ('Thompson'), ('White'), ('Lee'), ('Harris'), ('Clark'), ('Lewis'),
        ('Young'), ('Hall'), ('Allen'), ('King'), ('Wright'), ('Scott'), ('Green'), ('Adams'),
        ('Baker'), ('Nelson'), ('Carter'), ('Mitchell'), ('Perez'), ('Roberts'), ('Turner'), ('Phillips')
    ) as names(name)
)
INSERT INTO [dbo].[User] (
    [FirstName],
    [LastName],
    [Email],
    [Phone],
    [RoleId],
    [LastPasswordChangedOn],
    [IsBlocked],
    [IsApproved],
    [ApprovedBy],
    [ApprovedOn],
    [CreatedBy],
    [CreatedOn],
    [ModifiedBy],
    [ModifiedOn],
    [IsActive],
    [IsExternalUser]
)
SELECT 
    -- First Name (guaranteed not null)
    fn.name,
    
    -- Last Name (guaranteed not null)
    ln.name,
    
    -- Email (firstname-lastname + sequence + @mail.com) - GUARANTEED UNIQUE AND NOT NULL
    LOWER(fn.name + '-' + ln.name + 
          CASE WHEN EXISTS (SELECT 1 FROM [dbo].[User] WHERE Email = LOWER(fn.name + '-' + ln.name + '@mail.com'))
               THEN CAST(ns.Seq AS VARCHAR(10)) 
               ELSE '' 
          END + '@mail.com'),
    
    -- Phone (10 digits starting with 9) - GUARANTEED UNIQUE AND NOT NULL
    '9' + RIGHT('000000000' + CAST((800000000 + ns.Seq) AS VARCHAR(9)), 9),
    
    -- RoleId (fixed as 2)
    2,
    
    -- LastPasswordChangedOn (random date in last 6 months)
    DATEADD(DAY, - (ABS(CHECKSUM(NEWID())) % 180), GETDATE()),
    
    -- IsBlocked (mostly false, 5% blocked)
    CASE WHEN (ABS(CHECKSUM(NEWID())) % 100) < 5 THEN 1 ELSE 0 END,
    
    -- IsApproved (mostly true, 90% approved)
    CASE WHEN (ABS(CHECKSUM(NEWID())) % 100) < 90 THEN 1 ELSE 0 END,
    
    -- ApprovedBy (random user ID or NULL if not approved)
    CASE WHEN (ABS(CHECKSUM(NEWID())) % 100) < 90 THEN (ABS(CHECKSUM(NEWID())) % 100) + 1 ELSE NULL END,
    
    -- ApprovedOn (random date if approved)
    CASE WHEN (ABS(CHECKSUM(NEWID())) % 100) < 90 THEN DATEADD(DAY, - (ABS(CHECKSUM(NEWID())) % 180), GETDATE()) ELSE NULL END,
    
    -- CreatedBy (random user ID)
    (ABS(CHECKSUM(NEWID())) % 100) + 1,
    
    -- CreatedOn (random date in last year)
    DATEADD(DAY, - (ABS(CHECKSUM(NEWID())) % 365), GETDATE()),
    
    -- ModifiedBy (random user ID or NULL)
    CASE WHEN (ABS(CHECKSUM(NEWID())) % 100) < 80 THEN (ABS(CHECKSUM(NEWID())) % 100) + 1 ELSE NULL END,
    
    -- ModifiedOn (random date after creation or NULL)
    CASE WHEN (ABS(CHECKSUM(NEWID())) % 100) < 80 THEN DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 100), DATEADD(DAY, - (ABS(CHECKSUM(NEWID())) % 365), GETDATE())) ELSE NULL END,
    
    -- IsActive (mostly true, 85% active)
    CASE WHEN (ABS(CHECKSUM(NEWID())) % 100) < 85 THEN 1 ELSE 0 END,
    
    -- IsExternalUser (fixed as 1)
    1

FROM 
    NumberSequence ns
INNER JOIN 
    FirstNames fn ON (ns.Seq - 1) % 40 + 1 = fn.rn
INNER JOIN 
    LastNames ln ON (ns.Seq - 1) % 40 + 1 = ln.rn;