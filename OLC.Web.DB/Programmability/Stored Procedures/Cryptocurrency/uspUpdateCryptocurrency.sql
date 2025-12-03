CREATE PROCEDURE [dbo].[uspUpdateCryptocurrency]
(
    @Id                    BIGINT,
    @Symbol                NVARCHAR(10),
    @Name                  NVARCHAR(100),
    @Blockchain            NVARCHAR(50),
    @ContractAddress       NVARCHAR(255)      = NULL,
    @Decimals              INT,
    @IsStablecoin          BIT,
    @MinDepositAmount      DECIMAL(28,18),
    @MinWithdrawalAmount   DECIMAL(28,18),
    @WithdrawalFee         DECIMAL(28,18),
    @IconUrl               NVARCHAR(500)      = NULL,
    @ModifiedBy            BIGINT             = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Cryptocurrency]
    SET
        Symbol = @Symbol,
        Name = @Name,
        Blockchain = @Blockchain,
        ContractAddress = @ContractAddress,
        Decimals = @Decimals,
        IsStablecoin = @IsStablecoin,
        MinDepositAmount = @MinDepositAmount,
        MinWithdrawalAmount = @MinWithdrawalAmount,
        WithdrawalFee = @WithdrawalFee,
        IconUrl = @IconUrl,
        ModifiedBy = @ModifiedBy,
        ModifiedOn = GETDATE()
    WHERE
        Id = @Id;
END