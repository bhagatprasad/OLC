CREATE TABLE [dbo].[UserLoginHistory]
(
    [Id]                  BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId]              BIGINT               NULL,        -- NULL if user doesn't exist
    [EmailAttempted]      VARCHAR(250)         NOT NULL,    -- Always capture the attempted email
    [LoginAttemptTime]    DATETIMEOFFSET      NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [IsSuccess]           BIT                  NOT NULL DEFAULT 0,
    [StatusCode]          INT                  NOT NULL,    -- 200=Success, 1000=Invalid user, 1001=Blocked, etc.
    [StatusMessage]       NVARCHAR(200)        NULL,
    [Notes]               NVARCHAR(500)        NULL,
    [Problem]             NVARCHAR(200)        NULL,
    [LoggedInFrom]        NVARCHAR(100)        NULL,        -- Web, Mobile, API
    [IpAddress]           VARCHAR(45)          NULL,
    [UserAgent]           NVARCHAR(500)        NULL,
    [FailedReason]        NVARCHAR(200)        NULL,
    [ConsecutiveFailures] INT                  DEFAULT 0,   -- Track consecutive failures for this attempt
    [TotalFailures15Min]  INT                  DEFAULT 0,   -- Total failures in last 15 mins at time of attempt
    [WasBlocked]          BIT                  DEFAULT 0,   -- Whether this attempt caused blocking
    [CreatedOn]           DATETIMEOFFSET      NOT NULL DEFAULT SYSDATETIMEOFFSET()
);