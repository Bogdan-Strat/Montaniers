CREATE TABLE [dbo].[Role] (
    [RoleId]   TINYINT       NOT NULL,
    [RoleName] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

