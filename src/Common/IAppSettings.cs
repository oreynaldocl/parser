namespace Common;

public interface IAppSettings
{
    string ConnectionString { get; set; }
    string FolderToWatch { get; set; }
    string LlmApiKey { get; set; }
    int ConversorBatchSize { get; set; }
    int ConversorWaitTimeSeconds { get; set; }
}
