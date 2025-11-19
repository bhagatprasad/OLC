CREATE TABLE [dbo].[VerifyUserKyc]
(
	[UserKycId]           BIGINT        NOT NULL  PRIMARY KEY  IDENTITY(1,1),
	[UserKycDcumentId]    BIGINT        NOT NULL,
	[Status]              VARCHAR(50)   NULL,
	[Modifiedby]          BIGINT        NULL,
	[Rejected Reason]     VARCHAR(MAX)  NULL
)
