namespace Parser.Interfaces;

public interface IParser
{
    Task ParseAsync(string[] files);
}
