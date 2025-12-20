CREATE PROCEDURE [dbo].[uspInsertEmailCampaign]
(
    @CampaignName     NVARCHAR(200),
    @CampaignType     NVARCHAR(50),
    @Description      NVARCHAR(MAX) = NULL,
    @StartDate        DATETIMEOFFSET = NULL,
    @EndDate          DATETIMEOFFSET = NULL,
    @CreatedBy        BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[EmailCampaign]
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
        @CampaignName,
        @CampaignType,
        @Description,
        @StartDate,
        @EndDate,
        @CreatedBy,
        GETDATE(),
        @CreatedBy,
        GETDATE(),
        1
    );
END

