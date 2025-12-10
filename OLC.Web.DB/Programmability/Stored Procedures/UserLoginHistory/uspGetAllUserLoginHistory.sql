CREATE PROCEDURE [dbo].[uspGetAllUserLoginHistory]
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
    ORDER BY 
        LoginAttemptTime DESC;  
END;