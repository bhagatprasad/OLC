CREATE PROCEDURE [dbo].[uspGetCampaignTypeById]
(
    @Id BIGINT
)
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
    WHERE Id = @Id;
END;

