CREATE TABLE [dbo].[Photo] (
    [PhotoId]   UNIQUEIDENTIFIER NOT NULL,
    [Path]      NVARCHAR (MAX)   NOT NULL,
    [IsDeleted] BIT              NOT NULL,
    CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED ([PhotoId] ASC)
);

