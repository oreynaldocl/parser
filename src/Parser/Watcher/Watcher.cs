namespace Parser;

public class FileChangeDetector : IDisposable
{
    private readonly FileSystemWatcher _watcher;
    private readonly ILogger<FileChangeDetector> _logger;

    public event Action<string>? FileChanged;
    public event Action<string>? FileRemoved;

    public FileChangeDetector(string path, ILogger<FileChangeDetector> logger)
    {
        _logger = logger;
        _watcher = new FileSystemWatcher(path)
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime,
            Filter = "*.*",
            EnableRaisingEvents = true,
            IncludeSubdirectories = false
        };

        _watcher.Created += OnChanged;
        _watcher.Changed += OnChanged;
        _watcher.Renamed += OnRenamed;
        _watcher.Deleted += OnRemoved;
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("File {ChangeType}: {FullPath}", e.ChangeType, e.FullPath);
        FileChanged?.Invoke(e.FullPath);
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        _logger.LogInformation("File Renamed: {OldFullPath} -> {FullPath}", e.OldFullPath, e.FullPath);
        FileChanged?.Invoke(e.FullPath);
    }

    private void OnRemoved(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("File {ChangeType}: {FullPath}", e.ChangeType, e.FullPath);
        FileRemoved?.Invoke(e.FullPath);
    }

    public void Dispose()
    {
        _watcher.Dispose();
    }
}