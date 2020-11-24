CREATE FUNCTION GetCurrencyValueByCharCode
(
	@CharCode NVARCHAR (10),
	@Date DATE
)
RETURNS FLOAT
WITH EXECUTE AS CALLER
AS
BEGIN
	DECLARE @Id NVARCHAR(10) = (SELECT Currencies.Id FROM Currencies WHERE Currencies.ISO_Char_Code = @CharCode);
	DECLARE @NominalValue FLOAT = (SELECT CurrenciesHistory.Value FROM CurrenciesHistory WHERE CurrencyId = @Id AND Date = @Date);
	DECLARE @Nominal INT = (SELECT Currencies.Nominal FROM Currencies WHERE Currencies.Id = @Id);
	RETURN @NominalValue / @Nominal;
END