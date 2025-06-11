using Parser.Interfaces;

namespace Parser;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args)
            ;

        string folder = "data";
        builder.Services.AddHostedService<Worker>();
        builder.Services.AddSingleton<IParser, Parser.Parser>();
        builder.Services.AddSingleton<FileChangeDetector>(
            new FileChangeDetector(folder, builder.Services.BuildServiceProvider().GetRequiredService<ILogger<FileChangeDetector>>())
        );

        var host = builder.Build();
        host.Run();
    }
}
