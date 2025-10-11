CREATE PROCEDURE [dbo].[uspDeleteStates]
	(
	
    @stateId BIGINT
)

WITH RECOMPILE

AS

BEGIN

    DELETE FROM [dbo].[State] WHERE Id = @stateId;

END