namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class KineticDamageType : DamageType
{
    public override string Name => "Kinetic";
    public override string Code => "KN";

    public override double CalculateDamage(DamageReceiver receiver, double damage)
        => damage * (1 - receiver.KineticResistance);
}