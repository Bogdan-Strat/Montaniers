CREATE TABLE [dbo].[Warning] (
    [WarningId]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserId] UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]      DATETIME         NOT NULL,
    [EndDate]         DATETIME         NOT NULL,
    [WarningMessage]  NVARCHAR (MAX)   NOT NULL,
    FOREIGN KEY ([CreatedByUserId]) REFERENCES [dbo].[User] ([UserId])
);

