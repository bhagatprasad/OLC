CREATE PROCEDURE [dbo].[uspGetUserWalletDetailsByUserId]  
(  
    @UserId BIGINT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    SELECT   
        uw.[Id],  
        uw.[UserId],     
        uw.[WalletId],  
        uw.[WalletType],  
        uw.[CurrentBalance],  
        uw.[TotalEarned],  
        uw.[TotalSpent],  
        uw.[Currency], 
		u.[Email] AS UserEmail,  
        u.[Phone] AS UserPhone,  
        uw.[IsActive],  
        uw.[CreatedBy],  
        uw.[CreatedOn],  
        uw.[ModifiedOn],  
        uw.[ModifiedBy]  
    FROM   
        [dbo].[UserWallet] uw  
   LEFT JOIN [dbo].[User] u ON uw.[UserId] = u.[Id]   
    WHERE   
        uw.[UserId] = @UserId  
END