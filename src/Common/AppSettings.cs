namespace Common;

public class AppSettings : IAppSettings
{
    public string ConnectionString { get; set; }
    public string FolderToWatch { get; set; }
    public string LlmApiKey { get; set; }
    public int ConversorBatchSize { get; set; }
    public int ConversorWaitTimeSeconds { get; set; }
}
