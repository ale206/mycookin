CREATE TABLE [dbo].[UsersCook] (
    [IDUserCook]         UNIQUEIDENTIFIER NOT NULL,
    [IsProfessionalCook] BIT              NOT NULL,
    [CookInRestaurant]   BIT              NOT NULL,
    [CookAtHome]         BIT              NOT NULL,
    [CookDescription]    NVARCHAR (MAX)   NULL,
    [CookMembership]     SMALLDATETIME    NOT NULL,
    CONSTRAINT [PK_UsersCook] PRIMARY KEY CLUSTERED ([IDUserCook] ASC),
    CONSTRAINT [FK_UsersCook_Users] FOREIGN KEY ([IDUserCook]) REFERENCES [dbo].[Users] ([IDUser]) ON DELETE CASCADE ON UPDATE CASCADE
);

