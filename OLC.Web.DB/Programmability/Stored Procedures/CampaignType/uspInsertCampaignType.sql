CREATE PROCEDURE [dbo].[uspInsertCampaignType]
(
    @Name               VARCHAR(MAX),
    @Code               VARCHAR(10),
    @Description        NVARCHAR(500),
    @CreatedBy          BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[CampaignType]
    (
        [Name],
        [Code],
        [Description],
        [CreatedBy],
        [CreatedOn],
        [ModifiedBy],
        [ModifiedOn],
        [IsActive]

    )
    VALUES
    (
        @Name,
        @Code,
        @Description,
        @CreatedBy,
        GETDATE(),
        @CreatedBy,
        GETDATE(),
        1
    );
END;

