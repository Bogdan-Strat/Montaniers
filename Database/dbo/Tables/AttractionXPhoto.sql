CREATE TABLE [dbo].[AttractionXPhoto] (
    [AttractionId] UNIQUEIDENTIFIER NOT NULL,
    [PhotoId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_AttractionXPhoto] PRIMARY KEY CLUSTERED ([AttractionId] ASC, [PhotoId] ASC),
    CONSTRAINT [FK_AttractionXPhoto_Attraction] FOREIGN KEY ([AttractionId]) REFERENCES [dbo].[Attraction] ([AttractionId]),
    CONSTRAINT [FK_AttractionXPhoto_Photo] FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[Photo] ([PhotoId])
);

