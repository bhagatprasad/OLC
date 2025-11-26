CREATE TABLE [dbo].[UserBillingAddress]
(
	[Id]                            bigint                 NOT NULL    PRIMARY KEY   identity(1,1),
	[UserId]                        bigint                 NOT NULL,
	[AddessLineOne]                 varchar(max)           NULL,
	[AddessLineTwo]                 varchar(max)           NULL,
	[AddessLineThress]              varchar(max)           NULL,
	[Location]                      varchar(max)           NULL,
	[CountryId]                     bigint                 NULL,
	[StateId]                       bigint                 NULL,
	[CityId]                        bigint				   NULL,
	[PinCode]						varchar(max)           NULL,
	[CreatedBy]                     bigint                 NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    bigint                 NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL
)
