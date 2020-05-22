CREATE TABLE [dbo].[IngredientsAlternatives] (
    [IDIngredientAlternative] UNIQUEIDENTIFIER NOT NULL,
    [IDIngredientMain]        UNIQUEIDENTIFIER NOT NULL,
    [IDIngredientSlave]       UNIQUEIDENTIFIER NOT NULL,
    [AddedByUser]             UNIQUEIDENTIFIER NOT NULL,
    [AddedOn]                 SMALLDATETIME    NOT NULL,
    [CheckedBy]               UNIQUEIDENTIFIER NULL,
    [CheckedOn]               SMALLDATETIME    NULL,
    [Checked]                 BIT              NOT NULL,
    CONSTRAINT [PK_IngredientsAlternatives] PRIMARY KEY CLUSTERED ([IDIngredientAlternative] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Unique_Main-Slave_Ingr]
    ON [dbo].[IngredientsAlternatives]([IDIngredientMain] ASC, [IDIngredientSlave] ASC);

