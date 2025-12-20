CREATE PROCEDURE [dbo].[uspGetAllEmailCampaigns]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
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
    FROM [dbo].[EmailCampaign]
    ORDER BY CreatedOn DESC;
END