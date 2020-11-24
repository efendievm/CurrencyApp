CREATE FUNCTION GetCurrencyValueById
(
	@Id NVARCHAR(10),
	@Date DATE
)
RETURNS FLOAT
AS
BEGIN
	
	DECLARE @NominalValue FLOAT = (SELECT CurrenciesHistory.Value FROM CurrenciesHistory WHERE CurrencyId = @Id AND Date = @Date);
	DECLARE @Nominal INT = (SELECT Currencies.Nominal FROM Currencies WHERE Currencies.Id = @Id);
	RETURN @NominalValue / @Nominal;
END