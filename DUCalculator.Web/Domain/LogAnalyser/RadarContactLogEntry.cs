namespace DUCalculator.Web.Domain.LogAnalyser;

public record RadarContactLogEntry(
    ConstructEntry Construct,
    string Position,
    long Timestamp
) : ILuaLogEntry
{
    public string Type => "radar-contact";
    public long Timestamp { get; } = Timestamp;
    
    public string ToUniqueName()
    {
        return Construct.Name;
    }
}