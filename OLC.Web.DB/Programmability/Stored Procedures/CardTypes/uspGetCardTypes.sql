CREATE PROCEDURE [dbo].[uspGetCardTypes]
(
		@createdBy      bigint
)
AS
BEGIN

		SELECT * FROM [dbo].[CardType]

WHERE 
		CreatedBy					=@createdBy
END