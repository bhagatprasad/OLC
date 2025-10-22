CREATE PROCEDURE [dbo].[uspInsertCardType]
(
			@name				 NVARCHAR(255),
			@code				 NVARCHAR(255),
			@createdBy			 bigint
			
)
As
BEGIN
	INSERT INTO [dbo].[CardType]
(
			 Name
			,Code
			,CreatedBy
			,CreatedOn
			,ModifiedBy
			,ModifiedOn
			,IsActive
)
VALUES
(
			@name,
			@code,
			@createdBy,
			GETDATE(),
			@createdBy,
			GETDATE(),
			1
)
END

