CREATE TABLE [dbo].[Rating] (
    [RatingId]    TINYINT NOT NULL,
    [RatingScore] TINYINT NOT NULL,
    CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED ([RatingId] ASC)
);

