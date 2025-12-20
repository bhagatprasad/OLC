CREATE PROCEDURE [dbo].[uspUpdateEmailCampaign]
(
    @Id              BIGINT,
    @CampaignName    NVARCHAR(200) = NULL,
    @CampaignType    NVARCHAR(50)  = NULL,
    @Description     NVARCHAR(MAX) = NULL,
    @StartDate       DATETIMEOFFSET = NULL,
    @EndDate         DATETIMEOFFSET = NULL,
    @ModifiedBy      BIGINT,
    @IsActive        BIT = 1
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EmailCampaign]
    SET
        CampaignName = @CampaignName,
        CampaignType = @CampaignType,
        Description  = @Description,
        StartDate    = @StartDate,
        EndDate      = @EndDate,
        ModifiedBy   = @ModifiedBy,
        ModifiedOn   = GETDATE(),
        IsActive     = @IsActive
    WHERE Id = @Id;
END

