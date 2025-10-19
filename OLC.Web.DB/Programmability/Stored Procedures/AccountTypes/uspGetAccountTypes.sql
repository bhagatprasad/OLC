CREATE PROCEDURE [dbo].[uspGetAccountTypes]
(
		@createdBy      bigint
)
AS
BEGIN

		SELECT * FROM [dbo].[AccountType]

WHERE 
		CreatedBy					=@createdBy
END