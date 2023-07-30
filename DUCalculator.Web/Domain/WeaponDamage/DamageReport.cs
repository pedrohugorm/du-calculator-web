namespace DUCalculator.Web.Domain.WeaponDamage;

public class DamageReport
{
    public double TotalDamage { get; }
    public Dictionary<string, double> DamagePerWeaponId { get; }

    public DamageReport(double totalDamage, Dictionary<string, double> damagePerWeaponId)
    {
        TotalDamage = totalDamage;
        DamagePerWeaponId = damagePerWeaponId;
    }
}