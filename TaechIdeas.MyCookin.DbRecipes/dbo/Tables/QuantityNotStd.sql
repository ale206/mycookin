CREATE TABLE [dbo].[QuantityNotStd] (
    [IDQuantityNotStd] INT        IDENTITY (1, 1) NOT NULL,
    [PercentageFactor] FLOAT (53) NOT NULL,
    [EnabledToUser]    BIT        NOT NULL,
    CONSTRAINT [PK_QuantityNotStd] PRIMARY KEY CLUSTERED ([IDQuantityNotStd] ASC)
);

