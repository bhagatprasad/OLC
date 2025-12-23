

CREATE TABLE [dbo].[EmailRuleType]
(
[Id]					BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[RuleCode]				NVARCHAR(200)		 NOT NULL,
[RuleName]				NVARCHAR (100)		 NULL,
[Description]	        NVARCHAR (510)	     NULL,
[CreatedBy]             BIGINT               NULL,
[CreatedOn]             DATETIMEOFFSET       NULL DEFAULT GETDATE(),
[ModifiedBy]            BIGINT               NULL,
[ModifiedOn]            DATETIMEOFFSET       NULL DEFAULT GETDATE(),
[IsActive]              BIT                  NULL DEFAULT 1
)