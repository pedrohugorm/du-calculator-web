namespace DUCalculator.Web.Domain.Common;

public interface IConsoleWriter
{
    void WriteLine(string value);
    void WriteLine();
    void Write(string value);
    string Flush();
}