CREATE TABLE [dbo].[ServiceRequestReplies]
(
    Id                     bigint IDENTITY(1,1) PRIMARY KEY,
    TicketId               bigint NOT NULL,
    ReplierId              bigint NOT NULL,
    Message                nvarchar(max) NOT NULL,
    Status                 varchar(50) NOT NULL,
    IsInternal             bit NOT NULL DEFAULT 0,
    CreatedBy              bigint NOT NULL,
    CreatedOn              datetime NOT NULL DEFAULT GETDATE(),
    ModifiedBy             bigint NULL,
    ModifiedOn             datetime NULL,
    IsActive               bit NOT NULL DEFAULT 1
);