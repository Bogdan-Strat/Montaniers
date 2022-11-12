CREATE TABLE [dbo].[Invitation] (
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [TripId]     UNIQUEIDENTIFIER NOT NULL,
    [IsAccepted] BIT              NULL,
    [AnswerDate] DATETIME         NULL,
    CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED ([UserId] ASC, [TripId] ASC),
    CONSTRAINT [FK_Invitation_Trip] FOREIGN KEY ([TripId]) REFERENCES [dbo].[Trip] ([TripId]),
    CONSTRAINT [FK_Invitation_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

