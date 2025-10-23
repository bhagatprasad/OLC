CREATE PROCEDURE [dbo].[uspGetCardTypeById]
(
		@id			bigint
)
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
  FROM [dbo].[CardType]
	WHERE 
		Id			=@id
END