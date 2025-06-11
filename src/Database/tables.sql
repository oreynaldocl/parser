CREATE TABLE Drug (
    SetId VARCHAR(64) NOT NULL PRIMARY KEY, -- Unique DailyMed SETID
    Name VARCHAR(255) NOT NULL,
    Category VARCHAR(128),
    ParsedDate DATETIME NOT NULL
);

CREATE TABLE Indication (
    Id CHAR(36) NOT NULL PRIMARY KEY, -- UUID as string
    SetId VARCHAR(64) NOT NULL, -- FK to Drug
    RawText TEXT NOT NULL,
    Status ENUM('New', 'PendingConversion', 'Converted', 'ConversionFailed') NOT NULL DEFAULT 'New',
    CreatedAt DATETIME NOT NULL,
    LastConversionAttempt DATETIME NULL,
    LastConversionError TEXT,
    CONSTRAINT FK_Indication_Drug FOREIGN KEY (SetId) REFERENCES Drug(SetId)
);

CREATE TABLE Icd10Mapping (
    Id CHAR(36) NOT NULL PRIMARY KEY, -- UUID as string
    IndicationId CHAR(36) NOT NULL, -- FK to Indication
    Icd10Code VARCHAR(16) NOT NULL,
    Icd10Description VARCHAR(255) NOT NULL,
    ConfidenceScore DOUBLE NOT NULL,
    MappedAt DATETIME NOT NULL,
    CONSTRAINT FK_Icd10Mapping_Indication FOREIGN KEY (IndicationId) REFERENCES Indication(Id)
);