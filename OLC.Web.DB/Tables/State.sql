CREATE TABLE [dbo].[State]
(
	[Id]                            bigint                 NOT NULL    PRIMARY KEY   identity(1,1),
	[CountryId]                     bigint                 NULL,
	[Name]                          varchar(max)           NULL,
	[Code]                          varchar(max)           NULL,
	[CreatedBy]                     bigint                 NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    bigint                 NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL
)
