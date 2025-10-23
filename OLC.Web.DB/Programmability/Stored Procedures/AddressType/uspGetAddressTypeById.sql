CREATE PROCEDURE [dbo].[uspGetAddressTypeById]
(
		@addressTypeId		bigint
)
WITH RECOMPILE

AS

BEGIN

  SELECT
		   [Id]
		  ,[Name]
		  ,[Code]
		  ,[CreatedBy]
		  ,[CreatedOn]
		  ,[ModifiedBy]
		  ,[ModifiedOn]
		  ,[IsActive]
  FROM [dbo].[AddressType]
  WHERE 
		   Id  = @addressTypeId
END