CREATE PROCEDURE [dbo].[uspGetAllCampaignTypes]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Code,
        Description,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    FROM [dbo].[CampaignType]
END;