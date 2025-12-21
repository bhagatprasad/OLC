CREATE TABLE [dbo].[EmailRuleType]
(
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    RuleCode        NVARCHAR(50) NOT NULL UNIQUE,  
    RuleName        NVARCHAR(100) NOT NULL,         
    Description     NVARCHAR(255) NULL,
    IsActive        BIT NOT NULL DEFAULT 1,

    CreatedOn       DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy       NVARCHAR(50) NULL,
    ModifiedOn      DATETIME NULL,
    ModifiedBy      NVARCHAR(50) NULL
);
