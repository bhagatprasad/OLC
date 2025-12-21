CREATE PROCEDURE [dbo].[uspUpdateCampaignType]
(
    @Id           BIGINT,
    @Name         VARCHAR(MAX),
    @Code         VARCHAR(10),
    @Description  NVARCHAR(500),
    @ModifiedBy   BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[CampaignType]
    SET
        [Name]        = @Name,
        [Code]        = @Code,
        [Description] = @Description,
        [ModifiedBy]  = @ModifiedBy,
        [ModifiedOn]  =GETDATE()

    WHERE Id = @Id;
END;

