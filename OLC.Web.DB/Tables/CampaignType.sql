create TABLE [dbo].[CampaignType]
(
	[Id]                            bigint                 NOT NULL    PRIMARY KEY   identity(1,1),
	[Name]                          varchar(max)           NULL,
	[Code]                          varchar(10)            NULL,
	[Description]					nvarchar(500)		   NULL,
	[CreatedBy]                     bigint                 NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    bigint                 NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL
)