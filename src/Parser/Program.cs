using Parser.Interfaces;

namespace Parser;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args)
            ;
        // get from service
        string path = "D:\\projs\\data";
        builder.Services.AddHostedService<Worker>();
        builder.Services.AddSingleton<IParser, Parser.Parser>();
        builder.Services.AddSingleton<FileChangeDetector>(
            new FileChangeDetector(path, builder.Services.BuildServiceProvider().GetRequiredService<ILogger<FileChangeDetector>>())
        );

        var host = builder.Build();
        host.Run();
    }
}