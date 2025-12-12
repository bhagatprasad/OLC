CREATE PROCEDURE [dbo].[uspUpdateExecutiveAvilability]
(
	@userId bigint,
    @isAvilable bit
)
as 
begin  
update [dbo].[Executives] 
		set 
			IsAvailable= @isAvilable 
			WHERE UserId= @userId
END