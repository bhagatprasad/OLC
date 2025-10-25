CREATE PROCEDURE [dbo].[uspSaveUserBillingAddress]
(
	@userId							bigint,
	@addessLineOne					varchar(max),
	@addessLineTwo					varchar(max),
	@addessLineThress				varchar(max),
	@location                       varchar(max),
	@countryId						bigint,
	@stateId						bigint,
	@cityId							bigint,
	@pinCode						varchar(max)
)
AS
 BEGIN
   INSERT INTO [dbo].[UserBillingAddress] 
            (UserId
			,AddessLineOne
			,AddessLineTwo
			,AddessLineThress
			,Location
			,CountryId
			,StateId
			,CityId
			,PinCode
			,CreatedBy
			,CreatedOn
			,ModifiedBy
			,ModifiedOn
			,IsActive)
			Values
			(@userId
			,@addessLineOne
			,@addessLineTwo
			,@addessLineThress
			,@location
			,@countryId
			,@stateId
			,@cityId
			,@pinCode
			,@userId
			,GETDATE()
			,@userId
			,GETDATE()
			,1)
END
			