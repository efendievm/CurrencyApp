CREATE PROCEDURE AddCurrency
	@Id NVARCHAR(10),
	@RusName NVARCHAR(50),
	@EngName NVARCHAR(50),
	@Nominal INT,
	@ISO_Num_Code INT,
	@ISO_Char_Code NVARCHAR(10)
AS
	INSERT INTO Currencies (Id, RusName, EngName, Nominal, ISO_Num_Code, ISO_Char_Code)
    	VALUES (@Id, CONVERT(NVARCHAR, @RusName), @EngName, @Nominal, @ISO_Num_Code, @ISO_Char_Code)