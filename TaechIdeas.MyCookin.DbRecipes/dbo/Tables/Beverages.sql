CREATE TABLE [dbo].[Beverages] (
    [IDBeverage] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Beverages_1] PRIMARY KEY CLUSTERED ([IDBeverage] ASC),
    CONSTRAINT [FK_Beverages_Ingredients] FOREIGN KEY ([IDBeverage]) REFERENCES [dbo].[Ingredients] ([IDIngredient]) ON DELETE CASCADE ON UPDATE CASCADE
);

