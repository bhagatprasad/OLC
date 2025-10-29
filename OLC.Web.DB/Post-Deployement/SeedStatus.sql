/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [dbo].[Status] AS target
USING (
    VALUES 
        ('Active', 'ACTIVE'),
        ('Inactive', 'INACTIVE'),
        ('Pending', 'PENDING'),
        ('Approved', 'APPROVED'),
        ('Rejected', 'REJECTED'),
        ('Completed', 'COMPLETED'),
        ('Cancelled', 'CANCELLED'),
        ('Draft', 'DRAFT'),
        ('Submitted', 'SUBMITTED'),
        ('Under Review', 'UNDER_REVIEW'),
        ('Processing', 'PROCESSING'),
        ('Processed', 'PROCESSED'),
        ('Failed', 'FAILED'),
        ('Success', 'SUCCESS'),
        ('Expired', 'EXPIRED'),
        ('Suspended', 'SUSPENDED'),
        ('Archived', 'ARCHIVED'),
        ('Deleted', 'DELETED'),
        ('Locked', 'LOCKED'),
        ('Unlocked', 'UNLOCKED'),
        ('Verified', 'VERIFIED'),
        ('Unverified', 'UNVERIFIED'),
        ('Paid', 'PAID'),
        ('Unpaid', 'UNPAID'),
        ('Open', 'OPEN'),
        ('Closed', 'CLOSED'),
        ('Resolved', 'RESOLVED'),
        ('New', 'NEW'),
        ('In Progress', 'IN_PROGRESS'),
        ('On Hold', 'ON_HOLD'),
        ('Awaiting Approval', 'AWAITING_APPROVAL'),
        ('Awaiting Payment', 'AWAITING_PAYMENT'),
        ('Shipped', 'SHIPPED'),
        ('Delivered', 'DELIVERED'),
        ('Returned', 'RETURNED'),
        ('Refunded', 'REFUNDED'),
        ('Partially Paid', 'PARTIALLY_PAID'),
        ('Overdue', 'OVERDUE'),
        ('Blocked', 'BLOCKED'),
        ('Enabled', 'ENABLED'),
        ('Disabled', 'DISABLED'),
        ('Payment Receved', 'Payment_Receved'),
        ('Payment Deposited', 'Payment_Deposited'),
) AS source ([Name], [Code])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Code] = source.[Code],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;