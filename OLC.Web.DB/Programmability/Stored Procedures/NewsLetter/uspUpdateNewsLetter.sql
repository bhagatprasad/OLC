CREATE PROCEDURE [dbo].[uspUpdateNewsLetter]
(
    @Id               BIGINT,
    @Email            VARCHAR(255),
    @SubscribedOn     DATETIME,
    @UnsubscribedOn   DATETIME,
    @ModifiedBy       BIGINT,
    @IsActive         BIT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[NewsLetter]
    SET 
        Email = @Email,
        SubscribedOn = @SubscribedOn,
        UnsubscribedOn = @UnsubscribedOn,
        ModifiedBy = @ModifiedBy,
        ModifiedOn = GETDATE(),
        IsActive = @IsActive
    WHERE 
        Id = @Id;
END
GO
