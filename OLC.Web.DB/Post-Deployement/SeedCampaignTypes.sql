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
MERGE INTO [dbo].[CampaignType] AS Target
USING
(
    VALUES
        ('Email Campaign',        'EMAIL', 'Email based marketing campaign'),
        ('SMS Campaign',          'SMS',   'SMS based marketing campaign'),
        ('WhatsApp Campaign',     'WA',    'WhatsApp promotion campaign'),
        ('Push Notification',     'PUSH',  'Mobile push notifications'),
        ('Referral Campaign',     'REF',   'Customer referral campaign'),
        ('Seasonal Campaign',     'SEAS',  'Festival or seasonal offers'),
        ('Discount Campaign',     'DISC',  'Discount based promotions'),
        ('Cashback Campaign',     'CB',    'Cashback offers'),
        ('Loyalty Campaign',      'LOY',   'Loyalty reward programs'),
        ('Retention Campaign',    'RET',   'Customer retention campaign'),
        ('Acquisition Campaign',  'ACQ',   'New customer acquisition'),
        ('Upsell Campaign',       'UP',    'Upselling existing customers'),
        ('Cross Sell Campaign',   'CS',    'Cross selling products'),
        ('Abandoned Cart',        'ABN',   'Abandoned cart reminders'),
        ('Welcome Campaign',      'WEL',   'Welcome message campaign'),
        ('Reactivation Campaign', 'REA',   'Inactive user reactivation'),
        ('Survey Campaign',       'SUR',   'Customer feedback surveys'),
        ('Flash Sale',            'FLASH', 'Limited time offers'),
        ('Influencer Campaign',   'INF',   'Influencer marketing'),
        ('Announcement Campaign', 'ANN',   'General announcements')
) AS Source (Name, Code, Description)
ON Target.Code = Source.Code

WHEN MATCHED THEN
    UPDATE SET
        Target.Name        = Source.Name,
        Target.Description = Source.Description

WHEN NOT MATCHED THEN
    INSERT (Name, Code, Description, IsActive)
    VALUES (Source.Name, Source.Code, Source.Description, 1);
