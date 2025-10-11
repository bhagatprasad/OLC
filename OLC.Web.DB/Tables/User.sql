CREATE TABLE [dbo].[User]
(
	[Id]				bigint NOT NULL PRIMARY KEY identity(1,1),
	[FirstName]         varchar(250)                    NULL,
	[LastName]          varchar(250)                    NULL,
	[Email]             varchar(250)                    NULL,
	[Phone]             varchar(14)                     NULL,
	[PasswordHash]      nvarchar(max)                   NULL,
	[PasswordSalt]      nvarchar(max)                   NULL,
	[RoleId]            bigint                          NULL,
	[LastPasswordChangedOn] datetimeoffset              NULL,
	[IsBlocked]         bit                             NULL,
	[CreatedBy]			Bigint				NULL,
	[CreatedOn]			datetimeoffset					NULL,
	[ModifiedBy]		Bigint				NULL,
	[ModifiedOn]		datetimeoffset					NULL,
	[IsActive]			bit								NULL,
)
