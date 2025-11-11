CREATE TABLE [dbo].[DepositOrder]
(
	[Id]                       BIGINT          NOT NULL    PRIMARY KEY  IDENTITY(1,1),
	[PaymentOrderId]           BIGINT          NOT NULL,
	[OrderReference]           Nvarchar(50)    NOT NULL,
	[DepositAmount]            Decimal(18,6)   NOT NULL,
	[ActualDepositAmount]      Decimal(18,6)   NOT NULL,
	[PendingDepositAmount]     Decimal(18,6)   NOT NULL,
	[StripeDepositIntentId]    Nvarchar(255)   NOT NULL,
	[StripeDepositChargeId]    Nvarchar(255)   NOT NULL,
	[IsPartialPayment]         BIGINT          NULL,
	[CreatedBy]                BIGINT          NULL,
	[CreatedOn]                DateTimeOffSet  NULL,
	[ModifiedBy]               BIGINT          NULL,
	[ModifiedOn]               DateTimeOffSet  NULL,
	[IsActive]                 BIT             NULL,

)
