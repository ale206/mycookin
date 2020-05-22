CREATE TABLE [dbo].[UsersPreferedObjects] (
    [IDUserPreferedObject] UNIQUEIDENTIFIER NOT NULL,
    [IDUser]               UNIQUEIDENTIFIER NOT NULL,
    [IDObject]             UNIQUEIDENTIFIER NOT NULL,
    [IDObjectType]         INT              NOT NULL,
    [PreferenceDate]       SMALLDATETIME    NOT NULL,
    [PreferenceValue]      FLOAT (53)       NULL,
    CONSTRAINT [PK_UsersPrefiredRecipes] PRIMARY KEY CLUSTERED ([IDUserPreferedObject] ASC)
);

