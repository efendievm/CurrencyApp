CREATE TABLE [dbo].[CurrenciesHistory] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Date]       DATE          NOT NULL,
    [CurrencyId] NVARCHAR (10) NOT NULL,
    [Value]      FLOAT (53)    NOT NULL
);

