namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class ThermicDamageType : IDamageType
{
    public double CalculateDamage(DamageReceiver receiver, double damage)
        => damage * (1 - receiver.ThermicResistance);
    
    public override string ToString() => "TH";
}