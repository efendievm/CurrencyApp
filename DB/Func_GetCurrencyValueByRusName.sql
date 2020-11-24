CREATE FUNCTION GetCurrencyValueByRusName
(
	@RusName NVARCHAR(50),
	@Date DATE
)
RETURNS FLOAT
AS
BEGIN
	DECLARE @Id NVARCHAR(10) = (SELECT Currencies.Id FROM Currencies WHERE Currencies.RusName = @RusName);
	DECLARE @NominalValue FLOAT = (SELECT CurrenciesHistory.Value FROM CurrenciesHistory WHERE CurrencyId = @Id AND Date = @Date);
	DECLARE @Nominal INT = (SELECT Currencies.Nominal FROM Currencies WHERE Currencies.Id = @Id);
	RETURN @NominalValue / @Nominal;
END