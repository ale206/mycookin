CREATE TABLE [dbo].[MediaUploadConfig] (
    [IDMediaUploadConfig]        INT            IDENTITY (1, 1) NOT NULL,
    [MediaType]                  NVARCHAR (50)  NOT NULL,
    [UploadPath]                 NVARCHAR (550) NOT NULL,
    [UploadOriginalFilePath]     NVARCHAR (550) NOT NULL,
    [MaxSizeByte]                INT            CONSTRAINT [DF_MediaUploadConfig_MaxSizeByte] DEFAULT ((0)) NOT NULL,
    [AcceptedContentTypes]       NVARCHAR (550) NOT NULL,
    [AcceptedFileExtension]      NVARCHAR (550) NOT NULL,
    [AcceptedFileExtensionRegex] NVARCHAR (550) NULL,
    [EnableUploadForMediaType]   BIT            CONSTRAINT [DF_MediaUploadConfig_EnableUploadForObjectCode] DEFAULT ((1)) NOT NULL,
    [ComputeMD5Hash]             BIT            CONSTRAINT [DF_MediaUploadConfig_ComputeMD5Hash] DEFAULT ((1)) NOT NULL,
    [MediaFinalHeight]           INT            NOT NULL,
    [MediaFinalWidth]            INT            NOT NULL,
    [MediaPercPlusSizeForCrop]   INT            NOT NULL,
    [MediaSmallerSideMinSize]    INT            NOT NULL,
    CONSTRAINT [PK_MediaUploadConfig] PRIMARY KEY CLUSTERED ([IDMediaUploadConfig] ASC),
    CONSTRAINT [CK_MediaPercPlusSizeForCrop] CHECK ([MediaPercPlusSizeForCrop]>(0) AND [MediaPercPlusSizeForCrop]<=(100)),
    CONSTRAINT [CK_MediaSmallerSideMinSize] CHECK ([MediaSmallerSideMinSize]>=[MediaFinalHeight] AND [MediaSmallerSideMinSize]>=[MediaFinalWidth])
);

