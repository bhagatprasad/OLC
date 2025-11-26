CREATE PROCEDURE [dbo].[uspUpdateUserBillingAddress]
 (
	 @id					bigint,
	 @userId				bigint,
	 @addessLineOne         varchar(max),
	 @addessLineTwo         varchar(max),
	 @addessLineThress      varchar(max),
	 @location				varchar(max),
	 @countryId				bigint,
	 @stateId				bigint,
	 @cityId				bigint,
	 @pinCode				varchar(max)
 )

 AS

 BEGIN

 update [dbo].[UserBillingAddress] 
		set
		     UserId            = @userId
			,AddessLineOne     = @addessLineOne
			,AddessLineTwo     = @addessLineTwo
			,AddessLineThress  = @addessLineThress
			,Location          = @location
			,CountryId         = @countryId
			,StateId           = @stateId
			,CityId            = @cityId
			,PinCode           = @pinCode
		    ,ModifiedBy		   = @userId
		    ,ModifiedOn		   = GetDate()
		 Where Id = @id
  END

