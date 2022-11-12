CREATE TABLE [dbo].[User] (
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [Email]          NVARCHAR (50)    NOT NULL,
    [Username]       NVARCHAR (15)    NOT NULL,
    [FirstName]      NVARCHAR (30)    NOT NULL,
    [LastName]       NVARCHAR (30)    NOT NULL,
    [RegisteredDate] DATETIME         NOT NULL,
    [HashedPassword] NVARCHAR (256)   NOT NULL,
    [RoleId]         TINYINT          NOT NULL,
    [IsDeleted]      BIT              NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]),
    CONSTRAINT [UK_EMAIL] UNIQUE NONCLUSTERED ([Email] ASC),
    CONSTRAINT [UK_USERNAME] UNIQUE NONCLUSTERED ([Username] ASC)
);

