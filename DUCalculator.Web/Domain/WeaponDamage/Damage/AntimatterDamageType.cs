namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class AntimatterDamageType : IDamageType
{
    public double CalculateDamage(DamageReceiver receiver, double damage) 
        => damage * (1 - receiver.AntimatterResistance);
    
    public override string ToString() => "AM";
}