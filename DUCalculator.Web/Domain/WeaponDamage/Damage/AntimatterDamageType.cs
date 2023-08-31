namespace DUCalculator.Web.Domain.WeaponDamage.Damage;

public class AntimatterDamageType : DamageType
{
    public override string Name => "Anti-Matter";
    public override string Code => "AM";

    public override double CalculateDamage(DamageReceiver receiver, double damage) 
        => damage * (1 - receiver.AntimatterResistance);
}