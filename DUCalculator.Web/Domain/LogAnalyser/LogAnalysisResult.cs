namespace DUCalculator.Web.Domain.LogAnalyser;

public record LogAnalysisResult(
    bool Success,
    string Message,
    IEnumerable<ILuaLogEntry> Entries
);