CREATE PROCEDURE [dbo].[uspDeleteAddressType]
(
    @addressTypeId BIGINT
)
AS
BEGIN
    UPDATE [dbo].[AddressType]
    SET IsActive = 0
       
    WHERE Id = @addressTypeId;
END
