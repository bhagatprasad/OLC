CREATE PROCEDURE [dbo].[uspInsertNewsLetter]
(
    @Email            VARCHAR(255),
    @SubscribedOn     DATETIME,
    @UnSubscribedOn   DATETIME = NULL,   -- ⭐ Make it optional
    @CreatedBy        BIGINT,
    @IsActive         BIT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[NewsLetter]
    (
        Email,
        SubscribedOn,
        UnsubscribedOn,
        CreatedBy,
        CreatedOn,
        IsActive
    )
    VALUES
    (
        @Email,
        @SubscribedOn,
        @UnSubscribedOn,
        @CreatedBy,
        GETDATE(),
        @IsActive
    );
END
GO
