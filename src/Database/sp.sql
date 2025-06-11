DELIMITER $$
-- DRUG Store Procedures --------------------------------
CREATE PROCEDURE IF NOT EXISTS InsertDrug(
    IN p_SetId VARCHAR(64),
    IN p_Name VARCHAR(255),
    IN p_Category VARCHAR(128),
    IN p_ParsedDate DATETIME
)
BEGIN
    INSERT INTO Drug (SetId, Name, Category, ParsedDate)
    VALUES (p_SetId, p_Name, p_Category, p_ParsedDate);
END$$

CREATE PROCEDURE IF NOT EXISTS UpdateDrug(
    IN p_SetId VARCHAR(64),
    IN p_Name VARCHAR(255),
    IN p_Category VARCHAR(128),
    IN p_ParsedDate DATETIME
)
BEGIN
    UPDATE Drug
    SET Name = p_Name,
        Category = p_Category,
        ParsedDate = p_ParsedDate
    WHERE SetId = p_SetId;
END$$

CREATE PROCEDURE IF NOT EXISTS RemoveDrug(
    IN p_SetId VARCHAR(64)
)
BEGIN
    -- Remove Icd10Mapping entries related to Indications of this Drug
    DELETE FROM Icd10Mapping
    WHERE IndicationId IN (
        SELECT Id FROM Indication WHERE SetId = p_SetId
    );

    -- Remove Indications related to this Drug
    DELETE FROM Indication WHERE SetId = p_SetId;

    -- Remove the Drug itself
    DELETE FROM Drug WHERE SetId = p_SetId;
END$$

-- INDICATION Store Procedures --------------------------------
CREATE PROCEDURE IF NOT EXISTS InsertIndication(
    IN p_Id CHAR(36),
    IN p_SetId VARCHAR(64),
    IN p_RawText TEXT,
    IN p_Status ENUM('New', 'PendingConversion', 'Converted', 'ConversionFailed'),
    IN p_CreatedAt DATETIME,
    IN p_LastConversionAttempt DATETIME,
    IN p_LastConversionError TEXT
)
BEGIN
    INSERT INTO Indication (
        Id, SetId, RawText, Status, CreatedAt, LastConversionAttempt, LastConversionError
    ) VALUES (
        p_Id, p_SetId, p_RawText, p_Status, p_CreatedAt, p_LastConversionAttempt, p_LastConversionError
    );
END$$

CREATE PROCEDURE IF NOT EXISTS UpdateIndication(
    IN p_Id CHAR(36),
    IN p_SetId VARCHAR(64),
    IN p_RawText TEXT,
    IN p_Status ENUM('New', 'PendingConversion', 'Converted', 'ConversionFailed'),
    IN p_CreatedAt DATETIME,
    IN p_LastConversionAttempt DATETIME,
    IN p_LastConversionError TEXT
)
BEGIN
    UPDATE Indication
    SET
        SetId = p_SetId,
        RawText = p_RawText,
        Status = p_Status,
        CreatedAt = p_CreatedAt,
        LastConversionAttempt = p_LastConversionAttempt,
        LastConversionError = p_LastConversionError
    WHERE Id = p_Id;
END$$

CREATE PROCEDURE IF NOT EXISTS RemoveIndication(
    IN p_Id CHAR(36)
)
BEGIN
    -- Remove related Icd10Mapping entries
    DELETE FROM Icd10Mapping WHERE IndicationId = p_Id;

    -- Remove the Indication itself
    DELETE FROM Indication WHERE Id = p_Id;
END$$

-- ICD10MAPPING Store Procedures --------------------------------
CREATE PROCEDURE IF NOT EXISTS InsertIcd10Mapping(
    IN p_Id CHAR(36),
    IN p_IndicationId CHAR(36),
    IN p_Icd10Code VARCHAR(16),
    IN p_Icd10Description VARCHAR(255),
    IN p_ConfidenceScore DOUBLE,
    IN p_MappedAt DATETIME
)
BEGIN
    INSERT INTO Icd10Mapping (
        Id, IndicationId, Icd10Code, Icd10Description, ConfidenceScore, MappedAt
    ) VALUES (
        p_Id, p_IndicationId, p_Icd10Code, p_Icd10Description, p_ConfidenceScore, p_MappedAt
    );
END$$

CREATE PROCEDURE IF NOT EXISTS UpdateIcd10Mapping(
    IN p_Id CHAR(36),
    IN p_IndicationId CHAR(36),
    IN p_Icd10Code VARCHAR(16),
    IN p_Icd10Description VARCHAR(255),
    IN p_ConfidenceScore DOUBLE,
    IN p_MappedAt DATETIME
)
BEGIN
    UPDATE Icd10Mapping
    SET
        IndicationId = p_IndicationId,
        Icd10Code = p_Icd10Code,
        Icd10Description = p_Icd10Description,
        ConfidenceScore = p_ConfidenceScore,
        MappedAt = p_MappedAt
    WHERE Id = p_Id;
END$$

CREATE PROCEDURE IF NOT EXISTS RemoveIcd10Mapping(
    IN p_Id CHAR(36)
)
BEGIN
    DELETE FROM Icd10Mapping WHERE Id = p_Id;
END$$
DELIMITER ;
