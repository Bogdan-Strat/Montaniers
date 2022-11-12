CREATE TABLE [dbo].[Follow] (
    [FollowingUserId] UNIQUEIDENTIFIER NOT NULL,
    [FollowedUserId]  UNIQUEIDENTIFIER NOT NULL,
    [IsAccepted]      BIT              NULL,
    [ModifiedDate]    DATETIME         NOT NULL,
    CONSTRAINT [PK_Follow] PRIMARY KEY CLUSTERED ([FollowingUserId] ASC, [FollowedUserId] ASC),
    CONSTRAINT [FK_Follow_FollowingUser] FOREIGN KEY ([FollowingUserId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_Follow_UserFollowed] FOREIGN KEY ([FollowedUserId]) REFERENCES [dbo].[User] ([UserId])
);

