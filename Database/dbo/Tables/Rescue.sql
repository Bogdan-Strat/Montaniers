CREATE TABLE [dbo].[Rescue] (
    [RescueId]    INT            NOT NULL,
    [RescueName]  NVARCHAR (100) NOT NULL,
    [PhoneNumber] NVARCHAR (10)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Rescue] PRIMARY KEY CLUSTERED ([RescueId] ASC)
);

