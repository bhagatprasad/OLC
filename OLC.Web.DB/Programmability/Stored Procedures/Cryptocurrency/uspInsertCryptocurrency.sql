CREATE PROCEDURE [dbo].[uspInsertCryptocurrency]
(
    @Symbol                NVARCHAR(10),
    @Name                  NVARCHAR(100),
    @Blockchain            NVARCHAR(50),
    @ContractAddress       NVARCHAR(255)      = NULL,
    @Decimals              INT,
    @IsStablecoin          BIT                = 0,
    @MinDepositAmount      DECIMAL(28,18)     = 0.0001,
    @MinWithdrawalAmount   DECIMAL(28,18)     = 0.001,
    @WithdrawalFee         DECIMAL(28,18)     = 0.0005,
    @IconUrl               NVARCHAR(500)      = NULL,
    @CreatedBy             BIGINT             = NULL

)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Cryptocurrency]
    (
        Symbol,
        Name,
        Blockchain,
        ContractAddress,
        Decimals,
        IsStablecoin,
        MinDepositAmount,
        MinWithdrawalAmount,
        WithdrawalFee,
        IconUrl,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES
    (
        @Symbol,
        @Name,
        @Blockchain,
        @ContractAddress,
        @Decimals,
        @IsStablecoin,
        @MinDepositAmount,
        @MinWithdrawalAmount,
        @WithdrawalFee,
        @IconUrl,
        @CreatedBy,
        GETDATE(),
        @CreatedBy,
        GETDATE(),
        1
    );
END
