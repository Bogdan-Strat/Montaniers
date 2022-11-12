CREATE TABLE [dbo].[TripXAttraction] (
    [AttractionId] UNIQUEIDENTIFIER NOT NULL,
    [TripId]       UNIQUEIDENTIFIER NOT NULL,
    [OrderNumber]  INT              NOT NULL,
    [MarkingId]    TINYINT          NOT NULL,
    [Duration]     INT              NOT NULL,
    CONSTRAINT [PK_TripXAttraction] PRIMARY KEY CLUSTERED ([AttractionId] ASC, [TripId] ASC, [OrderNumber] ASC),
    CONSTRAINT [FK_TripXAttraction_Attraction] FOREIGN KEY ([AttractionId]) REFERENCES [dbo].[Attraction] ([AttractionId]),
    CONSTRAINT [FK_TripXAttraction_Marking] FOREIGN KEY ([MarkingId]) REFERENCES [dbo].[Marking] ([MarkingId]),
    CONSTRAINT [FK_TripXAttraction_Trip] FOREIGN KEY ([TripId]) REFERENCES [dbo].[Trip] ([TripId])
);

