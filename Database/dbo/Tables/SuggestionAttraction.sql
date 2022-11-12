CREATE TABLE [dbo].[SuggestionAttraction] (
    [SuggestionAttractionId] UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserId]        UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]             DATETIME         NOT NULL,
    [IsApproved]             BIT              NULL,
    [AttractionName]         NVARCHAR (50)    NOT NULL,
    [TypeAttractionId]       TINYINT          NOT NULL,
    [Description]            NVARCHAR (MAX)   NULL,
    [Latitude]               DECIMAL (12, 10) NULL,
    [Longitude]              DECIMAL (12, 10) NULL,
    [Location]               NVARCHAR (500)   NULL,
    [Height]                 INT              NULL,
    [Mountains]              NVARCHAR (100)   NULL,
    CONSTRAINT [PK_SuggestionAttraction] PRIMARY KEY CLUSTERED ([SuggestionAttractionId] ASC),
    CONSTRAINT [FK_SuggestionAttraction_TypeAttraction] FOREIGN KEY ([TypeAttractionId]) REFERENCES [dbo].[TypeAttraction] ([TypeAttractionId]),
    CONSTRAINT [FK_SuggestionAttraction_User] FOREIGN KEY ([CreatedByUserId]) REFERENCES [dbo].[User] ([UserId])
);

