CREATE TABLE [dbo].[KitchenTools] (
    [IDKitchenTool] UNIQUEIDENTIFIER CONSTRAINT [DF_KitchenTools_IDKitchenTool] DEFAULT (newid()) NOT NULL,
    [isDish]        BIT              NOT NULL,
    [isGlass]       BIT              NOT NULL,
    [isTool]        BIT              NOT NULL,
    [isDecoration]  BIT              NOT NULL,
    CONSTRAINT [PK_KitchenTools] PRIMARY KEY CLUSTERED ([IDKitchenTool] ASC)
);

