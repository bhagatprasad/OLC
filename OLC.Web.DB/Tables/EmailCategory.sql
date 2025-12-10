CREATE TABLE [dbo].[EmailCategory]
(
[Id]					BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Name]					NVARCHAR(200)		 NOT NULL,
[Code]					NVARCHAR (100)		null,
[CreatedBy]             BIGINT              NULL,
[CreatedOn]             DATETIMEOFFSET      NULL DEFAULT GETDATE(),
[ModifiedBy]            BIGINT              NULL,
[ModifiedOn]            DATETIMEOFFSET      NULL DEFAULT GETDATE(),
[IsActive]              BIT                 NULL DEFAULT 1
)
