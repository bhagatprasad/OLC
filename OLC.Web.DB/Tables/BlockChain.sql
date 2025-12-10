CREATE TABLE [dbo].[BlockChain]
(
	[Id]               BIGINT       NOT NULL    PRIMARY KEY  IDENTITY(1,1),
	[Name]             VARCHAR(MAX)  NULL,
	[Code]             VARCHAR(MAX)  NULL,
	[CreatedBy]        BIGINT    NULL,
	[CreatedOn]        DATETIMEOFFSET    NULL,
	[ModifiedBy]       BIGINT            NULL,
	[ModifiedOn]       DATETIMEOFFSET    NULL,
	[IsActive]         BIT               NULL
)
