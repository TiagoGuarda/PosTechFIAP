﻿CREATE TABLE FileRecords (
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[FileName] VARCHAR(100) NOT NULL,
	[FilePath] VARCHAR(500) NOT NULL
)