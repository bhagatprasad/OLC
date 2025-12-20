CREATE PROCEDURE [dbo].[uspGetEmailCampaignById]
(
    @Id BIGINT
)
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
    WHERE Id = @Id;
END
