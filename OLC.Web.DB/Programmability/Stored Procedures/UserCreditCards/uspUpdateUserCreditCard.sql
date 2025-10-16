CREATE PROCEDURE [dbo].[uspUpdateUserCreditCard]
(
@id                             bigint,
@userId							bigint,
@cardholderName			     	NVARCHAR(100),
@encryptedCardNumber		    VARBINARY(MAX),
@maskedCardNumber			    VARCHAR(19),
@lastFourDigits					CHAR(4),
@expiryMonth					TINYINT ,
@expiryYear						SMALLINT ,
@encryptedCVV					VARBINARY (MAX),
@cardType						NVARCHAR (50),
@issuingBank					NVARCHAR(100),
@billingAddress					NVARCHAR(250)

)

AS
BEGIN 
	UPDATE [dbo].[UserCreditCard]
	SET
		 UserId							=@userId
		,CardHolderName					=@cardholderName
		,EncryptedCardNumber			=@encryptedCardNumber
		,MaskedCardNumber				=@maskedCardNumber
		,LastFourDigits					=@lastFourDigits
		,ExpiryMonth					=@expiryMonth
		,ExpiryYear						=@expiryYear
		,EncryptedCVV					=@encryptedCVV
		,CardType						=@cardType
		,IssuingBank					=@issuingBank
		,BillingAddress					=@billingAddress
		,ModifiedBy						=@userId

    WHERE Id						=@id
END



		

		
