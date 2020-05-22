CREATE TABLE [dbo].[UserConfigurationParameters] (
    [IDUserConfigurationParameter] UNIQUEIDENTIFIER CONSTRAINT [DF_UserConfigurationParameters_IDConfigurationParameter] DEFAULT (newid()) NOT NULL,
    [ConfigurationName]            NVARCHAR (50)    NOT NULL,
    [ConfigurationValue]           NVARCHAR (500)   NULL,
    [ConfigurationNote]            NVARCHAR (MAX)   NULL
);

