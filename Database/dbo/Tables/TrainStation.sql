CREATE TABLE [dbo].[TrainStation] (
    [TrainStationId] UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (50)    NOT NULL,
    [Latitude]       DECIMAL (12, 10) NOT NULL,
    [Longitude]      DECIMAL (12, 10) NOT NULL,
    [Description]    NVARCHAR (MAX)   NULL,
    [Location]       NVARCHAR (100)   NOT NULL,
    [IsDeleted]      BIT              NOT NULL,
    CONSTRAINT [PK_TrainStation] PRIMARY KEY CLUSTERED ([TrainStationId] ASC)
);

