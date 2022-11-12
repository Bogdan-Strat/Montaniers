CREATE TABLE [dbo].[ParticipationInTrip] (
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [TripId]       UNIQUEIDENTIFIER NOT NULL,
    [ResponseDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_ParticipationInTrip] PRIMARY KEY CLUSTERED ([UserId] ASC, [TripId] ASC),
    CONSTRAINT [FK_Participation_Trip] FOREIGN KEY ([TripId]) REFERENCES [dbo].[Trip] ([TripId]),
    CONSTRAINT [FK_Participation_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

