CREATE PROCEDURE [dbo].[uspInsertUserCreditCards]
(
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
      INSERT INTO  [dbo].[UserCreditCard] 
          	 
           	(
				 UserId
				,CardHolderName
				,EncryptedCardNumber
				,MaskedCardNumber
				,LastFourDigits
				,ExpiryMonth
				,ExpiryYear
				,EncryptedCVV
				,CardType
				,IssuingBank
				,IsDefault
				,IsActive
				,CreatedBy
				,CreatedOn
				,ModifiedBy
				,ModifiedOn
			)
	 VALUES
			(
				 @userId
				,@cardholderName
				,@encryptedCardNumber
				,@maskedCardNumber
				,@lastFourDigits
				,@expiryMonth
				,@expiryYear
				,@encryptedCVV
				,@cardType
				,@issuingBank
				,0
				,1
				,@userId
				,GETDATE()
				,@userId
				,GETDATE()
			)
END
