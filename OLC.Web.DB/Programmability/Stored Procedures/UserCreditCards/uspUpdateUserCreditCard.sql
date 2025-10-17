CREATE PROCEDURE [dbo].[uspUpdateUserCreditCard]
(
	@id                             bigint,
	@userId							bigint,
	@cardholderName			     	NVARCHAR(MAX),
	@encryptedCardNumber		    NVARCHAR(MAX),
	@maskedCardNumber			    NVARCHAR(MAX),
	@lastFourDigits					NVARCHAR(MAX),
	@expiryMonth					VARCHAR(25),
	@expiryYear						VARCHAR(25) ,
	@encryptedCVV					VARCHAR(25),
	@cardType						VARCHAR(25),
	@issuingBank					VARCHAR(100)
	
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
		,ModifiedBy						=@userId
		,ModifiedOn                     =GETDATE()
    WHERE 
		Id								=@id
END



		

		
