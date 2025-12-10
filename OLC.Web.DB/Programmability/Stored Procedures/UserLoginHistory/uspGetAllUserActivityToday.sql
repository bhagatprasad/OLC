CREATE PROCEDURE [dbo].[uspGetAllUserActivityToday]
	AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
			Id,
			UserId,
			EmailAttempted,
			LoginAttemptTime,
			IsSuccess,
			StatusCode,
			StatusMessage,
			Notes,
			Problem,
			LoggedInFrom,
			IpAddress,
			UserAgent,
			FailedReason,
			ConsecutiveFailures,
			TotalFailures15Min,
			WasBlocked,
			CreatedOn
    FROM 
         [dbo].[UserLoginHistory]
    WHERE 
        CAST(LoginAttemptTime AS DATE) = CAST(SYSDATETIMEOFFSET() AS DATE)
    ORDER BY 
        LoginAttemptTime DESC;
END;
