CREATE TABLE [dbo].[Photo] (
    [IDPhoto] UNIQUEIDENTIFIER NOT NULL,
    [Height]  INT              NULL,
    [Width]   INT              NULL,
    CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED ([IDPhoto] ASC),
    CONSTRAINT [FK_Photo_Media] FOREIGN KEY ([IDPhoto]) REFERENCES [dbo].[Media] ([IDMedia]) ON DELETE CASCADE ON UPDATE CASCADE
);

