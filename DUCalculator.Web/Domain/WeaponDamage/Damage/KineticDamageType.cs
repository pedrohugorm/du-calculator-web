namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class KineticDamageType : IDamageType
{
    public double CalculateDamage(DamageReceiver receiver, double damage)
        => damage * (1 - receiver.KineticResistance);
    
    public override string ToString() => "KN";
}