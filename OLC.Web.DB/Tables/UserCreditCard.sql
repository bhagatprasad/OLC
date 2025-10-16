CREATE TABLE [dbo].[UserCreditCard]

(

	[Id]									BIGINT					NOT NULL PRIMARY KEY IDENTITY(1,1),

	[UserId]							    BIGINT					NOT NULL,

	[CardHolderName]						NVARCHAR(MAX)			    NULL,

	[EncryptedCardNumber]					NVARCHAR(MAX)               NULL,

	[MaskedCardNumber]						NVARCHAR(MAX)				NULL, -- Format: XXXX-XXXX-XXXX-1234

	[LastFourDigits]						NVARCHAR(MAX)				NULL,

	[ExpiryMonth]							VARCHAR(25)					NULL,

	[ExpiryYear]							VARCHAR(25)					NULL,

	[EncryptedCVV]							VARCHAR(25)					NULL,

	[CardType]								VARCHAR(25)					NULL,

	[IssuingBank]							VARCHAR(100)			    NULL,

	[IsDefault]								BIT							NULL,
	
	[CreatedBy]								BIGINT						NULL,

	[CreatedOn]								DATETIMEOFFSET				NULL DEFAULT SYSDATETIMEOFFSET(),

	[ModifiedBy]							BIGINT						NULL,

	[ModifiedOn]							DATETIMEOFFSET				NULL DEFAULT SYSDATETIMEOFFSET(),

	[IsActive]								BIT							NULL
)