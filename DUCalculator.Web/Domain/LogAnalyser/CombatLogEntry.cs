namespace DUCalculator.Web.Domain.LogAnalyser;

public record CombatLogEntry(string Type, long Timestamp) : ILuaLogEntry
{
    public string ToUniqueName() => $"{Timestamp}";
}

public record HitEntry(
    long Timestamp,
    string OriginName,
    string WeaponName,
    double Damage,
    string TargetName
) : CombatLogEntry("hit", Timestamp)
{
    
}

public record MissEntry(string OriginName, string WeaponName, string TargetName, long Timestamp) : CombatLogEntry("miss", Timestamp)
{
    
}