namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class ElectromagneticDamageType : IDamageType
{
    public double CalculateDamage(DamageReceiver receiver, double damage)
        => damage * (1 - receiver.ElectromagneticResistance);

    public override string ToString() => "EM";
}