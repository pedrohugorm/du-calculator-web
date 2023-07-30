using System.Text;

namespace DUCalculator.Web.Domain.Common;

public class WebConsoleOutputWriter : IConsoleWriter
{
    private readonly StringBuilder _stringBuilder = new();
    
    public void WriteLine(string value)
    {
        _stringBuilder.AppendLine(value);
    }

    public void WriteLine()
    {
        _stringBuilder.AppendLine();
    }

    public void Write(string value)
    {
        _stringBuilder.Append(value);
    }

    public string Flush()
    {
        var result = _stringBuilder.ToString();
        _stringBuilder.Clear();
        
        return result;
    }
}