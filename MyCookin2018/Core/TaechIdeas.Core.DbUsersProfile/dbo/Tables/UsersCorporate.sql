CREATE TABLE [dbo].[UsersCorporate] (
    [IDUserCorporate]               UNIQUEIDENTIFIER NOT NULL,
    [PIVA]                          NVARCHAR (30)    NULL,
    [AllowSiteOrder]                BIT              NOT NULL,
    [AllowSMSOrder]                 BIT              NOT NULL,
    [AllowEventOrganizzazion]       BIT              NOT NULL,
    [AllowEventWithRecipeSelection] BIT              NOT NULL,
    CONSTRAINT [PK_UsersCorporate] PRIMARY KEY CLUSTERED ([IDUserCorporate] ASC),
    CONSTRAINT [FK_UsersCorporate_Users] FOREIGN KEY ([IDUserCorporate]) REFERENCES [dbo].[Users] ([IDUser]) ON DELETE CASCADE ON UPDATE CASCADE
);

