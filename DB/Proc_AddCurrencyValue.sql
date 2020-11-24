CREATE PROCEDURE AddCurrencyValue
	@Date DATE,
	@CurrencyId NVARCHAR(10),
	@Value FLOAT
AS
	INSERT INTO CurrenciesHistory(Date, CurrencyId, Value)
    	VALUES (@Date, @CurrencyId, @Value)
