CREATE TABLE [dbo].[KitchenToolsLanguages] (
    [IDKitchenToolLanguage]  UNIQUEIDENTIFIER CONSTRAINT [DF_KitchenToolsLanguages_IDKitchenToolLanguage] DEFAULT (newid()) NOT NULL,
    [IDKitchenTool]          UNIQUEIDENTIFIER NOT NULL,
    [IDLanguage]             INT              NOT NULL,
    [KitchenToolSingular]    NVARCHAR (150)   NOT NULL,
    [KitchenToolPlural]      NVARCHAR (150)   NOT NULL,
    [KitchenTooldescription] NVARCHAR (550)   NULL,
    [isAutoTranslate]        BIT              NOT NULL,
    CONSTRAINT [PK_KitchenToolsLanguages] PRIMARY KEY CLUSTERED ([IDKitchenToolLanguage] ASC),
    CONSTRAINT [FK_KitchenToolsLanguages_KitchenTools] FOREIGN KEY ([IDKitchenTool]) REFERENCES [dbo].[KitchenTools] ([IDKitchenTool]) ON DELETE CASCADE ON UPDATE CASCADE
);

