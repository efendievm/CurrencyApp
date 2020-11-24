CREATE FUNCTION GetCurrencyValueByNumCode
(
	@NumCode INT,
	@Date DATE
)
RETURNS FLOAT
AS
BEGIN
	DECLARE @Id NVARCHAR(10) = (SELECT Currencies.Id FROM Currencies WHERE Currencies.ISO_Num_Code = @NumCode);
	DECLARE @NominalValue FLOAT = (SELECT CurrenciesHistory.Value FROM CurrenciesHistory WHERE CurrencyId = @Id AND Date = @Date);
	DECLARE @Nominal INT = (SELECT Currencies.Nominal FROM Currencies WHERE Currencies.Id = @Id);
	RETURN @NominalValue / @Nominal;
END