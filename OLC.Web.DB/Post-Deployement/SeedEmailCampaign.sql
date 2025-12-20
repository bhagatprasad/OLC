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
MERGE INTO [dbo].[EmailCampaign] AS Target
USING
(
    VALUES
        ('New Year Cashback', 'Cashback', '10% cashback on all payments', '2025-12-25 00:00:00 +05:30', '2026-01-02 23:59:59 +05:30', 101, 101, 1),
        ('Diwali Mega Promo', 'Promo', 'Flat 20% discount during Diwali', '2025-10-20 00:00:00 +05:30', '2025-11-05 23:59:59 +05:30', 101, 101, 1),
        ('UPI Payment Offer', 'PaymentOffer', 'Extra rewards on UPI payments', '2025-08-01 00:00:00 +05:30', '2025-08-15 23:59:59 +05:30', 102, 102, 1),
        ('Christmas Special', 'Promo', 'Christmas exclusive shopping deals', '2025-12-20 00:00:00 +05:30', '2025-12-26 23:59:59 +05:30', 101, 101, 1),
        ('Republic Day Offer', 'PaymentOffer', 'Special offers on Republic Day', '2026-01-24 00:00:00 +05:30', '2026-01-26 23:59:59 +05:30', 103, 103, 1),
        ('Holi Cashback Blast', 'Cashback', '5% cashback during Holi festival', '2026-03-05 00:00:00 +05:30', '2026-03-10 23:59:59 +05:30', 104, 104, 1),
        ('Summer Sale Promo', 'Promo', 'Up to 30% off on summer collection', '2026-04-01 00:00:00 +05:30', '2026-04-15 23:59:59 +05:30', 101, 101, 1),
        ('Monsoon Deals', 'Promo', 'Rainy season special discounts', '2026-07-01 00:00:00 +05:30', '2026-07-10 23:59:59 +05:30', 105, 105, 1),
        ('Independence Day Offer', 'PaymentOffer', 'Freedom Day exclusive deals', '2026-08-13 00:00:00 +05:30', '2026-08-15 23:59:59 +05:30', 102, 102, 1),
        ('Raksha Bandhan Cashback', 'Cashback', 'Cashback for Rakhi gifts', '2026-08-20 00:00:00 +05:30', '2026-08-23 23:59:59 +05:30', 103, 103, 1),
        ('Ganesh Chaturthi Promo', 'Promo', 'Festival shopping discounts', '2026-09-05 00:00:00 +05:30', '2026-09-10 23:59:59 +05:30', 104, 104, 1),
        ('Dussehra Special', 'Promo', 'Celebrate Dussehra with big savings', '2026-10-15 00:00:00 +05:30', '2026-10-20 23:59:59 +05:30', 101, 101, 1),
        ('Black Friday Deal', 'Promo', 'Limited time Black Friday offers', '2026-11-27 00:00:00 +05:30', '2026-11-27 23:59:59 +05:30', 105, 105, 1),
        ('Year End Cashback', 'Cashback', 'End of year cashback rewards', '2026-12-20 00:00:00 +05:30', '2026-12-31 23:59:59 +05:30', 102, 102, 1),
        ('Welcome Offer', 'PaymentOffer', 'Special welcome offer for new users', '2025-01-01 00:00:00 +05:30', '2025-12-31 23:59:59 +05:30', 100, 100, 1)
) AS Source
(
    CampaignName,
    CampaignType,
    Description,
    StartDate,
    EndDate,
    CreatedBy,
    ModifiedBy,
    IsActive
)
ON Target.CampaignName = Source.CampaignName
   AND Target.CampaignType = Source.CampaignType

WHEN MATCHED THEN
    UPDATE SET
        Target.Description = Source.Description,
        Target.StartDate   = Source.StartDate,
        Target.EndDate     = Source.EndDate,
        Target.ModifiedBy  = Source.ModifiedBy,
        Target.ModifiedOn  = GETDATE(),
        Target.IsActive    = Source.IsActive

WHEN NOT MATCHED BY TARGET THEN
    INSERT
    (
        CampaignName,
        CampaignType,
        Description,
        StartDate,
        EndDate,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES
    (
        Source.CampaignName,
        Source.CampaignType,
        Source.Description,
        Source.StartDate,
        Source.EndDate,
        Source.CreatedBy,
        GETDATE(),
        Source.ModifiedBy,
        GETDATE(),
        Source.IsActive
    );
