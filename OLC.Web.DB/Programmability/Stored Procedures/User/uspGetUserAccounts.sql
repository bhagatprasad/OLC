CREATE PROCEDURE [dbo].[uspGetUserAccounts]
AS

BEGIN
  
 SELECT 
    usr.Id,
    usr.FirstName,
    usr.LastName,
    usr.Email,
    usr.Phone,
    usr.RoleId,
    usr.LastPasswordChangedOn,
    usr.IsBlocked,
    usr.IsApproved,
    usr.ApprovedBy,
    usr.ApprovedOn,
    usr.CreatedBy,
    usr.CreatedOn,
    usr.ModifiedBy,
    usr.ModifiedOn,
    usr.IsActive,
    usr.IsExternalUser,
    ISNULL(usrkyc.KycStatus, 'Pending') AS KycStatus
FROM [dbo].[User] usr 
LEFT JOIN 
     [dbo].[UserKyc] usrkyc ON usr.Id = usrkyc.UserId
ORDER BY usr.ModifiedOn DESC; 
END