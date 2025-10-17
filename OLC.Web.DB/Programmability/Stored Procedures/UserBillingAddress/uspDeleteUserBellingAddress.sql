CREATE PROCEDURE [dbo].[uspDeleteUserBellingAddress]
	(@id  bigint)

AS

BEGIN

update [dbo].[UserBillingAddress]
       set  IsActive = 0
       WHERE Id = @id
END
