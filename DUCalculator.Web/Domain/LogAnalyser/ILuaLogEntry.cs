namespace DUCalculator.Web.Domain.LogAnalyser;

public interface ILuaLogEntry
{
    string Type { get; }
    long Timestamp { get; }

    string ToUniqueName();
}