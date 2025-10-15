CREATE TABLE [dbo].[UserCreditCard]

(

	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),

	[UserId] BIGINT NOT NULL,

	[CardHolderName] NVARCHAR(100) NOT NULL,

	[EncryptedCardNumber] VARBINARY(MAX) NOT NULL,

	[MaskedCardNumber] VARCHAR(19) NOT NULL, -- Format: XXXX-XXXX-XXXX-1234

	[LastFourDigits] CHAR(4) NOT NULL,

	[ExpiryMonth] TINYINT NOT NULL,

	[ExpiryYear] SMALLINT NOT NULL,

	[EncryptedCVV] VARBINARY(MAX) NOT NULL,

	[CardType] NVARCHAR(50) NULL, -- Visa, MasterCard, American Express, Discover

	[IssuingBank] NVARCHAR(100) NULL,

	[BillingAddress] NVARCHAR(255) NULL,

	[IsDefault] BIT NOT NULL DEFAULT 0,

	[IsActive] BIT NOT NULL DEFAULT 1,

	[CreatedBy] BIGINT NULL,

	[CreatedOn] DATETIMEOFFSET NULL DEFAULT SYSDATETIMEOFFSET(),

	[ModifiedBy] BIGINT NULL,

	[ModifiedOn] DATETIMEOFFSET NULL DEFAULT SYSDATETIMEOFFSET(),

	CONSTRAINT [FK_UserCreditCard_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]),

	CONSTRAINT [CHK_UserCreditCard_ExpiryMonth] CHECK ([ExpiryMonth] BETWEEN 1 AND 12),

	CONSTRAINT [CHK_UserCreditCard_ExpiryYear] CHECK ([ExpiryYear] >= YEAR(GETDATE())),

	
)
 

