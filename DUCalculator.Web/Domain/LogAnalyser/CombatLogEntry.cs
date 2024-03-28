namespace DUCalculator.Web.Domain.LogAnalyser;

public record CombatLogEntry(
    string Type,
    long Timestamp,
    string TargetName,
    double Damage
) : ILuaLogEntry
{
    public string ToUniqueName() => $"{Timestamp}";
}

public record HitEntry(
    long Timestamp,
    string OriginName,
    string WeaponName,
    double Damage,
    string TargetName
) : CombatLogEntry("hit", Timestamp, TargetName, Damage);

public record MissEntry(
    string OriginName,
    string WeaponName,
    string TargetName,
    long Timestamp
) : CombatLogEntry("miss", Timestamp, TargetName, 0);