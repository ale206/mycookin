CREATE TABLE [dbo].[DefaultMediaForObjects] (
    [IDDefaultMediaForObject] INT              IDENTITY (1, 1) NOT NULL,
    [IDMedia]                 UNIQUEIDENTIFIER NOT NULL,
    [ObjectCode]              NVARCHAR (50)    NOT NULL,
    [Description]             NVARCHAR (550)   NULL,
    [Enabled]                 BIT              NOT NULL,
    CONSTRAINT [PK_DefaultMediaForObjects] PRIMARY KEY CLUSTERED ([IDDefaultMediaForObject] ASC),
    CONSTRAINT [FK_DefaultMediaForObjects_Media] FOREIGN KEY ([IDMedia]) REFERENCES [dbo].[Media] ([IDMedia]) ON DELETE CASCADE ON UPDATE CASCADE
);

