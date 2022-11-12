CREATE TABLE [dbo].[TripXPhoto] (
    [TripId]  UNIQUEIDENTIFIER NOT NULL,
    [PhotoId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TripXPhoto] PRIMARY KEY CLUSTERED ([TripId] ASC, [PhotoId] ASC),
    CONSTRAINT [FK_TripXPhoto_Photo] FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[Photo] ([PhotoId]),
    CONSTRAINT [FK_TripXPhoto_Trip] FOREIGN KEY ([TripId]) REFERENCES [dbo].[Trip] ([TripId])
);

