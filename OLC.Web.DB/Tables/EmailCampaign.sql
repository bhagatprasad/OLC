CREATE  TABLE [dbo].[EmailCampaign]
(
	[Id]                    BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
    [CampaignName]          NVARCHAR(200)       NULL,
    [CampaignType]          NVARCHAR(50)        NULL, -- PaymentOffer, Cashback, Promo
    [Description]           NVARCHAR(MAX)       NULL,
    [StartDate]             DATETIMEOFFSET      NULL,
    [EndDate]               DATETIMEOFFSET      NULL,
    [CreatedBy]             BIGINT              NULL,
    [CreatedOn]             DATETIMEOFFSET      NULL DEFAULT GETDATE(),
	[ModifiedBy]            BIGINT              NULL,
    [ModifiedOn]            DATETIMEOFFSET      NULL DEFAULT GETDATE(),
    [IsActive]              BIT                 NULL DEFAULT 1
)
