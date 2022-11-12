CREATE TABLE [dbo].[Trip] (
    [TripId]          UNIQUEIDENTIFIER NOT NULL,
    [UserId]          UNIQUEIDENTIFIER NOT NULL,
    [Description]     NVARCHAR (MAX)   NULL,
    [RatingId]        TINYINT          NOT NULL,
    [DifficultyId]    TINYINT          NOT NULL,
    [Equipment]       NVARCHAR (500)   NOT NULL,
    [TripDate]        DATETIME         NOT NULL,
    [TypePostId]      TINYINT          NOT NULL,
    [TypePublicityId] TINYINT          NOT NULL,
    [CreateDate]      DATETIME         NOT NULL,
    [IsDeleted]       BIT              NOT NULL,
    CONSTRAINT [PK_Trip] PRIMARY KEY CLUSTERED ([TripId] ASC),
    CONSTRAINT [FK_Trip_Difficulty] FOREIGN KEY ([DifficultyId]) REFERENCES [dbo].[Difficulty] ([DifficultyId]),
    CONSTRAINT [FK_Trip_Rating] FOREIGN KEY ([RatingId]) REFERENCES [dbo].[Rating] ([RatingId]),
    CONSTRAINT [FK_Trip_TypePost] FOREIGN KEY ([TypePostId]) REFERENCES [dbo].[TypePost] ([TypePostId]),
    CONSTRAINT [FK_Trip_TypePublicity] FOREIGN KEY ([TypePublicityId]) REFERENCES [dbo].[TypePublicity] ([TypePublicityId]),
    CONSTRAINT [FK_Trip_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

