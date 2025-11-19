CREATE TABLE [dbo].[Priority]
(
    [Id]            BIGINT IDENTITY(1,1)    NOT NULL PRIMARY KEY,
    [Name]          NVARCHAR(200)           NOT NULL,
    [Code]          NVARCHAR(100)           NOT NULL,
    [CreatedBy]     BIGINT                  NULL,
    [CreatedOn]     DATETIMEOFFSET(7)       NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]    BIGINT                  NULL,
    [ModifiedOn]    DATETIMEOFFSET(7)       NULL,
    [IsActive]      BIT                     NOT NULL DEFAULT 1
);