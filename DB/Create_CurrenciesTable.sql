CREATE TABLE [dbo].[Currencies] (
    [Id]            NVARCHAR (10) NOT NULL,
    [RusName]       NVARCHAR (50) NOT NULL,
    [EngName]       NVARCHAR (50) NOT NULL,
    [Nominal]       INT           NOT NULL,
    [ISO_Num_Code]  INT           NULL,
    [ISO_Char_Code] NVARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
