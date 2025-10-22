CREATE TABLE [dbo].[City]
(
	[Id]                            bigint                 NOT NULL    PRIMARY KEY   identity(1,1),
	[CountryId]                     bigint                 NULL,
	[StateId]                       bigint                 NULL,
	[Name]                          NVARCHAR(255)          NULL,
    [Code]                          NVARCHAR(255)          NULL,
    [CreatedBy]                     BIGINT                 NULL,
    [CreatedOn]                     DATETIMEOFFSET         NULL DEFAULT GETDATE(),
    [ModifiedBy]                    BIGINT                 NULL,
    [ModifiedOn]                    DATETIMEOFFSET         NULL DEFAULT GETDATE(),
    [IsActive]                      BIT                 NULL DEFAULT 1
)
