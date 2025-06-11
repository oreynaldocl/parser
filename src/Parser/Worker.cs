using Parser.Interfaces;

namespace Parser;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IParser _parser;
    private readonly FileChangeDetector _detector;

    public Worker(ILogger<Worker> logger,
        IParser parser,
        FileChangeDetector detector)
    {
        _logger = logger;
        _parser = parser;
        _detector = detector;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _detector.FileChanged += OnFileChanged;
        _detector.FileRemoved += OnFileRemoved;
        return Task.CompletedTask;
    }

    private void OnFileChanged(string filePath)
    {
        _parser.ParseAsync(new[] { filePath });
    }

    private void OnFileRemoved(string filePath)
    {
        _parser.ParseAsync(new[] { filePath });
    }
}
