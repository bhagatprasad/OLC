CREATE PROCEDURE [dbo].[uspGetByUserIdUserBellingAddress]
(
 @userId  bigint
)
WITH RECOMPILE

AS 

BEGIN

SELECT

      [Id]
     ,AddessLineOne
	 ,AddessLineTwo
	 ,AddessLineThress
	 ,Location
	 ,CountryId
	 ,StateId
	 ,CityId
	 ,PinCode
     ,[CreatedBy]
     ,[CreatedOn]
     ,[ModifiedBy]
     ,[ModifiedOn]
     ,[IsActive]
     
    FROM [dbo].[UserBillingAddress]

    WHERE Id=@userId

    END