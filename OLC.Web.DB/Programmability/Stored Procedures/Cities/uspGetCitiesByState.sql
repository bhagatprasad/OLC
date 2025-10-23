CREATE PROCEDURE [dbo].[uspGetCitiesByState]
(
   @stateId     bigint
)
AS
  BEGIN
       SELECT
			 [Id]
			,[CountryId]
			,[StateId]
			,[Name]
			,[Code]
			,[CreatedBy]
			,[CreatedOn]
			,[ModifiedBy]
			,[ModifiedOn]
			,[IsActive]
        FROM
             [dbo].[City]
	    WHERE     
	         StateId = @stateId
		END