namespace Common;

public class Drug
{
    public string SetId { get; set; } // Unique DailyMed SETID
    public string Name { get; set; }
    public string Category { get; set; }
    public DateTime ParsedDate { get; set; }
    public List<Indication> Indications { get; set; } = new List<Indication>();
}

public class Indication
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Unique ID for this specific indication text
    public string SetId { get; set; } // FK to Drug
    public string RawText { get; set; }
    public IndicationStatus Status { get; set; } = IndicationStatus.New;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastConversionAttempt { get; set; }
    public string LastConversionError { get; set; }
}

public enum IndicationStatus
{
    New,
    PendingConversion,
    Converted,
    ConversionFailed
}

public class Icd10Mapping
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Unique ID for this mapping
    public Guid IndicationId { get; set; } // FK to Indication
    public string Icd10Code { get; set; }
    public string Icd10Description { get; set; }
    public double ConfidenceScore { get; set; } // From LLM
    public DateTime MappedAt { get; set; } = DateTime.UtcNow;
}
