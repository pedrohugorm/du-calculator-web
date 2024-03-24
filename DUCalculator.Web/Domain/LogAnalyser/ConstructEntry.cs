namespace DUCalculator.Web.Domain.LogAnalyser;

public record ConstructEntry(
    string Size,
    string Name,
    params string[] Tags
);