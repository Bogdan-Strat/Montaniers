CREATE TABLE [dbo].[AvatarPhoto] (
    [UserId]  UNIQUEIDENTIFIER NOT NULL,
    [PhotoId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_AvatarPhoto] PRIMARY KEY CLUSTERED ([UserId] ASC, [PhotoId] ASC),
    CONSTRAINT [FK_AvatarPhoto_Photo] FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[Photo] ([PhotoId]),
    CONSTRAINT [FK_AvatarPhoto_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

