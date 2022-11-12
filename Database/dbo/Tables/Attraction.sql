CREATE TABLE [dbo].[Attraction] (
    [AttractionId]     UNIQUEIDENTIFIER NOT NULL,
    [TypeAttractionId] TINYINT          NOT NULL,
    [Name]             NVARCHAR (50)    NOT NULL,
    [Description]      NVARCHAR (MAX)   NULL,
    [Latitude]         DECIMAL (12, 10) NOT NULL,
    [Longitude]        DECIMAL (12, 10) NOT NULL,
    [Location]         NVARCHAR (500)   NULL,
    [Height]           INT              NULL,
    [Mountains]        NVARCHAR (100)   NULL,
    [IsDeleted]        BIT              NOT NULL,
    CONSTRAINT [PK_Attraction] PRIMARY KEY CLUSTERED ([AttractionId] ASC),
    CONSTRAINT [FK_Attraction_TypeAttraction] FOREIGN KEY ([TypeAttractionId]) REFERENCES [dbo].[TypeAttraction] ([TypeAttractionId])
);

