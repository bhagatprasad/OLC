CREATE TABLE [dbo].[UserBankAccount]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[UserId] BIGINT NOT NULL,
	[AccountHolderName] NVARCHAR(150) NOT NULL,
	[BankName] NVARCHAR(100) NOT NULL,
	[BranchName] NVARCHAR(100) NULL,
	[AccountNumber] NVARCHAR(MAX) NOT NULL, -- Encrypted storage
	[LastFourDigits] CHAR(4) NOT NULL, -- For display purposes
	[AccountType] NVARCHAR(50) NOT NULL, -- Savings, Checking, Current, etc.
	[RoutingNumber] NVARCHAR(MAX) NULL, -- Encrypted (for US banks)
	[IFSCCode] NVARCHAR(20) NULL, -- For Indian banks
	[SWIFTCode] NVARCHAR(20) NULL, -- For international transfers
	[Currency] NVARCHAR(10) NULL DEFAULT 'USD',
	[IsPrimary] BIT NOT NULL DEFAULT 0,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[VerifiedOn] DATETIMEOFFSET NULL,
	[VerificationStatus] NVARCHAR(20) NULL,
	[VerificationAttempts] INT NULL DEFAULT 0,
	[LastVerificationAttempt] DATETIMEOFFSET NULL,
	[CreatedBy] BIGINT NULL,
	[CreatedOn] DATETIMEOFFSET NULL DEFAULT SYSDATETIMEOFFSET(),
	[ModifiedBy] BIGINT NULL,
	[ModifiedOn] DATETIMEOFFSET NULL DEFAULT SYSDATETIMEOFFSET()
	
)
